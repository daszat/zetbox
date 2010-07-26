
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
        public new delegate NavigationScreenViewModel Factory(IKistlContext dataCtx, NavigationScreen screen);

        private readonly NavigationScreen _screen;
        private readonly ObservableCollection<NavigationScreenViewModel> _children = new ObservableCollection<NavigationScreenViewModel>();
        private readonly ReadOnlyObservableCollection<NavigationScreenViewModel> _childrenRO;
        private NavigatorViewModel _displayer = null;

        public NavigationScreenViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreen screen)
            : base(dependencies, dataCtx)
        {
            _screen = screen;
            foreach (var s in _screen.Children)
            {
                var t = s.ViewModelDescriptor.ViewModelRef.AsType(true);
                _children.Add(ModelFactory.CreateViewModel<NavigationScreenViewModel.Factory>(t).Invoke(DataContext, s));
            }
            _childrenRO = new ReadOnlyObservableCollection<NavigationScreenViewModel>(_children);
        }

        public override string Name
        {
            get { return _screen.Title; }
        }

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

        public ReadOnlyObservableCollection<NavigationScreenViewModel> Children
        {
            get
            {
                return _childrenRO;
            }
        }
    }
}
