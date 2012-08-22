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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class NavigatorViewModel
        : WindowViewModel
    {
        public new delegate NavigatorViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen root);

        private readonly NavigationEntryViewModel _root;
        private NavigationEntryViewModel _container;
        private NavigationEntryViewModel _current;
        private readonly ObservableCollection<NavigationEntryViewModel> _history;
        private readonly ReadOnlyObservableCollection<NavigationEntryViewModel> _historyRO;

        private readonly ObservableCollection<NavigationEntryViewModel> _location;
        private readonly ReadOnlyObservableCollection<NavigationEntryViewModel> _locationRO;

        public NavigatorViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, NavigationScreen root)
            : base(dependencies, dataCtx, parent)
        {
            _container = _current = _root = NavigationEntryViewModel.Fetch(ViewModelFactory, dataCtx, parent, root);
            _container.Displayer = _current.Displayer = this;

            _history = new ObservableCollection<NavigationEntryViewModel>() { _current };
            _historyRO = new ReadOnlyObservableCollection<NavigationEntryViewModel>(_history);

            _location = new ObservableCollection<NavigationEntryViewModel>() { _root };
            _locationRO = new ReadOnlyObservableCollection<NavigationEntryViewModel>(_location);
        }

        #region Name

        public override string Name
        {
            get { return GetTitle(_root, _current); }
        }

        private static string GetTitle(NavigationEntryViewModel root, NavigationEntryViewModel current)
        {
            return root.Name + ": " + current.Name;
        }

        #endregion

        #region Navigational Aides

        /// <summary>
        /// The top-most container of the CurrentScreen
        /// </summary>
        public NavigationEntryViewModel ContainerScreen
        {
            get
            {
                return _container;
            }

            private set
            {
                if (_container != value)
                {
                    _container = value;
                    _container.Displayer = this;
                    OnPropertyChanged("ContainerScreen");
                }
            }
        }

        /// <summary>
        /// The currently displayed NavigationScreenViewModel.
        /// </summary>
        public NavigationEntryViewModel CurrentScreen
        {
            get
            {
                return _current;
            }

            private set
            {
                if (_current != value)
                {
                    _current = value;
                    _current.Displayer = this;
                    _history.Add(_current);
                    UpdateLocation();
                    OnPropertyChanged("Name");
                    OnPropertyChanged("CurrentScreen");
                }
            }
        }

        /// <summary>
        /// The "path" to the CurrentScreen, as defined by its Parents.
        /// </summary>
        public ReadOnlyObservableCollection<NavigationEntryViewModel> Location
        {
            get { return _locationRO; }
        }

        private void UpdateLocation()
        {
            var newLocation = new List<NavigationEntryViewModel>();
            var screen = CurrentScreen;

            while (screen != null)
            {
                newLocation.Add(screen);
                screen = screen.ParentScreen;
            }
            newLocation.Reverse();

            int prefixLength = 0;
            while (prefixLength < newLocation.Count
                && prefixLength < Location.Count
                && newLocation[prefixLength] == Location[prefixLength])
            {
                prefixLength += 1;
            }
            // now we know that the first "prefixLength" items are equal
            // and we can replace the rest with the "newLocation" suffix

            // remove from end
            while (Location.Count > prefixLength)
            {
                _location.RemoveAt(Location.Count - 1);
            }

            // add remaining items
            while (prefixLength < newLocation.Count)
            {
                _location.Add(newLocation[prefixLength]);
                prefixLength += 1;
            }

            ContainerScreen = _location.FirstOrDefault(s => s.IsContainer) ?? CurrentScreen;
        }

        /// <summary>
        /// A list of recently visited screens.
        /// </summary>
        public ReadOnlyObservableCollection<NavigationEntryViewModel> History
        {
            get { return _historyRO; }
        }

        #endregion

        #region Commands

        private ICommandViewModel _HomeCommand = null;
        public ICommandViewModel HomeCommand
        {
            get
            {
                if (_HomeCommand == null)
                {
                    _HomeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        NavigatorViewModelResources.HomeCommand_Name,
                        NavigatorViewModelResources.HomeCommand_Tooltip,
                        Home,
                        () => CurrentScreen != _root,
                        null);
                }
                return _HomeCommand;
            }
        }

        public void Home()
        {
            CurrentScreen = _root;
        }

        private ICommandViewModel _BackCommand = null;
        public ICommandViewModel BackCommand
        {
            get
            {
                if (_BackCommand == null)
                {
                    _BackCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        NavigatorViewModelResources.BackCommand_Name,
                        NavigatorViewModelResources.BackCommand_Tooltip,
                        Back,
                        () => _history.Count > 1,
                        null);
                }
                return _BackCommand;
            }
        }

        public void Back()
        {
            if (_history.Count > 1)
            {
                // remove "current" screen from history
                _history.RemoveAt(_history.Count - 1);
                NavigateTo(_history.Last());
                // remove the back step from history too
                _history.RemoveAt(_history.Count - 1);
            }
        }

        private ICommandViewModel _NavigateToCommand = null;
        public ICommandViewModel NavigateToCommand
        {
            get
            {
                if (_NavigateToCommand == null)
                {
                    _NavigateToCommand = ViewModelFactory.CreateViewModel<SimpleParameterCommandViewModel<NavigationEntryViewModel>.Factory>().Invoke(
                                DataContext,
                                this,
                                NavigatorViewModelResources.NavigateToCommand_Name,
                                NavigatorViewModelResources.NavigateToCommand_Tooltip,
                                NavigateTo,
                                screen => CurrentScreen != screen);
                }
                return _NavigateToCommand;
            }
        }

        public void NavigateTo(NavigationEntryViewModel screen)
        {
            if (CurrentScreen != screen)
            {
                CurrentScreen = screen;
                while (screen.ParentScreen != null)
                {
                    // change CurrentScreen first, to avoid additional navigations
                    screen.CurrentScreen = CurrentScreen;
                    screen.ParentScreen.SelectedEntry = screen;
                    screen = screen.ParentScreen;
                }
            }
        }

        #endregion

        #region ReportProblemCommand
        private ICommandViewModel _ReportProblemCommand = null;
        public ICommandViewModel ReportProblemCommand
        {
            get
            {
                if (_ReportProblemCommand == null)
                {
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext, this);
                }
                return _ReportProblemCommand;
            }
        }
        #endregion

        #region Closing
        public override bool CanClose()
        {
            return ViewModelFactory.GetDecisionFromUser(NavigatorViewModelResources.CanClose, NavigatorViewModelResources.CanClose_Title);
        }
        #endregion
    }
}
