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

namespace Zetbox.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;

    public class DtoGroupedViewModel : DtoBaseViewModel
    {
        public DtoGroupedViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
        {
            Items = new ObservableCollection<DtoBaseViewModel>();
        }

        private DtoBaseViewModel _selectedItem;
        public DtoBaseViewModel SelectedItem
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

        public ObservableCollection<DtoBaseViewModel> Items { get; private set; }

        private string _alternateBackground = String.Empty;
        public string AlternateBackground
        {
            get
            {
                return _alternateBackground;
            }
            set
            {
                if (_alternateBackground != value)
                {
                    _alternateBackground = value;
                    OnPropertyChanged("AlternateBackground");
                }
            }
        }

        protected internal override void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            base.ApplyChangesFrom(otherBase);
            var other = otherBase as DtoGroupedViewModel;

            this.AlternateBackground = other.AlternateBackground;
            var selectedIdx = other.Items.IndexOf(other.SelectedItem);
            DtoBuilder.Merge(this.Items, other.Items);

            if (selectedIdx >= 0 && selectedIdx < this.Items.Count)
            {
                this.SelectedItem = this.Items[selectedIdx];
            }
            else
            {
                this.SelectedItem = this.Items.FirstOrDefault();
            }
        }
    }
}
