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
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(ObjectClass).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ObjectClass>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Interface
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(Interface).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Interface>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Enums
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(Enumeration).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Enumeration>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // CompoundObject
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(CompoundObject).GetObjectClass(FrozenContext), () => DataContext.GetQuery<CompoundObject>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Assembly
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(Assembly).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Assembly>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Commands.Add(ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<DataObjectViewModel>.Factory>().Invoke(DataContext,
                        "Refresh TypeRefs", "Refreshes the TypeRefs, ViewDescriptors and ViewModelDescriptors",
                        (i) =>
                        {
                            foreach (var mdl in i)
                            {
                                var ctx = ctxFactory(ClientIsolationLevel.PrefereClientData);
                                
                                var a = ctx.Find<Assembly>(mdl.ID);
                                var workspaceShown = a.RegenerateTypeRefs();

                                if (!workspaceShown)
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
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(TypeRef).GetObjectClass(FrozenContext), () => DataContext.GetQuery<TypeRef>().OrderBy(i => i.FullName));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Assembly.Module = @0", CurrentModule));
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Application
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(Application).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Application>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // NavigationScreens
                    var navScreenMdl = ViewModelFactory.CreateViewModel<NavigationScreenHierarchyViewModel.Factory>().Invoke(DataContext, CurrentModule);
                    lst.Add(navScreenMdl);

                    // ViewDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(ViewDescriptor).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ViewDescriptor>().OrderBy(i => i.ControlKind));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // ViewModelDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(ViewModelDescriptor).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ViewModelDescriptor>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // ServiceDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(ServiceDescriptor).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ServiceDescriptor>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // ControlKinds
                    var ctrlKindMdl = ViewModelFactory.CreateViewModel<ControlKindHierarchyViewModel.Factory>().Invoke(DataContext, CurrentModule);
                    lst.Add(ctrlKindMdl);

                    // Icons
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(Icon).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Icon>().OrderBy(i => i.IconFile));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Relation
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(Relation).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Relation>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Filter.Add(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Sequences
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, typeof(Sequence).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Sequence>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.Filter.Add(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);


                    var diagMdl = ViewModelFactory.CreateViewModel<DiagramViewModel.Factory>().Invoke(DataContext, CurrentModule);
                    lst.Add(diagMdl);
                    _TreeItems = new ReadOnlyObservableCollection<ViewModel>(lst);
                }
                return _TreeItems;
            }
        }

        private static void SetupViewModel(InstanceListViewModel lstMdl)
        {
            lstMdl.AllowAddNew = true;
            lstMdl.AllowDelete = true;
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

            newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, CurrentModule));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }

        private ICommandViewModel _ReportProblemCommand = null;
        public ICommandViewModel ReportProblemCommand
        {
            get
            {
                if (_ReportProblemCommand == null)
                {
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext);
                }
                return _ReportProblemCommand;
            }
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

            newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, newObj));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }
    }
}
