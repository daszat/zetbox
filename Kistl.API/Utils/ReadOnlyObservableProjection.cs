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

    public interface IReadOnlyObservableList<TValue>
        : IReadOnlyObservableCollection<TValue>, IReadOnlyList<TValue>
    {
    }

    public class ReadOnlyObservableProjectedCollection<TInput, TOutput>
        : ReadOnlyProjectedCollection<TInput, TOutput>, IReadOnlyObservableCollection<TOutput>, INotifyPropertyChanged
    {
        public ReadOnlyObservableProjectedCollection(ObservableCollection<TInput> collection, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(collection, select, inverter)
        {
            collection.CollectionChanged += new NotifyCollectionChangedEventHandler(OnUnderlyingCollectionChanged);
            if (collection is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)collection).PropertyChanged += new PropertyChangedEventHandler(OnUnderlyingPropertyChanged);
            }
        }

        public ReadOnlyObservableProjectedCollection(INotifyCollectionChanged notifier, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(MagicCollectionFactory.WrapAsCollection<TInput>(notifier), select, inverter)
        {
            notifier.CollectionChanged += new NotifyCollectionChangedEventHandler(OnUnderlyingCollectionChanged);
            if (notifier is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)notifier).PropertyChanged += new PropertyChangedEventHandler(OnUnderlyingPropertyChanged);
            }
        }

        void OnUnderlyingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        void OnUnderlyingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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

    public class ReadOnlyObservableProjectedList<TInput, TOutput>
        : ReadOnlyProjectedList<TInput, TOutput>, IReadOnlyObservableCollection<TOutput>, INotifyPropertyChanged
    {
        public ReadOnlyObservableProjectedList(ObservableCollection<TInput> list, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(list, select, inverter)
        {
            list.CollectionChanged += new NotifyCollectionChangedEventHandler(list_CollectionChanged);
            if (list is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)list).PropertyChanged += new PropertyChangedEventHandler(ReadOnlyObservableProjection_PropertyChanged);
            }
        }

        public ReadOnlyObservableProjectedList(INotifyCollectionChanged notifier, Func<TInput, TOutput> select, Func<TOutput, TInput> inverter)
            : base(MagicCollectionFactory.WrapAsList<TInput>(notifier), select, inverter)
        {
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
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(e.Action,
                        e.NewItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.NewStartingIndex,
                        e.OldStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(e.Action,
                        e.OldItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.OldStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(e.Action,
                        e.NewItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.OldItems.Cast<TInput>().Select(this.Selector).ToList(),
                        e.NewStartingIndex));
                    break;
                case NotifyCollectionChangedAction.Reset:
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(e.Action));
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
