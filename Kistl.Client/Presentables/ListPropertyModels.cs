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
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.Presentables
{
    public class StringListPropertyModel
        : PropertyModel<ICollection<string>>, IValueListViewModel<string, IReadOnlyObservableList<string>>
    {
        public new delegate StringListPropertyModel Factory(IKistlContext dataCtx, IDataObject obj, Property prop);

        private readonly StringProperty _property;

        public StringListPropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
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
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region IValueViewModel Members


        public void ClearValue()
        {
            throw new NotImplementedException();
        }

        public ICommand ClearValueCommand
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}