namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.Client.Presentables.FilterViewModels;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class FilterListEntryViewModel : ViewModel
    {
        public new delegate FilterListEntryViewModel Factory(IKistlContext dataCtx, FilterListViewModel parent, FilterViewModel vmdl);

        public static FilterListEntryViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, FilterListViewModel parent, FilterViewModel vmdl)
        {
            return (FilterListEntryViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(vmdl, () => f.CreateViewModel<FilterListEntryViewModel.Factory>().Invoke(dataCtx, parent, vmdl));
        }

        public FilterListEntryViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, FilterListViewModel parent, FilterViewModel vmdl)
            : base(appCtx, dataCtx, parent)
        {
            this.FilterViewModel = vmdl;
        }

        public new FilterListViewModel Parent
        {
            get
            {
                return (FilterListViewModel)base.Parent;
            }
        }

        public override string Name
        {
            get { return FilterViewModel.Name; }
        }

        public FilterViewModel FilterViewModel { get; private set; }

        private bool _isUserFilter = true;
        public bool IsUserFilter
        {
            get
            {
                return _isUserFilter;
            }
            set
            {
                if (_isUserFilter != value)
                {
                    _isUserFilter = value;
                    OnPropertyChanged("IsUserFilter");
                }
            }
        }

        private ICommandViewModel _RemoveCommand = null;
        public ICommandViewModel RemoveCommand
        {
            get
            {
                if (_RemoveCommand == null)
                {
                    _RemoveCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, 
                        FilterListEntryViewModelResources.RemoveCommand, 
                        FilterListEntryViewModelResources.RemoveCommand_Tooltip,
                        Remove, () => IsUserFilter, null);
                    _RemoveCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_delete_png);
                }
                return _RemoveCommand;
            }
        }

        public void Remove()
        {
            Parent.RemoveFilter(FilterViewModel.Filter);
        }
    }
}
