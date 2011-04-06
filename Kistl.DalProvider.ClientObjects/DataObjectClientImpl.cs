
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.DalProvider.Base;

    public abstract class DataObjectClientImpl
       : DataObjectBaseImpl, IClientObject
    {
        protected DataObjectClientImpl(Func<IFrozenContext> lazyCtx)
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

        protected override void AuditPropertyChange(string property, object oldValue, object newValue)
        {
            // client objects do not audit changes
        }
    }
}
