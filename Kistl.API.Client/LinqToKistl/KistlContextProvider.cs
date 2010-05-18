
namespace Kistl.API.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Kistl.API.Client.LinqToKistl;
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
        private IKistlContext _context;

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
        private LinkedList<Expression> _orderBy = null;

        private IProxy _proxy;

        internal KistlContextProvider(IKistlContext ctx, InterfaceType ifType, IProxy proxy)
        {
            _context = ctx;
            _type = ifType;
            _proxy = proxy;
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

        #region Operations GetListOf/GetList/GetObject
        internal List<IDataObject> GetListOfCall(int ID, string propertyName)
        {
            ResetState();

            List<IStreamable> auxObjects;
            List<IDataObject> serviceResult = _proxy.GetListOf(_context, _type, ID, propertyName, out auxObjects).ToList();
            List<IDataObject> result = new List<IDataObject>();

            foreach (IDataObject obj in serviceResult)
            {
                result.Add((IDataObject)_context.Attach(obj));
            }

            foreach (IPersistenceObject obj in auxObjects)
            {
                _context.Attach(obj);
            }

            return result;
        }

        /// <summary>
        /// Performs a GetListCall
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal T GetListCall<T>(Expression e)
        {
            ResetState();

            if (Logging.Linq.IsInfoEnabled)
            {
                Logging.Linq.Info(e.ToString());
            }

            List<IStreamable> auxObjects;
            List<IDataObject> serviceResult = VisitAndCallService(e, out auxObjects);
            // prepare caches
            foreach (IPersistenceObject obj in auxObjects)
            {
                _context.Attach(obj);
            }

            MethodCallExpression me = e as MethodCallExpression;

            // Projection
            if (e.IsMethodCallExpression("Select"))
            {
                // Get Selector and SourceType
                // Sourcetype should be of type IDataObject
                LambdaExpression selector = (LambdaExpression)me.Arguments[1].StripQuotes();
                Type sourceType = selector.Parameters[0].Type;

                // Create temporary result list for objects
                IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType));
                foreach (IDataObject obj in serviceResult)
                {
                    result.Add(_context.Attach(obj));
                }
                // Can't use T as it is a ListType
                AddNewLocalObjects(_type, result);

                IQueryable selectResult = result.AsQueryable().AddSelector(selector, sourceType, typeof(T).FindElementTypes().First());
                return (T)Activator.CreateInstance(typeof(T), selectResult.GetEnumerator());
            }
            else
            {
                T result = Activator.CreateInstance<T>();
                if (!(result is IList)) throw new InvalidOperationException("A GetListCall supports only ILists as return result");
                foreach (IDataObject obj in serviceResult)
                {
                    ((IList)result).Add(_context.Attach(obj));
                }
                // Can't use T as it is a ListType
                AddNewLocalObjects(_type, (IList)result);
                return result;
            }
        }

        /// <summary>
        /// Performs a GetListCall but returns a single Object
        /// </summary>
        /// <param name="e"></param>
        /// <returns>A Object an Expeption, if the Object was not found.</returns>
        internal T GetObjectCall<T>(Expression e)
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
            List<T> result = new List<T>();
            AddLocalObjects<T>(result);

            // If nothing found local -> goto Server
            if (result.Count == 0)
            {
                List<IStreamable> auxObjects;
                List<IDataObject> serviceResult = CallService(out auxObjects);
                // prepare caches
                foreach (IPersistenceObject obj in auxObjects)
                {
                    _context.Attach(obj);
                }

                foreach (IDataObject obj in serviceResult)
                {
                    result.Add((T)_context.Attach(obj));
                }
            }

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

        #region Local Object handling
        private void AddNewLocalObjects(InterfaceType ifType, IList result)
        {
            MethodInfo mi = typeof(KistlContextProvider).GetMethod("AddNewLocalObjectsGeneric", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(ifType.Type);
            mi.Invoke(this, new object[] { result });
        }

        private void AddNewLocalObjectsGeneric<T>(IList result)
        {
            var list = _context.AttachedObjects.AsQueryable().Where(o => o.ObjectState == DataObjectState.New).OfType<T>();
            if (_filter != null) _filter.ForEach(f => list = list.AddFilter(f));
            list.ForEach<T>(i => result.Add(i));
        }

        private void AddLocalObjects<T>(IList result)
        {
            var list = _context.AttachedObjects.AsQueryable().Where(o => o.ObjectState != DataObjectState.Deleted).OfType<T>();
            if (_filter != null) _filter.ForEach(f => list = list.AddFilter(f));
            list.ForEach<T>(i => result.Add(i));
        }
        #endregion

        #endregion

        #region IQueryProvider Members
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>)new KistlContextQuery<TElement>(_context, _type, this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null) { throw new ArgumentNullException("expression"); }

            Type elementType = expression.Type.FindElementTypes().First();
            return (IQueryable)Activator.CreateInstance(typeof(KistlContextQuery<>)
                .MakeGenericType(elementType), new object[] { _context, _type, this, expression });
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
                if (_orderBy == null) _orderBy = new LinkedList<Expression>();
                _orderBy.AddFirst(m.Arguments[1]);
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
            else if (m.IsMethodCallExpression("OfType") || m.IsMethodCallExpression("Cast"))
            {
                // OK - just a cast
                // No special processing needed
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("WithEagerLoading", typeof(KistlContextQueryableExtensions)))
            {
                _eagerLoadLists = true;
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
