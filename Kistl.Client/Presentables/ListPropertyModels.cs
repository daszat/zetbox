using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Utils;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{
    public interface IValueListModel<TElement>
        : IReadOnlyValueModel<IReadOnlyObservableCollection<TElement>>
    {

        /// <summary>
        /// Adds the given item to the underlying value. Triggers <see cref="CollectionChanged"/>
        /// on the <see cref="Value"/> when the change has propagated.
        /// </summary>
        void AddItem(TElement item);

        /// <summary>
        /// Remove the given item from the underlying value. Triggers <see cref="CollectionChanged"/>
        /// on the <see cref="Value"/> when the change has propagated.
        /// </summary>
        void RemoveItem(TElement item);

        /// <summary>
        /// Permanentely delete the given item from the data store.
        /// Triggers <see cref="CollectionChanged"/> on the <see cref="Value"/> when the change has propagated.
        /// </summary>
        void DeleteItem(TElement item);

        /// <summary>
        /// Activates the item for the user to edit.
        /// </summary>
        /// <param name="item">the item to activate</param>
        /// <param name="activate">whether or not to raise the item to the top</param>
        void ActivateItem(TElement item, bool activate);

        /// <summary>
        /// Stores the currently selected item of this list. 
        /// </summary>
        TElement SelectedItem { get; set; }
    }

    public abstract class ReferenceListPropertyModel<TElementModel, TElement>
        : PropertyModel<ICollection<TElementModel>>, IValueListModel<TElementModel>
    {
        public ReferenceListPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            //AllowNullInput = prop.IsNullable;

            RegisterCollectionChanged();
        }

        /// <summary>
        /// registers a callback to handle changes of the underlying model
        /// </summary>
        private void RegisterCollectionChanged()
        {
            Async.Queue(DataContext, () =>
            {
                INotifyCollectionChanged collection = Object.GetPropertyValue<INotifyCollectionChanged>(Property.PropertyName);
                collection.CollectionChanged += AsyncCollectionChanged;
            });
        }

        #region Public Interface and IReadOnlyValueModel<IList<TValue>> Members

        public bool HasValue { get { UI.Verify(); return true; } }
        public bool IsNull { get { UI.Verify(); return false; } }

        private TElementModel _selectedItem;
        public TElementModel SelectedItem
        {
            get
            {
                UI.Verify();
                return _selectedItem;
            }
            set
            {
                UI.Verify();
                // TODO: check whether that works for all structs
                if (!System.Object.Equals(_selectedItem, value))
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private ObservableCollection<TElementModel> _valueCache = new ObservableCollection<TElementModel>();
        private IReadOnlyObservableCollection<TElementModel> _valueView;
        public IReadOnlyObservableCollection<TElementModel> Value
        {
            get
            {
                UI.Verify();
                if (_valueView == null)
                {
                    _valueView = new ReadOnlyObservableCollectionWrapper<TElementModel>(_valueCache);
                }
                return _valueView;
            }
        }

        public void AddItem(TElementModel mdl)
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                Object.AddToCollection<TElement>(Property.PropertyName, GetItem(mdl));
                UI.Queue(UI, () => State = ModelState.Active);
            });
        }

        public void RemoveItem(TElementModel mdl)
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                Object.RemoveFromCollection<TElement>(Property.PropertyName, GetItem(mdl));
                UI.Queue(UI, () => State = ModelState.Active);
            });
        }

        public void DeleteItem(TElementModel mdl)
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                Object.RemoveFromCollection<TElement>(Property.PropertyName, GetItem(mdl));
                AsyncDeleteItem(GetItem(mdl));
                UI.Queue(UI, () => State = ModelState.Active);
            });
        }

        public abstract void ActivateItem(TElementModel mdl, bool activate);

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            var newValue = Object.GetPropertyValue<IEnumerable>(Property.PropertyName).AsQueryable().Cast<TElement>().ToList();
            UI.Queue(UI, () => SyncValues(newValue));
        }

        private void SyncValues(IList<TElement> elements)
        {
            UI.Verify();
            _valueCache.Clear();
            foreach (TElement e in elements)
            {
                _valueCache.Add(GetModel(e));
            }
        }

        private void AsyncCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // TODO: improve implementation
            Async.Verify();
            var newValue = Object.GetPropertyValue<IEnumerable>(Property.PropertyName).AsQueryable().Cast<TElement>().ToList();
            UI.Queue(UI, () => SyncValues(newValue));
        }

        #endregion

        /// <summary>
        /// retrieves the Model for a given item
        /// </summary>
        protected abstract TElementModel GetModel(TElement item);
        /// <summary>
        /// retrieves the item from a given Model
        /// </summary>
        protected abstract TElement GetItem(TElementModel mdl);
        /// <summary>
        /// deletes the given element from the data store after it is removed from the collection
        /// </summary>
        protected abstract void AsyncDeleteItem(TElement mdl);
    }

    /// <summary>
    /// A simple <see cref="ReferenceListPropertyModel"/> where TElement doesn't need to be modelled.
    /// This implies that TElement should not be an IDataObject
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public class SimpleReferenceListPropertyModel<TElement>
        : ReferenceListPropertyModel<TElement, TElement>, IValueListModel<string>
        where TElement : class
    {
        public SimpleReferenceListPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
        }

        protected override TElement GetModel(TElement item)
        {
            return item;
        }

        protected override TElement GetItem(TElement mdl)
        {
            return mdl;
        }

        protected override void AsyncDeleteItem(TElement mdl)
        {
            // since TElement needs no Model, it is no IDataObject.
            // since it is no IDataObject, it doesn't need to be deleted.
        }

        public override void ActivateItem(TElement mdl, bool activate)
        {
        }

        #region IValueListModel<string> Members

        /// <summary>
        /// Convert a string into a TElement
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual TElement ToItem(string str)
        {
            return (TElement)System.Convert.ChangeType(str, typeof(TElement));
        }

        void IValueListModel<string>.AddItem(string str)
        {
            AddItem(ToItem(str));
        }

        void IValueListModel<string>.RemoveItem(string str)
        {
            RemoveItem(ToItem(str));
        }

        void IValueListModel<string>.DeleteItem(string str)
        {
            DeleteItem(ToItem(str));
        }

        void IValueListModel<string>.ActivateItem(string str, bool activate)
        {
            ActivateItem(ToItem(str), activate);
        }

        string IValueListModel<string>.SelectedItem
        {
            get
            {
                UI.Verify();
                return SelectedItem != null ? SelectedItem.ToString() : "";
            }
            set
            {
                UI.Verify();
                SelectedItem = ToItem(value);
            }
        }

        #endregion

        #region IReadOnlyValueModel<ReadOnlyObservableCollection<string>> Members

        private ReadOnlyObservableProjection<TElement, string> _stringListCache;
        IReadOnlyObservableCollection<string> IReadOnlyValueModel<IReadOnlyObservableCollection<string>>.Value
        {
            get
            {
                UI.Verify();
                if (_stringListCache == null)
                {
                    _stringListCache = new ReadOnlyObservableProjection<TElement, string>(this.Value, e => e != null ? e.ToString() : "");
                }
                return _stringListCache;
            }
        }

        #endregion
    }
}