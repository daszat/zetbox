
namespace Kistl.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.Presentables.KistlBase;

    [ViewModelDescriptor]
    public class NavigationScreenHierarchyViewModel : ViewModel, IRefreshCommandListener
    {
        public new delegate NavigationScreenHierarchyViewModel Factory(IKistlContext dataCtx, ViewModel parent, Module module);

        public NavigationScreenHierarchyViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Module module, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, parent)
        {
            this.ctxFactory = ctxFactory;
            this.Module = module;
        }

        protected readonly Func<IKistlContext> ctxFactory;
        public Module Module { get; private set; }


        public override string Name
        {
            get { return "NavigationScreen Hierarchy"; }
        }

        public override string ToString()
        {
            return Name;
        }

        private NavigationEntryViewModel _selectedItem;
        public NavigationEntryViewModel SelectedItem
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

        private ReadOnlyObservableCollection<NavigationEntryViewModel> _rootScreens = null;
        public ReadOnlyObservableCollection<NavigationEntryViewModel> RootScreens
        {
            get
            {
                if (_rootScreens == null)
                {
                    var moduleID = Module.ID;
                    _rootScreens = new ReadOnlyObservableCollection<NavigationEntryViewModel>(new ObservableCollection<NavigationEntryViewModel>(
                        DataContext.GetQuery<NavigationScreen>()
                        .Where(i => i.Module.ID == moduleID)
                        .Where(i => i.Parent == null)
                        .OrderBy(i => i.Title)
                        .Select(i => NavigationEntryViewModel.Fetch(ViewModelFactory, DataContext, this, i))));
                }
                return _rootScreens;
            }
        }

        #region Commands
        private RefreshCommand _RefreshCommand;
        public RefreshCommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this, this);
                }
                return _RefreshCommand;
            }
        }

        private OpenDataObjectCommand _OpenCommand;
        public OpenDataObjectCommand OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(DataContext, this, null, null);
                }
                return _OpenCommand;
            }
        }

        private NewDataObjectCommand _NewCommand;
        public NewDataObjectCommand NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(DataContext, this, typeof(NavigationEntry).GetObjectClass(FrozenContext), null, null, this);
                    _NewCommand.ObjectCreated += (obj) => ((NavigationEntry)obj).Parent = SelectedItem != null ? obj.Context.Find<NavigationEntry>(SelectedItem.ID) : null;
                }
                return _NewCommand;
            }
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public DeleteDataObjectCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this, this, true);
                }
                return _DeleteCommand;
            }
        }
        #endregion

        #region IRefreshCommandListener Members

        public void Refresh()
        {
            _rootScreens = null;
            OnPropertyChanged("RootScreens");
        }

        #endregion
    }
}
