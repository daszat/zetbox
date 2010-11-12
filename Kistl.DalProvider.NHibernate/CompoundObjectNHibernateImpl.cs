
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public abstract class CompoundObjectNHibernateImpl
        : BaseCompoundObject
    {
        protected CompoundObjectNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }
    }
}
