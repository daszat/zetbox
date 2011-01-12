
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
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate ControlKindHierarchyViewModel Factory(IKistlContext dataCtx, Module module);
#else
        public new delegate NavigationScreenHierarchyViewModel Factory(IKistlContext dataCtx, Module module);
#endif

        public NavigationScreenHierarchyViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Module module, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
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

        private ReadOnlyObservableCollection<NavigationScreenViewModel> _rootScreens = null;
        public ReadOnlyObservableCollection<NavigationScreenViewModel> RootScreens
        {
            get
            {
                if (_rootScreens == null)
                {
                    var moduleID = Module.ID;
                    _rootScreens = new ReadOnlyObservableCollection<NavigationScreenViewModel>(new ObservableCollection<NavigationScreenViewModel>(
                        DataContext.GetQuery<NavigationScreen>()
                        .Where(i => i.Module.ID == moduleID)
                        .Where(i => i.Parent == null)
                        .OrderBy(i => i.Title)
                        .Select(i => NavigationScreenViewModel.Fetch(ViewModelFactory, DataContext, i))));
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
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this);
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
                    _OpenCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(DataContext, null, null);
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
                    _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(DataContext, typeof(NavigationScreen).GetObjectClass(FrozenContext), null, null, this);
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
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this, true);
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
