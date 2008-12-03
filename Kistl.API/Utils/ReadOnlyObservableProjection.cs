using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Kistl.API.Utils
{

    public interface IReadOnlyObservableCollection<TValue>
        : IReadOnlyCollection<TValue>, INotifyCollectionChanged
    {
    }

    public class ReadOnlyObservableProjection<TValue, TResult>
        : ReadOnlyProjection<TValue, TResult>, IReadOnlyObservableCollection<TResult>, INotifyPropertyChanged
    {
        public ReadOnlyObservableProjection(ObservableCollection<TValue> list, Func<TValue, TResult> select)
            : base(list, select)
        {
            list.CollectionChanged += new NotifyCollectionChangedEventHandler(list_CollectionChanged);
            ((INotifyPropertyChanged)list).PropertyChanged += new PropertyChangedEventHandler(ReadOnlyObservableProjection_PropertyChanged);
        }

        public ReadOnlyObservableProjection(INotifyCollectionChanged list, Func<TValue, TResult> select)
            : base((IList<TValue>)list, select)
        {
            list.CollectionChanged += new NotifyCollectionChangedEventHandler(list_CollectionChanged);
            ((INotifyPropertyChanged)list).PropertyChanged += new PropertyChangedEventHandler(ReadOnlyObservableProjection_PropertyChanged);
        }

        void ReadOnlyObservableProjection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        void list_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    break;
                case NotifyCollectionChangedAction.Add:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                        e.NewItems.Cast<TValue>().Select(this.Selector).ToList(),
                        e.NewStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                        e.OldItems.Cast<TValue>().Select(this.Selector).ToList(),
                        e.OldStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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

    /// <summary>
    /// Wrap the <see cref="ReadOnlyObservableCollection&lt;TValue&gt;"/> into
    /// the <see cref="IReadOnlyObservableCollection&lt;TValue&gt;"/> interface.
    /// </summary>
    /// <typeparam name="TValue">The Type of the elements of this collection.</typeparam>
    public class ReadOnlyObservableCollectionWrapper<TValue>
        : ReadOnlyObservableCollection<TValue>, IReadOnlyObservableCollection<TValue>
    {

        public ReadOnlyObservableCollectionWrapper(ObservableCollection<TValue> list)
            : base(list)
        {
        }
    }
}
