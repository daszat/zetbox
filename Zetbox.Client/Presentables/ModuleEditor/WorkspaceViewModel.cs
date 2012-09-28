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

namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ZetboxBase;
    using ObjectEditorWorkspace = Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel;

    public class WorkspaceViewModel : WindowViewModel
    {
        public new delegate WorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        protected readonly Func<ClientIsolationLevel, IZetboxContext> ctxFactory;

        public WorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Func<ClientIsolationLevel, IZetboxContext> ctxFactory)
            : base(appCtx, ctxFactory(ClientIsolationLevel.MergeServerData), parent) // Use another data context, this workspace does not edit anything
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            this.ctxFactory = ctxFactory;
        }

        private Module _CurrentModule;
        public Module CurrentModule
        {
            get
            {
                EnsureCurrentModule();
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

        private void EnsureCurrentModule()
        {
            if (_CurrentModule == null)
            {
                _CurrentModule = DataContext.GetQuery<Module>().FirstOrDefault();
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
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(ObjectClass).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ObjectClass>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Interface
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(Interface).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Interface>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Enums
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(Enumeration).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Enumeration>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // CompoundObject
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(CompoundObject).GetObjectClass(FrozenContext), () => DataContext.GetQuery<CompoundObject>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Assembly
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(Assembly).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Assembly>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.Commands.Add(ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<DataObjectViewModel>.Factory>().Invoke(DataContext,
                        this,
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
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(TypeRef).GetObjectClass(FrozenContext), () => DataContext.GetQuery<TypeRef>().OrderBy(i => i.FullName));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Assembly.Module = @0", CurrentModule));
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Application
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(Application).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Application>().OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // NavigationScreens
                    var navScreenMdl = ViewModelFactory.CreateViewModel<NavigationScreenHierarchyViewModel.Factory>().Invoke(DataContext, this, CurrentModule);
                    lst.Add(navScreenMdl);

                    // ViewDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(ViewDescriptor).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ViewDescriptor>().OrderBy(i => i.ControlKind.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // ViewModelDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(ViewModelDescriptor).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ViewModelDescriptor>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // ServiceDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(ServiceDescriptor).GetObjectClass(FrozenContext), () => DataContext.GetQuery<ServiceDescriptor>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // ControlKinds
                    var ctrlKindMdl = ViewModelFactory.CreateViewModel<ControlKindHierarchyViewModel.Factory>().Invoke(DataContext, this, CurrentModule);
                    lst.Add(ctrlKindMdl);

                    // Icons
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(Icon).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Icon>().OrderBy(i => i.IconFile));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Relation
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(Relation).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Relation>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.EnableAutoFilter = false;
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    lst.Add(lstMdl);

                    // Sequences
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this, typeof(Sequence).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Sequence>().OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ConstantValueFilterModel("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

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
                    _NewModuleCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "New Module", "Creates a new Module", () => CreateNewModule(), null, null);
                    _NewModuleCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext);
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
                    _RefreshCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Refresh", "Refresh", () => Refresh(), null, null);
                    _RefreshCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext);
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
                    _EditCurrentModuleCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Edit Module", "Opens the Editor for the current module", () => EditCurrentModule(), null, null);
                    _EditCurrentModuleCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext);
                }
                return _EditCurrentModuleCommand;
            }
        }

        public void EditCurrentModule()
        {
            if (CurrentModule == null) return;
            var newCtx = ctxFactory(ClientIsolationLevel.PrefereClientData);
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx, null);

            newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, newWorkspace, CurrentModule));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }

        private ICommandViewModel _ReportProblemCommand = null;
        public ICommandViewModel ReportProblemCommand
        {
            get
            {
                if (_ReportProblemCommand == null)
                {
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext, this);
                    _ReportProblemCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.info_png.Find(FrozenContext);
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
            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx ,null);
            var newObj = newCtx.Create<Module>();

            newWorkspace.ShowForeignModel(DataObjectViewModel.Fetch(ViewModelFactory, newCtx, newWorkspace, newObj));
            ViewModelFactory.ShowModel(newWorkspace, true);
        }
    }
}
