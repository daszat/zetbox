
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor("KistlBase", DefaultKind = "Kistl.App.GUI.ObjectListKind", Description = "A list of IDataObjects")]
    public class ObjectListModel
        : PropertyModel<IList<DataObjectModel>>, IValueListModel<DataObjectModel>
    {
        public new delegate ObjectListModel Factory(IKistlContext dataCtx, IDataObject obj, Property prop);

        private readonly ObjectReferenceProperty _property;
        private readonly ObjectClass _referencedClass;
        private readonly IList _list;
        private readonly INotifyCollectionChanged _notifier;

        public ObjectListModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            if (!prop.GetIsList()) { throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list"); }
            if (!GetHasPersistentOrder(prop)) { throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a sorted list"); }

            _property = prop;
            _referencedClass = _property.GetReferencedObjectClass();

            _list = Object.GetPropertyValue<IList>(Property.Name);
            _notifier = _list as INotifyCollectionChanged;

            if (_notifier == null) throw new ArgumentOutOfRangeException("prop", "Unterlying list does not implement INotifyCollectionChanged");
            _notifier.CollectionChanged += ValueListChanged;

            // Move to AddExistingItemCommand
            var relEnd = _property.RelationEnd;
            if (relEnd != null)
            {
                var rel = _property.RelationEnd.Parent;
                if (rel != null)
                {
                    var otherEnd = rel.GetOtherEnd(relEnd);
                    if (otherEnd != null && otherEnd.Multiplicity.UpperBound() > 1 && rel.Containment != ContainmentSpecification.Independent)
                    {
                        _allowAddExisting = false;
                        _allowRemove = false;
                    }
                }
            }
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectModel>> Members

        public bool HasValue
        {
            get { return true; }
        }

        public bool IsNull
        {
            get { return false; }
        }

        private ReadOnlyObservableProjectedList<IDataObject, DataObjectModel> _valueCache;
        public IReadOnlyObservableList<DataObjectModel> Value
        {
            get
            {
                EnsureValueCache();
                return _valueCache;
            }
        }

        private void EnsureValueCache()
        {
            if (_valueCache == null)
            {
                _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectModel>(
                    _notifier,
                    obj => ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(DataContext, obj),
                    mdl => mdl.Object);
            }
        }

        public ObjectClass ReferencedClass
        {
            get { return _referencedClass; }
        }

        public virtual GridDisplayConfiguration DisplayedColumns
        {
            get
            {
                GridDisplayConfiguration result = new GridDisplayConfiguration();
                result.BuildColumns(this.ReferencedClass);
                return result;
            }
        }
        
        private DataObjectModel _selectedItem;
        public DataObjectModel SelectedItem
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
                }
            }
        }

        private ObservableCollection<DataObjectModel> _selectedItems = null;
        public ObservableCollection<DataObjectModel> SelectedItems
        {
            get
            {
                if (_selectedItems == null)
                {
                    _selectedItems = new ObservableCollection<DataObjectModel>();
                }
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                OnPropertyChanged("SelectedItems");
            }
        }

        private static bool GetHasPersistentOrder(ObjectReferenceProperty prop)
        {
            return prop.RelationEnd.Parent.GetOtherEnd(prop.RelationEnd).HasPersistentOrder;
        }

        public bool HasPersistentOrder
        {
            get
            {
                return true;
            }
        }

        private bool _allowAddNew = true;
        public bool AllowAddNew
        {
            get
            {
                return _allowAddNew;
            }
            set
            {
                if (_allowAddNew != value)
                {
                    _allowAddNew = value;
                    OnPropertyChanged("AllowAddNew");
                }
            }
        }

        private bool _allowAddExisting = true;
        public bool AllowAddExisting
        {
            get
            {
                return _allowAddExisting;
            }
            set
            {
                if (_allowAddExisting != value)
                {
                    _allowAddExisting = value;
                    OnPropertyChanged("AllowAddExisting");
                }
            }
        }

        private bool _allowRemove = true;
        public bool AllowRemove
        {
            get
            {
                return _allowRemove;
            }
            set
            {
                if (_allowRemove != value)
                {
                    _allowRemove = value;
                    OnPropertyChanged("AllowRemove");
                }
            }
        }

        private bool _allowDelete = true;
        public bool AllowDelete
        {
            get
            {
                return _allowDelete;
            }
            set
            {
                if (_allowDelete != value)
                {
                    _allowDelete = value;
                    OnPropertyChanged("AllowDelete");
                }
            }
        }

        private ICommand _CreateNewCommand = null;
        public ICommand CreateNewCommand
        {
            get
            {
                if (_CreateNewCommand == null)
                {
                    _CreateNewCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Add new", "Creates a new Item suitable for adding to the list.", () => CreateNewItem(), () => AllowAddNew);
                }
                return _CreateNewCommand;
            }
        }

        private ICommand _AddExistingCommand = null;
        public ICommand AddExistingCommand
        {
            get
            {
                if (_AddExistingCommand == null)
                {
                    _AddExistingCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Add existing", "Adds an existing item into this list.", () => AddExistingItem(), () => AllowAddExisting);
                }
                return _AddExistingCommand;
            }
        }

        private ICommand _RemoveCommand = null;
        public ICommand RemoveCommand
        {
            get
            {
                if (_RemoveCommand == null)
                {
                    _RemoveCommand = ModelFactory.CreateViewModel<SimpleParameterCommandModel<IEnumerable<DataObjectModel>>.Factory>()
                        .Invoke(DataContext, "Remove", "Remove selection from list",
                        (items) => items.ToList().ForEach(i => RemoveItem(i)), // Collection will change while deleting!
                        (items) => items != null && items.Count() > 0 && AllowRemove);
                }
                return _RemoveCommand;
            }
        }

        private ICommand _DeleteCommand = null;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ModelFactory.CreateViewModel<SimpleParameterCommandModel<IEnumerable<DataObjectModel>>.Factory>()
                        .Invoke(DataContext, "Delete", "Delete selection from data store",
                        (items) => items.ToList().ForEach(i => DeleteItem(i)), // Collection will change while deleting!
                        (items) => items != null && items.Count() > 0 && AllowDelete);
                }
                return _DeleteCommand;
            }
        }

        /// <summary>
        /// Creates a new Item suitable for adding to the list. This may prompt 
        /// the user to choose a type of item to add or enter an initial value.
        /// </summary>
        public void CreateNewItem()
        {
            ObjectClass baseclass = ((ObjectReferenceProperty)this.Property).GetReferencedObjectClass();

            var children = new List<ObjectClass>() { baseclass };
            CollectChildClasses(baseclass.ID, children);

            if (children.Count == 1)
            {
                var targetType = baseclass.GetDescribedInterfaceType();
                CreateItemAndActivate(targetType);
            }
            else
            {
                ModelFactory.ShowModel(
                    ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                        DataContext,
                        null,
                        children.AsQueryable(),
                        new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                        {
                            if (chosen != null)
                            {
                                var targetType = ((ObjectClass)chosen.Object).GetDescribedInterfaceType();
                                CreateItemAndActivate(targetType);
                            }
                        }),
                    null), true);
            }
        }

        private void CreateItemAndActivate(InterfaceType targetType)
        {
            var item = this.DataContext.Create(targetType);
            var result = ModelFactory.CreateViewModel<DataObjectModel.Factory>().Invoke(DataContext, item);
            AddItem(result);
            ActivateItem(result, true);
        }


        /// <summary>
        /// Adds an existing item into this ObjectList. Asks the User which should be added.
        /// </summary>
        public void AddExistingItem()
        {
            var ifType = DataContext.GetInterfaceType(Property.GetPropertyType());

            ModelFactory.ShowModel(
                ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                    DataContext,
                    ifType.GetObjectClass(FrozenContext),
                    null,
                    new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                    {
                        if (chosen != null)
                        {
                            AddItem(chosen);
                        }
                    }),
                    null), true);
        }

        public void AddItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _list.Add(item.Object);

            SelectedItem = item;
        }

        public void MoveItemUp(DataObjectModel item)
        {
            if (item == null) { return; }

            var idx = _list.IndexOf(item.Object);
            if (idx > 0)
            {
                _list.RemoveAt(idx);
                _list.Insert(idx - 1, item.Object);
                SelectedItem = item;
            }
        }

        public void MoveItemDown(DataObjectModel item)
        {
            if (item == null) { return; }

            var idx = _list.IndexOf(item.Object);
            if (idx != -1 && idx + 1 < _list.Count)
            {
                _list.RemoveAt(idx);
                _list.Insert(idx + 1, item.Object);
                SelectedItem = item;
            }
        }

        public void RemoveItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _list.Remove(item.Object);
        }

        public void DeleteItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _list.Remove(item.Object);
            item.Delete();
        }

        public void ActivateItem(DataObjectModel item, bool activate)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            ModelFactory.ShowModel(item, activate);
        }

        #endregion

        #region Utilities and UI callbacks

        protected override void UpdatePropertyValue()
        {
            _valueCache = null;
            OnPropertyChanged("Value");
        }

        private void CollectChildClasses(int id, List<ObjectClass> children)
        {
            var nextChildren = FrozenContext.GetQuery<ObjectClass>()
                .Where(oc => oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id)
                .ToList();

            if (nextChildren.Count() > 0)
            {
                foreach (ObjectClass oc in nextChildren)
                {
                    children.Add(oc);
                    CollectChildClasses(oc.ID, children);
                };
            }
        }

        #endregion

        #region Event handlers

        private void ValueListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var prop in e.NewItems.OfType<INotifyPropertyChanged>())
                {
                    prop.PropertyChanged += AnyPropertyChangedHandler;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var prop in e.OldItems.OfType<INotifyPropertyChanged>())
                {
                    prop.PropertyChanged -= AnyPropertyChangedHandler;
                }
            }
        }

        private void AnyPropertyChangedHandler(object sender, EventArgs e)
        {
            OnPropertyChanged("Value");
        }

        #endregion
    }
}
