using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Client.Presentables.KistlBase;
using Kistl.App.GUI;
using ObjectEditorWorkspace = Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel;
using Kistl.Client.Models;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public class WorkspaceViewModel : WindowViewModel
    {
        public new delegate WorkspaceViewModel Factory(IKistlContext dataCtx);

        protected readonly Func<IKistlContext> ctxFactory;

        public WorkspaceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
        {
            this.ctxFactory = ctxFactory;
            _CurrentModule = dataCtx.GetQuery<Module>().FirstOrDefault();
        }

        private Module _CurrentModule;
        public Module CurrentModule 
        {
            get 
            { 
                return _CurrentModule; 
            }
            set
            {
                if (_CurrentModule != value)
                {
                    _CurrentModule = value;
                    _TreeItems = null;
                    OnPropertyChanged("CurrentModule");
                    OnPropertyChanged("TreeItems");
                }
            }
        }

        private ObservableCollection<Module> _modules;
        public ObservableCollection<Module> ModuleList
        {
            get
            {
                if (_modules == null)
                {
                    _modules = new ObservableCollection<Module>();
                    DataContext.GetQuery<Module>().OrderBy(m => m.Name).ForEach(m => _modules.Add(m));
                }
                return _modules;
            }
        }


        public override string Name
        {
            get { return "Module Editor Workspace"; }
        }

        private ReadOnlyObservableCollection<ViewModel> _TreeItems = null;
        public ReadOnlyObservableCollection<ViewModel> TreeItems
        {
            get
            {
                if (_TreeItems == null)
                {
                    var lst = new ObservableCollection<ViewModel>();

                    InstanceListViewModel lstMdl;

                    // Object Classes
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ObjectClass>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    //lstMdl.Filter.Add(ViewModelFactory.CreateViewModel<EnableFilterExpression.Factory>().Invoke(DataContext, "Only Baseclasses", "BaseObjectClass = null"));
                    lst.Add(lstMdl);

                    // Interface
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Interface>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Enums
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Enumeration>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // CompoundObject
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<CompoundObject>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Assembly
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Assembly>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Commands.Add(ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<DataObjectViewModel>.Factory>().Invoke(DataContext,
                        "Refresh TypeRefs", "Refreshes the TypeRefs, ViewDescriptors and ViewModelDescriptors",
                        (i) =>
                        {
                            foreach (var mdl in i)
                            {
                                ((Assembly)mdl.Object).RegenerateTypeRefs();                                
                            }
                            DataContext.SubmitChanges();
                        }));
                    lst.Add(lstMdl);

                    // TypeRefs
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<TypeRef>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Assembly.Module = @0", CurrentModule));
                    // lstMdl.Filter.Add(ViewModelFactory.CreateViewModel<EnableFilterExpression.Factory>().Invoke(DataContext, "Only Deleted", "Deleted = true"));
                    lstMdl.Commands.Add(ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext));
                    lst.Add(lstMdl);

                    // Application
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Application>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // NavigationScreens
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<NavigationScreen>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // ViewDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ViewDescriptor>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // ViewModelDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ViewModelDescriptor>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // ControlKinds
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ControlKind>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Relation
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Relation>());
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    // lstMdl.Filter.Add(ViewModelFactory.CreateViewModel<ToStringFilterExpression.Factory>().Invoke(DataContext, "Name"));
                    lst.Add(lstMdl);

                    var diagMdl = ViewModelFactory.CreateViewModel<DiagramViewModel.Factory>().Invoke(DataContext, CurrentModule);
                    lst.Add(diagMdl);

                    _TreeItems = new ReadOnlyObservableCollection<ViewModel>(lst);
                }
                return _TreeItems;
            }
        }

        private ViewModel _selectedItem;
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

        private ICommandViewModel _NewModuleCommand = null;
        public ICommandViewModel NewModuleCommand
        {
            get
            {
                if (_NewModuleCommand == null)
                {
                    _NewModuleCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "New Module", "Creates a new Module", () => CreateNewModule(), null);
                }
                return _NewModuleCommand;
            }
        }

        private ICommandViewModel _RefreshCommand = null;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Refresh", "Refresh", () => Refresh(), null);
                }
                return _RefreshCommand;
            }
        }

        private ICommandViewModel _EditCurrentModuleCommand = null;
        public ICommandViewModel EditCurrentModuleCommand
        {
            get
            {
                if (_EditCurrentModuleCommand == null)
                {
                    _EditCurrentModuleCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Edit Module", "Opens the Editor for the current module", () => EditCurrentModule(), null);
                }
                return _EditCurrentModuleCommand;
            }
        }

        public void EditCurrentModule()
        {
            if (CurrentModule == null) return;
            var newCtx = ctxFactory();
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);

            newWorkspace.ShowForeignModel(ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(CurrentModule).Invoke(newCtx, CurrentModule));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }

        public void Refresh()
        {
            _modules = null;
            OnPropertyChanged("ModuleList");
            OnPropertyChanged("TreeItems");
        }

        public void CreateNewModule()
        {
            var newCtx = ctxFactory();
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            var newObj = newCtx.Create<Module>();

            newWorkspace.ShowForeignModel(ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(newObj).Invoke(newCtx, newObj));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }
    }
}
