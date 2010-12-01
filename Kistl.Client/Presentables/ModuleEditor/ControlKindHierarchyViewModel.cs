using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using System.Collections.ObjectModel;
using Kistl.Client.Presentables.GUI;
using Kistl.App.GUI;
using Kistl.App.Extensions;
using Kistl.Client.Presentables.KistlBase;

namespace Kistl.Client.Presentables.ModuleEditor
{
    [ViewModelDescriptor]
    public class ControlKindHierarchyViewModel : ViewModel, IRefreshCommandListener
    {
        public new delegate ControlKindHierarchyViewModel Factory(IKistlContext dataCtx, Module module);

        public ControlKindHierarchyViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Module module, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
        {
            this.ctxFactory = ctxFactory;
            this.Module = module;
        }

        protected readonly Func<IKistlContext> ctxFactory;
        public Module Module { get; private set; }


        public override string Name
        {
            get { return "ControlKind Hierarchy"; }
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

        private ReadOnlyObservableCollection<ControlKindViewModel> _rootControlKinds = null;
        public ReadOnlyObservableCollection<ControlKindViewModel> RootControlKinds
        {
            get
            {
                if (_rootControlKinds == null)
                {
                    var moduleID = Module.ID;
                    _rootControlKinds = new ReadOnlyObservableCollection<ControlKindViewModel>(new ObservableCollection<ControlKindViewModel>(
                        DataContext.GetQuery<ControlKind>()
                        .Where(i => i.Module.ID == moduleID)
                        .Where(i => i.Parent == null)
                        .OrderBy(i => i.Name)
                        .Select(i => ViewModelFactory.CreateViewModel<ControlKindViewModel.Factory>().Invoke(DataContext, i))));
                }
                return _rootControlKinds;
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
                    _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(DataContext, typeof(ControlKind).GetObjectClass(FrozenContext), null, null, this);
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
            _rootControlKinds = null;
            OnPropertyChanged("RootControlKinds");
        }

        #endregion
    }
}
