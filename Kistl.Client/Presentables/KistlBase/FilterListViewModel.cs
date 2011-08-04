namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.FilterViewModels;
    using Kistl.Client.Models;
    using System.Collections.Specialized;
    using System.Collections.ObjectModel;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class FilterListViewModel : ViewModel
    {
        public new delegate FilterListViewModel Factory(IKistlContext dataCtx, ViewModel parent, ObjectClass type);

        private ObjectClass _type;

        public FilterListViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, ObjectClass type)
            : base(appCtx, dataCtx, parent)
        {
            _type = type;
        }

        public override string Name
        {
            get { return FilterListViewModelResources.Name; }
        }

        private bool _enableAutoFilter = true;

        /// <summary>
        /// Enables the auto filter feature. This is the default. Setting this property will cause the filter collection to be cleared.
        /// </summary>
        public bool EnableAutoFilter
        {
            get
            {
                return _enableAutoFilter;
            }
            set
            {
                if (_enableAutoFilter != value)
                {
                    _enableAutoFilter = value;
                    _filter = null;
                    OnPropertyChanged("EnableAutoFilter");
                    OnPropertyChanged("Filter");
                    OnPropertyChanged("ShowFilter");
                }
            }
        }

        private bool? _showFilter = null;
        public bool ShowFilter
        {
            get
            {
                return _showFilter ?? Filter.Count() > 0;
            }
            set
            {
                if (_showFilter != value)
                {
                    _showFilter = value;
                    OnPropertyChanged("ShowFilter");
                }
            }
        }

        private bool _RespectRequiredFilter = true;
        /// <summary>
        /// If set to false, no filter is required. Default value is true. Use this setting if a small, preselected list (query) is provides as data source.
        /// </summary>
        public bool RespectRequiredFilter
        {
            get
            {
                return !HasUserFilter && _RespectRequiredFilter;
            }
            set
            {
                if (_RespectRequiredFilter != value)
                {
                    _RespectRequiredFilter = value;
                    UpdateRespectRequieredFilter();
                    OnPropertyChanged("RespectRequiredFilter");
                }
            }
        }

        private void UpdateRespectRequieredFilter()
        {
            if (_FilterViewModels != null)
            {
                _FilterViewModels.ForEach(i => i.RespectRequiredFilter = this.RespectRequiredFilter);
            }
        }

        public bool HasUserFilter
        {
            get
            {
                if (_FilterListEntryViewModels == null) return false;
                return _FilterListEntryViewModels.Any(i => i.IsUserFilter);
            }
        }

        private ObservableCollection<IFilterModel> _filter = null;
        private ObservableCollection<FilterViewModel> _FilterViewModels = null;
        private ObservableCollection<FilterListEntryViewModel> _FilterListEntryViewModels = null;

        private ReadOnlyObservableCollection<IFilterModel> _filterRO = null;
        private ReadOnlyObservableCollection<FilterViewModel> _FilterViewModelsRO = null;
        private ReadOnlyObservableCollection<FilterListEntryViewModel> _FilterListEntryViewModelsRO = null;

        private void InitializeFilter()
        {
            if (_filter == null)
            {
                _filter = new ObservableCollection<IFilterModel>();
                _FilterViewModels = new ObservableCollection<FilterViewModel>();
                _FilterListEntryViewModels = new ObservableCollection<FilterListEntryViewModel>();

                _filterRO = new ReadOnlyObservableCollection<IFilterModel>(_filter);
                _FilterViewModelsRO = new ReadOnlyObservableCollection<FilterViewModel>(_FilterViewModels);
                _FilterListEntryViewModelsRO = new ReadOnlyObservableCollection<FilterListEntryViewModel>(_FilterListEntryViewModels);

                if (EnableAutoFilter)
                {
                    // Resolve default property filter
                    var t = _type;
                    while (t != null)
                    {
                        // Add ObjectClass filter expressions
                        foreach (var cfc in t.FilterConfigurations)
                        {
                            AddFilter(cfc.CreateFilterModel());
                        }

                        // Add Property filter expressions
                        foreach (var prop in t.Properties.Where(p => p.FilterConfiguration != null))
                        {
                            AddFilter(prop.FilterConfiguration.CreateFilterModel());
                        }
                        if (t is ObjectClass)
                        {
                            t = ((ObjectClass)t).BaseObjectClass;
                        }
                    }

                    if (_filter.Count == 0)
                    {
                        // Add default ToString Filter only if there is no filter configuration
                        AddFilter(new ToStringFilterModel(FrozenContext));
                    }
                }
            }
        }

        public ReadOnlyObservableCollection<IFilterModel> Filter
        {
            get
            {
                InitializeFilter();
                return _filterRO;
            }
        }

        public ReadOnlyObservableCollection<FilterViewModel> FilterViewModels
        {
            get
            {
                InitializeFilter();
                return _FilterViewModelsRO;
            }
        }

        public ReadOnlyObservableCollection<FilterListEntryViewModel> FilterListEntryViewModels
        {
            get
            {
                InitializeFilter();
                return _FilterListEntryViewModelsRO;
            }
        }

        public void AddFilter(IFilterModel mdl)
        {
            AddFilter(mdl, false);
        }

        public void AddFilter(IFilterModel mdl, bool allowRemove)
        {
            InitializeFilter();
            _filter.Add(mdl);
            if (mdl is IUIFilterModel)
            {
                var uimdl = (IUIFilterModel)mdl;
                // attach change events
                uimdl.FilterChanged += new EventHandler(delegate(object s, EventArgs a)
                {
                    var f = s as FilterModel;
                    if (f == null || !f.RefreshOnFilterChanged) return;

                    if (f.IsServerSideFilter)
                    {
                        OnExecuteFilter();
                    }
                    else
                    {
                        OnExecutePostFilter();
                    }
                });

                var vmdl = FilterViewModel.Fetch(ViewModelFactory, DataContext, this, uimdl);
                vmdl.RequestedKind = uimdl.RequestedKind;
                vmdl.RespectRequiredFilter = RespectRequiredFilter;
                
                var levmdl = FilterListEntryViewModel.Fetch(ViewModelFactory, DataContext, this, vmdl);
                levmdl.IsUserFilter = allowRemove;

                _FilterViewModels.Add(vmdl);
                _FilterListEntryViewModels.Add(levmdl);
            }
            if(allowRemove) UpdateRespectRequieredFilter();

            OnPropertyChanged("RespectRequiredFilter");
            OnPropertyChanged("HasUserFilter");
            OnPropertyChanged("Filter");
            OnPropertyChanged("FilterViewModels");
            OnPropertyChanged("FilterListEntryViewModels");
        }

        public bool RemoveFilter(IFilterModel mdl)
        {
            if (!_filter.Remove(mdl)) return false;
            if (mdl is IUIFilterModel)
            {
                var uimdl = (IUIFilterModel)mdl;
                var vmdl = FilterViewModel.Fetch(ViewModelFactory, DataContext, this, uimdl);
                var levmdl = FilterListEntryViewModel.Fetch(ViewModelFactory, DataContext, this, vmdl);

                _FilterViewModels.Remove(vmdl);
                _FilterListEntryViewModels.Remove(levmdl);
            }
            UpdateRespectRequieredFilter();

            OnPropertyChanged("RespectRequiredFilter");
            OnPropertyChanged("HasUserFilter");
            OnPropertyChanged("Filter");
            OnPropertyChanged("FilterViewModels");
            OnPropertyChanged("FilterListEntryViewModels");
            return true;
        }

        public event EventHandler ExecutePostFilter;
        private void OnExecutePostFilter()
        {
            var temp = ExecutePostFilter;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public event EventHandler ExecuteFilter;
        private void OnExecuteFilter()
        {
            var temp = ExecuteFilter;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }


        private ICommandViewModel _AddFilterCommand = null;
        public ICommandViewModel AddFilterCommand
        {
            get
            {
                if (_AddFilterCommand == null)
                {
                    _AddFilterCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.AddFilterCommand,
                        InstanceListViewModelResources.AddFilterCommand_Tooltip,
                        AddFilter,
                        () => ShowFilter);
                    _AddFilterCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.Icon_new_png);
                }
                return _AddFilterCommand;
            }
        }

        public void AddFilter()
        {
            var dlg = ViewModelFactory.CreateViewModel<PropertySelectionTaskViewModel.Factory>()
                .Invoke(DataContext,
                    this,
                    _type,
                    prop =>
                    {
                        AddFilter(FilterModel.FromProperty(FrozenContext, prop.Last()), true);
                        OnUserFilterAdded(prop.Last());
                    });
            dlg.FollowRelations = true;
            ViewModelFactory.ShowDialog(dlg);
        }

        public event UserFilterAddedEventHander UserFilterAdded;
        protected void OnUserFilterAdded(Property prop)
        {
            var temp = UserFilterAdded;
            if (temp != null)
            {
                temp(this, new UserFilterAddedEventArgs(prop));
            }
        }

        public IQueryable AppendFilter(IQueryable qry)
        {
            foreach (var f in Filter.Where(f => f.Enabled))
            {
                qry = f.GetQuery(qry);
            }

            return qry;
        }

        public List<DataObjectViewModel> AppendPostFilter(List<DataObjectViewModel> result)
        {
            foreach (var f in Filter.Where(f => f.Enabled && !f.IsServerSideFilter))
            {
                result = f.GetResult(result).Cast<DataObjectViewModel>().ToList();
            }

            return result;
        }
    }

    public delegate void UserFilterAddedEventHander(object sender, UserFilterAddedEventArgs e);
    public class UserFilterAddedEventArgs : EventArgs
    {
        public UserFilterAddedEventArgs(Property prop)
        {
            this.Property = prop;
        }

        public Property Property { get; private set; }
    }
}
