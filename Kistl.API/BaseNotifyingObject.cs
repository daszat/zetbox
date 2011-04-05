using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kistl.API
{
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            if (PropertyChangedWithValue != null)
                PropertyChangedWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
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
    }
}
