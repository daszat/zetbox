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
using Kistl.Client.Presentables.KistlBase;

namespace Kistl.Client.Presentables.ObjectBrowser
{
    public class WorkspaceViewModel
        : WindowViewModel
    {
        public WorkspaceViewModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
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
        public ObservableCollection<ApplicationViewModel> Applications
        {
            get
            {
                if (_applicationsCache == null)
                {
                    _applicationsCache = new ObservableCollection<ApplicationViewModel>();
                    LoadApplications();
                }
                return _applicationsCache;
            }
        }
        private ObservableCollection<ApplicationViewModel> _applicationsCache;

        private ViewModel _selectedItem;
        /// <summary>
        /// The last selected ViewModel.
        /// </summary>
        public ViewModel SelectedItem
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
            this.Applications.Add(new ApplicationViewModel(AppContext, DataContext, "GUI", typeof(GUI.DashboardModel)));
            this.Applications.Add(new ApplicationViewModel(AppContext, DataContext, "TimeRecords", typeof(TimeRecords.Dashboard)));
            this.Applications.Add(new ApplicationViewModel(AppContext, DataContext, "Module Editor", typeof(ModuleEditor.WorkspaceViewModel)));
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
        }

        public override string Name
        {
            get { return "Workspace"; }
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
                var newWorkspace = Factory.CreateSpecificModel<ObjectEditor.WorkspaceViewModel>(externalCtx);
                var newObject = externalCtx.Create(objectClass.GetDescribedInterfaceType());
                var newModel = (DataObjectModel)Factory.CreateDefaultModel(externalCtx, newObject);

                newWorkspace.SelectedItem = newModel;
                Factory.ShowModel(newWorkspace, true);
            }
        }
    }
}
