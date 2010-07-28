
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor("KistlBase", DefaultKind = "Kistl.App.GUI.ObjectCollectionKind", Description = "A collection of IDataObjects")]
    public class ObjectCollectionModel
        : PropertyModel<ICollection<DataObjectModel>>, IValueCollectionModel<DataObjectModel>
    {
        public new delegate ObjectCollectionModel Factory(IKistlContext dataCtx, IDataObject obj, Property prop);

        private readonly ObjectReferenceProperty _property;
        private readonly ObjectClass _referencedClass;
        private readonly ICollection _collection;
        private readonly INotifyCollectionChanged _notifier;

        public ObjectCollectionModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            if (!prop.GetIsList()) { throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list"); }
            if (GetHasPersistentOrder(prop)) { throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must not be a sorted list"); }

            _property = prop;
            _referencedClass = _property.GetReferencedObjectClass();

            _collection = Object.GetPropertyValue<ICollection>(Property.Name);
            _notifier = _collection as INotifyCollectionChanged;
            
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

        private SortedWrapper _wrapper = null;
        private class SortedWrapper : INotifyCollectionChanged, IList<IDataObject>
        {
            private List<IDataObject> _sortedList;
            private ICollection _collection;
            private INotifyCollectionChanged _notifier;

            private string _sortProp = "ID";
            private ListSortDirection _direction = ListSortDirection.Ascending;

            public SortedWrapper(ICollection collection, INotifyCollectionChanged notifier)
            {
                _collection = collection;
                _notifier = notifier;
                notifier.CollectionChanged += new NotifyCollectionChangedEventHandler(notifier_CollectionChanged);
                Sort(_sortProp, _direction);
            }

            public void Sort(string p, ListSortDirection direction)
            {
                _sortProp = p;
                _direction = direction;
                _sortedList = _collection.AsQueryable()
                    .OrderBy(string.Format("{0} {1}", _sortProp, _direction == ListSortDirection.Descending ? "desc" : string.Empty))
                    .Cast<IDataObject>()
                    .ToList();
                OnCollectionChanged();
            }

            private void OnCollectionChanged()
            {
                var temp = CollectionChanged;
                if (temp != null)
                {
                    temp(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }

            void notifier_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                Sort(_sortProp, _direction);
            }

            #region INotifyCollectionChanged Members

            public event NotifyCollectionChangedEventHandler CollectionChanged;

            #endregion

            #region IList<IDataObject> Members

            public int IndexOf(IDataObject item)
            {
                return _sortedList.IndexOf(item);
            }

            public void Insert(int index, IDataObject item)
            {
                throw new NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotImplementedException();
            }

            public IDataObject this[int index]
            {
                get
                {
                    return _sortedList[index];
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            #endregion

            #region ICollection<IDataObject> Members

            public void Add(IDataObject item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(IDataObject item)
            {
                return _sortedList.Contains(item);
            }

            public void CopyTo(IDataObject[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get { return _sortedList.Count; }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(IDataObject item)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable<IDataObject> Members

            public IEnumerator<IDataObject> GetEnumerator()
            {
                return _sortedList.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _sortedList.GetEnumerator();
            }

            #endregion
        }

        private void EnsureValueCache()
        {
            if (_valueCache == null)
            {
                _wrapper = new SortedWrapper(_collection, _notifier);
                _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectModel>(
                    _wrapper,
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
                GridDisplayConfiguration result = new GridDisplayConfiguration()
                {
                    ShowIcon = ReferencedClass.ShowIconInLists,
                    ShowId = ReferencedClass.ShowIdInLists,
                    ShowName = ReferencedClass.ShowNameInLists
                };

                var group = this.ReferencedClass.GetAllProperties()
                    .Where(p => (p.CategoryTags ?? String.Empty).Split(',', ' ').Contains("Summary"));
                if (group.Count() == 0)
                {
                    group = this.ReferencedClass.GetAllProperties().Where(p =>
                    {
                        var orp = p as ObjectReferenceProperty;
                        if (orp == null) { return true; }

                        switch (orp.RelationEnd.Parent.GetRelationType())
                        {
                            case RelationType.n_m:
                                return false; // don't display lists in grids
                            case RelationType.one_n:
                                return orp.RelationEnd.Multiplicity.UpperBound() > 1; // if we're "n", the navigator is a pointer, not a list
                            case RelationType.one_one:
                                return true; // can always display
                            default:
                                return false; // something went wrong
                        }
                    });
                }

                result.Columns = group
                    .Select(p => new ColumnDisplayModel()
                    {
                        Header = p.Name,
                        Name = p.Name,
                        ControlKind = p.ValueModelDescriptor.GetDefaultGridCellKind()
                    })
                    .ToList();
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

        private static bool GetHasPersistentOrder(ObjectReferenceProperty prop)
        {
            return prop.RelationEnd.Parent.GetOtherEnd(prop.RelationEnd).HasPersistentOrder;
        }

        public bool HasPersistentOrder
        {
            get
            {
                return false;
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
                        .Invoke(DataContext, "Remove", "Remove selection from list", (items) => items.ForEach(i => RemoveItem(i)), (items) => items.Count() > 0 && AllowRemove);
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
                        .Invoke(DataContext, "Delete", "Delete selection from data store", (items) => items.ForEach(i => DeleteItem(i)), (items) => items.Count() > 0 && AllowDelete);
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

            DataObjectModel result = null;

            if (children.Count == 1)
            {
                var targetType = baseclass.GetDescribedInterfaceType();
                var item = this.DataContext.Create(targetType);
                result = ModelFactory.CreateViewModel<DataObjectModel.Factory>().Invoke(DataContext, item);
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
                                var item = this.DataContext.Create(targetType);
                                result = ModelFactory.CreateViewModel<DataObjectModel.Factory>(item).Invoke(DataContext, item);
                            }
                        }),
                    null), true);
            }

            if (result != null)
            {
                AddItem(result);
                SelectedItem = result;
                ActivateItem(result, true);
            }
        }

        public void AddItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _collection.Add(item.Object, false);
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
                            SelectedItem = chosen;
                        }
                    }),
                    null), true);
        }

        public void RemoveItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _collection.Remove(item.Object);
        }

        public void DeleteItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _collection.Remove(item.Object);
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

        public void Sort(string propName, ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(propName)) throw new ArgumentNullException("propName");
            EnsureValueCache();
            _wrapper.Sort(propName, direction);
        }
    }
}
