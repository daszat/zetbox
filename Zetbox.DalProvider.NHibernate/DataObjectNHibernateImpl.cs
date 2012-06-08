
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public abstract class DataObjectNHibernateImpl
        : NHibernatePersistenceObject, IDataObject
    {

        protected DataObjectNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        #region IDataObject Members

        /// <inheritdoc />
        public virtual void NotifyPreSave()
        {
            LogAudits();
            SaveAudits();
        }

        /// <inheritdoc />
        public virtual void NotifyPostSave() { }

        /// <inheritdoc />
        public virtual void NotifyCreated() { }
        /// <inheritdoc />
        public virtual void NotifyDeleting() { }

        public abstract Guid ObjectClassID { get; }

        #endregion

        public virtual void UpdateParent(string propertyName, IDataObject parentObj)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }

        int System.IComparable.CompareTo(object other)
        {
            if (other == null) return 1;
            var aStr = this.ToString();
            var bStr = other.ToString();
            if (aStr == null && bStr == null) return 0;
            if (aStr == null) return -1;
            return aStr.CompareTo(bStr);
        }
    }
}
