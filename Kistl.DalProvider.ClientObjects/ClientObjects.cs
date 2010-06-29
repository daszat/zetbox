
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    internal interface IClientObject
    {
        void SetUnmodified();
        void SetDeleted();

        BasePersistenceObject UnderlyingObject { get; }
    }

    /// <summary>
    /// ClientObjects specific BaseClientDataObject functionality.
    /// </summary>
    public abstract class BaseClientDataObject_ClientObjects
        : BaseClientDataObject, IClientObject
    {
        protected BaseClientDataObject_ClientObjects() : base(null) { }
        protected BaseClientDataObject_ClientObjects(Func<IReadOnlyKistlContext> lazyCtx) : base(lazyCtx) { }

        #region IClientObject Members

        void IClientObject.SetUnmodified() { base.SetUnmodified(); }
        void IClientObject.SetDeleted() { base.SetDeleted(); }

        BasePersistenceObject IClientObject.UnderlyingObject
        {
            get { return this; }
        }

        #endregion
    }

    public abstract class BaseClientCollectionEntry_ClientObjects
        : BaseClientCollectionEntry, IClientObject
    {
        protected BaseClientCollectionEntry_ClientObjects(Func<IReadOnlyKistlContext> lazyCtx)
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
