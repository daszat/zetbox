using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.IO;


namespace Kistl.API.Client
{
    public class KistlContextQuery<T> : IOrderedQueryable<T>
    {
        private Expression _expression = null;
        private KistlContextProvider _provider = null;
        private Type _type = null;
        private IKistlContext _context;

        #region Constructor
        internal KistlContextQuery(IKistlContext ctx, Type type)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (type == null) throw new ArgumentNullException("type");

            _type = type;
            _context = ctx;
            _expression = System.Linq.Expressions.Expression.Constant(this);
            _provider = new KistlContextProvider(_context, _type);
        }

        internal KistlContextQuery(IKistlContext ctx, Type type, KistlContextProvider provider, Expression expression)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (type == null) throw new ArgumentNullException("type");
            if (provider == null) throw new ArgumentNullException("provider");
            if (expression == null) throw new ArgumentNullException("expression");

            _type = type;
            _context = ctx;
            _expression = expression;
            _provider = provider;
        }
        #endregion

        #region IEnumerable Members
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_provider.Execute<List<T>>(this._expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_provider.Execute(this._expression)).GetEnumerator();
        }
        #endregion

        #region IQueryable Members
        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression
        {
            get { return _expression; }
        }

        public IQueryProvider Provider
        {
            get { return _provider; }
        }
        #endregion
    }
}
