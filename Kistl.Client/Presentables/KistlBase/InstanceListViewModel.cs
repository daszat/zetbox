
namespace Kistl.Client.Presentables.KistlBase
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

    /// <summary>
    /// Models the specialities of <see cref="DataType"/>s.
    /// </summary>
    [ViewModelDescriptor]
    public class InstanceListViewModel
        : ViewModel, ILabeledViewModel, IRefreshCommandListener, ISortableViewModel
    {
        public new delegate InstanceListViewModel Factory(IKistlContext dataCtx, ViewModel parent, Func<IKistlContext> workingCtxFactory, ObjectClass type, Func<IQueryable> qry);

        protected readonly Func<IKistlContext> workingCtxFactory;

        /// <summary>
        /// Initializes a new instance of the DataTypeViewModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="config"></param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">Parent ViewModel</param>
        /// <param name="workingCtxFactory">A factory for creating a working context. If the InstanceList is embedded in a workspace which the user has to submit manually, the factory should return the same context as passed in the dataCtx parameter.</param>
        /// <param name="type">the data type to model. If null, qry must be a Query of a valid DataType</param>
        /// <param name="qry">optional: the query to display. If null, Query will be constructed from type</param>
        public InstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig config,
            IKistlContext dataCtx, ViewModel parent,
            Func<IKistlContext> workingCtxFactory,
            ObjectClass type,
            Func<IQueryable> qry)
            : base(appCtx, dataCtx, parent)
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
            this._filterList = ViewModelFactory.CreateViewModel<FilterListViewModel.Factory>().Invoke(DataContext, this, _type);
            this._filterList.ExecuteFilter += (s, e) => ReloadInstances();
            this._filterList.ExecutePostFilter += (s, e) => ExecutePostFilter();
            this._filterList.PropertyChanged += _filterList_PropertyChanged;
            this._filterList.UserFilterAdded += _filterList_UserFilterAdded;
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

        #region Filter
        private FilterListViewModel _filterList;
        public FilterListViewModel FilterList
        {
            get
            {
                return _filterList;
            }
        }

        void _filterList_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "EnableAutoFilter":
                case "RespectRequiredFilter":
                case "ShowFilter":
                case "Filter":
                case "FilterViewModels":
                    OnPropertyChanged(e.PropertyName);
                    break;
            }
        }

        void _filterList_UserFilterAdded(object sender, UserFilterAddedEventArgs e)
        {
            if (DisplayedProperties.Any(dp => dp.SequenceEqual(e.Properties))) return;
            DisplayedColumns.Columns.Add(GridDisplayConfiguration.CreateColumnDisplayModel(GridDisplayConfiguration.Mode.ReadOnly, e.Properties.ToArray()));
        }

        /// <summary>
        /// Enables the auto filter feature. This is the default. Setting this property will cause the filter collection to be cleared.
        /// </summary>
        public bool EnableAutoFilter
        {
            get
            {
                return _filterList.EnableAutoFilter;
            }
            set
            {
                _filterList.EnableAutoFilter = value;
            }
        }

        /// <summary>
        /// If set to false, no filter is required. Default value is true. Use this setting if a small, preselected list (query) is provides as data source.
        /// </summary>
        public bool RespectRequiredFilter
        {
            get
            {
                return _filterList.RespectRequiredFilter;
            }
            set
            {
                _filterList.RespectRequiredFilter = value;
            }
        }

        public bool ShowFilter
        {
            get
            {
                return _filterList.ShowFilter;
            }
            set
            {
                _filterList.ShowFilter = value;
            }
        }

        /// <summary>
        /// Allow the user to modify the filter collection
        /// </summary>
        public bool AllowUserFilter
        {
            get
            {
                return _filterList.AllowUserFilter;
            }
            set
            {
                _filterList.AllowUserFilter = value;
            }
        }

        public ReadOnlyObservableCollection<IFilterModel> Filter
        {
            get
            {
                return _filterList.Filter;
            }
        }

        public ReadOnlyObservableCollection<FilterViewModel> FilterViewModels
        {
            get
            {
                return _filterList.FilterViewModels;
            }
        }

        public void AddFilter(IFilterModel mdl)
        {
            _filterList.AddFilter(mdl);
        }

        public void RemoveFilter(IFilterModel mdl)
        {
            _filterList.RemoveFilter(mdl);
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

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var result = base.CreateCommands();

            if (AllowAddNew) result.Add(NewCommand);
            if (ShowOpenCommand) result.Add(OpenCommand);
            if (ShowRefreshCommand) result.Add(RefreshCommand);
            if (AllowDelete) result.Add(DeleteCommand);

            if (ShowExportCommand) result.Add(ExportContainerCommand);

            return result;
        }

        private void UpdateCommands()
        {
            if (commandsStore == null) return;
            if (!AllowAddNew && commandsStore.Contains(NewCommand)) commandsStore.Remove(NewCommand);
            if (!ShowOpenCommand && commandsStore.Contains(OpenCommand)) commandsStore.Remove(OpenCommand);
            if (!ShowRefreshCommand && commandsStore.Contains(RefreshCommand)) commandsStore.Remove(RefreshCommand);
            if (!AllowDelete && commandsStore.Contains(DeleteCommand)) commandsStore.Remove(DeleteCommand);
            if (!ShowExportCommand && commandsStore.Contains(ExportContainerCommand)) commandsStore.Remove(ExportContainerCommand);

            if (AllowAddNew && !commandsStore.Contains(NewCommand)) commandsStore.Insert(0, NewCommand);
            if (ShowOpenCommand && !commandsStore.Contains(OpenCommand)) commandsStore.Insert(AllowAddNew ? 1 : 0, OpenCommand);
            if (ShowRefreshCommand && !commandsStore.Contains(RefreshCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0), RefreshCommand);
            if (AllowDelete && !commandsStore.Contains(DeleteCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0) + (ShowRefreshCommand ? 1 : 0), DeleteCommand);
            if (ShowExportCommand && !commandsStore.Contains(ExportContainerCommand)) commandsStore.Insert((AllowAddNew ? 1 : 0) + (ShowOpenCommand ? 1 : 0) + (ShowRefreshCommand ? 1 : 0) + (AllowDelete ? 1 : 0), ExportContainerCommand);
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
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this, this);
                    _RefreshCommand.Icon = Kistl.NamedObjects.Gui.Icons.KistlBase.reload_png.Find(FrozenContext);
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
                        DataContext,
                        this,
                        CommonCommandsResources.OpenDataObjectCommand_Name,
                        CommonCommandsResources.OpenDataObjectCommand_Tooltip,
                        OpenObjects);
                    _OpenCommand.Icon = Kistl.NamedObjects.Gui.Icons.KistlBase.fileopen_png.Find(FrozenContext);
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
                        DataContext, this, CommonCommandsResources.NewDataObjectCommand_Name, CommonCommandsResources.NewDataObjectCommand_Tooltip, NewObject, () => AllowAddNew, null);
                    _NewCommand.Icon = Kistl.NamedObjects.Gui.Icons.KistlBase.new_png.Find(FrozenContext);
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
                // TODO: Reorganize this control - it's too complex
                var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
                if (_instancesCache == null) _instancesCache = new List<DataObjectViewModel>();
                _instancesCache.Add(mdl);
                _instances = new ObservableCollection<DataObjectViewModel>(_instancesCache);

                _proxyCache.Clear();
                _proxyInstances = null;

                OnPropertyChanged("ProxyInstances");
                OnPropertyChanged("Instances");
                OnPropertyChanged("InstancesCount");
                OnPropertyChanged("InstancesCountAsText");
                OnPropertyChanged("InstancesCountWarning");
                OnPropertyChanged("InstancesCountWarningText");

                this.SelectedItem = mdl;

                if (this.DataType.IsSimpleObject && !IsEditable)
                {
                    // Open in a Dialog
                    var dlg = ViewModelFactory.CreateViewModel<SimpleDataObjectEditorTaskViewModel.Factory>().Invoke(DataContext, this, mdl);
                    ViewModelFactory.ShowDialog(dlg);
                }
                else if (!this.DataType.IsSimpleObject)
                {
                    ViewModelFactory.ShowModel(mdl, true);
                }
                // Don't open simple objects
            }
            else
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(workingCtx, null);
                newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, workingCtx, newWorkspace, obj), RequestedEditorKind);
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
                        this,
                        CommonCommandsResources.DeleteDataObjectCommand_Name,
                        CommonCommandsResources.DeleteDataObjectCommand_Tooltip,
                        DeleteObjects);
                    _DeleteCommand.Icon = Kistl.NamedObjects.Gui.Icons.KistlBase.delete_png.Find(FrozenContext);
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
                }
            }
        }

        private ICommandViewModel _SelectColumnsCommand = null;
        public ICommandViewModel SelectColumnsCommand
        {
            get
            {
                if (_SelectColumnsCommand == null)
                {
                    _SelectColumnsCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext,
                        null, // Hide Ardorner
                        InstanceListViewModelResources.SelectColumnsCommand,
                        InstanceListViewModelResources.SelectColumnsCommand_Tooltip,
                        SelectColumns,
                        () => AllowSelectColumns,
                        null);
                }
                return _SelectColumnsCommand;
            }
        }

        public void SelectColumns()
        {
            var dlg = ViewModelFactory.CreateViewModel<PropertySelectionTaskViewModel.Factory>()
                .Invoke(DataContext,
                    this,
                    _type,
                    props => { });
            dlg.FollowRelationsOne = true;
            dlg.MultiSelect = true;
            dlg.UpdateInitialSelectedProperties(this.DisplayedProperties);
            dlg.SelectedPropertySelectionChanged += (s, e) =>
            {
                if (e.Item.IsSelected)
                {
                    DisplayedColumns.Columns.Add(GridDisplayConfiguration.CreateColumnDisplayModel(GridDisplayConfiguration.Mode.ReadOnly, e.Item.Properties));
                    ViewMethod = InstanceListViewMethod.Details;
                }
                else
                {
                    var col = DisplayedColumns.Columns.FirstOrDefault(c => c.Property == e.Item.Property);
                    if (col != null) DisplayedColumns.Columns.Remove(col);
                }
            };
            ViewModelFactory.ShowDialog(dlg);
        }
        #endregion

        #region Print/Export
        private ICommandViewModel _PrintCommand = null;
        public ICommandViewModel PrintCommand
        {
            get
            {
                if (_PrintCommand == null)
                {
                    _PrintCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.PrintCommand,
                        InstanceListViewModelResources.PrintCommand_Tooltip,
                        Print, null, null);
                    _PrintCommand.Icon = Kistl.NamedObjects.Gui.Icons.KistlBase.Printer_png.Find(FrozenContext);
                }
                return _PrintCommand;
            }
        }

        public void Print()
        {
            var doc = new MigraDoc.DocumentObjectModel.Document();
            var s = doc.AddSection();
            s.PageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Landscape;
            s.PageSetup.PageFormat = MigraDoc.DocumentObjectModel.PageFormat.A4;
            s.PageSetup.TopMargin = "2cm";
            s.PageSetup.BottomMargin = "2cm";
            s.PageSetup.LeftMargin = "2cm";
            s.PageSetup.RightMargin = "3cm";
            var tbl = s.AddTable();
            tbl.Borders.Visible = true;

            // Footer
            var p = s.Footers.Primary.AddParagraph();
            p.Format.Font.Size = 10;
            p.Format.AddTabStop("245mm", MigraDoc.DocumentObjectModel.TabAlignment.Right);

            p.AddText(DateTime.Today.ToShortDateString());
            p.AddTab();
            p.AddPageField();
            p.AddText("/");
            p.AddNumPagesField();

            var cols = DisplayedColumns.Columns
                .Where(i => i.Type != ColumnDisplayModel.ColumnType.MethodModel)
                .ToList();

            // TODO: Calc width more sophisticated
            var width = new MigraDoc.DocumentObjectModel.Unit(250.0 * (1.0 / (double)cols.Count), MigraDoc.DocumentObjectModel.UnitType.Millimeter);

            // Header
            for (int colIdx = 0; colIdx < cols.Count; colIdx++)
            {
                //var col = cols[colIdx];
                tbl.AddColumn(width);
            }

            var row = tbl.AddRow();
            row.HeadingFormat = true;
            for (int colIdx = 0; colIdx < cols.Count; colIdx++)
            {
                var col = cols[colIdx];
                p = row.Cells[colIdx].AddParagraph(col.Header ?? string.Empty);
                p.Format.Font.Bold = true;
            }


            // Data
            foreach (var obj in Instances)
            {
                row = tbl.AddRow();
                for (int colIdx = 0; colIdx < cols.Count; colIdx++)
                {
                    string val = cols[colIdx].ExtractFormattedValue(obj);
                    p = row.Cells[colIdx].AddParagraph(val ?? string.Empty);
                }
            }

            var filename = CreateTempFile("Export.pdf");

            var pdf = new MigraDoc.Rendering.PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.None);
            pdf.Document = doc;
            pdf.RenderDocument();
            pdf.Save(filename);

            new FileInfo(filename).ShellExecute();
        }

        private ICommandViewModel _ExportCommand = null;
        public ICommandViewModel ExportCommand
        {
            get
            {
                if (_ExportCommand == null)
                {
                    _ExportCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.ExportCSVCommand,
                        InstanceListViewModelResources.ExportCSVCommand_Tooltip,
                        Export, null, null);
                }
                return _ExportCommand;
            }
        }

        public void Export()
        {
            var tmpFile = CreateTempFile("Export.csv");
            StreamWriter sw;
            // http://stackoverflow.com/questions/545666/how-to-translate-ms-windows-os-version-numbers-into-product-names-in-net
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major <= 5) // assuming Windox XP or lower
                sw = new StreamWriter(tmpFile, false, Encoding.Default);
            else
                sw = new StreamWriter(tmpFile, false, Encoding.UTF8);
            using (sw) // use this constructor to ensure BOM
            {
                var cols = DisplayedColumns.Columns
                    .Where(i => i.Type != ColumnDisplayModel.ColumnType.MethodModel)
                    .ToList();
                // Header
                sw.WriteLine(string.Join(";",
                    cols.Select(i => i.Header).ToArray()));

                // Data
                foreach (var obj in Instances)
                {
                    for (int colIdx = 0; colIdx < cols.Count; colIdx++)
                    {
                        string val = cols[colIdx].ExtractFormattedValue(obj);
                        if (val != null)
                        {
                            var needsQuoting = val.IndexOfAny(new[] { ';', '\n', '\r', '"' }) >= 0;
                            if (needsQuoting)
                            {
                                val = val.Replace("\"", "\"\"");
                                val = "\"" + val + "\"";
                            }
                            sw.Write(val);
                        }
                        if (colIdx < cols.Count - 1)
                        {
                            sw.Write(";");
                        }
                        else
                        {
                            sw.WriteLine();
                        }
                    }
                }
            }

            new FileInfo(tmpFile).ShellExecute();
        }

        private ICommandViewModel _ExportContainerCommand = null;
        public ICommandViewModel ExportContainerCommand
        {
            get
            {
                if (_ExportContainerCommand == null)
                {
                    _ExportContainerCommand = ViewModelFactory.CreateViewModel<ContainerCommand.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.ExportContainerCommand,
                        InstanceListViewModelResources.ExportContainerCommand_Tooltip,
                        ExportCommand, PrintCommand);
                }
                return _ExportContainerCommand;
            }
        }

        protected string CreateTempFile(string filename)
        {
            // TODO: Move that to a global helper and delete files on shutdown
            var tmp = Path.GetTempFileName();
            if (File.Exists(tmp)) File.Delete(tmp);
            Directory.CreateDirectory(tmp);
            return Path.Combine(tmp, filename);
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
                p.Object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
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

        public int InstancesCount
        {
            get
            {
                return _instances != null ? _instances.Count : 0;
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
        #endregion

        #region Sorting
        private string _sortProperty = null;
        private System.ComponentModel.ListSortDirection _sortDirection = System.ComponentModel.ListSortDirection.Ascending;
        public void Sort(string propName, System.ComponentModel.ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(propName)) throw new ArgumentNullException("propName");
            _sortProperty = propName;
            _sortDirection = direction;
            if (_instancesCache != null && _instancesCache.Count < Helper.MAXLISTCOUNT)
            {
                ExecutePostFilter();
            }
            else
            {
                LoadInstances();
            }
        }
        public void SetInitialSort(string propName)
        {
            SetInitialSort(propName, System.ComponentModel.ListSortDirection.Ascending);
        }
        public void SetInitialSort(string propName, System.ComponentModel.ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(propName)) throw new ArgumentNullException("propName");
            _sortProperty = propName;
            _sortDirection = direction;
        }

        public string SortProperty { get { return _sortProperty; } }
        public System.ComponentModel.ListSortDirection SortDirection { get { return _sortDirection; } }
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
            }
            else
            {
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(workingCtx, null);
                ViewModelFactory.ShowModel(newWorkspace, RequestedWorkspaceKind, true);

                var loader = ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
                {
                    var openedItems = objects.Select(o => newWorkspace.ShowForeignModel(o, RequestedEditorKind)).ToList();

                    OnItemsOpened(newWorkspace, openedItems);

                    newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
                    newWorkspace.IsBusy = false;
                });

                loader.Trigger();
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

        public void OnItemsDefaultAction(IEnumerable<DataObjectViewModel> objects)
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
        public override Kistl.App.GUI.Icon Icon
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
            var result = FilterList.AppendFilter(_query());

            if (!string.IsNullOrEmpty(_sortProperty))
            {
                result = result.OrderBy(string.Format("it.{0} {1}",                // Sorting CompundObjects does not work
                                _sortProperty,                         // Maybe we should implement a custom comparer
                                _sortDirection == System.ComponentModel.ListSortDirection.Descending ? "desc" : string.Empty));
            }

            return result;
        }

        public IQueryable GetTypedQuery<T>() where T : class, IDataObject
        {
            return DataContext.GetQuery<T>(); // ToList would make all filter client side filter .ToList().OrderBy(obj => obj.ToString()).AsQueryable();
        }

        private IDelayedTask _loadInstancesLoader;
        private bool _loadingInstances = false;
        /// <summary>
        /// Loads the instances of this DataType and adds them to the Instances collection
        /// </summary>
        private void LoadInstances()
        {
            // Prevent loading instances twice
            if (_loadingInstances) return;
            _loadingInstances = true;
            if (_loadInstancesLoader == null) _loadInstancesLoader = ViewModelFactory.CreateDelayedTask(this, () =>
            {
                try
                {
                    if (_loadingInstances)
                    {
                        _instancesCache = LoadInstancesCore();
                        OnInstancesChanged();
                    }
                }
                finally
                {
                    _loadingInstances = false;
                }
            });

            _loadInstancesLoader.Trigger();
        }

        private List<DataObjectViewModel> LoadInstancesCore()
        {
            // Can execute?
            if (FilterList.RespectRequiredFilter && FilterList.Filter.Count(f => !f.Enabled && f.Required) > 0)
            {
                // return previous _instancesCache or empty list
                return _instancesCache ?? new List<DataObjectViewModel>();
            }

            var result = new List<DataObjectViewModel>();

            foreach (IDataObject obj in GetQuery()) // No order by - may be set from outside in LinqQuery! .Cast<IDataObject>().ToList().OrderBy(obj => obj.ToString()))
            {
                // Not interested in deleted objects
                // TODO: Discuss if a query should return deleted objects
                if (obj.ObjectState == DataObjectState.Deleted) continue;

                var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
                result.Add(mdl);
            }

            return result;
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
            // TODO: remove this bad hack
            if (_instancesCache == null)
            {
                try
                {
                    _instancesCache = LoadInstancesCore();
                }
                finally
                {
                    _loadingInstances = false;
                }
            }

            var tmp = FilterList.AppendPostFilter(new List<DataObjectViewModel>(this.InstancesCache));

            // Sort
            if (!string.IsNullOrEmpty(_sortProperty))
            {
                _instances =
                    new ObservableCollection<DataObjectViewModel>(
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
                _instances = new ObservableCollection<DataObjectViewModel>(tmp);
            }

            // Can be changed in UI -> listen to that
            _instances.CollectionChanged += new NotifyCollectionChangedEventHandler(_instances_CollectionChanged);

            _proxyCache.Clear();
            _proxyInstances = null;

            OnPropertyChanged("ProxyInstances");
            OnPropertyChanged("Instances");
            OnPropertyChanged("InstancesCount");
            OnPropertyChanged("InstancesCountAsText");
            OnPropertyChanged("InstancesCountWarning");
            OnPropertyChanged("InstancesCountWarningText");

            if (SelectFirstOnLoad && SelectedItem == null)
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
