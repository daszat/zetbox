
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.DalProvider.Base;

    public abstract class RelationEntryClientImpl<TA, TAImpl, TB, TBImpl>
        : RelationEntryBaseImpl<TA, TAImpl, TB, TBImpl>, IClientObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, IDataObject
        where TBImpl : class, IDataObject, TB
    {
        protected RelationEntryClientImpl(Func<IFrozenContext> lazyCtx)
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

        #endregion
    }
}
