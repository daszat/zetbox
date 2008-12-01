using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.App.Base;
using System.Collections.Specialized;

namespace Kistl.Client.Presentables
{
    public class ObjectListModel
        : PropertyModel<ICollection<DataObjectModel>>, IReadOnlyValueModel<ReadOnlyObservableCollection<DataObjectModel>>
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

        #region Public interface and IValueModel<ObservableCollection<DataObjectModel>> Members

        public bool HasValue { get { UI.Verify(); return true; } }
        public bool IsNull { get { UI.Verify(); return false; } }

        private ObservableCollection<DataObjectModel> _valueCache = new ObservableCollection<DataObjectModel>();
        private ReadOnlyObservableCollection<DataObjectModel> _valueView;
        public ReadOnlyObservableCollection<DataObjectModel> Value
        {
            get
            {
                UI.Verify();
                if (_valueView == null)
                {
                    _valueView = new ReadOnlyObservableCollection<DataObjectModel>(_valueCache);
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

        public void CreateNewElement(Action<DataObjectModel> onCreated)
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
                    Type targetType = baseclass.GetDataType();
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
                                            Type targetType = ((ObjectClass)chosen.Object).GetDataType();
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
            ObservableCollection<DataObjectModel> newValue = new ObservableCollection<DataObjectModel>();
            foreach (IDataObject obj in elements.Cast<IDataObject>())
            {
                newValue.Add(Factory.CreateSpecificModel<DataObjectModel>(DataContext, obj));
            }
            // almost optimal atomic update
            _valueCache = newValue;
            _valueView = new ReadOnlyObservableCollection<DataObjectModel>(_valueCache);
            OnPropertyChanged("Value");
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
