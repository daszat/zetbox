
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.GUI;

    [ViewModelDescriptor("GUI",
        DefaultKind = "Kistl.App.GUI.Navigator",
        Description = "Container for navigating between NavigationScreens. Displays the current position in the screen hierarchy and allows forward/backward stepping in the history.")]
    public class NavigatorViewModel
        : WindowViewModel
    {
        public new delegate NavigatorViewModel Factory(IKistlContext dataCtx, NavigationScreen root);

        private readonly NavigationScreenViewModel _root;
        private NavigationScreenViewModel _current;
        private readonly ObservableCollection<NavigationScreenViewModel> _history;
        private readonly ReadOnlyObservableCollection<NavigationScreenViewModel> _historyRO;

        private readonly ObservableCollection<NavigationScreenViewModel> _location;
        private readonly ReadOnlyObservableCollection<NavigationScreenViewModel> _locationRO;

        public NavigatorViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreen root)
            : base(dependencies, dataCtx)
        {
            _current = _root = NavigationScreenViewModel.Create(ModelFactory, dataCtx, null, root);
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

        public CommandModel HomeCommand
        {
            get
            {
                return ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(
                    DataContext,
                    "Home",
                    "Navigates back to the top-most screen",
                    () => CurrentScreen = _root,
                    () => CurrentScreen != _root);
            }
        }

        public CommandModel BackCommand
        {
            get
            {
                return ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(
                    DataContext,
                    "Back",
                    "Navigates back to the last screen",
                    () =>
                    {
                        // remove "current" screen from history
                        _history.RemoveAt(_history.Count - 1);
                        CurrentScreen = _history.Last();
                        // remove the back step from history too
                        _history.RemoveAt(_history.Count - 1);
                    },
                    () => _history.Count > 1);
            }
        }

        public CommandModel NavigateToCommand
        {
            get
            {
                return ModelFactory.CreateViewModel<SimpleParameterCommandModel<NavigationScreenViewModel>.Factory>().Invoke(
                    DataContext,
                    "Go to ...",
                    "Navigates to the selected screen",
                    screen => CurrentScreen = screen,
                    screen => CurrentScreen != screen);
            }
        }

        #endregion
    }
}
