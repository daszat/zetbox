using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.IO;
using System.Reflection;


namespace Kistl.API.Client
{
    /// <summary>
    /// Provider for Kistl Linq Provider
    /// </summary>
    /// <typeparam name="T">Type to search</typeparam>
    internal class KistlContextProvider : ExpressionTreeVisitor, IQueryProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private Type _type;
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
        private int ID = API.Helper.INVALIDID;


        /// <summary>
        /// 
        /// </summary>
        private enum SearchTypeEnum
        {
            GetObject,
            GetObjectOrNew,
            GetList,
        }

        /// <summary>
        /// Searchtype, default is GetList.
        /// </summary>
        private SearchTypeEnum SearchType = SearchTypeEnum.GetList;
        /// <summary>
        /// Filter Expression for GetList SearchType.
        /// </summary>
        private Expression _filter = null;
        /// <summary>
        /// OrderBy Expression for GetList SearchType.
        /// </summary>
        private Expression _orderBy = null;

        internal KistlContextProvider(IKistlContext ctx, Type type)
        {
            _context = ctx;
            _type = type;
        }

        internal List<IDataObject> GetListOf(int ID, string propertyName)
        {
            List<IDataObject> serviceResult = Proxy.Current.GetListOf(_type, ID, propertyName).Cast<IDataObject>().ToList();
            List<IDataObject> result = new List<IDataObject>();
            foreach (Kistl.API.IDataObject obj in serviceResult)
            {
                //CacheController<Kistl.API.IDataObject>.Current.Set(obj.GetType(), obj.ID,
                //    (Kistl.API.IDataObject)(obj).Clone());

                result.Add((IDataObject)_context.Attach(obj));
            }

            return result;
        }

