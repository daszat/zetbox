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
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.FilterViewModels;
    using Zetbox.Client.Presentables.ValueViewModels;
    using ObjectEditor = Zetbox.Client.Presentables.ObjectEditor;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.API.Async;

    /// <summary>
    /// Models the specialities of <see cref="DataType"/>s.
    /// </summary>
    [ViewModelDescriptor]
    public partial class InstanceListViewModel
        : ViewModel, ILabeledViewModel, ISortableViewModel
    {
        public new delegate InstanceListViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Func<IZetboxContext> workingCtxFactory, ObjectClass type, Func<IQueryable> qry);

        protected readonly Func<IZetboxContext> workingCtxFactory;
        protected readonly IFileOpener fileOpener;
        protected readonly ITempFileService tmpService;

        protected bool isEmbedded()
        {
            return workingCtxFactory == null;
        }

        /// <summary>
        /// Initializes a new instance of the DataTypeViewModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="config"></param>
        /// <param name="fileOpener"></param>
        /// <param name="tmpService"></param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">Parent ViewModel</param>
        /// <param name="workingCtxFactory">A factory for creating a working context. If the InstanceList is embedded in a workspace which the user has to submit manually, the factory should return the same context as passed in the dataCtx parameter.</param>
        /// <param name="type">the data type to model. If null, qry must be a Query of a valid DataType</param>
        /// <param name="qry">optional: the query to display. If null, Query will be constructed from type</param>
        public InstanceListViewModel(
            IViewModelDependencies appCtx,
            ZetboxConfig config,
            IFileOpener fileOpener,
            ITempFileService tmpService,
            IZetboxContext dataCtx, ViewModel parent,
            Func<IZetboxContext> workingCtxFactory,
            ObjectClass type,
            Func<IQueryable> qry)
            : base(appCtx, dataCtx, parent)
        {
            if (dataCtx == null) throw new ArgumentNullException("dataCtx");
            if (type == null) throw new ArgumentNullException("type");
            if (fileOpener == null) throw new ArgumentNullException("fileOpener");
            if (tmpService == null) throw new ArgumentNullException("tmpService");
            this.fileOpener = fileOpener;
            this.tmpService = tmpService;

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

            dataCtx.IsElevatedModeChanged += new EventHandler(dataCtx_IsElevatedModeChanged);
        }

        void dataCtx_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("AllowAddNew");
            OnPropertyChanged("AllowDelete");
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
                    return new ZbTask<ReadOnlyCollection<DataObjectViewModel>>(LoadInstancesCore())
                        .OnResult(t => t.Result = _filteredInstances.AsReadOnly());
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
                if (!string.IsNullOrEmpty(InstancesCountAsTextFormatString))
                    return string.Format(InstancesCountAsTextFormatString, InstancesCount);
                else
                    return string.Format("{0} {1}", InstancesCount, InstanceListViewModelResources.InstancesCountAsText);
            }
        }

        public bool InstancesCountWarning
        {
            get
            {
                return InstancesCount >= Helper.MAXLISTCOUNT;
            }
        }

        public string InstancesCountWarningText
        {
            get
            {
                return InstancesCount >= Helper.MAXLISTCOUNT ? InstanceListViewModelResources.InstancesCountWarning : string.Empty;
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
        public void OpenObjects(IEnumerable<DataObjectViewModel> objects)
        {
            if (objects == null) throw new ArgumentNullException("objects");
            if (isEmbedded())
            {
                foreach (var item in objects)
                {
                    if (this.DataType.IsSimpleObject)
                    {
                        var dlg = ViewModelFactory.CreateViewModel<SimpleDataObjectEditorTaskViewModel.Factory>().Invoke(DataContext, this, item);
                        ViewModelFactory.ShowDialog(dlg);
                    }
                    else
                    {
                        ViewModelFactory.ShowModel(item, true);
                    }
                }
                OnItemsOpened(ViewModelFactory.GetWorkspace(DataContext) as ObjectEditor.WorkspaceViewModel, objects);
            }
            else
            {
                var workingCtx = workingCtxFactory == null ? DataContext : workingCtxFactory();
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(workingCtx, null);
                ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);

                ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
                {
                    var openedItems = objects.Select(o => newWorkspace.ShowForeignModel(o, RequestedEditorKind)).ToList();

                    OnItemsOpened(newWorkspace, openedItems);

                    newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
                }).Trigger();
            }
        }

        public delegate void ItemsOpenedHandler(ObjectEditor.WorkspaceViewModel sender, IEnumerable<DataObjectViewModel> objects);

        /// <summary>
        /// Is triggered when items are opened in a new workspace. This can be used to give the ViewModels a context dependent finishing touch, like opening a non-default view
        /// </summary>
        public event ItemsOpenedHandler ItemsOpened = null;

        protected virtual void OnItemsOpened(ObjectEditor.WorkspaceViewModel sender, IEnumerable<DataObjectViewModel> objects)
        {
            var temp = ItemsOpened;
            if (temp != null)
            {
                temp(sender, objects);
            }
        }

        public delegate void ItemsDefaultActionHandler(InstanceListViewModel sender, IEnumerable<DataObjectViewModel> objects);
        public event ItemsDefaultActionHandler ItemsDefaultAction = null;

        public void ExecItemsDefaultAction(IEnumerable<DataObjectViewModel> objects)
        {
            ItemsDefaultActionHandler temp = ItemsDefaultAction;
            if (temp != null)
            {
                temp(this, objects);
            }
            else
            {
                OpenObjects(objects);
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

        private bool _ShowExportCommand = true;
        public bool ShowExportCommand
        {
            get
            {
                return _ShowExportCommand;
            }
            set
            {
                if (_ShowExportCommand != value)
                {
                    _ShowExportCommand = value;
                    UpdateCommands();
                }
            }
        }

        private bool _isInlineEditable = false;
        /// <summary>
        /// If true, all Items are editable in the list directly. Default is false
        /// </summary>
        /// <remarks>
        /// This does not affect the details pane. 
        /// </remarks>
        public bool IsInlineEditable
        {
            get
            {
                return _isInlineEditable;
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
                if (_type != null)
                    return IconConverter.ToImage(_type.DefaultIcon);
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
            var result = FilterList.AppendFilter(_query());

            if (!string.IsNullOrEmpty(_sortProperty))
            {
                result = result.OrderBy(string.Format("it.{0} {1}",                // Sorting CompundObjects does not work
                                _sortProperty,                         // Maybe we should implement a custom comparer
                                _sortDirection == System.ComponentModel.ListSortDirection.Descending ? "desc" : string.Empty));
            }

            // Limit to maxlistcount
            // Due to the fact, that the client provider is appending 
            // local objects to the query we have to ad an additional limit
            result = result.Take(Zetbox.API.Helper.MAXLISTCOUNT);

            return result;
        }

        public IQueryable GetTypedQuery<T>() where T : class, IDataObject
        {
            return DataContext.GetQuery<T>(); // ToList would make all filter client side filter .ToList().OrderBy(obj => obj.ToString()).AsQueryable();
        }

        public bool CanExecReloadInstances()
        {
            return !FilterList.RequiredFilterMissing;
        }

        public string CanExecReloadInstancesReason()
        {
            return FilterListEntryViewModelResources.RequiredFilterMissingReason;
        }

        /// <summary>
        /// Reload instances from context.
        /// </summary>
        public void ReloadInstances()
        {
            if (!CanExecReloadInstances())
                return;

            try
            {
                if (_loadInstancesCoreTask != null)
                    _loadInstancesCoreTask.Wait();
            }
            finally
            {
                _loadInstancesCoreTask = null;
            }
            LoadInstancesCore();
        }

        ZbTask _loadInstancesCoreTask;
        private ZbTask LoadInstancesCore()
        {
            if (_loadInstancesCoreTask != null || !CanExecReloadInstances() || IsInInit) return _loadInstancesCoreTask;

            SetBusy();
            var execQueryTask = GetQuery().ToListAsync(); // No order by - may be set from outside in LinqQuery! .Cast<IDataObject>().ToList().OrderBy(obj => obj.ToString()))
            _loadInstancesCoreTask = new ZbTask(execQueryTask);
            _loadInstancesCoreTask.OnResult(t =>
                {
                    _instancesFromServer = execQueryTask.Result.Cast<IDataObject>()
                        .Where(obj => obj.ObjectState != DataObjectState.Deleted) // Not interested in deleted objects TODO: Discuss if a query should return deleted objects
                        .Select(obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj))
                        .ToList();

                    UpdateFilteredInstances();
                    ClearBusy();
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
            if (!string.IsNullOrEmpty(_sortProperty))
            {
                _filteredInstances =
                    new List<DataObjectViewModel>(
                        tmp.Select(vm => vm.Object)                            // Back to a plain list of IDataObjects
                           .AsQueryable(this.InterfaceType.Type)               // To a typed List
                           .OrderBy(string.Format("it.{0} {1}",                // Sorting CompundObjects does not work
                                        _sortProperty,                         // Maybe we should implement a custom comparer
                                        _sortDirection == System.ComponentModel.ListSortDirection.Descending ? "desc" : string.Empty))
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
                    _savedListConfigurations.PropertyChanged += _savedListConfigurations_PropertyChanged;
                }
                return _savedListConfigurations;
            }
        }

        void _savedListConfigurations_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedItem":
                    ReloadInstances();
                    break;
            }
        }
        #endregion
    }
}
