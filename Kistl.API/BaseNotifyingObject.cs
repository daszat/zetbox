namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    public class PropertyChangeWithValueEventArgs : EventArgs
    {
        public PropertyChangeWithValueEventArgs(string propName, object oldValue, object newValue)
        {
            PropertyName = propName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string PropertyName { get; private set; }
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }
    }

    public delegate void PropertyChangeWithValueEventHandler(object sender, PropertyChangeWithValueEventArgs e);

    public interface INotifyingObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        event PropertyChangeWithValueEventHandler PropertyChangedWithValue;
        event PropertyChangeWithValueEventHandler PropertyChangingWithValue;
    }

    public abstract class BaseNotifyingObject : INotifyingObject
    {
        protected abstract void SetModified();

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue;
        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue;

        /// <summary>
        /// Notifies that a property will be changed
        /// </summary>
        /// <param name="property">Propertyname</param>
        /// <param name="oldValue">old value of the changing property</param>
        /// <param name="newValue">new value of the changing property</param>
        public virtual void NotifyPropertyChanging(string property, object oldValue, object newValue)
        {
            if (notifications == null)
            {
                OnPropertyChanging(property, oldValue, newValue);
            }
        }

        /// <summary>
        /// Notifies that a property has been changed
        /// </summary>
        /// <param name="property">Propertyname</param>
        /// <param name="oldValue">old value of the changed property</param>
        /// <param name="newValue">new value of the changed property</param>
        public virtual void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            if (property != "ObjectState" && property != "ID") SetModified();

            if (notifications == null)
            {
                OnPropertyChanged(property, oldValue, newValue);
            }
            else
            {
                if (notifications.ContainsKey(property))
                    notifications[property] = new Notification(notifications[property], newValue);
                else
                    notifications[property] = new Notification(property, oldValue, newValue);
            }
        }

        /// <summary>
        /// Fires PropertyChanging and PropertyChangingWithValue events.
        /// </summary>
        /// <remarks>This method should not be called directy. Use NofityPropertyChanging.</remarks>
        /// <param name="property">Propertyname</param>
        /// <param name="oldValue">old value of the changed property</param>
        /// <param name="newValue">new value of the changed property</param>
        protected virtual void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(property));

            if (PropertyChangingWithValue != null)
                PropertyChangingWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        /// <summary>
        /// Fires PropertyChanged and PropertyChangedWithValue events
        /// </summary>
        /// <remarks>This method should not be called directy. Use NofityPropertyChanging.</remarks>
        /// <param name="property">Propertyname</param>
        /// <param name="oldValue">old value of the changed property</param>
        /// <param name="newValue">new value of the changed property</param>
        protected virtual void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            SetInitializedProperty(property);

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            if (PropertyChangedWithValue != null)
                PropertyChangedWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));

            AuditPropertyChange(property, oldValue, newValue);
        }

        protected sealed class Notification
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
                if (oldNotification == null) throw new ArgumentNullException("oldNotification");
                this.property = oldNotification.property;
                this.oldValue = oldNotification.oldValue;
                this.newValue = newValue;
            }
        }

        private Dictionary<string, Notification> notifications = null;
        /// <summary>
        /// Records notifications. PropertyChanged &amp; PropertyChanging will not be fired until <see cref="PlaybackNotifications"/> is called.
        /// This function does nothing if it is called more then once.
        /// </summary>
        public void RecordNotifications()
        {
            if (notifications == null)
            {
                notifications = new Dictionary<string, Notification>();
            }
        }

        /// <summary>
        /// Playback all recorded notifications. Only the PropertyChanged event is fired
        /// This function does nothing if it is called more then once.
        /// </summary>
        public void PlaybackNotifications()
        {
            if (notifications == null) return;

            // enable normal notifications before playing back the old
            // this is neccessary to allow handlers to cause normal events
            Dictionary<string, Notification> localCopy = notifications;
            notifications = null;

            foreach (var notification in localCopy.Values)
            {
                OnPropertyChanged(notification.property, notification.oldValue, notification.newValue);
            }
        }

        protected bool IsRecordingNotifications
        {
            get
            {
                return notifications != null;
            }
        }

        #region Auditing

        private Dictionary<string, Notification> _auditLog;
        protected Dictionary<string, Notification> AuditLog { get { return _auditLog; } }

        private static readonly log4net.ILog _auditLogger = log4net.LogManager.GetLogger("Kistl.Audits");

        protected void LogAudits()
        {
            if (!_auditLogger.IsDebugEnabled || _auditLog == null || _auditLog.Count == 0)
                return;

            foreach (var msg in _auditLog.Values)
            {
                _auditLogger.DebugFormat("{0}.{1} changed from '{2}' to '{3}'",
                    this.GetType().Name, msg.property, msg.oldValue, msg.newValue);
            }
        }

        protected virtual void AuditPropertyChange(string property, object oldValue, object newValue)
        {
            if (Object.ReferenceEquals(oldValue, newValue) || (oldValue == null && newValue == null) || (oldValue != null && oldValue.Equals(newValue)))
                return;

            // save memory by allocating lazily
            if (_auditLog == null)
                _auditLog = new Dictionary<string, Notification>();

            if (_auditLog.ContainsKey(property))
                _auditLog[property] = new Notification(_auditLog[property], newValue);
            else
                _auditLog[property] = new Notification(property, oldValue, newValue);
        }

        #endregion

        #region IsInitialized
        /// <summary>
        /// Contains all NOT initialized properties
        /// </summary>
        private Dictionary<string, object> _notInitializedProperties;

        protected void SetNotInitializedProperty(string propName)
        {
            if (_notInitializedProperties == null)
            {
                _notInitializedProperties = new Dictionary<string, object>();
            }
            _notInitializedProperties.Add(propName, null);
        }

        protected void SetInitializedProperty(string propName)
        {
            if (_notInitializedProperties != null && _notInitializedProperties.ContainsKey(propName))
            {
                _notInitializedProperties.Remove(propName);
            }
        }

        public bool IsInitialized(string propName)
        {
            return _notInitializedProperties == null || _notInitializedProperties.ContainsKey(propName) == false;
        }
        #endregion

        public virtual void Recalculate(string propName)
        {
            throw new ArgumentOutOfRangeException("propName", string.Format("There is no calculated property '{0}' that could be re calculated", propName));
        }
    }
}