        private void AddNewLocalObjects(Type type, IList result)
        {
            MethodInfo mi = typeof(KistlContextProvider).GetMethod("AddNewLocalObjectsGeneric", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(type);
            mi.Invoke(this, new object[] { result });
        }


        private void AddNewLocalObjectsGeneric<T>(IList result) where T : IDataObject
        {
            var list = _context.AttachedObjects.AsQueryable().OfType<T>()
                .Where(o => o.ObjectState == DataObjectState.New);
            if(_filter != null) list = (IQueryable<T>)list.AddFilter(_filter);
            list.ForEach<T>(i => result.Add(i));
        }

        /// <summary>
        /// Performs a GetListCall
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private T GetListCall<T>(Expression e)
        {
            List<IDataObject> serviceResult = Proxy.Current.GetList(_type, _maxListCount, _filter, _orderBy).ToList();

            // Projection
            if (e.IsMethodCallExpression("Select"))
            {
                // Get Selector and SourceType
                // Sourcetype should be of type IDataObject
                LambdaExpression selector = (LambdaExpression)((MethodCallExpression)e).Arguments[1].StripQuotes();
                Type sourceType = selector.Parameters[0].Type;

                // Create temporary result list for objects
                IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType));
                foreach (IDataObject obj in serviceResult)
                {
                    result.Add(_context.Attach(obj));
                }
                AddNewLocalObjects(_type, result);

                IQueryable selectResult = result.AsQueryable().AddSelector(selector, sourceType, typeof(T).GetCollectionElementType());
                return (T)Activator.CreateInstance(typeof(T), selectResult.GetEnumerator());
            }
            else if (e.IsMethodCallExpression("First") || e.IsMethodCallExpression("FirstOrDefault"))
            {
                List<T> result = new List<T>();
                foreach (IDataObject obj in serviceResult)
                {
                    result.Add((T)_context.Attach(obj));
                }
                AddNewLocalObjects(_type, result);
                if (e.IsMethodCallExpression("First"))
                {
                    return result.First();
                }
                else
                {
                    return result.FirstOrDefault();
                }
            }
            else if (e.IsMethodCallExpression("Single") || e.IsMethodCallExpression("SingleOrDefault"))
            {
                List<T> result = new List<T>();
                foreach (IDataObject obj in serviceResult)
                {
                    result.Add((T)_context.Attach(obj));
                }
                AddNewLocalObjects(_type, result);
                if (e.IsMethodCallExpression("Single"))
                {
                    return result.Single();
                }
                else
                {
                    return result.SingleOrDefault();
                }
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
        /// Performs a GetObjectCall. if ID is Invalid or indicates a new Object, then an Exception will be thrown.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>A Object an Expeption, if the Object was not found.</returns>
        private T GetObjectCall<T>(Expression e)
        {
            if (ID == Helper.INVALIDID) throw new InvalidOperationException("Emtpy Object ID passed");

            IDataObject result = (IDataObject)_context.ContainsObject(_type, ID);
            if (result != null) return (T)result;

            //result = CacheController<IDataObject>.Current.Get(_type, ID);
            if (result == null)
            {
                result = Proxy.Current.GetObject(_type, ID);
                if (result == null) throw new InvalidOperationException(string.Format("Object ID {0} of Type {1} not found", ID, _type));

                //CacheController<IDataObject>.Current.Set(_type, ID, (IDataObject)result.Clone());
            }
            //else
            //{
            //    result = (IDataObject)result.Clone();
            //}
            return (T)_context.Attach(result);
        }

        private T GetObjectOrNewCall<T>(Expression e)
        {
            if (ID <= API.Helper.INVALIDID)
            {
                return (T)_context.Create(_type);
            }
            else
            {
                return GetObjectCall<T>(e);
            }
        }

        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("CreateQuery {0}", expression.ToString()));
            return (IQueryable<TElement>)new KistlContextQuery<TElement>(_context, _type, this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("CreateQuery {0}", expression.ToString()));
            Type elementType = expression.Type.GetCollectionElementType();
            return (IQueryable)Activator.CreateInstance(typeof(KistlContextQuery<>)
                .MakeGenericType(elementType), new object[] { _context, _type, this, expression });
        }

        public TResult Execute<TResult>(Expression e)
        {
            Visit(e);

            switch (SearchType)
            {
                case SearchTypeEnum.GetList:
                    return GetListCall<TResult>(e);
                case SearchTypeEnum.GetObject:
                    return GetObjectCall<TResult>(e);
                case SearchTypeEnum.GetObjectOrNew:
                    return GetObjectOrNewCall<TResult>(e);
                default:
                    throw new InvalidOperationException("Search Type could not be determinated");
            }
        }

        public object Execute(Expression e)
        {
            Visit(e);

            switch (SearchType)
            {
                case SearchTypeEnum.GetList:
                    return GetListCall<List<IDataObject>>(e);
                case SearchTypeEnum.GetObject:
                    return GetObjectCall<IDataObject>(e);
                case SearchTypeEnum.GetObjectOrNew:
                    return GetObjectOrNewCall<IDataObject>(e);
                default:
                    throw new InvalidOperationException("Search Type could not be determinated");
            }
        }

        #endregion

        #region Visits
        protected override void VisitBinary(BinaryExpression b)
        {
            // detect "ID == const" expressions to create GetObject Call
            // TODO: This is not symmetrical and fails to detect "const == ID" expressions
            // that needs improved logic!
            if (b.Left is MemberExpression)
            {
                MemberExpression m = (MemberExpression)b.Left;
                if (typeof(Kistl.API.IPersistenceObject).IsAssignableFrom(m.Member.DeclaringType) && m.Member.Name == "ID")
                {
                    ID = b.Right.GetExpressionValue<int>();
                }
            }

            base.VisitBinary(b);
        }

        protected override void VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                // Save filter
                if (_filter == null)
                {
                    _filter = m.Arguments[1];
                }
                // Override a GetObjectCall
                SearchType = SearchTypeEnum.GetList;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "OrderBy")
            {
                // Save orderby
                if (_orderBy == null)
                {
                    _orderBy = m.Arguments[1];
                }
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Select")
            {
                // It's OK
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "First")
            {
                _maxListCount = 1;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "FirstOrDefault")
            {
                _maxListCount = 1;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Take")
            {
                _maxListCount = m.Arguments[1].GetExpressionValue<int>();
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Single")
            {
                SearchType = SearchTypeEnum.GetObject;
            }
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "SingleOrDefault")
            {
                SearchType = SearchTypeEnum.GetObjectOrNew;
            }
            else
            {
                if (_filter == null)
                {
                    // It is OK to check that here, because GetList will only take a Lambda Expression as an argument
                    // and does _always_ a SELECT.
                    // Method Calls must be implemented here explicit & send explicit to the Server. 
                    // So there is no aggregation hole at this point. 
                    // The only thing that is possible is to do a count on Member Collections, which might be higher
                    // than the returned Objects in that collection (because of Security). 
                    // Sum on the other hand is not possible. This will be checked by the Expression Serializer/Deserializer
                    throw new NotSupportedException(string.Format("Method Call '{0}' is only supported in a WHERE clause", m.Method.Name));
                }
            }

            base.VisitMethodCall(m);
        }
        #endregion
    }
}
