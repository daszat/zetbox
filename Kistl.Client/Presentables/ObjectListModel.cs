
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
    public class ObjectListModel
        : PropertyModel<IList<DataObjectModel>>, IValueListModel<DataObjectModel>
    {
        public new delegate ObjectListModel Factory(IKistlContext dataCtx, IDataObject obj, Property prop);

        private readonly ObjectReferenceProperty _property;
        private readonly ObjectClass _referencedClass;
        private readonly ICollection _collection;
        private readonly IList _list;
        private readonly INotifyCollectionChanged _notifier;

        public ObjectListModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            if (!prop.GetIsList()) { throw new ArgumentOutOfRangeException("prop", "ObjectReferenceProperty must be a list"); }

            _property = prop;
            _referencedClass = _property.GetReferencedObjectClass();

            _collection = Object.GetPropertyValue<ICollection>(Property.Name);
            _notifier = _collection as INotifyCollectionChanged;
            // extract special references if available.
            _list = _collection as IList;

            AttachPropertyChangeHandlers();

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
                    }
                }
            }
        }

        private void AttachPropertyChangeHandlers()
        {
            if (_notifier != null)
            {
                _notifier.CollectionChanged += ValueListChanged;
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
                    Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name),
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

        public bool HasPersistentOrder
        {
            get
            {
                return _property.RelationEnd.Parent.GetOtherEnd(_property.RelationEnd).HasPersistentOrder;
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
            ObjectClass baseclass = ((ObjectReferenceProperty)this.Property).GetReferencedObjectClass();

            var children = new List<ObjectClass>() { baseclass };
            CollectChildClasses(baseclass.ID, children);

            if (children.Count == 1)
            {
                var targetType = baseclass.GetDescribedInterfaceType();
                var item = this.DataContext.Create(targetType);
                onCreated(ModelFactory.CreateViewModel<DataObjectModel.Factory>().Invoke(DataContext, item));
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
                                onCreated(ModelFactory.CreateViewModel<DataObjectModel.Factory>(item).Invoke(DataContext, item));
                            }
                            else
                            {
                                onCreated(null);
                            }
                        }),
                    null), true);
            }
        }

        public void AddItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _valueCache.Collection.Add(item.Object);
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
            _valueCache.Collection.Remove(item.Object);
        }

        public void DeleteItem(DataObjectModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            _valueCache.Collection.Remove(item.Object);
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
