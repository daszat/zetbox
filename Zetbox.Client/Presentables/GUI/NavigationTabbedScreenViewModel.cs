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
namespace Zetbox.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.FilterViewModels;
    using Zetbox.Client.Presentables.DtoViewModels;

    [ViewModelDescriptor]
    public class NavigationTabbedScreenViewModel : NavigationScreenViewModel
    {
        public new delegate NavigationTabbedScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);


        public NavigationTabbedScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
        }

        public override bool IsContainer
        {
            get { return true; }
        }

        private NavigationEntryViewModel _selected;
        public override NavigationEntryViewModel SelectedEntry
        {
            get
            {
                if (_selected == null && Children.Count > 0)
                {
                    _selected = Children.First();
                    if (_selected != null)
                        Displayer.NavigateTo(_selected.CurrentScreen);
                }
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged("SelectedEntry");

                    if (_selected != null)
                        Displayer.NavigateTo(_selected.CurrentScreen);
                }
            }
        }

        protected override NavigationEntryViewModel GetInitialScreen()
        {
            return Children.FirstOrDefault();
        }

        public IEnumerable<NavigationEntryViewModel> Tabs
        {
            get
            {
                return base.Children.Where(i => i.IsScreen);
            }
        }

        public IEnumerable<NavigationEntryViewModel> ActionEntries
        {
            get
            {
                return base.Children.Where(i => !i.IsScreen);
            }
        }
    }
}
