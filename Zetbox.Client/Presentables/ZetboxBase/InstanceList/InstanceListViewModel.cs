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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.FilterViewModels;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using ObjectEditor = Zetbox.Client.Presentables.ObjectEditor;

    /// <summary>
    /// Models the specialities of <see cref="DataType"/>s.
    /// </summary>
    [ViewModelDescriptor]
    public partial class InstanceListViewModel
        : ViewModel, ILabeledViewModel, ISortableViewModel, IRequestedEditorKinds
    {
        public new delegate InstanceListViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass type, Func<IQueryable> qry);

        protected readonly IFileOpener fileOpener;
        protected readonly ITempFileService tmpService;
        protected readonly Lazy<IUIExceptionReporter> errorReporter;

        /// <summary>
        /// Initializes a new instance of the DataTypeViewModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="config"></param>
        /// <param name="fileOpener"></param>
        /// <param name="tmpService"></param>
        /// <param name="errorReporter"></param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">Parent ViewModel</param>
        /// <param name="type">the data type to model. Must not be null.</param>
        /// <param name="qry">optional: the query to display. If null, Query will be constructed from type</param>
        public InstanceListViewModel(
            IViewModelDependencies appCtx,
            ZetboxConfig config,
            IFileOpener fileOpener,
            ITempFileService tmpService,
            Lazy<IUIExceptionReporter> errorReporter,
            IZetboxContext dataCtx,
            ViewModel parent,
            ObjectClass type,
            Func<IQueryable> qry)
            : base(appCtx, dataCtx, parent)
        {
            if (dataCtx == null) throw new ArgumentNullException("dataCtx");
            if (type == null) throw new ArgumentNullException("type");
            if (fileOpener == null) throw new ArgumentNullException("fileOpener");
            if (tmpService == null) throw new ArgumentNullException("tmpService");
            if (errorReporter == null) throw new ArgumentNullException("errorReporter");
            this.fileOpener = fileOpener;
            this.tmpService = tmpService;
            this.errorReporter = errorReporter;

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

            dataCtx.IsElevatedModeChanged += new EventHandler(dataCtx_IsElevatedModeChanged);
        }

        void dataCtx_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("AllowAddNew");
            OnPropertyChanged("AllowDelete");
            OnPropertyChanged("AllowMerge");
            UpdateCommands();
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

        #region Type Management
        private ObjectClass _type;
        public ObjectClass DataType
        {
            get
            {
                return _type;
            }
        }

        private Zetbox.Client.Presentables.ObjectClassViewModel _dataTypeMdl = null;
        public Zetbox.Client.Presentables.ObjectClassViewModel DataTypeViewModel
        {
            get
            {
                if (_dataTypeMdl == null)
                {
                    _dataTypeMdl = ViewModelFactory.CreateViewModel<ObjectClassViewModel.Factory>(_type).Invoke(DataContext, this, _type);
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
        private List<DataObjectViewModel> _instancesFromServer = new List<DataObjectViewModel>();
        private List<DataObjectViewModel> _filteredInstances = new List<DataObjectViewModel>();

        #region InstancesAsync

        private PropertyTask<ReadOnlyCollection<DataObjectViewModel>> _instancesTask;
        private PropertyTask<ReadOnlyCollection<DataObjectViewModel>> EnsureInstancesTask()
        {
            if (_instancesTask != null) return _instancesTask;

            return _instancesTask = new PropertyTask<ReadOnlyCollection<DataObjectViewModel>>(
                notifier: () =>
                {
                    OnInstancesChanged();
                },
                createTask: () =>
                {
                    return Task.Run(async () =>
                    {
                        await LoadInstancesCore();
                        return _filteredInstances.AsReadOnly();
                    });
                },
                set: null);
        }

        public ReadOnlyCollection<DataObjectViewModel> Instances
        {
            get { return EnsureInstancesTask().Get(); }
        }

        public ReadOnlyCollection<DataObjectViewModel> InstancesAsync
        {
            get { return EnsureInstancesTask().GetAsync(); }
        }

        private void InvalidateInstances()
        {
            if (_instancesTask != null)
                _instancesTask.Invalidate();
            OnInstancesChanged();
        }

        #endregion

        private void AddLocalInstance(DataObjectViewModel mdl)
        {
            _instancesFromServer.Add(mdl);
            _filteredInstances.Add(mdl);

            InvalidateInstances();
        }

        public int InstancesCount
        {
            get
            {
                return _filteredInstances.Count;
            }
        }

        private string _instancesCountAsTextFormatString;

        /// <summary>
        /// Format string for the <see cref="InstancesCountAsText" /> property. {0} = InstancesCount
        /// </summary>
        public string InstancesCountAsTextFormatString
        {
            get
            {
                return _instancesCountAsTextFormatString;
            }
            set
            {
                if (_instancesCountAsTextFormatString != value)
                {
                    _instancesCountAsTextFormatString = value;
                    OnPropertyChanged("InstancesCountAsTextFormatString");
                    OnPropertyChanged("InstancesCountAsText");
                }
            }
        }

        /// <summary>
        /// Uses <see cref="InstancesCountAsTextFormatString" /> as format string or the default format string "Items"
        /// </summary>
        public string InstancesCountAsText
        {
            get
            {
                string countStr = string.Empty;

                if (InstancesCount >= Helper.MAXLISTCOUNT || CurrentPage != 1)
                {
                    var from = (CurrentPage - 1) * Helper.MAXLISTCOUNT;
                    countStr = string.Format("{0} - {1}", from + 1, from + InstancesCount);
                }
                else
                {
                    countStr = InstancesCount.ToString();
                }

                if (!string.IsNullOrEmpty(InstancesCountAsTextFormatString))
                {
                    return string.Format(InstancesCountAsTextFormatString, countStr);
                }
                else
                {
                    return string.Format("{0} {1}", countStr, InstanceListViewModelResources.InstancesCountAsText);
                }
            }
        }

        private void OnInstancesChanged()
        {
            // TODO: Selection update!
            _proxyCache.Clear();
            if (_proxyInstancesTask != null)
                _proxyInstancesTask.Invalidate();
            OnPropertyChanged("ProxyInstances");
            OnPropertyChanged("ProxyInstancesAsync");

            OnPropertyChanged("Instances");
            OnPropertyChanged("InstancesAsync");
            OnPropertyChanged("InstancesCount");
            OnPropertyChanged("InstancesCountAsText");
            OnPropertyChanged("InstancesCountWarning");
            OnPropertyChanged("InstancesCountWarningText");
        }
        #endregion

        #region Opening items

        /// <summary>
        /// Is triggered before the selected items are opened. This can be used to redirect the open command to a different set of items.
        /// </summary>
        public event EventHandler<ItemsOpeningEventArgs> ItemsOpening
        {
            add
            {
                EnsureNewCommand();
                _NewCommand.ItemsOpening += value;

                EnsureOpenCommand();
                _OpenCommand.ItemsOpening += value;
            }
            remove
            {
                _NewCommand.ItemsOpening -= value;
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
                _NewCommand.ItemsOpened += value;

                EnsureOpenCommand();
                _OpenCommand.ItemsOpened += value;
            }
            remove
            {
                _NewCommand.ItemsOpened -= value;
                _OpenCommand.ItemsOpened -= value;
            }
        }

        #endregion

        #region Configuration
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

        private bool _allowOpen = true;
        [DefaultValue(true)]
        public bool AllowOpen
        {
            get
            {
                return _allowOpen;
            }
            set
            {
                if (_allowOpen != value)
                {
                    _allowOpen = value;
                    UpdateCommands();
                    OnPropertyChanged("AllowOpen");
                }
            }
        }

        private bool _allowExport = true;
        [DefaultValue(true)]
        public bool AllowExport
        {
            get
            {
                return _allowExport;
            }
            set
            {
                if (_allowExport != value)
                {
                    _allowExport = value;
                    UpdateCommands();
                    OnPropertyChanged("AllowExport");
                }
            }
        }

        private bool _allowMerge = true;
        private bool? _allowMergeResult;
        [DefaultValue(true)]
        public bool AllowMerge
        {
            get
            {
                if (DataContext.IsElevatedMode) return true;
                if(_allowMergeResult != null) return _allowMergeResult.Value;

                Task.Run(async () => _allowMergeResult = await DataType.ImplementsIMergeable() && _allowMerge).ContinueWith(t => OnPropertyChanged(nameof(AllowMerge)));
                return false;
            }
            set
            {
                if (_allowMerge != value)
                {
                    _allowMerge = value;
                    UpdateCommands();
                    OnPropertyChanged("AllowMerge");
                }
            }
        }


        private bool? _isInlineEditable = null;
        /// <summary>
        /// If true, all Items are editable in the list directly. By default, this is true, if the displayed data type is a simple object.
        /// </summary>
        /// <remarks>
        /// This does not affect the details pane. 
        /// </remarks>
        public bool IsInlineEditable
        {
            get
            {
                return !DataContext.IsReadonly && (_isInlineEditable ?? DataType.IsSimpleObject);
            }
            set
            {
                if (_isInlineEditable != value)
                {
                    _isInlineEditable = value;
                    _displayedColumns = null;
                    OnPropertyChanged("IsInlineEditable");
                    OnPropertyChanged("DisplayedColumns");
                }
            }
        }

        private bool _isMultiselect = true;
        /// <summary>
        /// If true, allow multiselect of items. Default is true
        /// </summary>
        [DefaultValue(true)]
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
        [DefaultValue(false)]
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

        private bool _allowDelete = false;
        /// <summary>
        /// If true, allow deleting Items in the list directly. Default is false.
        /// </summary>
        [DefaultValue(false)]
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

        /// <returns>the default icon of this <see cref="DataType"/></returns>
        public override System.Drawing.Image Icon
        {
            get
            {
                if (base.Icon == null && _type != null)
                    Task.Run(async () => base.Icon = await IconConverter.ToImage(_type.DefaultIcon));
                return base.Icon;
            }
        }

        private string _name;
        public override string Name
        {
            get
            {
                return _name ?? string.Format(InstanceListViewModelResources.Name, DataTypeViewModel.Name);
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

        public void AddDisplayColumn(Property[] propPath)
        {
            DisplayedColumns.Columns.Add(ColumnDisplayModel.Create(GridDisplayConfiguration.Mode.ReadOnly, propPath));
        }
        public void AddDisplayColumn(int index, Property[] propPath)
        {
            DisplayedColumns.Columns.Insert(index, ColumnDisplayModel.Create(GridDisplayConfiguration.Mode.ReadOnly, propPath));
        }

        public void RemoveDisplayColumn(Property property)
        {
            var col = DisplayedColumns.Columns.FirstOrDefault(c => c.Property == property);
            if (col != null) DisplayedColumns.Columns.Remove(col);
        }

        /// <summary>
        /// Properties displayed in <see cref="DisplayedColumns"/>
        /// </summary>
        public IEnumerable<Property[]> DisplayedProperties
        {
            get
            {
                return DisplayedColumns.Columns.Where(c => c.Properties != null).Select(c => c.Properties);
            }
        }

        public delegate void DisplayedColumnsCreatedHandler(GridDisplayConfiguration cols);
        public event DisplayedColumnsCreatedHandler DisplayedColumnsCreated;

        protected virtual GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = new GridDisplayConfiguration();
            result.BuildColumns(this._type, IsInlineEditable ? GridDisplayConfiguration.Mode.Editable : GridDisplayConfiguration.Mode.ReadOnly, IsInlineEditable);

            DisplayedColumnsCreatedHandler temp = DisplayedColumnsCreated;
            if (temp != null)
            {
                temp(result);
            }

            return result;
        }

        private bool _allowSelectColumns = true;
        /// <summary>
        /// Allow the user to modify the column collection if the ViewMethod is Details
        /// </summary>
        /// <remarks>
        /// Returns always false if the ViewMethod is List
        /// </remarks>
        public bool AllowSelectColumns
        {
            get
            {
                return ViewMethod == InstanceListViewMethod.Details && _allowSelectColumns;
            }
            set
            {
                if (_allowSelectColumns != value)
                {
                    _allowSelectColumns = value;
                    OnPropertyChanged("AllowSelectColumns");
                    OnPropertyChanged("ShowUtilities");
                }
            }
        }

        /// <summary>
        /// Whether or not the Utilities (Filter/Columns) should be shown.
        /// </summary>
        public bool ShowUtilities
        {
            get
            {
                return AllowUserFilter || AllowSelectColumns;
            }
        }

        private WidthHint _requestedDetailHeight = WidthHint.Default;
        public WidthHint RequestedDetailHeight
        {
            get
            {
                return _requestedDetailHeight;
            }
            set
            {
                if (_requestedDetailHeight != value)
                {
                    _requestedDetailHeight = value;
                    OnPropertyChanged("RequestedDetailHeight");
                }
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

        private int _isInInit = 0;
        public void BeginInit()
        {
            _isInInit++;
            OnPropertyChanged("IsInInit");
        }

        public void EndInit()
        {
            _isInInit--;
            if (_isInInit < 0)
                _isInInit = 0;
            OnPropertyChanged("IsInInit");
        }

        public bool IsInInit
        {
            get
            {
                return _isInInit > 0;
            }
        }
        #endregion

        #region Execute Filter and fill List
        private Func<IQueryable> _query;
        protected virtual IQueryable GetQuery()
        {
            var result = GetUnpagedQuery();

            var skip = (CurrentPage - 1) * Zetbox.API.Helper.MAXLISTCOUNT;

            // Limit to maxlistcount
            // Due to the fact, that the client provider is appending 
            // local objects to the query we have to add an additional limit
            result = result.Skip(skip).Take(Zetbox.API.Helper.MAXLISTCOUNT);

            return result;
        }

        private IQueryable GetUnpagedQuery()
        {
            var result = FilterList.AppendFilter(_query());

            if (!string.IsNullOrEmpty(orderByExpression))
            {
                result = result.OrderBy(string.Format("{0} {1}",            // Sorting CompoundObject does not work
                                orderByExpression,                         // Maybe we should implement a custom comparer
                                sortDirection == System.ComponentModel.ListSortDirection.Descending ? "desc" : string.Empty));
            }
            else if (UseNaturalSortOrder)
            {
                // do nothing
                // EF will break
                // this should be only used when custom queries are passed to the constructor
            }
            else
            {
                // default sort.
                // 1. It's a good idea to have a predictable result 
                // 2. EF needs that: case 10019: The method 'Skip' is only supported for sorted input in LINQ to Entities. The method 'OrderBy' must be called before the method
                result = result.OrderBy("ID");
            }
            return result;
        }

        public IQueryable GetTypedQuery<T>() where T : class, IDataObject
        {
            return DataContext.GetQuery<T>(); // ToList would make all filter client side filter .ToList().OrderBy(obj => obj.ToString()).AsQueryable();
        }

        /// <summary>
        /// Reload instances from context.
        /// </summary>
        public void Refresh()
        {
            Refresh(true);
        }

        private void Refresh(bool resetCurrentPage)
        {
            if (!FilterList.IsFilterValid)
                return;

            if (resetCurrentPage)
            {
                CurrentPage = 1;
            }

            if (_loadInstancesCoreTask != null)
            {
                ClearBusy(); // TODO: Workaround! Cancel should call Finally?
                // TODO: _loadInstancesCoreTask.Cancel();
            }
            _loadInstancesCoreTask = null;

            LoadInstancesCore();
        }

        System.Threading.Tasks.Task _loadInstancesCoreTask;
        private System.Threading.Tasks.Task LoadInstancesCore()
        {
            if (_loadInstancesCoreTask != null || FilterList.IsFilterValid == false || IsInInit) return _loadInstancesCoreTask;

            SetBusy();
            _loadInstancesCoreTask = Task.Run(async () =>
            {
                try
                {
                    // No order by - may be set from outside in LinqQuery!
                    var execQuery = await GetQuery().ToListAsync();

                    _instancesFromServer = execQuery.Cast<IDataObject>()
                            .Where(obj => obj.ObjectState != DataObjectState.Deleted) // Not interested in deleted objects TODO: Discuss if a query should return deleted objects
                            .Select(obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj))
                            .ToList();

                    UpdateFilteredInstances();
                }
                catch (Exception ex)
                {
                    errorReporter.Value.Show(ex);
                }
                finally
                {
                    ClearBusy();
                }
            });

            return _loadInstancesCoreTask;
        }

        /// <summary>
        /// Create a fresh <see cref="Instances"/> collection when something has changed.
        /// </summary>
        private void UpdateFilteredInstances()
        {
            var tmp = FilterList.AppendPostFilter(new List<DataObjectViewModel>(_instancesFromServer));

            // Sort
            if (!string.IsNullOrEmpty(orderByExpression))
            {
                _filteredInstances =
                    new List<DataObjectViewModel>(
                        tmp.Select(vm => vm.Object)                            // Back to a plain list of IDataObjects
                           .AsQueryable(this.InterfaceType.Type)               // To a typed List
                           .OrderBy(string.Format("{0} {1}",                // Sorting CompoundObject does not work
                                        orderByExpression,                         // Maybe we should implement a custom comparer
                                        sortDirection == System.ComponentModel.ListSortDirection.Descending ? "desc" : string.Empty))
                           .Cast<IDataObject>()
                           .Select(obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj))
                    );
            }
            else
            {
                _filteredInstances = new List<DataObjectViewModel>(tmp);
            }

            InvalidateInstances();

            if (SelectFirstOnLoad && SelectedItem == null)
            {
                this.SelectedItem = _filteredInstances.FirstOrDefault();
            }
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

        #region SavedListConfigurations
        private SavedListConfiguratorViewModel _savedListConfigurations;
        public SavedListConfiguratorViewModel SavedListConfigurations
        {
            get
            {
                if (_savedListConfigurations == null)
                {
                    _savedListConfigurations = ViewModelFactory.CreateViewModel<SavedListConfiguratorViewModel.Factory>().Invoke(DataContext, this);
                }
                return _savedListConfigurations;
            }
        }
        #endregion
    }
}
