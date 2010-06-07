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

namespace Kistl.Client.Presentables.ModuleEditor
{
    public class WorkspaceViewModel : WindowViewModel
    {
        public new delegate WorkspaceViewModel Factory(IKistlContext dataCtx);

        public WorkspaceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
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
    }
}
