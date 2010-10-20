
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    
    [CLSCompliant(false)]
    public abstract class BaseNhCollectionEntry
        : BaseNhPersistenceObject
    {
        protected BaseNhCollectionEntry(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        protected Guid? _fk_guid_A;
        protected Guid? _fk_guid_B;
        protected int? _fk_A;
        protected int? _fk_B;
    }
}
