
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.FilterViewModels;
    using Kistl.Client.Presentables.ValueViewModels;
    using ObjectEditor = Kistl.Client.Presentables.ObjectEditor;
    using System.Collections;

    public enum InstanceListViewMethod
    {
        List = 1,
        Details = 2,
    }

    /// <summary>
    /// Models the specialities of <see cref="DataType"/>s.
    /// </summary>
    [ViewModelDescriptor]
    public class InstanceListViewModel
        : ViewModel, IViewModelWithIcon, IRefreshCommandListener
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate InstanceListViewModel Factory(IKistlContext dataCtx, ObjectClass type, IQueryable qry);
#else
        public new delegate InstanceListViewModel Factory(IKistlContext dataCtx, ObjectClass type, IQueryable qry);
#endif

        protected readonly Func<IKistlContext> ctxFactory;

        /// <summary>
        /// Initializes a new instance of the DataTypeViewModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="config"></param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="type">optional: the data type to model. If null, qry must be a Query of a valid DataType</param>
        /// <param name="qry">optional: the query to display. If null, Query will be constructed from type</param>
        /// <param name="ctxFactory"></param>
        public InstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig config,
            IKistlContext dataCtx,
            ObjectClass type,
            IQueryable qry,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
        {
            if (dataCtx == null) throw new ArgumentNullException("dataCtx");
            if (qry == null && type == null) throw new ArgumentException("qry and type may not be null");

            if (type != null)
            {
                _type = type;
            }
            else
            {
                this._type = FrozenContext.GetQuery<ObjectClass>().SingleOrDefault(dt => dt.GetDataType() == qry.ElementType);
                if (_type == null) throw new ArgumentException("Cannot resolve type from Query");
            }
            if (qry == null)
            {
                MethodInfo mi = this.GetType().FindGenericMethod("GetTypedQuery", new Type[] { DataContext.GetInterfaceType(_type.GetDataType()).Type }, null);
                // See Case 552
                _query = (IQueryable)mi.Invoke(this, new object[] { });
            }
            else
            {
                _query = qry;
            }

            this.ctxFactory = ctxFactory;
        }

        #region Kind Management
        private ControlKind _requestedEditorKind;
        /// <summary>
        /// Requested editor kind for opening object
        /// </summary>
        public ControlKind RequestedEditorKind
        {
            get
            {
                return _requestedEditorKind;
            }
            set
            {
                if (_requestedEditorKind != value)
                {
                    _requestedEditorKind = value;
                    OnPropertyChanged("RequestedEditorKind");
                }
            }
        }

        private ControlKind _requestedWorkspaceKind;
        /// <summary>
        /// Requested workspace kind for opening object
        /// </summary>
        public ControlKind RequestedWorkspaceKind
        {
            get
            {
                return _requestedWorkspaceKind;
            }
            set
            {
                if (_requestedWorkspaceKind != value)
                {
                    _requestedWorkspaceKind = value;
                    OnPropertyChanged("RequestedWorkspaceKind");
                }
            }
        }
        #endregion

        #region Filter Collection
        private bool _enableAutoFilter = true;

        /// <summary>
        /// Enables the auto filter feature. This is the default. Setting this property will cause the filter collection to be cleared.
        /// </summary>
        public bool EnableAutoFilter
        {
            get
            {
                return _enableAutoFilter;
            }
            set
            {
                if (_enableAutoFilter != value)
                {
                    _enableAutoFilter = value;
                    _filter = null;
                    OnPropertyChanged("EnableAutoFilter");
                    OnPropertyChanged("Filter");
                    OnPropertyChanged("ShowFilter");
                }
            }
        }

        private bool? _showFilter = null;
        public bool ShowFilter
        {
            get
            {
                return _showFilter ?? Filter.Count > 0;
            }
            set
            {
                if (_showFilter != value)
                {
                    _showFilter = value;
                    OnPropertyChanged("ShowFilter");
                }
            }
        }

        private ObservableCollection<IFilterModel> _filter = null;
        public ICollection<IFilterModel> Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = new ObservableCollection<IFilterModel>();
                    // React on changes -> attach to FilterChanged Event
                    _filter.CollectionChanged += new NotifyCollectionChangedEventHandler(_filter_CollectionChanged);

                    if (EnableAutoFilter)
                    {
                        // Resolve default property filter
                        var t = _type;
                        while (t != null)
                        {
                            // Add ObjectClass filter expressions
                            foreach (var cfc in t.FilterConfigurations)
                            {
                                _filter.Add(cfc.CreateFilterModel());
                            }

                            // Add Property filter expressions
                            foreach (var prop in t.Properties.Where(p => p.FilterConfiguration != null))
                            {
                                _filter.Add(prop.FilterConfiguration.CreateFilterModel());
                            }
                            if (t is ObjectClass)
                            {
                                t = ((ObjectClass)t).BaseObjectClass;
                            }
                        }

                        // Add default ToString Filter for all
                        _filter.Add(new ToStringFilterModel(FrozenContext));
                    }
                }
                return _filter;
            }
        }

        void _filter_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems.OfType<IUIFilterModel>())
            {
                // attach change events
                item.FilterChanged += new EventHandler(delegate(object s, EventArgs a)
                {
                    var f = s as FilterModel;
                    if (f == null) return;

                    if (f.IsServerSideFilter)
                    {
                        ReloadInstances();
                    }
                    else
                    {
                        ExecutePostFilter();
                    }
                });
            }

            _FilterViewModels = null;
            OnPropertyChanged("FilterViewModels");
            OnPropertyChanged("ShowFilter");
        }

        private List<FilterViewModel> _FilterViewModels = null;
        public IEnumerable<FilterViewModel> FilterViewModels
        {
            get
            {
                if (_FilterViewModels == null)
                {
                    _FilterViewModels = new List<FilterViewModel>(Filter
                        .OfType<IUIFilterModel>()
                        .Select(f => ViewModelFactory.CreateViewModel<FilterViewModel.Factory>(f.ViewModelType).Invoke(DataContext, f)));
                }
                return _FilterViewModels;
            }
        }
        #endregion

        #region Commands

        private bool _ShowOpenCommand = true;
        public bool ShowOpenCommand
        {
            get
            {
                return _ShowOpenCommand;
            }
            set
            {
                if (_ShowOpenCommand != value)
                {
                    _ShowOpenCommand = value;
                    UpdateCommands();
                }
            }
        }
        private bool _ShowNewCommand = true;
        public bool ShowNewCommand
        {
            get
            {
                return _ShowNewCommand;
            }
            set
            {
                if (_ShowNewCommand != value)
                {
                    _ShowNewCommand = value;
                    UpdateCommands();
                }
            }
        }
        private bool _ShowDeleteCommand = true;
        public bool ShowDeleteCommand
        {
            get
            {
                return _ShowDeleteCommand;
            }
            set
            {
                if (_ShowDeleteCommand != value)
                {
                    _ShowDeleteCommand = value;
                    UpdateCommands();
                }
            }
        }
        private bool _ShowRefreshCommand = true;
        public bool ShowRefreshCommand
        {
            get
            {
                return _ShowRefreshCommand;
            }
            set
            {
                if (_ShowRefreshCommand != value)
                {
                    _ShowRefreshCommand = value;
                    UpdateCommands();
                }
            }
        }

        private void UpdateCommands()
        {
            if (_commands == null) return;
            if (!ShowNewCommand && _commands.Contains(NewCommand)) _commands.Remove(NewCommand);
            if (!ShowOpenCommand && _commands.Contains(OpenCommand)) _commands.Remove(OpenCommand);
            if (!ShowRefreshCommand && _commands.Contains(RefreshCommand)) _commands.Remove(RefreshCommand);
            if (!ShowDeleteCommand && _commands.Contains(DeleteCommand)) _commands.Remove(DeleteCommand);

            if (ShowNewCommand && !_commands.Contains(NewCommand)) _commands.Insert(0, NewCommand);
            if (ShowOpenCommand && !_commands.Contains(OpenCommand)) _commands.Insert(ShowNewCommand ? 1 : 0, OpenCommand);
            if (ShowRefreshCommand && !_commands.Contains(RefreshCommand)) _commands.Insert((ShowNewCommand ? 1 : 0) + (ShowOpenCommand ? 1 : 0), RefreshCommand);
            if (ShowDeleteCommand && !_commands.Contains(DeleteCommand)) _commands.Insert((ShowNewCommand ? 1 : 0) + (ShowOpenCommand ? 1 : 0) + (ShowRefreshCommand ? 1 : 0), DeleteCommand);
        }

        private bool? _showCommands = null;

        /// <summary>
        /// If true, commands will be shown in UI.
        /// </summary>
        /// <remarks>
        /// Can be set explicit. If not set, the value is true if commands are present
        /// </remarks>
        public bool ShowCommands
        {
            get
            {
                return _showCommands ?? Commands.Count > 0;
            }
            set
            {
                if (_showCommands != value)
                {
                    _showCommands = value;
                    OnPropertyChanged("ShowCommands");
                }
            }
        }

        private ObservableCollection<ICommandViewModel> _commands = null;
        public ObservableCollection<ICommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    _commands = new ObservableCollection<ICommandViewModel>();
                    UpdateCommands();
                }
                return _commands;
            }
        }

        private RefreshCommand _RefreshCommand;
        public RefreshCommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this);
                }
                return _RefreshCommand;
            }
        }

        private OpenDataObjectCommand _OpenCommand;
        public OpenDataObjectCommand OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(DataContext, RequestedWorkspaceKind, RequestedEditorKind);
                }
                return _OpenCommand;
            }
        }

        private NewDataObjectCommand _NewCommand;
        public NewDataObjectCommand NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(DataContext, _type, RequestedWorkspaceKind, RequestedEditorKind, null /* I do it my way */);
                    _NewCommand.ObjectCreated += new NewDataObjectCommand.ObjectCreatedHandler(_NewCommand_ObjectCreated);
                }
                return _NewCommand;
            }
        }

        void _NewCommand_ObjectCreated(IDataObject obj)
        {
            OnObjectCreated(obj);
            this.ReloadInstances();
            this.SelectedItem = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
        }

        public delegate void ObjectCreatedHandler(IDataObject obj);
        public event ObjectCreatedHandler ObjectCreated;

        protected void OnObjectCreated(IDataObject obj)
        {
            ObjectCreatedHandler temp = ObjectCreated;
            if (temp != null)
            {
                temp(obj);
            }
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public DeleteDataObjectCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this, IsItemsReadOnly);
                }
                return _DeleteCommand;
            }
        }
        #endregion

        #region Type Management
        private ObjectClass _type;
        public ObjectClass DataType
        {
            get
            {
                return _type;
            }
        }


        private Kistl.Client.Presentables.ObjectClassViewModel _dataTypeMdl = null;
        public Kistl.Client.Presentables.ObjectClassViewModel DataTypeViewModel
        {
            get
            {
                if (_dataTypeMdl == null)
                {
                    _dataTypeMdl = ViewModelFactory.CreateViewModel<ObjectClassViewModel.Factory>(_type).Invoke(DataContext, _type);
                }
                return _dataTypeMdl;
            }
        }

        public InterfaceType InterfaceType
        {
            get
            {
                return DataContext.GetInterfaceType(_type.GetDataType());
            }
        }
        #endregion

        #region Instances
        private ObservableCollection<DataObjectViewModel> _instancesCache = null;
        protected ObservableCollection<DataObjectViewModel> InstancesCache
        {
            get
            {
                if (_instancesCache == null)
                {
                    _instancesCache = new ObservableCollection<DataObjectViewModel>();
                    LoadInstances();
                }
                return _instancesCache;
            }
        }

        private ObservableCollection<DataObjectViewModel> _instances = null;
        /// <summary>
        /// Allow instances to be added external
        /// </summary>
        public ObservableCollection<DataObjectViewModel> Instances
        {
            get
            {
                if (_instances == null)
                {
                    ExecutePostFilter();
                }
                return _instances;
            }
        }

        public class Proxy
        {
            public Proxy()
            {
            }

            public DataObjectViewModel Object { get; set; }
        }
        
        private DataObjectViewModel GetObjectFromProxy(Proxy p)
        {
            if (p.Object == null)
            {
                var obj = DataContext.Create(DataContext.GetInterfaceType(_type.GetDataType()));
                p.Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
                _proxyCache[p.Object] = p;
            }
            return p.Object;
        }
        public class ProxyList : ObservableProjectedList<DataObjectViewModel, Proxy>, IList, IList<Proxy>
        {
            public ProxyList(INotifyCollectionChanged notifyingCollection, Func<DataObjectViewModel, Proxy> select, Func<Proxy, DataObjectViewModel> inverter)
                : base(notifyingCollection, notifyingCollection, select, inverter)
            {
            }
        }

        Dictionary<DataObjectViewModel, Proxy> _proxyCache = new Dictionary<DataObjectViewModel, Proxy>();
        private Proxy GetProxy(DataObjectViewModel vm)
        {
            Proxy result;
            if (!_proxyCache.TryGetValue(vm, out result))
            {
                result = new Proxy() { Object = vm };
                _proxyCache[vm] = result;
            }
            return result;
        }

        private ProxyList _proxyInstances = null;
        /// <summary>
        /// Allow instances to be added external
        /// </summary>
        public ProxyList ProxyInstances
        {
            get
            {
                if (_proxyInstances == null)
                {
                    _proxyInstances = new ProxyList(
                        Instances,
                        (vm) => GetProxy(vm),
                        (p) => GetObjectFromProxy(p));
                }
                return _proxyInstances;
            }
        }

        private ObservableProjectedList<DataObjectViewModel, Proxy> _selectedProxies = null;
        public ObservableProjectedList<DataObjectViewModel, Proxy> SelectedProxies
        {
            get
            {
                if (_selectedProxies == null)
                {
                    _selectedProxies = new ObservableProjectedList<DataObjectViewModel, Proxy>(
                        SelectedItems,
                        (vm) => GetProxy(vm),
                        (p) => GetObjectFromProxy(p));
                }
                return _selectedProxies;
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
                    _selectedItems.CollectionChanged += _selectedItems_CollectionChanged;
                }
                return _selectedItems;
            }
        }

        void _selectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("SelectedItem");
            OnPropertyChanged("SelectedDetailItem");
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
                OnPropertyChanged("SelectedDetailItem");
            }
        }

        /// <summary>
        /// Returns only the SelectedItem if ShowMasterDetail is set to true
        /// </summary>
        public DataObjectViewModel SelectedDetailItem
        {
            get
            {
                return ShowMasterDetail ? SelectedItem : null;
            }
            set
            {
                SelectedItem = value;
            }
        }

        private bool _ShowMasterDetail = false;
        /// <summary>
        /// If true, Details will be displayed
        /// </summary>
        public bool ShowMasterDetail
        {
            get
            {
                return _ShowMasterDetail;
            }
            set
            {
                if (_ShowMasterDetail != value)
                {
                    _ShowMasterDetail = value;
                    OnPropertyChanged("ShowMasterDetail");
                    OnPropertyChanged("SelectedDetailItem");
                }
            }
        }

        /// <summary>
        /// Reload instances from context.
        /// </summary>
        public void ReloadInstances()
        {
            if (_instancesCache != null)
            {
                _instancesCache.Clear();
                LoadInstances();
                ExecutePostFilter();
            }
        }

        private bool _SelectFirstOnLoad = false;
        /// <summary>
        /// If true, the first Item will be selected every time the Instances will be refreshed
        /// </summary>
        public bool SelectFirstOnLoad
        {
            get
            {
                return _SelectFirstOnLoad;
            }
            set
            {
                if (_SelectFirstOnLoad != value)
                {
                    _SelectFirstOnLoad = value;
                    OnPropertyChanged("SelectFirstOnLoad");
                }
            }
        }
        #endregion

        #region Sorting
        private string _sortProperty = null;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        public void Sort(string propName, ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(propName)) throw new ArgumentNullException("propName");
            _sortProperty = propName;
            _sortDirection = direction;
            ExecutePostFilter();
        }
        #endregion

        #region Opening items
        public void OpenObjects(IEnumerable<DataObjectViewModel> objects)
        {
            if (objects == null) throw new ArgumentNullException("objects");
            // TODO: Refactor this
            OpenCommand.Execute(objects);
        }

        public delegate void ItemsDefaultActionHandler(IEnumerable<DataObjectViewModel> objects);
        public event ItemsDefaultActionHandler ItemsDefaultAction = null;

        public void OnItemsDefaultAction(IEnumerable<DataObjectViewModel> objects)
        {
            ItemsDefaultActionHandler temp = ItemsDefaultAction;
            if (temp != null)
            {
                temp(objects);
            }
            else
            {
                OpenObjects(objects);
            }
        }
        #endregion

        #region UI
        private bool _isItemsReadOnly = true;
        /// <summary>
        /// If true, all Items will be set to readonly
        /// </summary>
        /// <remarks>
        /// Default value is true
        /// </remarks>
        public bool IsItemsReadOnly
        {
            get
            {
                return _isItemsReadOnly;
            }
            set
            {
                if (_isItemsReadOnly != value)
                {
                    _isItemsReadOnly = value;
                    OnIsItemsReadOnlyChanged();
                }
            }
        }

        protected virtual void OnIsItemsReadOnlyChanged()
        {
            _displayedColumns = null;
            if (_instancesCache != null)
            {
                foreach (var i in _instancesCache)
                {
                    i.IsReadOnly = _isItemsReadOnly;
                }
            }
            OnPropertyChanged("IsItemsReadOnly");
            OnPropertyChanged("IsEditable");
            OnPropertyChanged("DisplayedColumns");
        }

        private bool _isEditable = true;
        /// <summary>
        /// If true, all Items are editable in the list directly.
        /// </summary>
        /// <remarks>
        /// This does not affect the details pane. Returnes always false, if IsItemsReadOnly is set to true.
        /// </remarks>
        public bool IsEditable
        {
            get
            {
                if (IsItemsReadOnly) return false;
                return _isEditable;
            }
            set
            {
                if (_isEditable != value)
                {
                    _isEditable = value;
                    _displayedColumns = null;
                    OnPropertyChanged("IsEditable");
                    OnPropertyChanged("DisplayedColumns");
                }
            }
        }

        /// <returns>the default icon of this <see cref="DataType"/></returns>
        public Kistl.App.GUI.Icon Icon
        {
            get
            {
                if (_type != null)
                    return _type.DefaultIcon;
                else
                    return null;
            }
        }

        public override string Name
        {
            get
            {
                return string.Format(InstanceListViewModelResources.Name, _type.Name);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        private InstanceListViewMethod _viewMethod = InstanceListViewMethod.List;
        /// <summary>
        /// Displays the list as "List" or "Details"
        /// </summary>
        public InstanceListViewMethod ViewMethod
        {
            get
            {
                return _viewMethod;
            }
            set
            {
                if (_viewMethod != value)
                {
                    _viewMethod = value;
                    OnPropertyChanged("ViewMethod");
                }
            }
        }

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

        public delegate void DisplayedColumnsCreatedHandler(GridDisplayConfiguration cols);
        public event DisplayedColumnsCreatedHandler DisplayedColumnsCreated;

        protected virtual GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = new GridDisplayConfiguration(FrozenContext);
            result.BuildColumns(this._type, !IsEditable);

            DisplayedColumnsCreatedHandler temp = DisplayedColumnsCreated;
            if (temp != null)
            {
                temp(result);
            }

            return result;
        }
        #endregion

        #region Execute Filter and fill List
        private IQueryable _query;
        protected virtual IQueryable GetQuery()
        {
            var result = _query;

            foreach (var f in Filter.Where(f => f.Enabled))
            {
                result = f.GetQuery(result);
            }

            return result;
        }

        public IQueryable GetTypedQuery<T>() where T : class, IDataObject
        {
            return DataContext.GetQuery<T>();
        }

        /// <summary>
        /// Loads the instances of this DataType and adds them to the Instances collection
        /// </summary>
        private void LoadInstances()
        {
            // Can execute?
            if (Filter.Count(f => !f.Enabled && f.Required) > 0) return;

            foreach (var obj in GetQuery().Cast<IDataObject>().ToList().OrderBy(obj => obj.ToString()))
            {
                var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
                mdl.IsReadOnly = IsItemsReadOnly;
                _instancesCache.Add(mdl);
            }
            OnInstancesChanged();
        }

        /// <summary>
        /// Call this when the <see cref="InstancesCache"/> property or its 
        /// contents have changed. Override this to react on changes here.
        /// </summary>
        protected virtual void OnInstancesChanged()
        {
            ExecutePostFilter();
        }

        /// <summary>
        /// Create a fresh <see cref="Instances"/> collection when something has changed.
        /// </summary>
        private void ExecutePostFilter()
        {
            var tmp = new List<DataObjectViewModel>(this.InstancesCache);
            // poor man's full text search
            foreach (var filter in Filter.Where(i => !i.IsServerSideFilter))
            {
                if (filter.Enabled)
                {
                    tmp = filter.GetResult(tmp).Cast<DataObjectViewModel>().ToList();
                }
            }

            // Sort
            if (!string.IsNullOrEmpty(_sortProperty))
            {
                var tmpOrderBy = _sortProperty.Split('.').Select(i => String.Format("PropertyModelsByName[\"{0}\"]", i));
                var orderby = string.Join(".Value.", tmpOrderBy.ToArray());

                _instances =
                    new ObservableCollection<DataObjectViewModel>(
                    tmp
                        .AsQueryable()
                    // Sorting CompundObjects does not work
                    // Maybe we should implement a custom comparer
                        .OrderBy(string.Format("it.{0}.UntypedValue {1}",
                                    orderby,
                                    _sortDirection == ListSortDirection.Descending ? "desc" : string.Empty
                                )
                            )
                    );
            }
            else
            {
                _instances = new ObservableCollection<DataObjectViewModel>(tmp);
            }

            _proxyCache.Clear();
            _proxyInstances = null;

            OnPropertyChanged("Instances");
            OnPropertyChanged("ProxyInstances");

            if (SelectFirstOnLoad)
            {
                this.SelectedItem = _instances.FirstOrDefault();
            }
        }

        #endregion

        #region IRefreshCommandListener Members

        void IRefreshCommandListener.Refresh()
        {
            ReloadInstances();
        }

        #endregion
    }
}
