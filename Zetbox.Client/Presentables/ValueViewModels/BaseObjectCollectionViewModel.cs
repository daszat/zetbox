// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ZetboxBase;

    /// <summary>
    /// </summary>
    public abstract class BaseObjectCollectionViewModel<TModelCollection>
        : ValueViewModel<IReadOnlyObservableList<DataObjectViewModel>, TModelCollection>, IDeleteCommandParameter
        where TModelCollection : ICollection<IDataObject>
    {
        public new delegate BaseObjectCollectionViewModel<TModelCollection> Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public BaseObjectCollectionViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IObjectCollectionValueModel<TModelCollection> mdl)
            : base(appCtx, dataCtx, parent, mdl)
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
                        AllowRemove = false;
                    }
                    else if (rel.GetRelationType() == RelationType.n_m)
                    {
                        AllowAddNew = false;
                        AllowAddNewWhenAddingExisting = true;
                    }
                }
            }
            dataCtx.IsElevatedModeChanged += new EventHandler(dataCtx_IsElevatedModeChanged);
        }

        void dataCtx_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("AllowAddNew");
            OnPropertyChanged("AllowDelete");
            CreateCommands();
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
            GridDisplayConfiguration.Mode mode = IsInlineEditable ?
                  GridDisplayConfiguration.Mode.Editable
                : GridDisplayConfiguration.Mode.ReadOnly;

            result.BuildColumns(ReferencedClass, mode, true);
            return result;
        }

        private ObservableCollection<DataObjectViewModel> _selectedItems = null;
        public ObservableCollection<DataObjectViewModel> SelectedItems
        {
            get
            {
                if (_selectedItems == null)
                {
                    _selectedItems = new ObservableCollection<DataObjectViewModel>();
                    _selectedItems.CollectionChanged += _selectedItems_CollectionChanged;
                }
                return _selectedItems;
            }
        }

        void _selectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("SelectedItem");
        }

        /// <summary>
        /// The current first selected item
        /// </summary>
        /// <remarks>
        /// If set explicit, all other selected items will be cleard from the SelectesItems List
        /// </remarks>
        public DataObjectViewModel SelectedItem
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    return _selectedItems[0];
                }
                return null;
            }
            set
            {
                this.SelectedItems.Clear();
                if (value != null) this.SelectedItems.Add(value);
                OnPropertyChanged("SelectedItem");
            }
        }

        #region Allow*

        public bool IsInlineEditable
        {
            get
            {
                return ObjectCollectionModel.IsInlineEditable ?? ReferencedClass.IsSimpleObject;
            }
        }

        private bool _allowAddNew = true;
        public bool AllowAddNew
        {
            get
            {
                if (DataContext.IsElevatedMode) return true;
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

        private bool _allowAddNewWhenAddingExisting = true;
        public bool AllowAddNewWhenAddingExisting
        {
            get
            {
                return _allowAddNewWhenAddingExisting;
            }
            set
            {
                if (_allowAddNewWhenAddingExisting != value)
                {
                    _allowAddNew = value;
                    OnPropertyChanged("AllowAddNewWhenAddingExisting");
                }
            }
        }



        private bool? _allowInlineAddNew = null;
        public bool AllowInlineAddNew
        {
            get
            {
                return AllowAddNew && IsInlineEditable && !HasChildClasses && _allowInlineAddNew != false;
            }
            set
            {
                if (_allowInlineAddNew != value)
                {
                    _allowInlineAddNew = value;
                    OnPropertyChanged("AllowInlineAddNew");
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
                if (DataContext.IsElevatedMode) return true;
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

        // Case 2282: Sorting Lists will delete 1:n Entries
        // This is evil, as it would delete an Object during 
        // resort of an ObjectRefList.
        // so inline deleting is forbidden.
        private bool _allowInlineDelete = false;
        public bool AllowInlineDelete
        {
            get
            {
                return _allowInlineDelete;
            }
            set
            {
                if (_allowInlineDelete != value)
                {
                    if (value == true)
                    {
                        throw new NotSupportedException("Case 2282: Inline deleting is forbidden as it has some issues with resorting.");
                    }
                    _allowInlineDelete = value;
                    OnPropertyChanged("AllowInlineDelete");
                }
            }
        }
        #endregion

        #region Commands

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();

            if (AllowAddExisting) cmds.Add(AddExistingCommand);
            if (AllowAddNew) cmds.Add(CreateNewCommand);
            if (AllowRemove) cmds.Add(RemoveCommand);
            if (AllowDelete) cmds.Add(DeleteCommand);

            if (ObjectCollectionModel.RelEnd != null && ObjectCollectionModel is BasePropertyValueModel)
            {
                var obj = (IDataObject)((BasePropertyValueModel)ObjectCollectionModel).Object;
                var navigator = ObjectCollectionModel.RelEnd.Navigator;
                ObjectReferenceHelper.AddActionViewModels(cmds, obj, navigator.Methods, this, ViewModelFactory);
            }

            return cmds;
        }

        private ICommandViewModel _CreateNewCommand = null;
        public ICommandViewModel CreateNewCommand
        {
            get
            {
                if (_CreateNewCommand == null)
                {
                    _CreateNewCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        BaseObjectCollectionViewModelResources.CreateNewCommand_Name,
                        BaseObjectCollectionViewModelResources.CreateNewCommand_Tooltip,
                        () => CreateNewItem(),
                        () => AllowAddNew && !DataContext.IsReadonly && !IsReadOnly,
                        null);
                    _CreateNewCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext));
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
                    _AddExistingCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        BaseObjectCollectionViewModelResources.AddExistingCommand_Name,
                        BaseObjectCollectionViewModelResources.AddExistingCommand_Tooltip,
                        () => AddExistingItem(),
                        () => AllowAddExisting && !DataContext.IsReadonly && !IsReadOnly,
                        null);
                    _AddExistingCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.search_png.Find(FrozenContext));
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
                    _RemoveCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        BaseObjectCollectionViewModelResources.RemoveCommand_Name,
                        BaseObjectCollectionViewModelResources.RemoveCommand_Tooltip,
                        () => SelectedItems.ToList().ForEach(i => RemoveItem(i)), // Collection will change while deleting!
                        () => SelectedItems != null && SelectedItems.Count() > 0 && AllowRemove && !IsReadOnly,
                        null);
                }
                return _RemoveCommand;
            }
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this, this, null, false);
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
            NewDataObjectCommand.ChooseObjectClass(ViewModelFactory, DataContext, FrozenContext, this, ReferencedClass, CreateItemAndActivate); 
        }

        public event DataObjectSelectionTaskCreatedEventHandler DataObjectSelectionTaskCreated;
        protected virtual void OnDataObjectSelectionTaskCreated(DataObjectSelectionTaskViewModel vmdl)
        {
            var temp = DataObjectSelectionTaskCreated;
            if (temp != null)
            {
                temp(this, new DataObjectSelectionTaskEventArgs(vmdl));
            }
        }

        private void CreateItemAndActivate(ObjectClass targetClass)
        {
            var targetType = targetClass.GetDescribedInterfaceType();
            var item = this.DataContext.Create(targetType);
            var result = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), item);

            AddItem(result);
            ActivateItem(result);
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
            var lstMdl = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                    DataContext,
                    this,
                    ReferencedClass,
                    null,
                    (chosen) =>
                    {
                        if (chosen != null)
                        {
                            foreach (var obj in chosen)
                            {
                                AddItem(obj);
                            }
                        }
                    },
                    null);
            lstMdl.ListViewModel.AllowDelete = false;
            lstMdl.ListViewModel.ShowOpenCommand = false;
            lstMdl.ListViewModel.AllowAddNew = AllowAddNew || AllowAddNewWhenAddingExisting;
            OnDataObjectSelectionTaskCreated(lstMdl);
            ViewModelFactory.ShowDialog(lstMdl);
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

        public void ActivateItem(DataObjectViewModel item)
        {
            NewDataObjectCommand.ActivateItem(ViewModelFactory, DataContext, this, item, this.ReferencedClass, IsInlineEditable);
        }

        #endregion

        #endregion

        #region GotFromDerivedClasses

        protected void EnsureValueCache()
        {
            GetValueFromModel().Wait();
        }

        ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel> _valueCache;
        SortedWrapper _wrapper;
        private ZbTask<IReadOnlyObservableList<DataObjectViewModel>> _fetchValueTask;
        protected override ZbTask<IReadOnlyObservableList<DataObjectViewModel>> GetValueFromModel()
        {
            if (_fetchValueTask == null)
            {
                SetBusy();
                _fetchValueTask = new ZbTask<IReadOnlyObservableList<DataObjectViewModel>>(ObjectCollectionModel.GetValueAsync())
                    .OnResult(t =>
                    {
                        _wrapper = new SortedWrapper(ObjectCollectionModel.Value, ReferencedClass.GetDescribedInterfaceType(), ObjectCollectionModel, InitialSortProperty);
                        _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel>(
                            _wrapper,
                            obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj),
                            mdl => mdl.Object);
                        _valueCache.CollectionChanged += ValueListChanged;
                        t.Result = _valueCache;
                    });
                // TODO: Split here to avoid a stackoverflow exception!
                // -> OnPropertyChanged("ValueProxiesAsync") triggers ValueProxiesAsync.get
                // -> ValueProxiesAsync.get is calling GetValueFromModel()
                // -> _fetchValueTask has not been assigned yet!
                _fetchValueTask.OnResult(t =>
                {
                    OnPropertyChanged("Value");
                    OnPropertyChanged("ValueAsync");
                    OnPropertyChanged("ValueProxiesAsync");
                    ClearBusy();
                });
            };
            return _fetchValueTask;
        }

        //private IDelayedTask _valueLoader;
        public override IReadOnlyObservableList<DataObjectViewModel> Value
        {
            get
            {
                GetValueFromModel().Wait();
                return _valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override IReadOnlyObservableList<DataObjectViewModel> ValueAsync
        {
            get
            {
                GetValueFromModel();
                return _valueCache;
            }
        }

        protected override void SetValueToModel(IReadOnlyObservableList<DataObjectViewModel> value)
        {
            throw new NotSupportedException();
        }

        protected abstract string InitialSortProperty { get; }

        #endregion

        #region Utilities and UI callbacks

        private bool? _hasChildClasses;
        public bool HasChildClasses
        {
            get
            {
                if (_hasChildClasses == null)
                {
                    _hasChildClasses = FrozenContext.GetQuery<ObjectClass>()
                        .Any(oc => oc.BaseObjectClass == ReferencedClass);
                }
                return _hasChildClasses.Value;
            }
        }
        #endregion

        #region Event handlers

        protected void ValueListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var obj in e.NewItems.OfType<INotifyPropertyChanged>())
                {
                    obj.PropertyChanged += AnyPropertyChangedHandler;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var obj in e.OldItems.OfType<INotifyPropertyChanged>())
                {
                    obj.PropertyChanged -= AnyPropertyChangedHandler;
                }
            }

            NotifyValueChanged();
        }

        private void AnyPropertyChangedHandler(object sender, EventArgs e)
        {
            OnPropertyChanged("ValueAsync");
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

        #region Proxy
        private DataObjectViewModel GetObjectFromProxy(DataObjectViewModelProxy p)
        {
            if (p.Object == null)
            {
                var obj = DataContext.Create(DataContext.GetInterfaceType(this.ReferencedClass.GetDataType()));
                p.Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
                _proxyCache[p.Object.Object] = p;
            }
            return p.Object;
        }

        Dictionary<IDataObject, DataObjectViewModelProxy> _proxyCache = new Dictionary<IDataObject, DataObjectViewModelProxy>();
        private DataObjectViewModelProxy GetProxy(IDataObject obj)
        {
            DataObjectViewModelProxy result;
            if (!_proxyCache.TryGetValue(obj, out result))
            {
                result = new DataObjectViewModelProxy() { Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj) };
                _proxyCache[obj] = result;
            }
            return result;
        }

        //private IDelayedTask _proxyLoader;
        //private ZbTask _proxyLoader;
        private BaseObjectCollectionViewModelProxyList _proxyInstances = null;
        private bool _proxyInstancesLoading = false;
        /// <summary>
        /// Allow instances to be added external
        /// </summary>
        public BaseObjectCollectionViewModelProxyList ValueProxiesAsync
        {
            get
            {
                if (!_proxyInstancesLoading)
                {
                    _proxyInstancesLoading = true;
                    GetValueFromModel()
                        .OnResult(t =>
                        {
                            _proxyInstances = new BaseObjectCollectionViewModelProxyList(
                                ObjectCollectionModel,
                                ObjectCollectionModel.Value,
                                (vm) => GetProxy(vm),
                                (p) => GetObjectFromProxy(p).Object);
                            OnPropertyChanged("ValueProxiesAsync");
                        });
                }
                return _proxyInstances;
            }
        }

        private ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> _selectedProxies = null;
        public ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> SelectedProxies
        {
            get
            {
                if (_selectedProxies == null)
                {
                    _selectedProxies = new ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy>(
                        SelectedItems,
                        (vm) => GetProxy(vm.Object),
                        (p) => GetObjectFromProxy(p));
                }
                return _selectedProxies;
            }
        }
        #endregion

        #region Sorting
        private string _sortProperty = null;
        private System.ComponentModel.ListSortDirection _sortDirection = System.ComponentModel.ListSortDirection.Ascending;

        public void Sort(string propName, System.ComponentModel.ListSortDirection direction)
        {
            _sortProperty = propName;
            _sortDirection = direction;
            OnPropertyChanged("SortProperty");
            OnPropertyChanged("SortDirection");

            EnsureValueCache();
            _wrapper.Sort(propName, direction, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("ValueProxiesAsync");
        }

        public string SortProperty { get { return _sortProperty; } }
        public System.ComponentModel.ListSortDirection SortDirection { get { return _sortDirection; } }
        #endregion

        #region IFormattedValueViewModel Members
        protected override string FormatValue(IReadOnlyObservableList<DataObjectViewModel> value)
        {
            if (value.Count == 0) return BaseObjectCollectionViewModelResources.NoItems;
            if (value.Count == 1) return string.Format(BaseObjectCollectionViewModelResources.OneItem, value[0].Name);
            return string.Format(BaseObjectCollectionViewModelResources.MoreItems, value.Count, value[0].Name);
        }
        protected override ParseResult<IReadOnlyObservableList<DataObjectViewModel>> ParseValue(string str)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region IDeleteCommandParameter Members
        IEnumerable<ViewModel> IDeleteCommandParameter.SelectedItems { get { return SelectedItems; } }
        #endregion
    }

    /// <summary>
    /// Hack for those who do not check element types by traversing from inherited interfaces
    /// e.g. DataGrid from WPF
    /// Can't be a inner class, because it's deriving the generic parameter from BaseObjectCollectionViewModel. This confuses the WPF DataGrid
    /// </summary>
    public sealed class BaseObjectCollectionViewModelProxyList : AbstractObservableProjectedList<IDataObject, DataObjectViewModelProxy>, IList, IList<DataObjectViewModelProxy>
    {
        public BaseObjectCollectionViewModelProxyList(INotifyCollectionChanged notifier, object collection, Func<IDataObject, DataObjectViewModelProxy> select, Func<DataObjectViewModelProxy, IDataObject> inverter)
            : base(notifier, collection, select, inverter, false)
        {
        }
    }
}
