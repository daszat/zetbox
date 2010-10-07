
namespace Kistl.Client.Presentables.ValueViewModels
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
    using Kistl.Client.Models;

    /// <summary>
    /// </summary>
    public abstract class BaseObjectCollectionViewModel<TValue>
        : ValueViewModel<TValue>
    {
        public new delegate BaseObjectCollectionViewModel<TValue> Factory(IKistlContext dataCtx, IValueModel mdl);

        public BaseObjectCollectionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            BaseObjectCollectionModel = (IObjectListValueModel)mdl;

            if (BaseObjectCollectionModel.RelEnd != null)
            {
                var rel = BaseObjectCollectionModel.RelEnd.Parent;
                if (rel != null)
                {
                    var otherEnd = rel.GetOtherEnd(BaseObjectCollectionModel.RelEnd);
                    if (otherEnd != null && otherEnd.Multiplicity.UpperBound() > 1 && rel.Containment != ContainmentSpecification.Independent)
                    {
                        AllowAddExisting = false;
                        AllowAddExisting = false;
                    }
                }
            }
        }

        public IObjectListValueModel BaseObjectCollectionModel { get; private set; }
        public ObjectClass ReferencedClass { get { return BaseObjectCollectionModel.ReferencedClass; } }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectModel>> Members
        private GridDisplayConfiguration _displayedColumns = null;
        public GridDisplayConfiguration DisplayedColumns
        {
            get
            {
                if (_displayedColumns == null)
                {
                    _displayedColumns = CreateDisplayedColumns();
                }
                return _displayedColumns;
            }
        }
        protected virtual GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = new GridDisplayConfiguration();
            result.BuildColumns(ReferencedClass, false);
            return result;
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

        #region Allow*
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
        #endregion

        #region Commands
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
            ObjectClass baseclass = ReferencedClass;

            var children = new List<ObjectClass>() { baseclass };
            CollectChildClasses(baseclass.ID, children);

            if (children.Count == 1)
            {
                CreateItemAndActivate(baseclass);
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
                                var targetClass = ((ObjectClass)chosen.Object);
                                CreateItemAndActivate(targetClass);
                            }
                        }),
                    null), true);
            }
        }

        private void CreateItemAndActivate(ObjectClass targetClass)
        {
            var targetType = targetClass.GetDescribedInterfaceType();
            var item = this.DataContext.Create(targetType);
            var result = ModelFactory.CreateViewModel<DataObjectModel.Factory>().Invoke(DataContext, item);
            AddItem(result);
            if (!targetClass.IsSimpleObject)
            {
                ActivateItem(result, true);
            }
        }


        public abstract void AddItem(DataObjectModel item);

        /// <summary>
        /// Adds an existing item into this ObjectList. Asks the User which should be added.
        /// </summary>
        public void AddExistingItem()
        {
            var ifType = ReferencedClass.GetDescribedInterfaceType();

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

        public abstract void RemoveItem(DataObjectModel item);

        public abstract void DeleteItem(DataObjectModel item);

        public void ActivateItem(DataObjectModel item, bool activate)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            ModelFactory.ShowModel(item, activate);
        }
        #endregion

        #endregion

        #region Utilities and UI callbacks

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

        protected void ValueListChanged(object sender, NotifyCollectionChangedEventArgs e)
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

        #region IValueViewModel Members

        public override void ClearValue()
        {
            throw new NotImplementedException();
        }

        public override ICommand ClearValueCommand
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected override void ParseValue(string str)
        {
            throw new NotSupportedException();
        }
    }
}
