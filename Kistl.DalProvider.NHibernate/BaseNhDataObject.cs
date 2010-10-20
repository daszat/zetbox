
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class BaseNhDataObject
        : BaseNhPersistenceObject, IDataObject
    {
        protected BaseNhDataObject(Func<IFrozenContext> lazyCtx) : base(lazyCtx) { }

        #region IDataObject Members

        /// <inheritdoc />
        public virtual void NotifyPreSave() { }
        /// <inheritdoc />
        public virtual void NotifyPostSave() { }

        /// <inheritdoc />
        public virtual void NotifyCreated() { }
        /// <inheritdoc />
        public virtual void NotifyDeleting() { }

        /// <inheritdoc />
        public AccessRights CurrentAccessRights
        {
            get;
            protected set;
        }

        #endregion

        public virtual void UpdateParent(string propertyName, int? id)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }
    }
}
