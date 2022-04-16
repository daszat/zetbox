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
    using System.Threading.Tasks;
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
        : ValueViewModel<IReadOnlyObservableList<DataObjectViewModel>, TModelCollection>, IDeleteCommandParameter, INewCommandParameter, IOpenCommandParameter
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

        private bool _allowOpen = true;
        public bool AllowOpen
        {
            get
            {
                if (DataContext.IsElevatedMode) return true;
                return _allowOpen;
            }
            set
            {
                if (_allowOpen != value)
                {
                    _allowOpen = value;
                    OnPropertyChanged("AllowOpen");
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

        #region Opening items

        /// <summary>
        /// Is triggered before the selected items are opened. This can be used to redirect the open command to a different set of items.
        /// </summary>
        public event EventHandler<ItemsOpeningEventArgs> ItemsOpening
        {
            add
            {
                EnsureNewCommand();
                _CreateNewCommand.ItemsOpening += value;

                EnsureOpenCommand();
                _OpenCommand.ItemsOpening += value;
            }
            remove
            {
                _CreateNewCommand.ItemsOpening -= value;
                _OpenCommand.ItemsOpening -= value;
            }
        }

        /// <summary>
        /// Is triggered when items are opened in a new workspace. This can be used to give the ViewModels a context dependent finishing touch, like opening a non-default view
        /// </summary>
        public event EventHandler<ItemsOpenedEventArgs> ItemsOpened
        {
            add
            {
                EnsureNewCommand();
                _CreateNewCommand.ItemsOpened += value;

                EnsureOpenCommand();
                _OpenCommand.ItemsOpened += value;
            }
            remove
            {
                _CreateNewCommand.ItemsOpened -= value;
                _OpenCommand.ItemsOpened -= value;
            }
        }

        #endregion

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();

            if (AllowAddExisting) cmds.Add(AddExistingCommand);
            if (AllowAddNew) cmds.Add(CreateNewCommand);
            if (AllowOpen) cmds.Add(OpenCommand);
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

        private NewDataObjectCommand _CreateNewCommand = null;
        public ICommandViewModel CreateNewCommand
        {
            get
            {
                EnsureNewCommand();
                return _CreateNewCommand;
            }
        }

        private void EnsureNewCommand()
        {
            if (_CreateNewCommand == null)
            {
                _CreateNewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(
                    DataContext,
                    this,
                    ReferencedClass);

                _CreateNewCommand.ObjectCreated += OnObjectCreated;
                _CreateNewCommand.LocalModelCreated += vm => Add(vm);
            }
        }

        public void CreateNew()
        {
            if (CreateNewCommand.CanExecute(null))
                CreateNewCommand.Execute(null);
        }

        public delegate void ObjectCreatedHandler(IDataObject obj);
        public event ObjectCreatedHandler ObjectCreated;

        private void OnObjectCreated(IDataObject obj)
        {
            ObjectCreatedHandler temp = ObjectCreated;
            if (temp != null)
            {
                temp(obj);
            }
        }

        private OpenDataObjectCommand _OpenCommand;
        public ICommandViewModel OpenCommand
        {
            get
            {
                EnsureOpenCommand();
                return _OpenCommand;
            }
        }

        private void EnsureOpenCommand()
        {
            if (_OpenCommand == null)
            {
                _OpenCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(DataContext, this);
            }
        }

        public void Open()
        {
            if (OpenCommand.CanExecute(null))
                OpenCommand.Execute(null);
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
                        () => AllowAddExisting && !IsReadOnly,
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
                        Remove, // Collection will change while deleting!
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
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            if (DeleteCommand.CanExecute(null))
                DeleteCommand.Execute(null);
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

        public virtual void Add(DataObjectViewModel item)
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
                                Add(obj);
                            }
                        }
                    },
                    null);
            lstMdl.ListViewModel.AllowDelete = false;
            lstMdl.ListViewModel.AllowOpen = false;
            lstMdl.ListViewModel.AllowAddNew = AllowAddNew || AllowAddNewWhenAddingExisting;
            OnDataObjectSelectionTaskCreated(lstMdl);
            ViewModelFactory.ShowDialog(lstMdl);
        }

        public virtual void Remove()
        {
            if (SelectedItems == null || SelectedItems.Count == 0) return;

            EnsureValueCache();
            foreach (var item in SelectedItems.ToArray())
            {
                ValueModel.Value.Remove(item.Object);
            }
        }

        #endregion

        #endregion

        #region GotFromDerivedClasses

        protected void EnsureValueCache()
        {
            GetValueFromModelAsync().Wait();
        }

        protected override bool NeedsValidation
        {
            get
            {
                // Shortcut
                if (!base.NeedsValidation) return false;

                // TODO: Hack! 
                var obj = (ObjectCollectionModel as Zetbox.Client.Models.BasePropertyValueModel).IfNotNull(i => i.Object as IDataObject);

                // Non-Properties should always be validated
                if (obj == null) return true;

                // New Objects needs a validation as the value cache might not be initialized, because no one set a value.
                if (obj != null && obj.ObjectState == DataObjectState.New) return true;

                return _valueCacheInititalized;
            }
        }

        private bool _valueCacheInititalized = false;
        ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel> _valueCache;
        SortFilterWrapper<IDataObject> _wrapper;
        private System.Threading.Tasks.Task<IReadOnlyObservableList<DataObjectViewModel>> _fetchValueTask;
        protected override System.Threading.Tasks.Task<IReadOnlyObservableList<DataObjectViewModel>> GetValueFromModelAsync()
        {
            if (_fetchValueTask == null)
            {
                SetBusy();
                _fetchValueTask = Task.Run(async () =>
                {
                    await ObjectCollectionModel.GetValueAsync();
                    _wrapper = new SortFilterWrapper<IDataObject>(ObjectCollectionModel.Value, ReferencedClass.GetDescribedInterfaceType(), ObjectCollectionModel, InitialSortProperty);
                    _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel>(
                        _wrapper,
                        obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, GetWorkspace(), obj),
                        mdl => mdl.Object);
                    _valueCache.CollectionChanged += ValueListChanged;
                    _valueCacheInititalized = true;

                    OnPropertyChanged("Value");
                    OnPropertyChanged("ValueAsync");
                    OnPropertyChanged("ValueProxiesAsync");
                    ClearBusy();

                    return (IReadOnlyObservableList<DataObjectViewModel>)_valueCache;
                });
            }
            return _fetchValueTask;
        }

        //private IDelayedTask _valueLoader;
        public override IReadOnlyObservableList<DataObjectViewModel> Value
        {
            get
            {
                GetValueFromModelAsync().Wait();
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
                GetValueFromModelAsync();
                return _valueCache;
            }
            set
            {
                throw new NotSupportedException();
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
                p.Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, GetWorkspace(), obj);
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
                result = new DataObjectViewModelProxy() { Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, GetWorkspace(), obj) };
                _proxyCache[obj] = result;
            }
            return result;
        }

        //private IDelayedTask _proxyLoader;
        //private System.Threading.Tasks.Task _proxyLoader;
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
                    GetValueFromModelAsync()
                        .OnResult(t =>
                        {
                            if (AllowInlineAddNew)
                            {
                                _proxyInstances = new BaseObjectCollectionViewModelProxyList(
                                    ObjectCollectionModel,
                                    ObjectCollectionModel.Value,
                                    (vm) => GetProxy(vm),
                                    (p) => GetObjectFromProxy(p).Object,
                                    false);
                            }
                            else
                            {
                                // Supports sorting but no inline adding
                                _proxyInstances = new BaseObjectCollectionViewModelProxyList(
                                    ObjectCollectionModel,
                                    _wrapper,
                                    (vm) => GetProxy(vm),
                                    (p) => GetObjectFromProxy(p).Object,
                                    true);
                                _wrapper.CollectionChanged += (s, e) => { _proxyInstances.NotifyCollectionChanged(); };
                            }
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

        #region Filtering
        private bool? _allowFilter = null;
        public bool AllowFilter
        {
            get
            {
                return _allowFilter ?? ObjectCollectionModel.AllowFilter;
            }
            set
            {
                if (_allowFilter != value)
                {
                    _allowFilter = value;
                    OnPropertyChanged("AllowFilter");
                }
            }
        }

        private ClassValueModel<string> _filterTextMdl = null;
        private StringValueViewModel _filterText = null;

        public ViewModel FilterText
        {
            get
            {
                if (_filterText == null)
                {
                    _filterTextMdl = new ClassValueModel<string>(
                        BaseObjectCollectionViewModelResources.TextFilter,
                        BaseObjectCollectionViewModelResources.TextFilter_Tooltip,
                        true, false);
                    _filterTextMdl.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "Value")
                        {
                            OnPropertyChanged("FilterText");
                            OnPropertyChanged("IsFiltering");

                            EnsureValueCache();
                            _wrapper.Filter(_filterText.Value, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                            OnPropertyChanged("ValueProxiesAsync");
                        }
                    };
                    _filterText = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, _filterTextMdl);
                }
                return _filterText;
            }
        }

        public string FilterHeader
        {
            get
            {
                return BaseObjectCollectionViewModelResources.FilterHeader;
            }
        }

        public bool IsFiltering
        {
            get
            {
                return !string.IsNullOrEmpty(_filterTextMdl?.Value);
            }
        }
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
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return SelectedItems; } }
        #endregion

        #region DragDrop
        public virtual bool CanDrop
        {
            get
            {
                return !IsReadOnly && AllowAddExisting;
            }
        }

        public virtual Task<bool> OnDrop(object data)
        {
            if (data is IDataObject[])
            {
                var lst = (IDataObject[])data;
                foreach (var obj in lst)
                {
                    var realObj = obj.Context == DataContext
                                ? obj
                                : DataContext.Find(obj.Context.GetInterfaceType(obj), obj.ID);
                    if (ReferencedClass.GetDescribedInterfaceType().Type.IsAssignableFrom(DataContext.GetInterfaceType(realObj).Type))
                    {
                        Add(DataObjectViewModel.Fetch(ViewModelFactory, DataContext, GetWorkspace(), realObj));
                    }
                }
            }
            return Task.FromResult(false);
        }

        public virtual object DoDragDrop()
        {
            var lst = SelectedItems
                .Where(i => i.ObjectState.In(DataObjectState.Unmodified, DataObjectState.Modified, DataObjectState.New))
                .Select(i => i.Object)
                .Cast<IDataObject>()
                .ToArray();
            return lst.Length > 0 ? lst : null;
        }
        #endregion
    }

    /// <summary>
    /// Hack for those who do not check element types by traversing from inherited interfaces
    /// e.g. DataGrid from WPF
    /// Can't be a inner class, because it's deriving the generic parameter from BaseObjectCollectionViewModel. This confuses the WPF DataGrid
    /// </summary>
    public sealed class BaseObjectCollectionViewModelProxyList : AbstractObservableProjectedList<IDataObject, DataObjectViewModelProxy>, IList, IList<DataObjectViewModelProxy>
    {
        public BaseObjectCollectionViewModelProxyList(INotifyCollectionChanged notifier, object collection, Func<IDataObject, DataObjectViewModelProxy> select, Func<DataObjectViewModelProxy, IDataObject> inverter, bool isReadOnly)
            : base(notifier, collection, select, inverter, isReadOnly)
        {
        }

        public void NotifyCollectionChanged()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
