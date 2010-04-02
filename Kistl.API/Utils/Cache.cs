using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kistl.API.Utils
{
    public abstract class Cache : INotifyPropertyChanged
    {
        private static readonly object _staticlock = new object();

        private static List<Cache> _caches = new List<Cache>();
        public static event EventHandler CachesCollectionChanged = null;

        protected Cache()
        {
            lock (_staticlock)
            {
                _caches.Add(this);
            }
            EventHandler temp = CachesCollectionChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        ~Cache()
        {
            lock (_staticlock)
            {
                _caches.Remove(this);
            }
            EventHandler temp = CachesCollectionChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public static void ClearAll()
        {
            lock (_staticlock)
            {
                foreach (var c in _caches)
                {
                    c.Clear();
                }
            }
            EventHandler temp = CachesCollectionChanged;
            if (temp != null)
            {
                temp(typeof(Cache), EventArgs.Empty);
            }
        }

        public static ReadOnlyCollection<Cache> Caches
        {
            get
            {
                lock (_staticlock)
                {
                    return _caches.AsReadOnly();
                }
            }
        }

        public abstract int ItemCount { get; }
        public abstract void Clear();

        public virtual string Name
        {
            get
            {
                return this.GetType().FullName;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual void ItemAdded()
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs("ItemCount"));
            }
        }

        public virtual void ItemRemoved()
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs("ItemCount"));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
