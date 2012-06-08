
namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;

    // http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx

    internal class ZetboxContextQuery<T> : IOrderedQueryable<T>
    {
        private Expression _expression = null;
        private ZetboxContextProvider _provider = null;
        private InterfaceType _type;
        private ZetboxContextImpl _context;
        private IPerfCounter _perfCounter;

        #region Constructor
        public ZetboxContextQuery(ZetboxContextImpl ctx, InterfaceType type, IProxy proxy, IPerfCounter perfCounter)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            // if (type == null) throw new ArgumentNullException("type");

            _perfCounter = perfCounter;
            _type = type;
            _context = ctx;
            _expression = System.Linq.Expressions.Expression.Constant(this);
            _provider = new ZetboxContextProvider(_context, _type, proxy, _perfCounter);
        }

        public ZetboxContextQuery(ZetboxContextImpl ctx, InterfaceType type, ZetboxContextProvider provider, Expression expression, IPerfCounter perfCounter)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            // if (type == null) throw new ArgumentNullException("type");
            if (provider == null) throw new ArgumentNullException("provider");
            if (expression == null) throw new ArgumentNullException("expression");
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");

            _type = type;
            _context = ctx;
            _expression = expression;
            _provider = provider;
            _perfCounter = perfCounter;
        }
        #endregion

        #region IEnumerable Members

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_provider.GetListCall<T>(this._expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_provider.GetListCall<T>(this._expression)).GetEnumerator();
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
