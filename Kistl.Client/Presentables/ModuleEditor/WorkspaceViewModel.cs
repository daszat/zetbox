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
        public WorkspaceViewModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            _CurrentModule = dataCtx.GetQuery<Module>().FirstOrDefault();
        }

        public WorkspaceViewModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, int moduleID)
            : base(appCtx, dataCtx)
        {
            _CurrentModule = dataCtx.Find<Module>(moduleID);
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
                    // ObjectClass
                    lst.Add(new ObjectClassInstanceListViewModel(AppContext, DataContext, CurrentModule));
                    // Enumeration
                    lst.Add(new EnumerationInstanceListViewModel(AppContext, DataContext, CurrentModule));
                    // CompoundObject
                    lst.Add(new CompoundObjectInstanceListViewModel(AppContext, DataContext, CurrentModule));
                    // Assemblies
                    lst.Add(new AssemblyInstanceListViewModel(AppContext, DataContext, CurrentModule));

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
