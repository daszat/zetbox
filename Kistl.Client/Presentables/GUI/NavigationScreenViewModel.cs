
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
        DefaultKind = "Kistl.App.GUI.NavigationScreen",
        Description = "Displays the NavigationScreen and the reachable Children, either as list or tree.")]
    public class NavigationScreenViewModel
        : ViewModel
    {
        public new delegate NavigationScreenViewModel Factory(IKistlContext dataCtx, NavigationScreenViewModel parent, NavigationScreen screen);

        public static NavigationScreenViewModel Create(IModelFactory ModelFactory, IKistlContext dataCtx, NavigationScreenViewModel parent, NavigationScreen screen)
        {
            if (ModelFactory == null) throw new ArgumentNullException("ModelFactory");
            if (screen == null) throw new ArgumentNullException("screen");

            if (screen.ViewModelDescriptor != null)
            {
                var t = screen.ViewModelDescriptor.ViewModelRef.AsType(true);
                return ModelFactory.CreateViewModel<NavigationScreenViewModel.Factory>(t).Invoke(dataCtx, parent, screen);
            }
            else
            {
                return ModelFactory.CreateViewModel<NavigationScreenViewModel.Factory>().Invoke(dataCtx, parent, screen);
            }
        }

        private readonly NavigationScreen _screen;
        private readonly NavigationScreenViewModel _parent;
        private readonly ObservableCollection<NavigationScreenViewModel> _children = new ObservableCollection<NavigationScreenViewModel>();
        private readonly ReadOnlyObservableCollection<NavigationScreenViewModel> _childrenRO;

        private readonly ObservableCollection<CommandModel> _additionalCommands = new ObservableCollection<CommandModel>();
        private readonly ReadOnlyObservableCollection<CommandModel> _additionalCommandsRO;
        
        private NavigatorViewModel _displayer = null;

        public NavigationScreenViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreenViewModel parent, NavigationScreen screen)
            : base(dependencies, dataCtx)
        {
            if (screen == null) throw new ArgumentNullException("screen");
            if (parent == null && screen.Parent != null) throw new ArgumentOutOfRangeException("parent", "parent missing");
            if (parent != null && parent._screen != screen.Parent) throw new ArgumentOutOfRangeException("parent", "inconsistent parent found");

            _parent = parent;
            _screen = screen;
            foreach (var s in _screen.Children)
            {
                _children.Add(NavigationScreenViewModel.Create(ModelFactory, DataContext, this, s));
            }
            _childrenRO = new ReadOnlyObservableCollection<NavigationScreenViewModel>(_children);
            _additionalCommandsRO = new ReadOnlyObservableCollection<CommandModel>(_additionalCommands);
        }

        public override string Name
        {
            get { return _screen.Title; }
        }

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
                    foreach (var c in _children)
                    {
                        c.Displayer = value;
                    }
                    OnPropertyChanged("Displayer");
                }
            }
        }

        public NavigationScreenViewModel Parent
        {
            get { return _parent; }
        }

        public ReadOnlyObservableCollection<NavigationScreenViewModel> Children
        {
            get
            {
                return _childrenRO;
            }
        }

        protected ObservableCollection<CommandModel> AdditionalCommandsRW
        {
            get { return _additionalCommands; }
        }

        public ReadOnlyObservableCollection<CommandModel> AdditionalCommands
        {
            get { return _additionalCommandsRO; }
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
    }
}
