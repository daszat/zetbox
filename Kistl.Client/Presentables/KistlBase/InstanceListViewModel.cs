
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using ObjectEditor = Kistl.Client.Presentables.ObjectEditor;
    using System.ComponentModel;

    public enum InstanceListViewMethod
    {
        List = 1,
        Details = 2,
    }

    /// <summary>
    /// Models the specialities of <see cref="DataType"/>s.
    /// </summary>
    [ViewModelDescriptor("KistlBase", DefaultKind = "Kistl.App.GUI.InstanceListKind", Description = "DataObjectModel with specific extensions for DataTypes")]
    public class InstanceListViewModel
        : ViewModel, IViewModelWithIcon, IRefreshCommandListener
    {
        public new delegate InstanceListViewModel Factory(IKistlContext dataCtx, ObjectClass type, IQueryable qry);

        protected readonly Func<IKistlContext> ctxFactory;

        /// <summary>
        /// Initializes a new instance of the DataTypeModel class.
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
                }
            }
        }

        private ObservableCollection<IFilterExpression> _filter = null;
        public ICollection<IFilterExpression> Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = new ObservableCollection<IFilterExpression>();
                    // React on changes -> attach to FilterChanged Event
                    _filter.CollectionChanged += new NotifyCollectionChangedEventHandler(_filter_CollectionChanged);

                    if (EnableAutoFilter)
                    {
                        // Resolve default property filter
                        var t = _type;
                        while (t != null)
                        {
                            // Add Property filter expressions
                            foreach (var prop in t.Properties.Where(p => p.FilterConfiguration != null))
                            {
                                var cfg = prop.FilterConfiguration;
                                _filter.Add(ModelFactory.CreateViewModel<PropertyFilterExpressionFactory>(cfg.ViewModelDescriptor.ViewModelRef.AsType(true)).Invoke(DataContext, prop, cfg));
                            }
                            if (t is ObjectClass)
                            {
                                t = ((ObjectClass)t).BaseObjectClass;
                            }
                        }

                        // Add default ToString Filter for all
                        _filter.Add(ModelFactory.CreateViewModel<ToStringFilterExpression.Factory>().Invoke(DataContext, "Name"));
                    }
                }
                return _filter;
            }
        }

        void _filter_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems.OfType<IUIFilterExpression>())
            {
                // attach change events
                item.FilterChanged += new EventHandler(delegate(object s, EventArgs a)
                {
                    if (s is IPostFilterExpression)
                    {
                        ExecutePostFilter();
                    }
                    else
                    {
                        ReloadInstances();
                    }
                });
            }

            OnPropertyChanged("FilterViewModels");
        }

        public IEnumerable<IUIFilterExpression> FilterViewModels
        {
            get
            {
                return Filter.OfType<IUIFilterExpression>();
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

            if (ShowNewCommand && !_commands.Contains(NewCommand)) _commands.Insert(0, NewCommand);
            if (ShowOpenCommand && !_commands.Contains(OpenCommand)) _commands.Insert(ShowNewCommand ? 1 : 0, OpenCommand);
            if (ShowRefreshCommand && !_commands.Contains(RefreshCommand)) _commands.Insert((ShowNewCommand ? 1 : 0) + (ShowOpenCommand ? 1 : 0), RefreshCommand);
        }

        private ObservableCollection<ICommand> _commands = null;
        public ObservableCollection<ICommand> Commands
        {
            get
            {
                if (_commands == null)
                {
                    _commands = new ObservableCollection<ICommand>();
                    // Add default actions
                    if(ShowNewCommand) _commands.Add(NewCommand);
                    if (ShowOpenCommand) _commands.Add(OpenCommand);
                    if (ShowRefreshCommand) _commands.Add(RefreshCommand);
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
                    _RefreshCommand = ModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this);
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
                    _OpenCommand = ModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(DataContext, RequestedWorkspaceKind, RequestedEditorKind);
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
                    _NewCommand = ModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(DataContext, _type, RequestedWorkspaceKind, RequestedEditorKind);
                }
                return _NewCommand;
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


        private Kistl.Client.Presentables.ObjectClassModel _dataTypeMdl = null;
        public Kistl.Client.Presentables.ObjectClassModel DataTypeModel
        {
            get
            {
                if (_dataTypeMdl == null)
                {
                    _dataTypeMdl = ModelFactory.CreateViewModel<ObjectClassModel.Factory>(_type).Invoke(DataContext, _type);
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
        // TODO: make readonly, take care of new and deleted+submitted objects
        private ObservableCollection<DataObjectModel> _instances = null;
        public ObservableCollection<DataObjectModel> Instances
        {
            get
            {
                if (_instances == null)
                {
                    _instances = new ObservableCollection<DataObjectModel>();

                    // As this is a "calculated" collection, only insider should modify this
                    ////_instances.CollectionChanged += _instances_CollectionChanged;
                    LoadInstances();
                }
                return _instances;
            }
        }

        private ReadOnlyObservableCollection<DataObjectModel> _instancesFiltered = null;
        public ReadOnlyObservableCollection<DataObjectModel> InstancesFiltered
        {
            get
            {
                if (_instancesFiltered == null)
                {
                    ExecutePostFilter();
                }
                return _instancesFiltered;
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
        /// <summary>
        /// Reload instances from context.
        /// </summary>
        public void ReloadInstances()
        {
            if (_instances != null)
            {
                _instances.Clear();
                LoadInstances();
                ExecutePostFilter();
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
        public void OpenObjects(IEnumerable<DataObjectModel> objects)
        {
            if (objects == null) throw new ArgumentNullException("objects");
            // TODO: Refactor this
            OpenCommand.Execute(objects);
        }

        public delegate void ItemsDefaultActionHandler(IEnumerable<DataObjectModel> objects);
        public event ItemsDefaultActionHandler ItemsDefaultAction = null;

        public void OnItemsDefaultAction(IEnumerable<DataObjectModel> objects)
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
            if (_instances != null)
            {
                foreach (var i in _instances)
                {
                    i.IsReadOnly = _isItemsReadOnly;
                }
            }
            OnPropertyChanged("IsItemsReadOnly");
            OnPropertyChanged("DisplayedColumns");
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
                return _type.Name;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        private InstanceListViewMethod _viewMethod = InstanceListViewMethod.List;
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
        protected virtual GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = new GridDisplayConfiguration();
            result.BuildColumns(this._type, IsItemsReadOnly);
            return result;
        }
        #endregion

        #region Execute Filter and fill List
        private IQueryable _query;
        protected virtual IQueryable GetQuery()
        {
            var result = _query;

            foreach (var f in Filter.OfType<ILinqFilterExpression>().Where(f => f.Enabled))
            {
                result = result.Where(f.Predicate, f.FilterValues);
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
            if (Filter.Count(f => !f.Enabled && f.Requiered) > 0) return;

            foreach (var obj in GetQuery().Cast<IDataObject>().ToList().OrderBy(obj => obj.ToString()))
            {
                var mdl = ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(DataContext, obj);
                mdl.IsReadOnly = IsItemsReadOnly;
                _instances.Add(mdl);
            }
            OnInstancesChanged();
        }

        /// <summary>
        /// Call this when the <see cref="Instances"/> property or its 
        /// contents have changed. Override this to react on changes here.
        /// </summary>
        protected virtual void OnInstancesChanged()
        {
            OnPropertyChanged("Instances");
            ExecutePostFilter();
        }

        /// <summary>
        /// Create a fresh <see cref="InstancesFiltered"/> collection when something has changed.
        /// </summary>
        private void ExecutePostFilter()
        {
            _instancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(this.Instances);
            // poor man's full text search
            foreach (var filter in Filter.OfType<IPostFilterExpression>())
            {
                if (filter.Enabled)
                {
                    _instancesFiltered = filter.Execute(_instancesFiltered);
                }
            }

            // Sort
            if (!string.IsNullOrEmpty(_sortProperty))
            {
                _instancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(
                    new ObservableCollection<DataObjectModel>(
                    _instancesFiltered
                        .AsQueryable()
                        .OrderBy(string.Format("it.Object.{0} {1}",
                                    _sortProperty,
                                    _sortDirection == ListSortDirection.Descending ? "desc" : string.Empty
                                )
                            )
                        )
                    );
            }

            OnPropertyChanged("InstancesFiltered");
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
