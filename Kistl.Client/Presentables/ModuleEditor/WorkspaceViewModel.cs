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
using Kistl.API.Client;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public class WorkspaceViewModel : WindowViewModel
    {
        public new delegate WorkspaceViewModel Factory(IKistlContext dataCtx);

        protected readonly Func<ClientIsolationLevel, IKistlContext> ctxFactory;

        public WorkspaceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Func<ClientIsolationLevel, IKistlContext> ctxFactory)
            : base(appCtx, ctxFactory(ClientIsolationLevel.MergeServerData)) // Use another data context, this workspace does not edit anything
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            this.ctxFactory = ctxFactory;
        }

        private Module _CurrentModule;
        public Module CurrentModule 
        {
            get 
            {
                if (_CurrentModule == null)
                {
                    _CurrentModule = DataContext.GetQuery<Module>().FirstOrDefault();
                }
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
                                var ctx = ctxFactory(ClientIsolationLevel.PrefereClientData);
                                
                                var a = ctx.Find<Assembly>(mdl.ID);
                                a.RegenerateTypeRefs();

                                if (ctx.AttachedObjects.OfType<ViewModelDescriptor>().Count(d => d.ObjectState == DataObjectState.New) == 0
                                    && ctx.AttachedObjects.OfType<ViewDescriptor>().Count(d => d.ObjectState == DataObjectState.New) == 0)
                                {
                                    // Submit only if no new Descriptor has been created
                                    // when new Descriptor has been created a Workspace will 
                                    // show those Descriptors and the user has to submit them
                                    ctx.SubmitChanges();
                                }
                            }

                        }));
                    lst.Add(lstMdl);

                    // TypeRefs
                    lstMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<TypeRef>());
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Assembly.Module = @0", CurrentModule));
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
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
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
            var newCtx = ctxFactory(ClientIsolationLevel.PrefereClientData);
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
            var newCtx = ctxFactory(ClientIsolationLevel.PrefereClientData);
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            var newObj = newCtx.Create<Module>();

            newWorkspace.ShowForeignModel(ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(newObj).Invoke(newCtx, newObj));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }
    }
}
