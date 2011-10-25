
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Client.PerfCounter;
    using Kistl.API.Utils;

    /// <summary>
    /// Provider for Kistl Linq Provider. See http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx for details.
    /// </summary>
    public class KistlContextProvider : ExpressionTreeVisitor, IKistlQueryProvider
    {
        /// <summary>
        /// The result type of this provider
        /// </summary>
        private InterfaceType _type;
        /// <summary>
        /// 
        /// </summary>
        private KistlContextImpl _context;

        /// <summary>
        /// 
        /// </summary>
        private int _maxListCount = API.Helper.MAXLISTCOUNT;

        /// <summary>
        /// 
        /// </summary>
        private bool? _eagerLoadLists = null;

        /// <summary>
        /// Filter Expression for GetList SearchType.
        /// </summary>
        private LinkedList<Expression> _filter = null;
        /// <summary>
        /// OrderBy Expression for GetList SearchType.
        /// </summary>
        private LinkedList<OrderBy> _orderBy = null;

        private IProxy _proxy;
        private readonly IPerfCounter perfCounter;

        internal KistlContextProvider(KistlContextImpl ctx, InterfaceType ifType, IProxy proxy, IPerfCounter perfCounter)
        {
            _context = ctx;
            _type = ifType;
            _proxy = proxy;
            this.perfCounter = perfCounter;
        }

        private void ResetState()
        {
            _maxListCount = API.Helper.MAXLISTCOUNT;
            _eagerLoadLists = null;
            _filter = null;
            _orderBy = null;
        }

        #region CallService
        private List<IDataObject> VisitAndCallService(Expression e, out List<IStreamable> auxObjects)
        {
            e = TransformExpression(e);
            Visit(e);
            return CallService(out auxObjects);
        }

        private Expression TransformExpression(Expression e)
        {
            e = QueryTranslator.Translate(e);
            // TODO: Maybe merge constant evaluator into QueryTranslator
            e = ConstantEvaluator.PartialEval(e);
            return e;
        }

        private List<IDataObject> CallService(out List<IStreamable> auxObjects)
        {
            return _proxy.GetList(_context, _type, _maxListCount, _eagerLoadLists ?? _maxListCount == 1, _filter, _orderBy, out auxObjects).ToList();
        }
        #endregion

        #region Operations GetListOf/GetList/GetObject/InvokeServerMethod

        internal List<IDataObject> GetListOfCall(int ID, string propertyName)
        {
            ResetState();

            List<IStreamable> auxObjects;
            List<IDataObject> serviceResult = _proxy.GetListOf(_context, _type, ID, propertyName, out auxObjects).ToList();
            List<IDataObject> result = new List<IDataObject>();

            foreach (IDataObject obj in serviceResult)
            {
                result.Add((IDataObject)_context.AttachRespectingIsolationLevel(obj));
            }

            foreach (IPersistenceObject obj in auxObjects)
            {
                _context.AttachRespectingIsolationLevel(obj);
            }

            _context.PlaybackNotifications();
            return result;
        }

        /// <summary>
        /// Performs a GetListCall
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal T GetListCall<T>(Expression e)
        {
            int objectCount = 0;
            var ticks = perfCounter.IncrementQuery(_type);
            try
            {
                ResetState();

                if (Logging.Linq.IsInfoEnabled)
                {
                    Logging.Linq.Info(e.ToString());
                }

                List<IStreamable> auxObjects;
                List<IDataObject> serviceResult = VisitAndCallService(e, out auxObjects);
                foreach (IDataObject obj in serviceResult)
                {
                    _context.AttachRespectingIsolationLevel(obj);
                }
                // prepare caches
                foreach (IPersistenceObject obj in auxObjects)
                {
                    _context.AttachRespectingIsolationLevel(obj);
                }
                objectCount = serviceResult.Count;

                // in the face of local changes, we have to re-query against local objects, to provide a consistent view of the objects
                var result = _context.IsModified ? QueryFromLocalObjectsHack(_type) : serviceResult;

                _context.PlaybackNotifications();

                // Projection
                if (e.IsMethodCallExpression("Select"))
                {
                    // Get Selector and SourceType
                    // Sourcetype should be of type IDataObject
                    MethodCallExpression me = e as MethodCallExpression;
                    LambdaExpression selector = (LambdaExpression)me.Arguments[1].StripQuotes();
                    Type sourceType = selector.Parameters[0].Type;

                    IQueryable selectResult = result.AsQueryable().AddSelector(selector, sourceType, selector.Body.Type); // typeof(T).FindElementTypes().First());
                    return (T)Activator.CreateInstance(typeof(T), selectResult.AddCast(typeof(T).FindElementTypes().First()).GetEnumerator());
                }
                else
                {
                    T castResult = Activator.CreateInstance<T>();
                    if (!(castResult is IList)) throw new InvalidOperationException("A GetListCall supports only ILists as return result");
                    foreach (var obj in result)
                    {
                        ((IList)castResult).Add(obj);
                    }
                    return castResult;
                }
            }
            finally
            {
                perfCounter.DecrementQuery(_type, objectCount, ticks);
            }
        }

        /// <summary>
        /// Performs a GetListCall but returns a single Object
        /// </summary>
        /// <param name="e"></param>
        /// <returns>A Object an Expeption, if the Object was not found.</returns>
        internal T GetObjectCall<T>(Expression e)
        {
            int objectCount = 0;
            var ticks = perfCounter.IncrementQuery(_type);
            try
            {
                ResetState();

                if (Logging.Linq.IsInfoEnabled)
                {
                    Logging.Linq.Info(e.ToString());
                }

                // Visit
                e = TransformExpression(e);
                Visit(e);

                // Try to find a local object first
                var result = QueryFromLocalObjects<T>();

                // If nothing found local -> goto Server
                if (result.Count == 0)
                {
                    List<IStreamable> auxObjects;
                    List<IDataObject> serviceResult = CallService(out auxObjects);
                    // prepare caches
                    foreach (IPersistenceObject obj in auxObjects)
                    {
                        _context.AttachRespectingIsolationLevel(obj);
                    }

                    foreach (IDataObject obj in serviceResult)
                    {
                        result.Add((T)_context.AttachRespectingIsolationLevel(obj));
                    }

                    _context.PlaybackNotifications();
                }

                objectCount = result.Count;

                if (e.IsMethodCallExpression("First"))
                {
                    return result.First();
                }
                else if (e.IsMethodCallExpression("FirstOrDefault"))
                {
                    return result.FirstOrDefault();
                }
                else if (e.IsMethodCallExpression("Single"))
                {
                    return result.Single();
                }
                else if (e.IsMethodCallExpression("SingleOrDefault"))
                {
                    return result.SingleOrDefault();
                }
                else
                {
                    throw new NotSupportedException("Expression is not supported");
                }
            }
            finally
            {
                perfCounter.DecrementQuery(_type, objectCount, ticks);
            }
        }

        #region Local Object handling
        private IList QueryFromLocalObjectsHack(InterfaceType ifType)
        {
            MethodInfo mi = typeof(KistlContextProvider).GetMethod("QueryFromLocalObjects", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(ifType.Type);
            return (IList)mi.Invoke(this, new object[] { });
        }

        private List<T> QueryFromLocalObjects<T>()
        {
            List<T> result = new List<T>();
            var list = _context.AttachedObjects.AsQueryable().Where(o => o.ObjectState != DataObjectState.Deleted).OfType<T>();
            if (_filter != null) _filter.ForEach(f => list = list.AddFilter(f));
            list.ForEach<T>(result.Add);
            return result;
        }
        #endregion

        #endregion

        #region IQueryProvider Members
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>)new KistlContextQuery<TElement>(_context, _type, this, expression, perfCounter);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null) { throw new ArgumentNullException("expression"); }

            Type elementType = expression.Type.FindElementTypes().First();
            return (IQueryable)Activator.CreateInstance(typeof(KistlContextQuery<>)
                .MakeGenericType(elementType), new object[] { _context, _type, this, expression, perfCounter });
        }

        public TResult Execute<TResult>(Expression e)
        {
            return GetObjectCall<TResult>(e);
        }

        public object Execute(Expression e)
        {
            return GetObjectCall<IDataObject>(e);
        }

        #endregion

        #region Visits
        protected override void VisitMethodCall(MethodCallExpression m)
        {
            if (m.IsMethodCallExpression("Where"))
            {
                if (_filter == null) _filter = new LinkedList<Expression>();
                _filter.AddFirst(m.Arguments[1]);
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("OrderBy") || m.IsMethodCallExpression("ThenBy"))
            {
                if (_orderBy == null) _orderBy = new LinkedList<OrderBy>();
                _orderBy.AddFirst(new OrderBy(OrderByType.ASC, m.Arguments[1]));
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("OrderByDescending") || m.IsMethodCallExpression("ThenByDescending"))
            {
                if (_orderBy == null) _orderBy = new LinkedList<OrderBy>();
                _orderBy.AddFirst(new OrderBy(OrderByType.DESC, m.Arguments[1]));
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("Select"))
            {
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("Take"))
            {
                _maxListCount = m.Arguments[1].GetExpressionValue<int>();
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("First") ||
                        m.IsMethodCallExpression("FirstOrDefault") ||
                        m.IsMethodCallExpression("Single") ||
                        m.IsMethodCallExpression("SingleOrDefault")
                )
            {
                _maxListCount = 1;
                if (m.Arguments.Count == 2)
                {
                    if (_filter == null) _filter = new LinkedList<Expression>();
                    _filter.AddFirst(m.Arguments[1]);
                }
                else
                    base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("WithEagerLoading", typeof(KistlContextQueryableExtensions)))
            {
                _eagerLoadLists = true;
            }
            else if (m.IsMethodCallExpression(typeof(IQueryable)))
            {
                // Lets serialize, server has to ensure security
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else if (m.IsMethodCallExpression(typeof(Queryable)))
            {
                // Lets serialize, server has to ensure security
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else if (m.IsMethodCallExpression(typeof(IEnumerable)))
            {
                // Lets serialize, server has to ensure security
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else if (m.IsMethodCallExpression(typeof(string)))
            {
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else
            {
                throw new NotSupportedException(string.Format("Method Call '{0}' is not allowed", m.Method.Name));
            }

            // Do not call base - only first expression is important
        }
        #endregion
    }
}
