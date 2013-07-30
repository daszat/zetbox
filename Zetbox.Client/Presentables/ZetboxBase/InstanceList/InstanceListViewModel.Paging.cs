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
        #region NextPage command
        private ICommandViewModel _NextPageCommand = null;
        public ICommandViewModel NextPageCommand
        {
            get
            {
                if (_NextPageCommand == null)
                {
                    _NextPageCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        this, 
                        InstanceListViewModelResources.NextPageCommand,
                        InstanceListViewModelResources.NextPageCommand_Tooltip, 
                        NextPage, 
                        CanNextPage, 
                        null);
                    _NextPageCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.forward_png.Find(FrozenContext));
                }
                return _NextPageCommand;
            }
        }

        public bool CanNextPage()
        {
            return InstancesCount >= Helper.MAXLISTCOUNT;
        }

        public void NextPage()
        {
            if (!CanNextPage()) return;

            CurrentPage = CurrentPage + 1;
            Refresh(false);
        }
        #endregion

        #region PrevPage command
        private ICommandViewModel _PrevPageCommand = null;
        public ICommandViewModel PrevPageCommand
        {
            get
            {
                if (_PrevPageCommand == null)
                {
                    _PrevPageCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        this,
                        InstanceListViewModelResources.PrevPageCommand,
                        InstanceListViewModelResources.PrevPageCommand_Tooltip, 
                        PrevPage, 
                        CanPrevPage, 
                        null);
                    _PrevPageCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.back_png.Find(FrozenContext));
                }
                return _PrevPageCommand;
            }
        }

        public bool CanPrevPage()
        {
            return CurrentPage > 1;
        }

        public void PrevPage()
        {
            if (!CanPrevPage()) return;

            CurrentPage = CurrentPage - 1;
            Refresh(false);
        }
        #endregion

        private int _currentPage = 1;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            private set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
            }
        }
    }
}
