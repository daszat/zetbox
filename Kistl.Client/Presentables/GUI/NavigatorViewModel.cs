
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class NavigatorViewModel
        : WindowViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate NavigatorViewModel Factory(IKistlContext dataCtx, NavigationScreen root);
#else
        public new delegate NavigatorViewModel Factory(IKistlContext dataCtx, NavigationScreen root);
#endif

        private readonly NavigationScreenViewModel _root;
        private NavigationScreenViewModel _current;
        private readonly ObservableCollection<NavigationScreenViewModel> _history;
        private readonly ReadOnlyObservableCollection<NavigationScreenViewModel> _historyRO;

        private readonly ObservableCollection<NavigationScreenViewModel> _location;
        private readonly ReadOnlyObservableCollection<NavigationScreenViewModel> _locationRO;

        public NavigatorViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreen root)
            : base(dependencies, dataCtx)
        {
            _current = _root = NavigationScreenViewModel.Fetch(ViewModelFactory, dataCtx, root);
            _current.Displayer = this;

            _history = new ObservableCollection<NavigationScreenViewModel>() { _current };
            _historyRO = new ReadOnlyObservableCollection<NavigationScreenViewModel>(_history);

            _location = new ObservableCollection<NavigationScreenViewModel>() { _root };
            _locationRO = new ReadOnlyObservableCollection<NavigationScreenViewModel>(_location);
        }

        #region Name

        public override string Name
        {
            get { return GetTitle(_root, _current); }
        }

        private static string GetTitle(NavigationScreenViewModel root, NavigationScreenViewModel current)
        {
            return root.Name + ": " + current.Name;
        }

        #endregion

        #region Navigational Aides

        public NavigationScreenViewModel CurrentScreen
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
                    OnPropertyChanged("CurrentScreen");
                }
            }
        }

        public ReadOnlyObservableCollection<NavigationScreenViewModel> Location
        {
            get { return _locationRO; }
        }

        private void UpdateLocation()
        {
            var newLocation = new List<NavigationScreenViewModel>();
            var screen = CurrentScreen;
            while (screen != null)
            {
                newLocation.Add(screen);
                screen = screen.Parent;
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
        }

        public ReadOnlyObservableCollection<NavigationScreenViewModel> History
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
                        NavigatorViewModelResources.HomeCommand_Name,
                        NavigatorViewModelResources.HomeCommand_Tooltip,
                        Home,
                        () => CurrentScreen != _root);
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
                        NavigatorViewModelResources.BackCommand_Name,
                        NavigatorViewModelResources.BackCommand_Tooltip,
                        Back,
                        () => _history.Count > 1);
                }
                return _BackCommand;
            }
        }

        public void Back()
        {
            // remove "current" screen from history
            _history.RemoveAt(_history.Count - 1);
            CurrentScreen = _history.Last();
            // remove the back step from history too
            _history.RemoveAt(_history.Count - 1);
        }

        private ICommandViewModel _NavigateToCommand = null;
        public ICommandViewModel NavigateToCommand
        {
            get
            {
                if (_NavigateToCommand == null)
                {
                    _NavigateToCommand = ViewModelFactory.CreateViewModel<SimpleParameterCommandViewModel<NavigationScreenViewModel>.Factory>().Invoke(
                                DataContext,
                                NavigatorViewModelResources.NavigateToCommand_Name,
                                NavigatorViewModelResources.NavigateToCommand_Tooltip,
                                NavigateTo,
                                screen => CurrentScreen != screen);
                }
                return _NavigateToCommand;
            }
        }

        public void NavigateTo(NavigationScreenViewModel screen)
        {
            CurrentScreen = screen;
        }
        #endregion
    }
}
