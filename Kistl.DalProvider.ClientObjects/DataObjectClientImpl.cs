
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.DalProvider.Base;
    using System.IO;

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

        void IClientObject.MakeAccessDeniedProxy()
        {
            _currentAccessRights = AccessRights.None;
            SetUnmodified();
        }

        #endregion

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// Base implementations returnes always Full
        /// </summary>
        private Kistl.API.AccessRights _currentAccessRights = Kistl.API.AccessRights.Full;
        public override Kistl.API.AccessRights CurrentAccessRights
        {
            get
            {
                return _currentAccessRights;
            }
        }

        protected override void AuditPropertyChange(string property, object oldValue, object newValue)
        {
            // client objects do not audit changes
        }

        protected override void ApplyRightsFromStream(AccessRights rights)
        {
            base.ApplyRightsFromStream(rights);
            _currentAccessRights = rights;
        }
    }
}
