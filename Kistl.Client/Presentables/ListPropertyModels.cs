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

    /// <summary>
    /// Manages a mutable list of TElements wrapped in TElementModels
    /// </summary>
    /// <typeparam name="TElementModel"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    public abstract class ReferenceListPropertyModel<TElementModel, TElement>
        : PropertyModel<ICollection<TElementModel>>, IValueListModel<TElementModel>
    {
        protected ReferenceListPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            //AllowNullInput = prop.IsNullable;
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

        private TElementModel _selectedItem;
        public TElementModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                // TODO: check whether that works for all structs
                if (!System.Object.Equals(_selectedItem, value))
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private ReadOnlyObservableProjectedList<TElement, TElementModel> _valueCache;
        public IReadOnlyObservableCollection<TElementModel> Value
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

        protected override void GetPropertyValue()
        {
            // AsyncList takes care of all that
        }
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

        public override void ActivateItem(TElement mdl, bool activate)
        {
        }

        protected override void DeleteItemFromDataStore(TElement mdl)
        {
            // Values are deleted automatically from DataStore, when being removed from the collection
        }

        #region IValueListModel<string> Members

        /// <summary>
        /// Convert a string into a TElement, should be overridden if the systems default conversion is not enough.
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
                return SelectedItem != null ? SelectedItem.ToString() : "";
            }
            set
            {
                SelectedItem = ToItem(value);
            }
        }

        private ReadOnlyObservableProjectedList<TElement, string> _stringListCache;
        IReadOnlyObservableCollection<string> IReadOnlyValueModel<IReadOnlyObservableCollection<string>>.Value
        {
            get
            {
                if (_stringListCache == null)
                {
                    _stringListCache = new ReadOnlyObservableProjectedList<TElement, string>(
                        Value,
                        i => i.ToString(),
                        null);
                }
                return _stringListCache;
            }
        }
        #endregion

    }
}