// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.FilterViewModels;
    using Zetbox.API.Client;
    using System.Threading.Tasks;
    using System.Threading;

    [ViewModelDescriptor]
    public class FilterListViewModel : ViewModel
    {
        public new delegate FilterListViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass type);

        private ObjectClass _type;
        private IFulltextSupport _fulltextSupport;

        private SemaphoreSlim _initlock = new SemaphoreSlim(1, 1);

        public FilterListViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, ObjectClass type, IFulltextSupport fulltextSupport = null)
            : base(appCtx, dataCtx, parent)
        {
            if (type == null) throw new ArgumentNullException("type");

            _type = type;
            _fulltextSupport = fulltextSupport;
        }

        public override string Name
        {
            get { return FilterListViewModelResources.Name; }
        }

        private bool? _hasFulltextSupportCache;
        public async Task<bool> GetHasFulltextSupport()
        {
            return _fulltextSupport != null && (await _fulltextSupport.HasIndexedFields(_type));
        }

        public bool HasFulltextSupport
        {
            get
            {
                if (_hasFulltextSupportCache == null)
                {
                    _hasFulltextSupportCache = false;
                    GetHasFulltextSupport().ContinueWith(t =>
                    {
                        _hasFulltextSupportCache = t.Result;
                        OnPropertyChanged(nameof(HasFulltextSupport));
                    }, ViewModelFactory.UITaskScheduler);
                }
                return _hasFulltextSupportCache.Value;
            }
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

        public bool ShowFilter
        {
            get
            {
                return AllowFilter && Filter.Count() > 0;
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

        private bool _allowFilter = true;
        /// <summary>
        /// Allow the user to filter the collection
        /// </summary>
        public bool AllowFilter
        {
            get
            {
                return _allowFilter;
            }
            set
            {
                if (_allowFilter != value)
                {
                    _allowFilter = value;
                    OnPropertyChanged("AllowFilter");
                    OnPropertyChanged("ShowFilter");
                    OnPropertyChanged("AllowUserFilter");
                }
            }
        }

        private bool _allowUserFilter = true;
        /// <summary>
        /// Allow the user to modify the filter collection
        /// </summary>
        public bool AllowUserFilter
        {
            get
            {
                return _allowFilter && _allowUserFilter;
            }
            set
            {
                if (_allowUserFilter != value)
                {
                    _allowUserFilter = value;
                    OnPropertyChanged("AllowUserFilter");
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

        private void UpdateExclusiveFilter()
        {
            if (_FilterViewModels != null)
            {
                var isExclusiveFilterActive = IsExclusiveFilterActive;
                foreach (var vm in _FilterViewModels)
                {
                    vm.IsEnabled = isExclusiveFilterActive == false // no exclusive Filter set, enable all other
                               || (vm.Filter.IsExclusiveFilter && vm.Filter.Enabled && vm.IsEnabled);
                }
            }
        }

        public bool IsExclusiveFilterActive
        {
            get
            {
                return _FilterViewModels != null && _FilterViewModels.Any(f => f.Filter.IsExclusiveFilter && f.Filter.Enabled);
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

        public bool RequiredFilterMissing
        {
            get
            {
                return RespectRequiredFilter && Filter.Any(f => !f.Enabled && f.Required);
            }
        }

        public bool IsFilterValid
        {
            get
            {
                return IsExclusiveFilterActive
                    || !RequiredFilterMissing;
            }
        }

        private ObservableCollection<IFilterModel> _filter = null;
        private ObservableCollection<FilterViewModel> _FilterViewModels = null;
        private ObservableCollection<FilterListEntryViewModel> _FilterListEntryViewModels = null;

        private ReadOnlyObservableCollection<IFilterModel> _filterRO = new(new ObservableCollection<IFilterModel>());
        private ReadOnlyObservableCollection<FilterViewModel> _FilterViewModelsRO = new(new ObservableCollection<FilterViewModel>());
        private ReadOnlyObservableCollection<FilterListEntryViewModel> _FilterListEntryViewModelsRO = new(new ObservableCollection<FilterListEntryViewModel>());

        private async Task InitializeFilter()
        {
            await _initlock.WaitAsync();
            try
            {
                if (_filter == null)
                {
                    _filter = new ObservableCollection<IFilterModel>();
                    _FilterViewModels = new ObservableCollection<FilterViewModel>();
                    _FilterListEntryViewModels = new ObservableCollection<FilterListEntryViewModel>();

                    if (EnableAutoFilter)
                    {
                        // Resolve default property filter
                        var t = _type;
                        while (t != null)
                        {
                            // Add ObjectClass filter expressions
                            foreach (var cfc in t.FilterConfigurations)
                            {
                                await AddFilter(cfc.CreateFilterModel(DataContext));
                            }

                            // Add Property filter expressions
                            foreach (var prop in t.Properties.Where(p => p.FilterConfiguration != null))
                            {
                                await AddFilter(prop.FilterConfiguration.CreateFilterModel(DataContext));
                            }
                            if (t is ObjectClass)
                            {
                                t = ((ObjectClass)t).BaseObjectClass;
                            }
                        }

                        if (await GetHasFulltextSupport())
                        {
                            await AddFilter(new FulltextFilterModel(FrozenContext));
                        }
                        else if (_filter.Count == 0)
                        {
                            // Add default ToString Filter only if there is no filter configuration & no fulltext filter can be applied
                            await AddFilter(new ToStringFilterModel(FrozenContext));
                        }

                        if (await _type.ImplementsIDeactivatable())
                        {
                            await AddFilter(new WithDeactivatedFilterModel(FrozenContext));
                        }
                    }

                    _filterRO = new ReadOnlyObservableCollection<IFilterModel>(_filter);
                    _FilterViewModelsRO = new ReadOnlyObservableCollection<FilterViewModel>(_FilterViewModels);
                    _FilterListEntryViewModelsRO = new ReadOnlyObservableCollection<FilterListEntryViewModel>(_FilterListEntryViewModels);
                }
            }
            finally
            {
                _initlock.Release();
            }
        }

        public ReadOnlyObservableCollection<IFilterModel> Filter
        {
            get
            {
                _ = InitializeFilter();
                return _filterRO;
            }
        }

        public ReadOnlyObservableCollection<FilterViewModel> FilterViewModels
        {
            get
            {
                _ = InitializeFilter();
                return _FilterViewModelsRO;
            }
        }

        public ReadOnlyObservableCollection<FilterListEntryViewModel> FilterListEntryViewModels
        {
            get
            {
                _ = InitializeFilter();
                return _FilterListEntryViewModelsRO;
            }
        }

        public Task AddFilter(IFilterModel mdl)
        {
            return AddFilter(-1, mdl, false, null);
        }
        public Task AddFilter(int index, IFilterModel mdl)
        {
            return AddFilter(index, mdl, false, null);
        }
        public Task AddFilter(IFilterModel mdl, bool allowRemove)
        {
            return AddFilter(-1, mdl, allowRemove, null);
        }
        public Task AddFilter(int index, IFilterModel mdl, bool allowRemove)
        {
            return AddFilter(index, mdl, allowRemove, null);
        }
        public Task AddFilter(IFilterModel mdl, bool allowRemove, IEnumerable<Property> sourceProperties)
        {
            return AddFilter(-1, mdl, allowRemove, sourceProperties);
        }
        public async Task AddFilter(int index, IFilterModel mdl, bool allowRemove, IEnumerable<Property> sourceProperties)
        {
            await InitializeFilter();

            if (index >= 0)
            {
                _filter.Insert(index, mdl);
            }
            else
            {
                _filter.Add(mdl);
            }

            if (mdl is IUIFilterModel)
            {
                var uimdl = (IUIFilterModel)mdl;

                var vmdl = FilterViewModel.Fetch(ViewModelFactory, DataContext, this, uimdl);
                vmdl.RequestedKind = uimdl.RequestedKind;
                vmdl.RespectRequiredFilter = RespectRequiredFilter;

                var levmdl = FilterListEntryViewModel.Fetch(ViewModelFactory, DataContext, this, vmdl);
                levmdl.IsUserFilter = allowRemove;
                levmdl.SourceProperties = sourceProperties;

                // attach change events
                uimdl.FilterChanged += OnUIFilterChanged;

                _FilterViewModels.Add(vmdl);
                _FilterListEntryViewModels.Add(levmdl);
            }
            if (allowRemove) UpdateRespectRequieredFilter();
            UpdateExclusiveFilter();

            NotifyFilterChanged();
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
            UpdateExclusiveFilter();

            NotifyFilterChanged();
            return true;
        }

        private void OnUIFilterChanged(object sender, EventArgs e)
        {
            var filter = sender as FilterModel;
            if (filter == null) return;

            UpdateExclusiveFilter();
            OnPropertyChanged("IsExclusiveFilterActive");

            if (filter.RefreshOnFilterChanged)
            {
                if (filter.IsServerSideFilter)
                {
                    OnExecuteFilter();
                }
                else
                {
                    OnExecutePostFilter();
                }
            }
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
                        () => Task.FromResult(AllowFilter && AllowUserFilter),
                        null);
                    Task.Run(async () => _AddFilterCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext)));
                }
                return _AddFilterCommand;
            }
        }

        public Task AddFilter()
        {
            var dlg = ViewModelFactory.CreateViewModel<PropertySelectionTaskViewModel.Factory>()
                .Invoke(DataContext,
                    this,
                    _type,
                    props =>
                    {
                        if (props != null)
                        {
                            AddFilter(FilterModel.FromProperty(DataContext, FrozenContext, props), true, props);
                            OnUserFilterAdded(props);
                        }
                    });
            dlg.FollowRelationsOne = true;
            dlg.FollowRelationsMany = true;
            ViewModelFactory.ShowDialog(dlg);
            return Task.CompletedTask;
        }

        public void ResetUserFilter()
        {
            if (_FilterListEntryViewModels != null)
            {
                foreach (var f in _FilterListEntryViewModels.Where(i => i.IsUserFilter).ToList())
                {
                    RemoveFilter(f.FilterViewModel.Filter);
                }
            }
        }

        public event UserFilterAddedEventHander UserFilterAdded;
        protected void OnUserFilterAdded(IEnumerable<Property> props)
        {
            var temp = UserFilterAdded;
            if (temp != null)
            {
                temp(this, new UserFilterAddedEventArgs(props));
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

        private void NotifyFilterChanged()
        {
            OnPropertyChanged("IsExclusiveFilterActive");
            OnPropertyChanged("RespectRequiredFilter");
            OnPropertyChanged("HasUserFilter");
            OnPropertyChanged("Filter");
            OnPropertyChanged("ShowFilter");
            OnPropertyChanged("FilterViewModels");
            OnPropertyChanged("FilterListEntryViewModels");
        }
    }

    public delegate void UserFilterAddedEventHander(object sender, UserFilterAddedEventArgs e);
    public class UserFilterAddedEventArgs : EventArgs
    {
        public UserFilterAddedEventArgs(IEnumerable<Property> props)
        {
            this.Properties = props;
        }

        public IEnumerable<Property> Properties { get; private set; }
    }
}
