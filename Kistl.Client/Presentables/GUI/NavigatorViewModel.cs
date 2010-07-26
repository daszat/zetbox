
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

        public NavigatorViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreen root)
            : base(dependencies, dataCtx)
        {
            _current = _root = ModelFactory.CreateViewModel<NavigationScreenViewModel.Factory>().Invoke(dataCtx, root);
            _current.Displayer = this;
            _history = new ObservableCollection<NavigationScreenViewModel>() { _current };
            _historyRO = new ReadOnlyObservableCollection<NavigationScreenViewModel>(_history);
        }

        public override string Name
        {
            get { return GetTitle(_root, _current); }
        }

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
                    OnPropertyChanged("CurrentScreen");
                }
            }
        }

        public ReadOnlyObservableCollection<NavigationScreenViewModel> History
        {
            get
            {
                return _historyRO;
            }
        }

        private static string GetTitle(NavigationScreenViewModel root, NavigationScreenViewModel current)
        {
            return root.Name + ": " + current.Name;
        }

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
                    () => { CurrentScreen = _history.Last(); _history.RemoveAt(_history.Count - 1); },
                    () => _history.Count <= 1);
            }
        }

        public CommandModel NavigateToCommand
        {
            get
            {
                // capture "this"
                var self = this;
                return ModelFactory.CreateViewModel<SimpleParameterCommandModel<NavigationScreenViewModel>.Factory>().Invoke(
                    DataContext,
                    "Go to ...",
                    "Navigates to the selected screen",
                    screen => self.CurrentScreen = screen,
                    screen => self.CurrentScreen != screen);
            }
        }
    }
}
