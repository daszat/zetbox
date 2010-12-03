
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
    public abstract class BaseObjectCollectionViewModel<TCollection, TModelCollection>
        : ValueViewModel<TCollection, TModelCollection>
        where TModelCollection : ICollection<IDataObject>
    {
        public new delegate BaseObjectCollectionViewModel<TCollection, TModelCollection> Factory(IKistlContext dataCtx, IValueModel mdl);

        public BaseObjectCollectionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IObjectCollectionValueModel<TModelCollection> mdl)
            : base(appCtx, dataCtx,     mdl)
        {
            ObjectCollectionModel = mdl;

            if (ObjectCollectionModel.RelEnd != null)
            {
                var rel = ObjectCollectionModel.RelEnd.Parent;
                if (rel != null)
                {
                    var otherEnd = rel.GetOtherEnd(ObjectCollectionModel.RelEnd);
                    if (otherEnd != null && otherEnd.Multiplicity.UpperBound() > 1 && rel.Containment != ContainmentSpecification.Independent)
                    {
                        AllowAddExisting = false;
                        AllowAddExisting = false;
                    }
                }
            }
        }

        public IObjectCollectionValueModel<TModelCollection> ObjectCollectionModel { get; private set; }
        public ObjectClass ReferencedClass { get { return ObjectCollectionModel.ReferencedClass; } }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectViewModel>> Members
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

        private DataObjectViewModel _selectedItem;
        public DataObjectViewModel SelectedItem
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

        private ObservableCollection<DataObjectViewModel> _selectedItems = null;
        public ObservableCollection<DataObjectViewModel> SelectedItems
        {
            get
            {
                if (_selectedItems == null)
                {
                    _selectedItems = new ObservableCollection<DataObjectViewModel>();
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

        private ObservableCollection<ICommandViewModel> _Commands;
        public ObservableCollection<ICommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    _Commands = CreateCommands();
                }
                return _Commands;
            }
        }

        protected virtual ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = new ObservableCollection<ICommandViewModel>();

            if(AllowAddExisting) cmds.Add(AddExistingCommand);
            if(AllowAddNew) cmds.Add(CreateNewCommand);
            if(AllowRemove) cmds.Add(RemoveCommand);
            if(AllowDelete) cmds.Add(DeleteCommand);

            return cmds;
        }

        private ICommandViewModel _CreateNewCommand = null;
        public ICommandViewModel CreateNewCommand
        {
            get
            {
                if (_CreateNewCommand == null)
                {
                    _CreateNewCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Add new", "Creates a new Item suitable for adding to the list.", () => CreateNewItem(), () => AllowAddNew && !DataContext.IsReadonly && !IsReadOnly);
                }
                return _CreateNewCommand;
            }
        }

        private ICommandViewModel _AddExistingCommand = null;
        public ICommandViewModel AddExistingCommand
        {
            get
            {
                if (_AddExistingCommand == null)
                {
                    _AddExistingCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Add existing", "Adds an existing item into this list.", () => AddExistingItem(), () => AllowAddExisting && !DataContext.IsReadonly && !IsReadOnly);
                }
                return _AddExistingCommand;
            }
        }

        private ICommandViewModel _RemoveCommand = null;
        public ICommandViewModel RemoveCommand
        {
            get
            {
                if (_RemoveCommand == null)
                {
                    _RemoveCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Remove", "Remove selection from list",
                        () => SelectedItems.ToList().ForEach(i => RemoveItem(i)), // Collection will change while deleting!
                        () => SelectedItems != null && SelectedItems.Count() > 0 && AllowRemove && !IsReadOnly);
                }
                return _RemoveCommand;
            }
        }

        private ICommandViewModel _DeleteCommand = null;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Delete", "Delete selection from data store", 
                        () => SelectedItems.ToList().ForEach(i => DeleteItem(i)), // Collection will change while deleting!
                        () => SelectedItems != null && SelectedItems.Count() > 0 && AllowDelete && !DataContext.IsReadonly && !IsReadOnly);
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
                ViewModelFactory.ShowModel(
                    ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        children.AsQueryable(),
                        new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
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
            var result = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, item);
            AddItem(result);
            if (!targetClass.IsSimpleObject)
            {
                ActivateItem(result, true);
            }
        }

        public virtual void AddItem(DataObjectViewModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            ValueModel.Value.Add(item.Object);

            SelectedItem = item;
        }

        /// <summary>
        /// Adds an existing item into this ObjectList. Asks the User which should be added.
        /// </summary>
        public void AddExistingItem()
        {
            var ifType = ReferencedClass.GetDescribedInterfaceType();

            ViewModelFactory.ShowModel(
                ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                    DataContext,
                    ifType.GetObjectClass(FrozenContext),
                    null,
                    new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                    {
                        if (chosen != null)
                        {
                            AddItem(chosen);
                        }
                    }),
                    null), true);
        }

        public virtual void RemoveItem(DataObjectViewModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            ValueModel.Value.Remove(item.Object);
        }

        public virtual void DeleteItem(DataObjectViewModel item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            EnsureValueCache();
            ValueModel.Value.Remove(item.Object);
            item.Delete();
        }

        public void ActivateItem(DataObjectViewModel item, bool activate)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            ViewModelFactory.ShowModel(item, activate);
        }

        #endregion

        #endregion

        #region Utilities and UI callbacks

        protected abstract void EnsureValueCache();

        private void CollectChildClasses(int id, List<ObjectClass> children)
        {
            var nextChildren = FrozenContext.GetQuery<ObjectClass>()
                .Where(oc => oc.BaseObjectClass != null && oc.BaseObjectClass.ID == id)
                .ToList();

            if (nextChildren.Count() > 0)
            {
                foreach (ObjectClass oc in nextChildren)
                {
                    if (!oc.IsAbstract) children.Add(oc);
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
            EnsureValueCache();
            ValueModel.Value.Clear();
        }

        #endregion

        protected override void ParseValue(string str)
        {
            throw new NotSupportedException();
        }
    }
}
