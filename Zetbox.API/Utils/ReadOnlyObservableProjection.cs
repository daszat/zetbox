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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Zetbox.API.Utils
{
    public interface IReadOnlyObservableList<TValue>
        : IReadOnlyList<TValue>, INotifyCollectionChanged
    {
    }

    public abstract class AbstractObservableProjectedList<TInput, TOutput>
        : AbstractProjectedList<TInput, TOutput>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        protected AbstractObservableProjectedList(ObservableCollection<TInput> list, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter, bool isReadonly)
            : base(list, select, inverter, isReadonly)
        {
            list.CollectionChanged += new NotifyCollectionChangedEventHandler(list_CollectionChanged);
            if (list is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)list).PropertyChanged += new PropertyChangedEventHandler(ReadOnlyObservableProjection_PropertyChanged);
            }
        }

        public AbstractObservableProjectedList(INotifyCollectionChanged notifyingCollection, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter, bool isReadonly)
            : this(notifyingCollection, notifyingCollection, select, inverter, isReadonly)
        {
        }

        public AbstractObservableProjectedList(INotifyCollectionChanged notifier, object collection, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter, bool isReadonly)
            : base(MagicCollectionFactory.WrapAsList<TInput>(collection), select, inverter, isReadonly)
        {
            if(notifier == null) throw new ArgumentNullException("notifier");
            notifier.CollectionChanged += new NotifyCollectionChangedEventHandler(list_CollectionChanged);
            if (notifier is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)notifier).PropertyChanged += new PropertyChangedEventHandler(ReadOnlyObservableProjection_PropertyChanged);
            }
        }

        void ReadOnlyObservableProjection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        void list_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(e.Action,
                        e.NewItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.NewStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Move:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(e.Action,
                        e.NewItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.NewStartingIndex,
                        e.OldStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(e.Action,
                        e.OldItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.OldStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(e.Action,
                        e.NewItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.OldItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.NewStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Reset:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(e.Action));
                    break;
                default:
                    throw new InvalidOperationException(String.Format("Unknown NotifyCollectionChangedAction: {0}", e.Action));
            }
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, args);
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler _propertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, args);
            }
        }

        #endregion
    }

    public sealed class ReadOnlyObservableProjectedList<TInput, TOutput> : AbstractObservableProjectedList<TInput, TOutput>, IReadOnlyObservableList<TOutput>
    {
        public ReadOnlyObservableProjectedList(INotifyCollectionChanged notifyingCollection, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(notifyingCollection, notifyingCollection, select, inverter, true)
        {
        }

        public ReadOnlyObservableProjectedList(INotifyCollectionChanged notifier, object collection, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(notifier, collection, select, inverter, true)
        {
        }
    }

    public sealed class ObservableProjectedList<TInput, TOutput> : AbstractObservableProjectedList<TInput, TOutput>
    {
        public ObservableProjectedList(INotifyCollectionChanged notifyingCollection, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(notifyingCollection, notifyingCollection, select, inverter, false)
        {
        }

        public ObservableProjectedList(INotifyCollectionChanged notifier, object collection, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(notifier, collection, select, inverter, false)
        {
        }
    }
}
