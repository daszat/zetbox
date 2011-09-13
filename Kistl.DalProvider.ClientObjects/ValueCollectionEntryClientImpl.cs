
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.DalProvider.Base;

    public abstract class ValueCollectionEntryClientImpl<TA, TAImpl, TB>
        : CollectionEntryBaseImpl, IClientObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
    {
        protected ValueCollectionEntryClientImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        #region IClientObject Members

        void IClientObject.SetUnmodified() { base.SetUnmodified(); }
        void IClientObject.SetDeleted() { base.SetDeleted(); }

        BasePersistenceObject IClientObject.UnderlyingObject
        {
            get { return this; }
        }
        
        void IClientObject.MakeAccessDeniedProxy()
        {
        }

        #endregion
    }
}