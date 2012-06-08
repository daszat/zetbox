
namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class ControlKindHierarchyViewModel : ViewModel, IRefreshCommandListener
    {
        public new delegate ControlKindHierarchyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Module module);

        public ControlKindHierarchyViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Module module, Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent)
        {
            this.ctxFactory = ctxFactory;
            this.Module = module;
        }

        protected readonly Func<IZetboxContext> ctxFactory;
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
                        .ToList()
                        .Union(DataContext.GetQuery<ControlKind>()
                            .Where(i => i.Module.ID == moduleID)
                            .Where(i => i.Parent != null)
                            .Where(i => i.Parent.Module.ID != moduleID)
                            .ToList()
                        )
                        .OrderBy(i => i.Name)
                        .Select(i => ViewModelFactory.CreateViewModel<ControlKindViewModel.Factory>().Invoke(DataContext, this, i))));
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
                    _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(DataContext, this, typeof(ControlKind).GetObjectClass(FrozenContext), null, null, this);
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
            _rootControlKinds = null;
            OnPropertyChanged("RootControlKinds");
        }

        #endregion
    }
}
