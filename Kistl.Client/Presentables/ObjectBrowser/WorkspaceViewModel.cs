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
        public new delegate WorkspaceViewModel Factory(IKistlContext dataCtx);

        public WorkspaceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
        }

        #region Data

        /// <summary>
        /// A collection of all <see cref="Module"/>s, to display as entry 
        /// point into the object hierarchy
        /// </summary>
        public ObservableCollection<ModuleViewModel> Modules
        {
            get
            {
                if (_modulesCache == null)
                {
                    _modulesCache = new ObservableCollection<ModuleViewModel>();
                    LoadModules();
                }
                return _modulesCache;
            }
        }
        private ObservableCollection<ModuleViewModel> _modulesCache;

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
                    foreach (var app in FrozenContext.GetQuery<Kistl.App.GUI.Application>())
                    {
                        _applicationsCache.Add(ViewModelFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(DataContext, app));
                    }
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
        public ICommandViewModel CreateNewInstanceExternallyCommand
        {
            get
            {
                if (_createNewInstanceExternallyCommand == null)
                {
                    _createNewInstanceExternallyCommand = ViewModelFactory.CreateViewModel<CreateNewInstanceExternallyCommand.Factory>().Invoke(DataContext);
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
                Modules.Add(ViewModelFactory.CreateViewModel<ModuleViewModel.Factory>(m).Invoke(DataContext, m));
            }
        }

        #endregion

        /// <summary>
        /// Show a foreign model by finding and creating the equivalent model on the local DataContext.
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public void ShowForeignModel(DataObjectViewModel dataObject)
        {
            if (dataObject == null || dataObject.Object == null)
                return;

            var other = dataObject.Object;
            var here = DataContext.Find(DataContext.GetInterfaceType(other), other.ID);
            SelectedItem = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, here);
        }

        public override string Name
        {
            get { return WorkspaceViewModelResources.Name; }
        }
    }



    /// <summary>
    /// Creates a new instance of an <see cref="ObjectClass"/> and opens it in a new WorkspaceView.
    /// </summary>
    internal class CreateNewInstanceExternallyCommand : CommandViewModel
    {
        public new delegate CreateNewInstanceExternallyCommand Factory(IKistlContext dataCtx);

        private readonly Func<IKistlContext> ctxFactory;

        public CreateNewInstanceExternallyCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, WorkspaceViewModelResources.CreateNewInstanceExternallyCommand_Name, WorkspaceViewModelResources.CreateNewInstanceExternallyCommand_Tooltip)
        {
            this.ctxFactory = ctxFactory;
        }

        public override bool CanExecute(object data)
        {
            return data != null
                && data is ObjectClassViewModel;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                var externalCtx = ctxFactory();
                var objectClass = data as ObjectClassViewModel;

                // responsibility to externalCtx's disposal passes to newWorkspace
                var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(externalCtx);
                var newObject = externalCtx.Create(objectClass.GetDescribedInterfaceType());
                var newModel = DataObjectViewModel.Fetch(ViewModelFactory, externalCtx, newObject);

                newWorkspace.SelectedItem = newModel;
                ViewModelFactory.ShowModel(newWorkspace, true);
            }
        }
    }
}
