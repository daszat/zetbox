
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.App.GUI;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.KistlBase;

    [ViewModelDescriptor]
    public class NavigationScreenViewModel
        : DataObjectViewModel
    {
        public new delegate NavigationScreenViewModel Factory(IKistlContext dataCtx, NavigationScreen screen);

        public static NavigationScreenViewModel Fetch(IViewModelFactory ModelFactory, IKistlContext dataCtx, NavigationScreen screen)
        {
            if (ModelFactory == null) throw new ArgumentNullException("ModelFactory");
            if (screen == null) throw new ArgumentNullException("screen");

            return (NavigationScreenViewModel)dataCtx.GetViewModelCache().LookupOrCreate(screen, () =>
            {
                if (screen.ViewModelDescriptor != null)
                {
                    var t = screen.ViewModelDescriptor.ViewModelRef.AsType(true);
                    return ModelFactory.CreateViewModel<NavigationScreenViewModel.Factory>(t).Invoke(dataCtx, screen);
                }
                else
                {
                    return ModelFactory.CreateViewModel<NavigationScreenViewModel.Factory>().Invoke(dataCtx, screen);
                }
            });
        }

        private readonly NavigationScreen _screen;
        private NavigationScreenViewModel _parent;

        private NavigatorViewModel _displayer = null;

        public NavigationScreenViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreen screen)
            : base(dependencies, dataCtx, screen)
        {
            if (screen == null) throw new ArgumentNullException("screen");

            if (!CurrentIdentity.IsAdmininistrator() && !screen.Groups.Any(g => CurrentIdentity.Groups.Any(grp => grp.ExportGuid == g.ExportGuid)))
                throw new InvalidOperationException("The current identity is not allowed to see this screen. The screen should not be displayed! Check your filters.");

            _screen = screen;
            _screen.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_screen_PropertyChanged);
        }

        void _screen_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Title":
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Title");
                    break;
                case "Parent":
                    OnPropertyChanged("Parent");
                    break;
                case "Children":
                    OnPropertyChanged("Children");
                    break;
            }
        }

        public override string Name
        {
            get { return _screen.Title; }
        }

        public string Title
        {
            get { return _screen.Title; }
        }

        public NavigationScreen Screen { get { return _screen; } }

        public Guid ExportGuid { get { return _screen.ExportGuid; } }

        public NavigatorViewModel Displayer
        {
            get
            {
                return _displayer;
            }
            set
            {
                if (_displayer != value)
                {
                    _displayer = value;
                    foreach (var c in Children)
                    {
                        c.Displayer = value;
                    }
                    OnPropertyChanged("Displayer");
                }
            }
        }

        public NavigationScreenViewModel Parent
        {
            get
            {
                if (_parent == null && _screen.Parent != null)
                {
                    _parent = Fetch(ViewModelFactory, DataContext, _screen.Parent);
                }
                return _parent;
            }
        }

        private ObservableCollection<NavigationScreenViewModel> _children;
        private ReadOnlyObservableCollection<NavigationScreenViewModel> _childrenRO;
        public ReadOnlyObservableCollection<NavigationScreenViewModel> Children
        {
            get
            {
                if (_childrenRO == null)
                {
                    _children = new ObservableCollection<NavigationScreenViewModel>();
                    foreach (var s in _screen.Children.Where(c => CurrentIdentity.IsAdmininistrator() || c.Groups.Any(g => CurrentIdentity.Groups.Select(grp => grp.ExportGuid).Contains(g.ExportGuid))))
                    {
                        _children.Add(NavigationScreenViewModel.Fetch(ViewModelFactory, DataContext, s));
                    }
                    _childrenRO = new ReadOnlyObservableCollection<NavigationScreenViewModel>(_children);
                }
                return _childrenRO;
            }
        }

        private ObservableCollection<CommandViewModel> _additionalCommandsRW;
        protected ObservableCollection<CommandViewModel> AdditionalCommandsRW
        {
            get
            {
                if (_additionalCommandsRW == null)
                {
                    _additionalCommandsRW = new ObservableCollection<CommandViewModel>(CreateAdditionalCommands());
                }
                return _additionalCommandsRW;
            }
        }

        private ReadOnlyObservableCollection<CommandViewModel> _additionalCommands;
        public ReadOnlyObservableCollection<CommandViewModel> AdditionalCommands
        {
            get
            {
                if (_additionalCommands == null)
                {
                    _additionalCommands = new ReadOnlyObservableCollection<CommandViewModel>(AdditionalCommandsRW);
                }
                return _additionalCommands;
            }
        }

        protected virtual List<CommandViewModel> CreateAdditionalCommands()
        {
            return new List<CommandViewModel>(); 
        }

        public string Color
        {
            get
            {
                var tmp = _screen;
                while (tmp != null)
                {
                    if (!string.IsNullOrEmpty(tmp.Color)) return tmp.Color;
                    tmp = tmp.Parent;
                }
                return null;
            }
        }

        #region ReportProblemCommand
        private ICommandViewModel _ReportProblemCommand = null;
        public ICommandViewModel ReportProblemCommand
        {
            get
            {
                if (_ReportProblemCommand == null)
                {
                    _ReportProblemCommand = ViewModelFactory.CreateViewModel<ReportProblemCommand.Factory>().Invoke(DataContext);
                }
                return _ReportProblemCommand;
            }
        }
        #endregion

    }
}
