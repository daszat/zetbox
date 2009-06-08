using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Client;

namespace Kistl.Client.Presentables
{
    public class WorkspaceModel : PresentableModel
    {
        public WorkspaceModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            RecentObjects = new ObservableCollection<DataObjectModel>();
        }

        #region Data

        /// <summary>
        /// A collection of all <see cref="Module"/>s, to display as entry 
        /// point into the object hierarchy
        /// </summary>
        public ObservableCollection<ModuleModel> Modules
        {
            get
            {
                if (_modulesCache == null)
                {
                    _modulesCache = new ObservableCollection<ModuleModel>();
                    LoadModules();
                }
                return _modulesCache;
            }
        }
        private ObservableCollection<ModuleModel> _modulesCache;

        /// <summary>
        /// A collection of all applications, to display as entry 
        /// points.
        /// </summary>
        public ObservableCollection<PresentableModel> Applications
        {
            get
            {
                if (_applicationsCache == null)
                {
                    _applicationsCache = new ObservableCollection<PresentableModel>();
                    LoadApplications();
                }
                return _applicationsCache;
            }
        }
        private ObservableCollection<PresentableModel> _applicationsCache;

        /// <summary>
        /// Gets a list of instances of the currently selected item
        /// </summary>
        public ObservableCollection<DataObjectModel> Instances { get; private set; }

        private string _instancesSearchString = String.Empty;
        public string InstancesSearchString
        {
            get
            {
                return _instancesSearchString;
            }
            set
            {
                if (_instancesSearchString != value)
                {
                    _instancesSearchString = value;
                    OnInstancesSearchStringChanged(_instancesSearchString);
                }
            }
        }

        public ReadOnlyObservableCollection<DataObjectModel> InstancesFiltered { get; private set; }

        /// <summary>
        /// A list of "active" <see cref="IDataObject"/>s
        /// </summary>
        public ObservableCollection<DataObjectModel> RecentObjects { get; private set; }

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
                    if (_selectedItem != null)
                        HistoryTouch(_selectedItem);
                    UpdateInstances();
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private DataObjectModel _selectedInstance;
        public DataObjectModel SelectedInstance
        {
            get
            {
                return _selectedInstance;
            }
            set
            {
                if (_selectedInstance != value)
                {
                    _selectedInstance = value;
                    if (_selectedInstance != null)
                        HistoryTouch(_selectedInstance);
                    UpdateInstances();
                    OnPropertyChanged("SelectedInstance");
                }
            }
        }

        #endregion

        #region Commands

        #region Save Context

        private SaveContextCommand _saveCommand;
        /// <summary>
        /// This command submits all outstanding changes of this Workspace to the data store.
        /// The parameter has to be <value>null</value>.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = Factory.CreateSpecificModel<SaveContextCommand>(DataContext);

