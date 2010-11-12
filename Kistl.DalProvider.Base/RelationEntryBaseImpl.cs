
namespace Kistl.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class RelationEntryBaseImpl<AType, AImplType, BType, BImplType>
        : CollectionEntryBaseImpl
        where AType : class, IDataObject
        where AImplType : class, IDataObject, AType
        where BType : class, IDataObject
        where BImplType : class, IDataObject, BType
    {
        protected RelationEntryBaseImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public abstract Guid RelationID { get; }
    }
}
