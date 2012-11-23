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
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.FilterViewModels;

    public partial class InstanceListViewModel
    {
        private FilterListViewModel _filterList;
        public FilterListViewModel FilterList
        {
            get
            {
                if (_filterList == null)
                {
                    this._filterList = ViewModelFactory.CreateViewModel<FilterListViewModel.Factory>().Invoke(DataContext, this, _type);
                    this._filterList.ExecuteFilter += (s, e) => ReloadInstances();
                    this._filterList.ExecutePostFilter += (s, e) => UpdateFilteredInstances();
                    this._filterList.PropertyChanged += _filterList_PropertyChanged;
                    this._filterList.UserFilterAdded += _filterList_UserFilterAdded;
                }
                return _filterList;
            }
        }

        void _filterList_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "EnableAutoFilter":
                case "RespectRequiredFilter":
                case "Filter":
                case "FilterViewModels":
                    OnPropertyChanged(e.PropertyName);
                    break;
                case "ShowFilter":
                    OnPropertyChanged("ShowFilter");
                    OnPropertyChanged("ShowConfig");
                    break;
                case "AllowUserFilter":
                    OnPropertyChanged("AllowUserFilter");
                    OnPropertyChanged("ShowUtilities");
                    break;
            }
        }

        void _filterList_UserFilterAdded(object sender, UserFilterAddedEventArgs e)
        {
            if (DisplayedProperties.Any(dp => dp.SequenceEqual(e.Properties))) return;
            DisplayedColumns.Columns.Add(ColumnDisplayModel.Create(GridDisplayConfiguration.Mode.ReadOnly, e.Properties.ToArray()));
        }

        /// <summary>
        /// Enables the auto filter feature. This is the default. Setting this property will cause the filter collection to be cleared.
        /// </summary>
        public bool EnableAutoFilter
        {
            get
            {
                return FilterList.EnableAutoFilter;
            }
            set
            {
                FilterList.EnableAutoFilter = value;
            }
        }

        /// <summary>
        /// If set to false, no filter is required. Default value is true. Use this setting if a small, preselected list (query) is provides as data source.
        /// </summary>
        public bool RespectRequiredFilter
        {
            get
            {
                return FilterList.RespectRequiredFilter;
            }
            set
            {
                FilterList.RespectRequiredFilter = value;
            }
        }

        public bool ShowFilter
        {
            get
            {
                return FilterList.ShowFilter;
            }
            set
            {
                FilterList.ShowFilter = value;
            }
        }

        public bool ShowConfig
        {
            get
            {
                return this.ShowFilter;
            }
        }

        /// <summary>
        /// Allow the user to modify the filter collection
        /// </summary>
        public bool AllowUserFilter
        {
            get
            {
                return FilterList.AllowUserFilter;
            }
            set
            {
                FilterList.AllowUserFilter = value;
            }
        }

        public ReadOnlyObservableCollection<IFilterModel> Filter
        {
            get
            {
                return FilterList.Filter;
            }
        }

        public ReadOnlyObservableCollection<FilterViewModel> FilterViewModels
        {
            get
            {
                return FilterList.FilterViewModels;
            }
        }

        public void AddFilter(IFilterModel mdl)
        {
            FilterList.AddFilter(mdl);
        }

        public void RemoveFilter(IFilterModel mdl)
        {
            FilterList.RemoveFilter(mdl);
        }
    }
}
