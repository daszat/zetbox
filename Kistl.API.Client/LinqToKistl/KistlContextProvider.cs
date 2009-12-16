using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Kistl.API.Utils;
using Kistl.API.Client.LinqToKistl;

// http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx


namespace Kistl.API.Client
{
    /// <summary>
    /// Provider for Kistl Linq Provider
    /// </summary>
    public class KistlContextProvider : ExpressionTreeVisitor, IQueryProvider
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
        /// Filter Expression for GetList SearchType.
        /// </summary>
        private Expression _filter = null;
        /// <summary>
        /// OrderBy Expression for GetList SearchType.
        /// </summary>
        private LinkedList<Expression> _orderBy = null;

        internal KistlContextProvider(IKistlContext ctx, InterfaceType ifType)
        {
            _context = ctx;
            _type = ifType;
        }

        internal List<IDataObject> GetListOf(int ID, string propertyName)
        {
            List<IStreamable> auxObjects;
            List<IDataObject> serviceResult = Proxy.Current.GetListOf(_type, ID, propertyName, out auxObjects).ToList();
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
            return Proxy.Current.GetList(_type, _maxListCount, _filter, _orderBy, out auxObjects).ToList();
        }

        private void AddNewLocalObjects(InterfaceType ifType, IList result)
        {
            MethodInfo mi = typeof(KistlContextProvider).GetMethod("AddNewLocalObjectsGeneric", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(ifType.Type);
            mi.Invoke(this, new object[] { result });
        }


        private void AddNewLocalObjectsGeneric<T>(IList result) where T : IDataObject
        {
            var list = _context.AttachedObjects.AsQueryable().OfType<T>()
                .Where(o => o.ObjectState == DataObjectState.New);
            if (_filter != null) list = (IQueryable<T>)list.AddFilter(_filter);
            list.ForEach<T>(i => result.Add(i));
        }

        /// <summary>
        /// Performs a GetListCall
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal T GetListCall<T>(Expression e)
        {
            if (Logging.Linq.IsDebugEnabled)
            {
                Logging.Linq.Debug(e.ToString());
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
            if (Logging.Linq.IsDebugEnabled)
            {
                Logging.Linq.Debug(e.ToString());
            }

            // Visit
            e = TransformExpression(e);
            Visit(e);

            List<T> result = new List<T>();
            AddNewLocalObjects(_type, result);

            // If nothing found or a List is expected -> goto Server
            if (result.Count == 0 ||
                    e.IsMethodCallExpression("First") ||
                    e.IsMethodCallExpression("FirstOrDefault"))
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

        #region IQueryProvider Members
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>)new KistlContextQuery<TElement>(_context, _type, this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
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
            if (_filter != null) throw new InvalidOperationException("Filter is already set");

            if (m.IsMethodCallExpression("Where"))
            {
                _filter = m.Arguments[1];
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
                    _filter = m.Arguments[1];
                else
                    base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("OfType") || m.IsMethodCallExpression("Cast"))
            {
                // OK - just a cast
                // No special processing needed
            }
            else
            {
                throw new NotSupportedException(string.Format("Method Call '{0}' is not supported", m.Method.Name));
            }

            // Do not call base - only first expression is important
        }
        #endregion
    }
}