                return _saveCommand;
            }
        }

        #endregion

        #region Create New Instance

        private CreateNewInstanceCommand _createNewInstanceCommand = null;
        /// <summary>
        /// Creates a new instance of an <see cref="ObjectClass"/> and makes it the currently selected instance.
        /// </summary>
        public ICommand CreateNewInstanceCommand
        {
            get
            {
                if (_createNewInstanceCommand == null)
                {
                    _createNewInstanceCommand = new CreateNewInstanceCommand(AppContext, DataContext, this);
                }
                return _createNewInstanceCommand;
            }
        }

        #endregion

        #region Create New Instance Externally

        private static CreateNewInstanceExternallyCommand _createNewInstanceExternallyCommand = null;
        /// <summary>
        /// Creates a new instance of an <see cref="ObjectClass"/> and makes it the currently selected instance.
        /// </summary>
        public ICommand CreateNewInstanceExternallyCommand
        {
            get
            {
                if (_createNewInstanceExternallyCommand == null)
                {
                    _createNewInstanceExternallyCommand = new CreateNewInstanceExternallyCommand(AppContext, DataContext);
                }
                return _createNewInstanceExternallyCommand;
            }
        }

        #endregion

        #region History Touch

        /// <summary>
        /// registers a user contact with the mdl in this <see cref="WorkspaceModel"/>'s history
        /// </summary>
        /// <param name="mdl"></param>
        public void HistoryTouch(DataObjectModel mdl)
        {
            // fetch old SelectedItem to reestablish selection after modifying RecentObjects
            var item = SelectedItem;
            if (!RecentObjects.Contains(mdl))
            {
                RecentObjects.Add(mdl);
            }
            // reestablish selection 
            SelectedItem = item;
        }

        #endregion

        #endregion

        #region Utilities and UI callbacks

        private void LoadModules()
        {
            var modules = DataContext.GetQuery<Module>().ToList();
            foreach (var m in modules)
            {
                Modules.Add((ModuleModel)Factory.CreateDefaultModel(DataContext, m));
            }
        }

        private void LoadApplications()
        {
            this.Applications.Add(new GUI.DashboardModel(AppContext, DataContext));
            this.Applications.Add(new TimeRecords.Dashboard(AppContext, DataContext));
        }

        private void UpdateInstances()
        {
            this.Instances = null;
            var dtm = this.SelectedItem as DataTypeModel;
            if (dtm != null)
            {
                this.Instances = dtm.Instances;
                if (!this.Instances.Contains(this.SelectedInstance))
                {
                    this.SelectedInstance = null;
                }
            }
            OnInstancesChanged();
        }

        private void OnInstancesChanged()
        {
            OnPropertyChanged("Instances");
            ExecuteFilter();
        }

        private void OnInstancesSearchStringChanged(string oldValue)
        {
            OnPropertyChanged("InstancesSearchString");
            ExecuteFilter();
        }

        private void ExecuteFilter()
        {
            if (this.Instances == null)
            {
                this.InstancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(new ObservableCollection<DataObjectModel>());
            }
            else if (InstancesSearchString == String.Empty)
            {
                this.InstancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(this.Instances);
            }
            else
            {
                // poor man's full text search
                this.InstancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(
                    new ObservableCollection<DataObjectModel>(
                        this.Instances.Where(
                            o => o.Name.Contains(this.InstancesSearchString)
                            || o.ID.ToString().Contains(this.InstancesSearchString))));
            }
            OnPropertyChanged("InstancesFiltered");
        }

        #endregion

        /// <summary>
        /// Show a foreign model by finding and creating the equivalent model on the local DataContext.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public void ShowForeignModel(DataObjectModel dataObject)
        {
            var other = dataObject.Object;
            if (other == null)
                return;

            var here = DataContext.Find(other.GetInterfaceType(), other.ID);
            SelectedInstance = (DataObjectModel)AppContext.Factory.CreateDefaultModel(DataContext, here);
            HistoryTouch(SelectedInstance);
        }
    }

    /// <summary>
    /// This command submits all outstanding changes of this Workspace to the data store.
    /// The parameter has to be <value>null</value>.
    /// </summary>
    internal class SaveContextCommand : CommandModel
    {
        public SaveContextCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            Label = "Save";
            ToolTip = "Commits outstanding changes to the data store.";
        }

        /// <summary>
        /// Returns true if no data was passed.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool CanExecute(object data)
        {
            return data == null;
        }

        protected override void DoExecute(object data)
        {
            DataContext.SubmitChanges();
        }

    }

    /// <summary>
    /// Creates a new instance of an <see cref="ObjectClass"/> and makes it the currently selected instance.
    /// </summary>
    internal class CreateNewInstanceCommand : CommandModel
    {
        public CreateNewInstanceCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx, WorkspaceModel parent)
            : base(appCtx, dataCtx)
        {
            Label = "New";
            _parent = parent;
        }

        private WorkspaceModel _parent;

        public override bool CanExecute(object data)
        {
            return DataContext != null
                && !DataContext.IsReadonly
                && _parent != null
                && data != null
                && data is ObjectClassModel;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                var objectClass = data as ObjectClassModel;
                var newObject = DataContext.Create(objectClass.GetDescribedInterfaceType());
                var newModel = (DataObjectModel)Factory.CreateDefaultModel(DataContext, newObject);
                _parent.HistoryTouch(newModel);
                _parent.SelectedInstance = newModel;
            }
        }
    }

    /// <summary>
    /// Creates a new instance of an <see cref="ObjectClass"/> and opens it in a new WorkspaceView.
    /// </summary>
    internal class CreateNewInstanceExternallyCommand : CommandModel
    {
        public CreateNewInstanceExternallyCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            Label = "External New ...";
        }

        private WorkspaceModel _parent;

        public override bool CanExecute(object data)
        {
            return data != null
                && data is ObjectClassModel;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                var objectClass = data as ObjectClassModel;
                var externalCtx = KistlContext.GetContext();
                var newWorkspace = new WorkspaceModel(AppContext, externalCtx);
                var newObject = externalCtx.Create(objectClass.GetDescribedInterfaceType());
                var newModel = (DataObjectModel)Factory.CreateDefaultModel(externalCtx, newObject);
                newWorkspace.HistoryTouch(newModel);
                newWorkspace.SelectedInstance = newModel;
                Factory.ShowModel(newWorkspace, true, false);
            }
        }
    }
}
