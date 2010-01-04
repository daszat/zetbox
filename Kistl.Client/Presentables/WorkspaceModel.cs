using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Client;
using System.ComponentModel;

namespace Kistl.Client.Presentables
{
    public class WorkspaceModel
        : PresentableModel
    {
        public WorkspaceModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            RecentObjects = new ObservableCollection<PresentableModel>();
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
        /// A list of "active" <see cref="IDataObject"/>s
        /// </summary>
        public ObservableCollection<PresentableModel> RecentObjects { get; private set; }

        private PresentableModel _selectedItem;
        /// <summary>
        /// The last selected PresentableModel.
        /// </summary>
        public PresentableModel SelectedItem
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
        public void HistoryTouch(PresentableModel mdl)
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

        #endregion

        /// <summary>
        /// Show a foreign model by finding and creating the equivalent model on the local DataContext.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public void ShowForeignModel(DataObjectModel dataObject)
        {
            if (dataObject == null || dataObject.Object == null)
                return;
            
            var other = dataObject.Object;
            var here = DataContext.Find(other.GetInterfaceType(), other.ID);
            SelectedItem = AppContext.Factory.CreateDefaultModel(DataContext, here);
            HistoryTouch(SelectedItem);
        }

        public override string Name
        {
            get { return "Workspace"; }
        }
    }

    /// <summary>
    /// This command submits all outstanding changes of this Workspace to the data store.
    /// The parameter has to be <value>null</value>.
    /// </summary>
    internal class SaveContextCommand : CommandModel
    {
        public SaveContextCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx, "Save", "Saves outstanding changes to the data store.")
        {
        }

        private bool CheckValidity()
        {
            var errors = GetErrors();
            return true; // GetErrors().Count() == 0;
        }

        private IEnumerable<string> GetErrors()
        {
            return DataContext.AttachedObjects.Where(po => po.ObjectState != DataObjectState.Unmodified).OfType<IDataErrorInfo>().Select(o => o.Error).Where(s => !String.IsNullOrEmpty(s));
        }
        /// <summary>
        /// Returns true if no data was passed.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool CanExecute(object data)
        {
            return data == null && CheckValidity();
        }

        protected override void DoExecute(object data)
        {
            var errors = GetErrors().ToArray();
            if (errors.Length > 0)
            {
                // throw new InvalidOperationException("Cannot save due to the following errors: " + String.Join("\n", errors));
            }
            DataContext.SubmitChanges();
        }

    }

    /// <summary>
    /// Creates a new instance of an <see cref="ObjectClass"/> and makes it the currently selected instance.
    /// </summary>
    internal class CreateNewInstanceCommand : CommandModel
    {
        public CreateNewInstanceCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx, WorkspaceModel parent)
            : base(appCtx, dataCtx, "New", "Create a new instance")
        {
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
                _parent.SelectedItem = newModel;
            }
        }
    }

    /// <summary>
    /// Creates a new instance of an <see cref="ObjectClass"/> and opens it in a new WorkspaceView.
    /// </summary>
    internal class CreateNewInstanceExternallyCommand : CommandModel
    {
        public CreateNewInstanceExternallyCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx, "External New ...", "Create a new instance of this object class in a new window")
        {
        }

        public override bool CanExecute(object data)
        {
            return data != null
                && data is ObjectClassModel;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                var externalCtx = KistlContext.GetContext();
                var objectClass = data as ObjectClassModel;
                
                // responsibility to externalCtx's disposal passes to newWorkspace
                var newWorkspace = Factory.CreateSpecificModel<WorkspaceModel>(externalCtx);
                var newObject = externalCtx.Create(objectClass.GetDescribedInterfaceType());
                var newModel = (DataObjectModel)Factory.CreateDefaultModel(externalCtx, newObject);

                newWorkspace.HistoryTouch(newModel);
                newWorkspace.SelectedItem = newModel;
                Factory.ShowModel(newWorkspace, true);
            }
        }
    }
}
