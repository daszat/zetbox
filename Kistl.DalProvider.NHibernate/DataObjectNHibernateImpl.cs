
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
        private readonly IAuditable _auditable;
        private Dictionary<string, Notification> _auditLog;

        protected DataObjectNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
            _auditable = this as IAuditable;
        }

        #region IDataObject Members

        /// <inheritdoc />
        public virtual void NotifyPreSave()
        {
            AuditLog();
            AuditSave();
        }

        private void AuditLog()
        {
            if (!Kistl.API.Utils.Logging.Log.IsWarnEnabled || _auditLog == null)
                return;

            foreach (var msg in _auditLog.Values)
            {
                Kistl.API.Utils.Logging.Log.WarnFormat("{0}.{1} changed from '{2}' to '{3}'",
                    this.GetType().Name, msg.property, msg.oldValue, msg.newValue);
            }
        }

        private void AuditSave()
        {
            if (_auditable == null) return;

            if (_auditLog != null)
            {
                foreach (var msg in _auditLog.Values)
                {
                    var entry = Context.CreateCompoundObject<AuditEntry>();
                    entry.Identity = "unknown";
                    entry.MessageFormat = "{0} changed from '{1}' to '{2}'";
                    entry.PropertyName = msg.property;
                    entry.OldValue = msg.oldValue == null ? String.Empty : msg.oldValue.ToString();
                    entry.NewValue = msg.newValue == null ? String.Empty : msg.newValue.ToString();
                    _auditable.AuditJournal.Add(entry);
                }
            }
            else if (this.ObjectState == DataObjectState.New)
            {
                var entry = Context.CreateCompoundObject<AuditEntry>();
                entry.Identity = "unknown";
                entry.MessageFormat = "object created";
                entry.PropertyName = String.Empty;
                entry.OldValue = String.Empty;
                entry.NewValue = String.Empty;
                _auditable.AuditJournal.Add(entry);
            }
        }

        /// <inheritdoc />
        public virtual void NotifyPostSave() { }

        /// <inheritdoc />
        public virtual void NotifyCreated() { }
        /// <inheritdoc />
        public virtual void NotifyDeleting() { }

        /// <inheritdoc />
        public virtual Kistl.API.AccessRights CurrentAccessRights
        {
            get { return Kistl.API.AccessRights.Full; }
        }

        #endregion

        public virtual void UpdateParent(string propertyName, int? id)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            AuditPropertyChanged(property, oldValue, newValue);
        }

        private void AuditPropertyChanged(string property, object oldValue, object newValue)
        {
            // only audit if we can/want record the changes
            if (_auditable == null)
                return;

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
                case "AuditJournal":
                    return;
            }

            // save memory by allocating lazily
            if (_auditLog == null)
                _auditLog = new Dictionary<string, Notification>();

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
