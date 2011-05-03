
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections;
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
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.FilterViewModels;
    using Kistl.Client.Presentables.ValueViewModels;
    using ObjectEditor = Kistl.Client.Presentables.ObjectEditor;

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
        : ViewModel, IViewModelWithIcon, ILabeledViewModel, IRefreshCommandListener
    {
        public new delegate InstanceListViewModel Factory(IKistlContext dataCtx, Func<IKistlContext> workingCtxFactory, ObjectClass type, Func<IQueryable> qry);

        protected readonly Func<IKistlContext> workingCtxFactory;

        /// <summary>
        /// Initializes a new instance of the DataTypeViewModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="config"></param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="workingCtxFactory">A factory for creating a working context. If the InstanceList is embedded in a workspace which the user has to submit manually, the factory should return the same context as passed in the dataCtx parameter.</param>
        /// <param name="type">the data type to model. If null, qry must be a Query of a valid DataType</param>
        /// <param name="qry">optional: the query to display. If null, Query will be constructed from type</param>
        public InstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig config,
            IKistlContext dataCtx,
            Func<IKistlContext> workingCtxFactory,
            ObjectClass type,
            Func<IQueryable> qry)
            : base(appCtx, dataCtx)
        {
            if (dataCtx == null) throw new ArgumentNullException("dataCtx");
            if (workingCtxFactory == null) throw new ArgumentNullException("workingCtxFactory");
            if (type == null) throw new ArgumentNullException("type");

            _type = type;
            if (qry == null)
            {
                MethodInfo mi = this.GetType().FindGenericMethod("GetTypedQuery", new Type[] { DataContext.GetInterfaceType(_type.GetDataType()).Type }, null);
                // See Case 552
                _query = () => (IQueryable)mi.Invoke(this, new object[] { });
            }
            else
            {
                _query = qry;
            }

            this.workingCtxFactory = workingCtxFactory;
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

        private bool _RespectRequieredFilter = true;
        /// <summary>
        /// If set to false, no filter is requiered. Default value is true. Use this setting if a small, preselected list (query) is provides as data source.
        /// </summary>
        public bool RespectRequieredFilter
        {
            get
            {
                return _RespectRequieredFilter;
            }
            set
            {
                if (_RespectRequieredFilter != value)
                {
                    _RespectRequieredFilter = value;
                    OnPropertyChanged("RespectRequieredFilter");
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

                        if (_filter.Count == 0)
                        {
                            // Add default ToString Filter only if there is no filter configuration
                            _filter.Add(new ToStringFilterModel(FrozenContext));
                        }
                    }
                }
                return _filter;
            }
        }

        void _filter_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
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

        /// <summary>
        /// Retruns true if DataContext is the same as the working context returned by the working context factory.
        /// This is an indicator for an embedded InstanceList (embedded in a workspace which a User must submit manually).
        /// </summary>
        /// <param name="workingCtx"></param>
        /// <returns></returns>
        protected bool isEmbedded(IKistlContext workingCtx)
        {
            return workingCtx == DataContext;
        }

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

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var result = base.CreateCommands();

            if (AllowAddNew) result.Add(NewCommand);
            if (ShowOpenCommand) result.Add(OpenCommand);
            if (ShowRefreshCommand) result.Add(RefreshCommand);
            if (AllowDelete) result.Add(DeleteCommand);

            return result;
        }

        private void UpdateCommands()
        {
            if (commandsStore == null) return;
            if (!AllowAddNew && commandsStore.Contains(NewCommand)) commandsStore.Remove(NewCommand);
            if (!ShowOpenCommand && commandsStore.Contains(OpenCommand)) commandsStore.Remove(OpenCommand);
            if (!ShowRefreshCommand && commandsStore.Contains(RefreshCommand)) commandsStore.Remove(RefreshCommand);
            if (!AllowDelete && commandsStore.Contains(DeleteCommand)) commandsStore.Remove(DeleteCommand);

            if (AllowAddNew && !commandsStore.Contains(NewCommand)) commandsStore.Insert(0, NewCommand);
            if (ShowOpenCommand && !commandsStore.Contains(OpenCommand)) commandsStore.Insert(AllowAddNew ? 1 : 0, OpenCommand);
            if (ShowRefreshCommand && !commandsStore.Contains(RefreshCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0), RefreshCommand);
            if (AllowDelete && !commandsStore.Contains(DeleteCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0) + (ShowRefreshCommand ? 1 : 0), DeleteCommand);
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

        private RefreshCommand _RefreshCommand;
        public RefreshCommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this);
                    _RefreshCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_reload_png);
                }
                return _RefreshCommand;
            }
        }

        private ICommandViewModel _OpenCommand;
        public ICommandViewModel OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<DataObjectViewModel>.Factory>().Invoke(
                        DataContext, CommonCommandsResources.OpenDataObjectCommand_Name,
                        CommonCommandsResources.OpenDataObjectCommand_Tooltip,
                        OpenObjects);
                    _OpenCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_fileopen_png);
                }
                return _OpenCommand;
            }
        }

        private ICommandViewModel _NewCommand;
        public ICommandViewModel NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, CommonCommandsResources.NewDataObjectCommand_Name, CommonCommandsResources.NewDataObjectCommand_Tooltip, NewObject, () => AllowAddNew);
                    _NewCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_new_png);
                }
                return _NewCommand;
            }
        }

        private void NewObject()
        {
            var workingCtx = workingCtxFactory();
            var obj = workingCtx.Create(DataContext.GetInterfaceType(_type.GetDataType()));
            OnObjectCreated(obj);

            if (isEmbedded(workingCtx))
            {
                this.ReloadInstances();
                var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
                this.SelectedItem = mdl;
                ViewModelFactory.ShowModel(mdl, true);
            }
            else
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(workingCtx);
                newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, workingCtx, obj), RequestedEditorKind);
                ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);
            }
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

        private ICommandViewModel _DeleteCommand;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<DataObjectViewModel>.Factory>().Invoke(
                        DataContext,
                        CommonCommandsResources.DeleteDataObjectCommand_Name,
                        CommonCommandsResources.DeleteDataObjectCommand_Tooltip,
                        DeleteObjects);
                    _DeleteCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_delete_png);
                }
                return _DeleteCommand;
            }
        }

        public void DeleteObjects(IEnumerable<DataObjectViewModel> objects)
        {
            if (objects == null) throw new ArgumentNullException("objects");
            if (!AllowDelete) throw new InvalidOperationException("Deleting items is not allowed. See AllowDelete property");

            var workingCtx = workingCtxFactory();

            foreach (var item in objects)
            {
                var working = workingCtx.Find(workingCtx.GetInterfaceType(item.Object), item.ID);
                workingCtx.Delete(working);
            }

            if (!isEmbedded(workingCtx))
            {
                workingCtx.SubmitChanges();
            }

            ReloadInstances();
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
        private List<DataObjectViewModel> _instancesCache = null;
        protected List<DataObjectViewModel> InstancesCache
        {
            get
            {
                if (_instancesCache == null)
                {
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

        #region Proxy
        private DataObjectViewModel GetObjectFromProxy(DataObjectViewModelProxy p)
        {
            if (p.Object == null)
            {
                var obj = DataContext.Create(DataContext.GetInterfaceType(_type.GetDataType()));
                p.Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
                _proxyCache[p.Object] = p;
                OnObjectCreated(obj);
            }
            return p.Object;
        }

        Dictionary<DataObjectViewModel, DataObjectViewModelProxy> _proxyCache = new Dictionary<DataObjectViewModel, DataObjectViewModelProxy>();
        private DataObjectViewModelProxy GetProxy(DataObjectViewModel vm)
        {
            DataObjectViewModelProxy result;
            if (!_proxyCache.TryGetValue(vm, out result))
            {
                result = new DataObjectViewModelProxy() { Object = vm };
                _proxyCache[vm] = result;
            }
            return result;
        }

        /// <summary>
        /// Hack for those who do not check element types by traversing from inherited interfaces
        /// e.g. DataGrid from WPF
        /// </summary>
        public sealed class ProxyList : AbstractObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy>, IList, IList<DataObjectViewModelProxy>
        {
            public ProxyList(INotifyCollectionChanged notifyingCollection, Func<DataObjectViewModel, DataObjectViewModelProxy> select, Func<DataObjectViewModelProxy, DataObjectViewModel> inverter)
                : base(notifyingCollection, notifyingCollection, select, inverter, false)
            {
            }
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

        private ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> _selectedProxies = null;
        public ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy> SelectedProxies
        {
            get
            {
                if (_selectedProxies == null)
                {
                    _selectedProxies = new ObservableProjectedList<DataObjectViewModel, DataObjectViewModelProxy>(
                        SelectedItems,
                        (vm) => GetProxy(vm),
                        (p) => GetObjectFromProxy(p));
                }
                return _selectedProxies;
            }
        }
        #endregion

        #region SelectedItems
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
        #endregion

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
            LoadInstances();
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
            var workingCtx = workingCtxFactory();
            if (isEmbedded(workingCtx))
            {
                foreach (var item in objects)
                {
                    ViewModelFactory.ShowModel(item, true);
                }
            }
            else
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(workingCtx);
                ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);

                var loader = ViewModelFactory.CreatePropertyLoader(newWorkspace, () =>
                {
                    foreach (var item in objects)
                    {
                        //var newMdl = 
                        newWorkspace.ShowForeignModel(item, RequestedEditorKind);
                        //ModelCreatedEventHandler temp = ModelCreated;
                        //if (temp != null)
                        //{
                        //    temp(newMdl);
                        //}
                    }
                    newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
                    newWorkspace.IsBusy = false;
                });

                loader.Reload();
            }
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
        private bool _isEditable = false;
        /// <summary>
        /// If true, all Items are editable in the list directly. Default is false
        /// </summary>
        /// <remarks>
        /// This does not affect the details pane. 
        /// </remarks>
        public bool IsEditable
        {
            get
            {
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

        private bool _isMultiselect = true;
        /// <summary>
        /// If true, allow multiselect of items. Default is true
        /// </summary>
        public bool IsMultiselect
        {
            get
            {
                return _isMultiselect;
            }
            set
            {
                if (_isMultiselect != value)
                {
                    _isMultiselect = value;
                    OnPropertyChanged("IsMultiselect");
                }
            }
        }

        private bool _allowAddNew = false;
        /// <summary>
        /// If true, allow add new Items in the list directly. Default is false
        /// </summary>
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

        private bool _allowDelete = false;
        /// <summary>
        /// If true, allow deleting Items in the list directly. Default is false.
        /// </summary>
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

        private string _name;
        public override string Name
        {
            get
            {
                return _name ?? string.Format(InstanceListViewModelResources.Name, _type.Name);
            }
        }

        public void SetName(string value)
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged("Name");
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
            var result = new GridDisplayConfiguration();
            result.BuildColumns(this._type, IsEditable ? GridDisplayConfiguration.Mode.Editable : GridDisplayConfiguration.Mode.ReadOnly, IsEditable);

            DisplayedColumnsCreatedHandler temp = DisplayedColumnsCreated;
            if (temp != null)
            {
                temp(result);
            }

            return result;
        }
        #endregion

        #region Execute Filter and fill List
        private Func<IQueryable> _query;
        protected virtual IQueryable GetQuery()
        {
            var result = _query();

            foreach (var f in Filter.Where(f => f.Enabled))
            {
                result = f.GetQuery(result);
            }

            return result;
        }

        public IQueryable GetTypedQuery<T>() where T : class, IDataObject
        {
            return DataContext.GetQuery<T>(); // ToList would make all filter client side filter .ToList().OrderBy(obj => obj.ToString()).AsQueryable();
        }

        private IPropertyLoader _loadInstancesLoader;
        private bool loadingInstances = false;
        /// <summary>
        /// Loads the instances of this DataType and adds them to the Instances collection
        /// </summary>
        private void LoadInstances()
        {
            // Prevent loading instances twice
            if (loadingInstances) return;
            loadingInstances = true;
            if (_loadInstancesLoader == null) _loadInstancesLoader = ViewModelFactory.CreatePropertyLoader(this, () =>
            {
                try
                {
                    // Can execute?
                    if (RespectRequieredFilter && Filter.Count(f => !f.Enabled && f.Required) > 0)
                    {
                        // leave result or return empty result
                        if (_instancesCache == null) _instancesCache = new List<DataObjectViewModel>();
                        return;
                    }

                    _instancesCache = new List<DataObjectViewModel>();
                    foreach (IDataObject obj in GetQuery()) // No order by - may be set from outside in LinqQuery! .Cast<IDataObject>().ToList().OrderBy(obj => obj.ToString()))
                    {
                        // Not interested in deleted objects
                        // TODO: Discuss if a query should return deleted objects
                        if (obj.ObjectState == DataObjectState.Deleted) continue;

                        var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj);
                        _instancesCache.Add(mdl);
                    }
                    OnInstancesChanged();
                }
                finally
                {
                    loadingInstances = false;
                }
            });

            _loadInstancesLoader.Reload();

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

            // Can be changed in UI -> listen to that
            _instances.CollectionChanged += new NotifyCollectionChangedEventHandler(_instances_CollectionChanged);

            _proxyCache.Clear();
            _proxyInstances = null;

            OnPropertyChanged("ProxyInstances");
            OnPropertyChanged("Instances");

            if (SelectFirstOnLoad)
            {
                this.SelectedItem = _instances.FirstOrDefault();
            }
        }

        void _instances_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Add new is not very interesting, since the object was attached while creating it
            // More interessting is deleting: Deleting is removing from the list, but we have to delete it from the database
            if (e.OldItems != null && e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var obj in e.OldItems.OfType<DataObjectViewModel>())
                {
                    DataContext.Delete(obj.Object);
                }
            }
        }

        #endregion

        #region IRefreshCommandListener Members

        void IRefreshCommandListener.Refresh()
        {
            ReloadInstances();
        }

        #endregion

        #region ILabeledViewModel
        private string _label;
        public string Label
        {
            get
            {
                return _label ?? Name;
            }
            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        private string _toolTip;
        public string ToolTip
        {
            get
            {
                return _toolTip ?? Name;
            }
            set
            {
                if (_toolTip != value)
                {
                    _toolTip = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        public ViewModel Model
        {
            get { return this; }
        }

        public bool Required
        {
            get { return false; }
        }
        #endregion
    }
}
