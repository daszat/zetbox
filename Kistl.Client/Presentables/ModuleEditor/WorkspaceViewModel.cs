using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

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
                    lst.Add(ModelFactory.CreateViewModel<ObjectClassInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<InterfaceInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<EnumerationInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<CompoundObjectInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<AssemblyInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<ViewDescriptorInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<ViewModelDescriptorInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<RelationInstanceListViewModel.Factory>().Invoke(DataContext, CurrentModule));
                    lst.Add(ModelFactory.CreateViewModel<DiagramViewModel.Factory>().Invoke(DataContext, CurrentModule));

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
