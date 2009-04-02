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

    public delegate void PropertyChangeWithValueEventHandler(object sender, PropertyChangeWithValueEventArgs args);

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
        /// Property is beeing changing
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanging(string property, object oldValue, object newValue)
        {
            if (notifications == null)
            {
                OnPropertyChanging(property, oldValue, newValue);
            }
        }

        /// <summary>
        /// Property has been changed
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            if (notifications == null)
            {
                SetModified();
                OnPropertyChanged(property, oldValue, newValue);
            }
            else
            {
                if (!notifications.Contains(property))
                    notifications.Add(property);
            }
        }

        protected virtual void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(property));

            if (PropertyChangingWithValue != null)
                PropertyChangingWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        protected virtual void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            if (PropertyChangedWithValue != null)
                PropertyChangedWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        private List<string> notifications = null;
        public void RecordNotifications()
        {
            if (notifications == null)
            {
                notifications = new List<string>();
            }
        }

        public void PlaybackNotifications()
        {
            if (notifications == null) return;

            // enable normal notifications before playing back the old
            // this is neccessary to allow handlers to cause normal events
            var localCopy = notifications;
            notifications = null;

            if (PropertyChanged != null)
            {
                localCopy.ForEach(p => OnPropertyChanged(p, null, null));
            }
        }
    }
}
