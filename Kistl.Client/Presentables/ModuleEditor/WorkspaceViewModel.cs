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
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ObjectClass>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lstMdl.Filter.Add(ModelFactory.CreateViewModel<EnableFilterExpression.Factory>().Invoke(DataContext, "Only Baseclasses", "BaseObjectClass = null"));
                    lst.Add(lstMdl);

                    // Interface
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Interface>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Enums
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Enumeration>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // CompoundObject
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<CompoundObject>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Assembly
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Assembly>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lstMdl.Commands.Add(ModelFactory.CreateViewModel<SimpleItemCommandModel<DataObjectModel>.Factory>().Invoke(DataContext,
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
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<TypeRef>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Assembly.Module = @0", CurrentModule));
                    lstMdl.Filter.Add(ModelFactory.CreateViewModel<EnableFilterExpression.Factory>().Invoke(DataContext, "Only Deleted", "Deleted = true"));
                    lstMdl.Commands.Add(ModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext));
                    lst.Add(lstMdl);

                    // ViewDescriptor
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ViewDescriptor>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // ViewModelDescriptor
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ViewModelDescriptor>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // ControlKinds
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<ControlKind>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    // Relation
                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Relation>());
                    lstMdl.Filter.Add(new ConstantFilterExpression("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    var diagMdl = ModelFactory.CreateViewModel<DiagramViewModel.Factory>().Invoke(DataContext, CurrentModule);
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

        private ICommand _NewModuleCommand = null;
        public ICommand NewModuleCommand
        {
            get
            {
                if (_NewModuleCommand == null)
                {
                    _NewModuleCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(DataContext, "New Module", "Creates a new Module", () => CreateNewModule(), null);
                }
                return _NewModuleCommand;
            }
        }

        private ICommand _RefreshCommand = null;
        public ICommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(DataContext, "Refresh", "Refresh", () => Refresh(), null);
                }
                return _RefreshCommand;
            }
        }

        private ICommand _EditCurrentModuleCommand = null;
        public ICommand EditCurrentModuleCommand
        {
            get
            {
                if (_EditCurrentModuleCommand == null)
                {
                    _EditCurrentModuleCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(DataContext, "Edit Module", "Opens the Editor for the current module", () => EditCurrentModule(), null);
                }
                return _EditCurrentModuleCommand;
            }
        }

        public void EditCurrentModule()
        {
            if (CurrentModule == null) return;
            var newCtx = ctxFactory();
            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);

            newWorkspace.ShowForeignModel(ModelFactory.CreateViewModel<DataObjectModel.Factory>(CurrentModule).Invoke(newCtx, CurrentModule));
            ModelFactory.ShowModel(newWorkspace, true);
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
            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            var newObj = newCtx.Create<Module>();

            newWorkspace.ShowForeignModel(ModelFactory.CreateViewModel<DataObjectModel.Factory>(newObj).Invoke(newCtx, newObj));
            ModelFactory.ShowModel(newWorkspace, true);
        }
    }
}
