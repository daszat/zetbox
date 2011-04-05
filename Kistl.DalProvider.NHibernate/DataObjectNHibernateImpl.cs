
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class DataObjectNHibernateImpl
        : NHibernatePersistenceObject, IDataObject
    {
        private Dictionary<string, Notification> _auditLog;

        protected DataObjectNHibernateImpl(Func<IFrozenContext> lazyCtx) : base(lazyCtx) { }

        #region IDataObject Members

        /// <inheritdoc />
        public virtual void NotifyPreSave()
        {
            if (Kistl.API.Utils.Logging.Log.IsWarnEnabled && _auditLog != null)
            {
                foreach (var msg in _auditLog.Values)
                {
                    Kistl.API.Utils.Logging.Log.WarnFormat("{0}.{1} changed from '{2}' to '{3}'",
                        this.GetType().Name, msg.property, msg.oldValue, msg.newValue);
                }
            }
        }

        /// <inheritdoc />
        public virtual void NotifyPostSave() { }

        /// <inheritdoc />
        public virtual void NotifyCreated() { }
        /// <inheritdoc />
        public virtual void NotifyDeleting() { }

        /// <inheritdoc />
        public virtual AccessRights CurrentAccessRights
        {
            get { return AccessRights.Full; }
        }

        #endregion

        public virtual void UpdateParent(string propertyName, int? id)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // only audit on modified objects
            if (ObjectState != DataObjectState.Modified)
                return;

            // do not audit internal properties
            switch (property)
            {
                case "ID":
                case "ObjectState":
                case "ChangedOn":
                case "ChangedBy":
                    return;
            }

            if (_auditLog == null)
            {
                _auditLog = new Dictionary<string, Notification>();
            }

            if (_auditLog.ContainsKey(property))
                _auditLog[property] = new Notification(_auditLog[property], newValue);
            else
                _auditLog[property] = new Notification(property, oldValue, newValue);
        }

        private sealed class Notification
        {
            public readonly string property;
            public readonly object oldValue;
            public readonly object newValue;
            public Notification(string property, object oldValue, object newValue)
            {
                this.property = property;
                this.oldValue = oldValue;
                this.newValue = newValue;
            }
            public Notification(Notification oldNotification, object newValue)
            {
                this.property = oldNotification.property;
                this.oldValue = oldNotification.oldValue;
                this.newValue = newValue;
            }
        }
    }
}
