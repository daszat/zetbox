
namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.DalProvider.Base;

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

        public abstract Guid PropertyID { get; }
    }
}