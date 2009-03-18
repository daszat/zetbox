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
            RegisterCollectionChanged();
        }

        /// <summary>
        /// deprecated BackReferenceProperty
        /// </summary>
        public ObjectListModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, BackReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            RegisterCollectionChanged();
        }

        private void RegisterCollectionChanged()
        {
            Async.Queue(DataContext, () =>
            {
                INotifyCollectionChanged collection = Object.GetPropertyValue<INotifyCollectionChanged>(Property.PropertyName);
                collection.CollectionChanged += AsyncCollectionChanged;
            });
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectModel>> Members

        public bool HasValue { get { UI.Verify(); return true; } }
        public bool IsNull { get { UI.Verify(); return false; } }

        private ObservableCollection<DataObjectModel> _valueCache = new ObservableCollection<DataObjectModel>();
        private IReadOnlyObservableCollection<DataObjectModel> _valueView;
        public IReadOnlyObservableCollection<DataObjectModel> Value
        {
            get
            {
                UI.Verify();
                if (_valueView == null)
                {
                    _valueView = new ReadOnlyObservableCollectionWrapper<DataObjectModel>(_valueCache);
                }
                return _valueView;
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
                if (this.Property is BackReferenceProperty)
                {
                    baseclass = ((BackReferenceProperty)this.Property).ReferenceProperty.ObjectClass as ObjectClass;
                    // TODO: non-ObjectClass references have to be handled properly too
                    if (baseclass == null)
                        throw new InvalidOperationException();
                }
                else
                {
                    baseclass = ((ObjectReferenceProperty)this.Property).ReferenceObjectClass;
                }

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
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                Object.AddToCollection<IDataObject>(Property.PropertyName, item.Object);
                UI.Queue(UI, () => State = ModelState.Active);
            });
        }

        public void RemoveItem(DataObjectModel item)
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                Object.RemoveFromCollection<IDataObject>(Property.PropertyName, item.Object);
                UI.Queue(UI, () => State = ModelState.Active);
            });
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
            Async.Verify();
            var newValue = Object.GetPropertyValue<IEnumerable>(Property.PropertyName).AsQueryable().Cast<IDataObject>().ToList();
            UI.Queue(UI, () => SyncValues(newValue));
        }

        private void SyncValues(IList<IDataObject> elements)
        {
            UI.Verify();
            _valueCache.Clear();
            foreach (IDataObject obj in elements.Cast<IDataObject>())
            {
                _valueCache.Add(Factory.CreateSpecificModel<DataObjectModel>(DataContext, obj));
            }
        }

        private void AsyncCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // TODO: improve implementation
            Async.Verify();
            var newValue = Object.GetPropertyValue<IEnumerable>(Property.PropertyName).AsQueryable().Cast<IDataObject>().ToList();
            UI.Queue(UI, () => SyncValues(newValue));
        }

        private void AsyncCollectChildClasses(int id, List<ObjectClass> children)
        {
            Async.Verify();

            var nextChildren = GuiContext.GetQuery<ObjectClass>().Where(oc =>
                oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id);

            if (nextChildren.Count() > 0)
                foreach (ObjectClass oc in nextChildren)
                {
                    children.Add(oc);
                    AsyncCollectChildClasses(oc.ID, children);
                };
        }

        #endregion

    }
}
