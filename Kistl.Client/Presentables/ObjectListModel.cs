using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Utils;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Client.Presentables
{
    public class ObjectListModel
        : PropertyModel<ICollection<DataObjectModel>>, IValueListModel<DataObjectModel>
    {

        public ObjectListModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            if (!prop.IsList)
                throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list");
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectModel>> Members

        public bool HasValue { get { UI.Verify(); return true; } }
        public bool IsNull { get { UI.Verify(); return false; } }

        private AsyncList<IDataObject, DataObjectModel> _valueCache;
        public ReadOnlyObservableCollection<DataObjectModel> Value
        {
            get
            {
                UI.Verify();
                if (_valueCache == null)
                {
                    _valueCache = AsyncListFactory.UiCreateMutable<IDataObject, DataObjectModel>(
                        AppContext, DataContext, this,
                        () => Object.GetPropertyValue<INotifyCollectionChanged>(Property.PropertyName),
                        () => MagicCollectionFactory.WrapAsList<IDataObject>(Object.GetPropertyValue<object>(Property.PropertyName)),
                        asyncObj => Factory.CreateSpecificModel<DataObjectModel>(DataContext, asyncObj),
                        uiObject => uiObject.Object);
                }
                return _valueCache.GetUiView();
            }
        }

        private DataObjectModel _selectedItem;
        public DataObjectModel SelectedItem
        {
            get
            {
                UI.Verify();
                return _selectedItem;
            }
            set
            {
                UI.Verify();
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        /// <summary>
        /// Creates a new Item suitable for adding to the list. This may prompt 
        /// the user to choose a type of item to add or enter an initial value.
        /// </summary>
        /// <param name="onCreated">this callback will be called with the newly created item on the UI thread</param>
        /// 
        /// This example creates a new item and activates it for the user to edit:
        /// <example><![CDATA[
        /// model.CreateNewElement(newitem =>
        /// {
        ///     if (newitem != null)
        ///     {
        ///         model.AddItem(newitem);
        ///         model.SelectedItem = newitem;
        ///         model.ActivateItem(model.SelectedItem, true);
        ///     }
        /// });]]>
        /// </example>
        public void CreateNewItem(Action<DataObjectModel> onCreated)
        {
            UI.Verify();

            Async.Queue(DataContext, () =>
            {
                ObjectClass baseclass;

                baseclass = ((ObjectReferenceProperty)this.Property).ReferenceObjectClass;

                var children = new List<ObjectClass>() { baseclass };
                AsyncCollectChildClasses(baseclass.ID, children);

                if (children.Count == 1)
                {
                    var targetType = baseclass.GetDescribedInterfaceType();
                    var item = this.DataContext.Create(targetType);
                    UI.Queue(UI, () => onCreated(Factory.CreateSpecificModel<DataObjectModel>(DataContext, item)));
                }
                else
                {
                    UI.Queue(UI, () =>
                    {
                        // sort by name, create models
                        // TODO: filter non-instantiable classes
                        var childModels = children
                            .OrderBy(oc => oc.ClassName)
                            .Select(oc => (DataObjectModel)Factory.CreateSpecificModel<ObjectClassModel>(DataContext, oc))
                            .ToList();

                        Factory.ShowModel(
                            Factory.CreateSpecificModel<DataObjectSelectionTaskModel>(
                                DataContext,
                                childModels,
                                new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                                {
                                    UI.Verify();
                                    Async.Queue(DataContext, () =>
                                    {
                                        if (chosen != null)
                                        {
                                            var targetType = ((ObjectClass)chosen.Object).GetDescribedInterfaceType();
                                            var item = this.DataContext.Create(targetType);
                                            UI.Queue(UI, () => onCreated(Factory.CreateSpecificModel<DataObjectModel>(DataContext, item)));
                                        }
                                        else
                                        {
                                            UI.Queue(UI, () => onCreated(null));
                                        }
                                    });
                                })), true);
                    });
                }
            });
        }

        public void AddItem(DataObjectModel item)
        {
            UI.Verify();
            _valueCache.AddItem(item);
        }

        public void RemoveItem(DataObjectModel item)
        {
            UI.Verify();
            _valueCache.RemoveItem(item);
        }

        public void DeleteItem(DataObjectModel item)
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                Object.RemoveFromCollection<IDataObject>(Property.PropertyName, item.Object);
                UI.Queue(UI, () =>
                {
                    item.Delete();
                    State = ModelState.Active;
                });
            });
        }

        public void ActivateItem(DataObjectModel item, bool activate)
        {
            Factory.ShowModel(item, activate);
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
        }

        private void AsyncCollectChildClasses(int id, List<ObjectClass> children)
        {
            Async.Verify();

            var nextChildren = MetaContext.GetQuery<ObjectClass>()
                .Where(oc => oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id)
                .ToList();

            if (nextChildren.Count() > 0)
            {
                foreach (ObjectClass oc in nextChildren)
                {
                    children.Add(oc);
                    AsyncCollectChildClasses(oc.ID, children);
                };
            }
        }

        #endregion

    }
}
