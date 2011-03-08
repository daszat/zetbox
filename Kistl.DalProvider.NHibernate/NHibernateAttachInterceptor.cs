
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using global::NHibernate;

    internal sealed class NHibernateAttachInterceptor
        : EmptyInterceptor
    {
        public NHibernateAttachInterceptor()
        {
        }

        private NHibernateContext _context;
        private bool _contextIsSet = false;
        public NHibernateContext Context
        {
            get
            {
                return _context;
            }
            set
            {
                if (_contextIsSet) { throw new InvalidOperationException("Cannot set NHibernateAttachInterceptor.Context twice"); }
                if (value == null) { throw new ArgumentNullException("value"); }

                _contextIsSet = true;
                _context = value;
            }
        }

        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, global::NHibernate.Type.IType[] types)
        {
            if (Context == null) { throw new InvalidOperationException("Cannot load entity before NHibernateAttachInterceptor.Context is set"); }

            var proxy = entity as IProxyObject;
            if (proxy != null)
                Context.AttachAndWrap(proxy);

            return base.OnLoad(entity, id, state, propertyNames, types);
        }
    }
}
