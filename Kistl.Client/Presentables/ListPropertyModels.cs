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
        : IReadOnlyValueModel<IReadOnlyObservableList<TElement>>
    {
        /// <summary>
        /// Gets a value whether or not this list has a persistent order. 
        /// </summary>
        /// <remarks>
        /// While lists on the client always have a definite order, the order 
        /// is only persisted if the underlying datamodel actually supports 
        /// this.
        /// </remarks>
        bool HasPersistentOrder { get; }

        /// <summary>
        /// Adds the given item to the underlying value. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IReadOnlyValueModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void AddItem(TElement item);

        /// <summary>
        /// Moves the given item one item up in the list. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IReadOnlyValueModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void MoveItemUp(TElement item);

        /// <summary>
        /// Moves the given item one item down in the list. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IReadOnlyValueModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void MoveItemDown(TElement item);

        /// <summary>
        /// Remove the given item from the underlying value. Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// on the underlying <see cref="IReadOnlyValueModel{TValue}.Value"/> property when the change has propagated.
        /// </summary>
        void RemoveItem(TElement item);

        /// <summary>
        /// Permanentely delete the given item from the data store.
        /// Triggers <see cref="INotifyCollectionChanged.CollectionChanged"/> on the underlying <see cref="IReadOnlyValueModel{TValue}.Value"/> property when the change has propagated.
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

    /// <summary>
    /// Manages a mutable list of TElements wrapped in TElementModels
    /// </summary>
    /// <typeparam name="TElementModel"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    public abstract class ReferenceListPropertyModel<TElementModel, TElement>
        : PropertyModel<ICollection<TElementModel>>, IValueListModel<TElementModel>
    {
        private readonly ValueTypeProperty _property;
        protected ReferenceListPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            if (!prop.IsList) { throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list"); }

            _property = prop;
            //AllowNullInput = prop.IsNullable();
        }

        #region Public Interface and IReadOnlyValueModel<IList<TValue>> Members

        public bool HasValue
        {
            get { return true; }
        }

        public bool IsNull
        {
            get { return false; }
        }

        public bool HasPersistentOrder
        {
            get
            {
                return _property.HasPersistentOrder;
            }
        }

        private TElementModel _selectedItem;
        public TElementModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                // TODO: check whether that works for all CompoundObject
                if (!System.Object.Equals(_selectedItem, value))
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private ReadOnlyObservableProjectedList<TElement, TElementModel> _valueCache;
        public IReadOnlyObservableList<TElementModel> Value
        {
            get
            {
                if (_valueCache == null)
                {
                    _valueCache = new ReadOnlyObservableProjectedList<TElement, TElementModel>(
                        Object.GetPropertyValue<INotifyCollectionChanged>(Property.PropertyName),
                        GetModel, null);
                }
                return _valueCache;
            }
        }

        public void AddItem(TElementModel mdl)
        {
            Object.AddToCollectionQuick(Property.PropertyName, GetItem(mdl));
        }

        public void MoveItemUp(TElementModel item)
        {
            throw new NotImplementedException();
        }

        public void MoveItemDown(TElementModel item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(TElementModel mdl)
        {
            Object.RemoveFromCollectionQuick(Property.PropertyName, GetItem(mdl));
        }

        public void DeleteItem(TElementModel mdl)
        {
            RemoveItem(mdl);
            DeleteItemFromDataStore(GetItem(mdl));
        }

        public abstract void ActivateItem(TElementModel mdl, bool activate);

        #endregion

        #region Utilities and UI callbacks

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
        protected abstract void DeleteItemFromDataStore(TElement mdl);

        protected override void UpdatePropertyValue()
        {
            _valueCache = null;
            OnPropertyChanged("Value");
        }
    }

    public class StringListPropertyModel
        : PropertyModel<ICollection<string>>, IValueListModel<string>
    {
        private readonly StringProperty _property;

        public StringListPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, StringProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            if (!prop.IsList) { throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list"); }

            _property = prop;
            UpdatePropertyValueCore();
        }

        private void UpdatePropertyValueCore()
        {
            _valueCache = Object.GetPropertyValue<ICollection<string>>(Property.PropertyName);
            _observableValueCache = new ObservableCollection<string>(_valueCache);
            _valueView = new ReadOnlyObservableCollectionWrapper<string>(_observableValueCache);
        }

        private ICollection<string> _valueCache;
        private ObservableCollection<string> _observableValueCache;
        private ReadOnlyObservableCollectionWrapper<string> _valueView;

        protected override void UpdatePropertyValue()
        {
            UpdatePropertyValueCore();
            OnPropertyChanged("Value");
        }

        #region IValueListModel<string> Members

        public bool HasPersistentOrder
        {
            get
            {
                return _property.HasPersistentOrder;
            }
        }

        public void AddItem(string item)
        {
            try
            {
                _valueCache.Add(item);
                _observableValueCache.Add(item);
            }
            catch
            {
                UpdatePropertyValue();
                throw;
            }
        }

        public void MoveItemUp(string item)
        {
            throw new NotImplementedException();
        }

        public void MoveItemDown(string item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string item)
        {
            try
            {
                _valueCache.Remove(item);
                _observableValueCache.Remove(item);
            }
            catch
            {
                UpdatePropertyValue();
                throw;
            }
        }

        public void DeleteItem(string item)
        {
            RemoveItem(item);
        }

        public void ActivateItem(string item, bool activate)
        {
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("SelectedItemEditable");
                }
            }
        }

        public string SelectedItemEditable
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    RemoveItem(_selectedItem);
                    _selectedItem = value;
                    AddItem(_selectedItem);
                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("SelectedItemEditable");
                }
            }
        }

        #endregion

        #region IReadOnlyValueModel<IReadOnlyObservableCollection<string>> Members

        public bool HasValue
        {
            get { return true; }
        }

        public bool IsNull
        {
            get { return !HasValue; }
        }

        public IReadOnlyObservableList<string> Value
        {
            get { return _valueView; }
        }

        #endregion
    }
}