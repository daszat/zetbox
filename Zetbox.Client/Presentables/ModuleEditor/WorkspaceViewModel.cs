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

    [ViewModelDescriptor]
    public class WorkspaceViewModel : WindowViewModel, IRefreshCommandListener, INewCommandParameter
    {
        public new delegate WorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public WorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, appCtx == null ? null : appCtx.Factory.CreateNewContext(ContextIsolationLevel.MergeQueryData), parent) // Use another data context, this workspace does not edit anything
        {
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
                    GroupingTreeItemViewModel grpMdl;

                    grpMdl = ViewModelFactory.CreateViewModel<GroupingTreeItemViewModel.Factory>().Invoke(DataContext, this, "Data model");
                    grpMdl.Icon = IconConverter.ToImage(NamedObjects.Base.Classes.Zetbox.App.Base.DataType.Find(FrozenContext).DefaultIcon);
                    lst.Add(grpMdl);

                    // Object Classes
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(ObjectClass).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<ObjectClass>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    lstMdl.AllowAddNew = false; // Remove default add new button
                    lstMdl.Commands.Insert(0, NewObjectClassCommand);
                    grpMdl.Children.Add(lstMdl);

                    // Interface
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Interface).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Interface>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // Enums
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Enumeration).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Enumeration>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // CompoundObject
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(CompoundObject).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<CompoundObject>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // Properties
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Property).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Property>().Where(i => i.Module == CurrentModule));
                    lstMdl.SetInitialSort("Name");
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // Assembly
                    var assemblyLstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Assembly).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Assembly>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Name));
                    SetupViewModel(assemblyLstMdl);
                    assemblyLstMdl.Commands.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext,
                        this,
                        "Refresh descriptors", "Refreshes the ViewDescriptors and ViewModelDescriptors",
                        () =>
                        {
                            foreach (var mdl in assemblyLstMdl.SelectedItems)
                            {
                                var scope = ViewModelFactory.CreateNewScope();
                                var ctx = scope.ViewModelFactory.CreateNewContext();

                                var a = ctx.Find<Assembly>(mdl.ID);
                                var workspaceShown = a.RegenerateTypeRefs();

                                if (!workspaceShown)
                                {
                                    // Submit only if no new Descriptor has been created
                                    // when new Descriptor has been created a Workspace will 
                                    // show those Descriptors and the user has to submit them
                                    ctx.SubmitChanges();
                                    scope.Dispose();
                                }
                                else
                                {
                                    // TODO: This will produce a scope leak!
                                }
                            }
                        },
                        () => assemblyLstMdl.SelectedItems.Count > 0,
                        () => "Nothing selected"));
                    lst.Add(assemblyLstMdl);

                    grpMdl = ViewModelFactory.CreateViewModel<GroupingTreeItemViewModel.Factory>().Invoke(DataContext, this, "Application & UI");
                    grpMdl.Icon = IconConverter.ToImage(NamedObjects.Base.Classes.Zetbox.App.GUI.Application.Find(FrozenContext).DefaultIcon);
                    lst.Add(grpMdl);

                    // Application
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Application).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Application>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // NavigationScreens
                    var navScreenMdl = ViewModelFactory.CreateViewModel<NavigationScreenHierarchyViewModel.Factory>().Invoke(DataContext, this, CurrentModule);
                    grpMdl.Children.Add(navScreenMdl);

                    // ViewDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(ViewDescriptor).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<ViewDescriptor>().Where(i => i.Module == CurrentModule).OrderBy(i => i.ControlKind.Name));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // ViewModelDescriptor
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(ViewModelDescriptor).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<ViewModelDescriptor>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // ControlKinds
                    var ctrlKindMdl = ViewModelFactory.CreateViewModel<ControlKindHierarchyViewModel.Factory>().Invoke(DataContext, this, CurrentModule);
                    grpMdl.Children.Add(ctrlKindMdl);

                    // Icons
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemIconInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Icon).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Icon>().Where(i => i.Module == CurrentModule).OrderBy(i => i.IconFile),
                        CurrentModule);
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);
                    
                    grpMdl = ViewModelFactory.CreateViewModel<GroupingTreeItemViewModel.Factory>().Invoke(DataContext, this, "Other meta data");
                    grpMdl.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.propertiesORoptions_ico.Find(FrozenContext));
                    lst.Add(grpMdl);

                    // Relation
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Relation).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Relation>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    lstMdl.AddFilter(new ToStringFilterModel(FrozenContext));
                    grpMdl.Children.Add(lstMdl);

                    // Sequences
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Sequence).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Sequence>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Description));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    // Groups
                    lstMdl = ViewModelFactory.CreateViewModel<TreeItemInstanceListViewModel.Factory>().Invoke(DataContext, this,
                        typeof(Group).GetObjectClass(FrozenContext),
                        () => DataContext.GetQuery<Group>().Where(i => i.Module == CurrentModule).OrderBy(i => i.Name));
                    SetupViewModel(lstMdl);
                    grpMdl.Children.Add(lstMdl);

                    _TreeItems = new ReadOnlyObservableCollection<ViewModel>(lst);
                }
                return _TreeItems;
            }
        }

        private static void SetupViewModel(InstanceListViewModel lstMdl)
        {
            lstMdl.BeginInit();
            lstMdl.AllowAddNew = true;
            lstMdl.AllowDelete = true;
            lstMdl.ViewMethod = InstanceListViewMethod.Details;
            var toRemove = lstMdl.Filter.SingleOrDefault(f => f.ValueSource != null && f.ValueSource.LastInnerExpression == "Module");
            if (toRemove != null)
                lstMdl.FilterList.RemoveFilter(toRemove);
            lstMdl.EndInit();
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

        private NewDataObjectCommand _NewModuleCommand = null;
        public ICommandViewModel NewModuleCommand
        {
            get
            {
                if (_NewModuleCommand == null)
                {
                    _NewModuleCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(DataContext, this, typeof(Module).GetObjectClass(FrozenContext));
                }
                return _NewModuleCommand;
            }
        }

        public void NewModule()
        {
            if (NewModuleCommand.CanExecute(null))
                NewModuleCommand.Execute(null);
        }

        private RefreshCommand _RefreshCommand = null;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this);
                }
                return _RefreshCommand;
            }
        }

        public void Refresh()
        {
            _modules = null;
            OnPropertyChanged("ModuleList");
            OnPropertyChanged("TreeItems");
        }

        private ICommandViewModel _EditCurrentModuleCommand = null;
        public ICommandViewModel EditCurrentModuleCommand
        {
            get
            {
                if (_EditCurrentModuleCommand == null)
                {
                    _EditCurrentModuleCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Edit Module", "Opens the Editor for the current module", () => EditCurrentModule(), null, null);
                    _EditCurrentModuleCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext));
                }
                return _EditCurrentModuleCommand;
            }
        }

        public void EditCurrentModule()
        {
            if (CurrentModule == null) return;
            var newScope = ViewModelFactory.CreateNewScope();
            var newCtx = newScope.ViewModelFactory.CreateNewContext();
            var ws = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);

            ws.ShowObject(CurrentModule);
            newScope.ViewModelFactory.ShowModel(ws, true);
        }

        private ICommandViewModel _ReportProblemCommand = null;
        public ICommandViewModel ReportProblemCommand
        {
            get
            {
                if (_ReportProblemCommand == null)
                {
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext, this);
                }
                return _ReportProblemCommand;
            }
        }

        private NewObjectClassCommand _NewObjectClassCommand = null;
        public ICommandViewModel NewObjectClassCommand
        {
            get
            {
                if (_NewObjectClassCommand == null)
                {
                    _NewObjectClassCommand = ViewModelFactory.CreateViewModel<NewObjectClassCommand.Factory>().Invoke(DataContext, this, null);
                    _NewObjectClassCommand.GetCurrentModule = () => this.CurrentModule;
                }
                return _NewObjectClassCommand;
            }
        }

        #region INewCommandParameter members
        bool INewCommandParameter.IsReadOnly { get { return false; } }
        bool INewCommandParameter.AllowAddNew { get { return true; } }
        #endregion
    }
}
