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

    public class StringListPropertyModel
        : PropertyModel<ICollection<string>>, IValueListModel<string>
    {
        public new delegate StringListPropertyModel Factory(IKistlContext dataCtx, IDataObject obj, Property prop);

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
            _valueCache = Object.GetPropertyValue<ICollection<string>>(Property.Name);
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