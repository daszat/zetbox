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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public partial class InstanceListViewModel
    {
        private ObservableCollection<DataObjectViewModel> _selectedItems = null;
        public ObservableCollection<DataObjectViewModel> SelectedItems
        {
            get
            {
                if (_selectedItems == null)
                {
                    _selectedItems = new ObservableCollection<DataObjectViewModel>();
                    _selectedItems.CollectionChanged += _selectedItems_CollectionChanged;
                }
                return _selectedItems;
            }
        }

        IEnumerable<ViewModel> IOpenCommandParameter.SelectedItems
        {
            get { return SelectedItems; }
        }


        void _selectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("SelectedItem");
            OnPropertyChanged("SelectedItems");
            OnPropertyChanged("SelectedDetailItem");
        }

        /// <summary>
        /// The current first selected item
        /// </summary>
        /// <remarks>
        /// If set explicit, all other selected items will be cleard from the SelectesItems List
        /// </remarks>
        public DataObjectViewModel SelectedItem
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    return _selectedItems[0];
                }
                return null;
            }
            set
            {
                this.SelectedItems.Clear();
                if (value != null) this.SelectedItems.Add(value);
                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("SelectedDetailItem");
            }
        }

        /// <summary>
        /// Returns only the SelectedItem if ShowMasterDetail is set to true
        /// </summary>
        public DataObjectViewModel SelectedDetailItem
        {
            get
            {
                return ShowMasterDetail ? SelectedItem : null;
            }
            set
            {
                SelectedItem = value;
            }
        }
    }
}
