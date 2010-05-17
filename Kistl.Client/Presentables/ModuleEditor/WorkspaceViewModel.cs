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

                    InstanceListViewModel lstMdl;

                    lstMdl = ModelFactory.CreateViewModel<ObjectClassInstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("20888DFC-1FBC-47C8-9F3C-C6A30A5C0048")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("7EA88D99-88F0-44A7-B0A3-DA725E57595D")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("EE475DE2-D626-49E9-9E40-6BB12CB026D4")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("2CB3F778-DD6A-46C7-AD2B-5F8691313035")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("A590A975-66E5-421C-AA97-7AB3169E0E9B")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("FFDA4604-1536-43B6-B951-F8753D5092CA")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("5D152C6F-6C1E-48B7-B03E-669E30468808")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
                    lst.Add(lstMdl);

                    lstMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, DataContext.FindPersistenceObject<DataType>(new Guid("1C0E894F-4EB4-422F-8094-3095735B4917")));
                    lstMdl.ConstantFilter.Add(new KeyValuePair<string, object>("Module = @0", CurrentModule));
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
