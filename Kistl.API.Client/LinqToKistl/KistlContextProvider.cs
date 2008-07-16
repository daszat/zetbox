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
    internal class KistlContextProvider<T> : ExpressionTreeVisitor, IQueryProvider
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

        internal List<T> GetListOf(int ID, string propertyName)
        {
            List<T> serviceResult = Proxy.Current.GetListOf(_type, ID, propertyName).OfType<T>().ToList();
            List<T> result = new List<T>();
            foreach (Kistl.API.IDataObject obj in serviceResult.OfType<Kistl.API.IDataObject>())
            {
                CacheController<Kistl.API.IDataObject>.Current.Set(obj.GetType(), obj.ID,
                    (Kistl.API.IDataObject)(obj).Clone());

                result.Add((T)_context.Attach(obj));
            }

            return result;
        }

        /// <summary>
        /// Performs a GetListCall
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private object GetListCall(Expression e)
        {
            List<T> serviceResult = Proxy.Current.GetList(_type, _maxListCount, _filter, _orderBy).OfType<T>().ToList();
            List<T> result = new List<T>();
            foreach (IDataObject obj in serviceResult.OfType<IDataObject>())
            {
                CacheController<IDataObject>.Current.Set(obj.GetType(), obj.ID, (IDataObject)obj.Clone());

                result.Add((T)_context.Attach(obj));
            }
            return result;
        }

        /// <summary>
        /// Performs a GetObjectCall. if ID is Invalid or indicates a new Object, then an Exception will be thrown.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>A Object an Expeption, if the Object was not found.</returns>
        private object GetObjectCall(Expression e)
        {
            if (ID == Helper.INVALIDID) throw new InvalidOperationException("Emtpy Object ID passed");

            IDataObject result = (IDataObject)_context.ContainsObject(_type, ID);
            if (result != null) return result;

            result = CacheController<IDataObject>.Current.Get(_type, ID);
            if (result == null)
            {
                result = Proxy.Current.GetObject(_type, ID);
                if (result == null) throw new InvalidOperationException(string.Format("Object ID {0} of Type {1} not found", ID, _type));

                CacheController<IDataObject>.Current.Set(_type, ID, (IDataObject)result.Clone());
            }
            else
            {
                result = (IDataObject)result.Clone();
            }
            return (T)_context.Attach(result);
        }

        private object GetObjectOrNewCall(Expression e)
        {
            if (ID <= API.Helper.INVALIDID)
            {
                return (T)_context.Create(_type);
            }
            else
            {
                return GetObjectCall(e);
            }
        }

        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression e)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("CreateQuery {0}", e.ToString()));
            return new KistlContextQuery<TElement>(_context, _type, this as KistlContextProvider<TElement>, e) as IQueryable<TElement>;
        }

        public IQueryable CreateQuery(Expression e)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("CreateQuery {0}", e.ToString()));
            return new KistlContextQuery<T>(_context, _type, this, e);
        }

        public TResult Execute<TResult>(Expression e)
        {
            return (TResult)(this as IQueryProvider).Execute(e);
        }

        public object Execute(Expression e)
        {
            // Find SearchType
            Visit(e);

            switch(SearchType)
            {
                case SearchTypeEnum.GetList:
                    return GetListCall(e);
                case SearchTypeEnum.GetObject:
                    return GetObjectCall(e);
                case SearchTypeEnum.GetObjectOrNew:
                    return GetObjectOrNewCall(e);
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
