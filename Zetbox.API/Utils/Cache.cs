// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

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
