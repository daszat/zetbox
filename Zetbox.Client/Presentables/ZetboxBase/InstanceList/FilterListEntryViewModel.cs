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
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.FilterViewModels;
    using Zetbox.App.GUI;
    using Zetbox.App.Base;

    [ViewModelDescriptor]
    public class FilterListEntryViewModel : ViewModel
    {
        public new delegate FilterListEntryViewModel Factory(IZetboxContext dataCtx, FilterListViewModel parent, FilterViewModel vmdl);

        public static FilterListEntryViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, FilterListViewModel parent, FilterViewModel vmdl)
        {
            return (FilterListEntryViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(vmdl, () => f.CreateViewModel<FilterListEntryViewModel.Factory>().Invoke(dataCtx, parent, vmdl));
        }

        public FilterListEntryViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, FilterListViewModel parent, FilterViewModel vmdl)
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

        private IEnumerable<Property> _sourceProperties;
        public IEnumerable<Property> SourceProperties
        {
            get
            {
                return _sourceProperties;
            }
            set
            {
                if (_sourceProperties != value)
                {
                    _sourceProperties = value;
                    OnPropertyChanged("SourceProperties");
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
                    _RemoveCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.delete_png.Find(FrozenContext));
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
