
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
    using Kistl.App.Base;

    [ViewModelDescriptor]
    public class NavigationScreenViewModel
        : NavigationEntryViewModel
    {
        public new delegate NavigationScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public NavigationScreenViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, NavigationEntry screen)
            : base(dependencies, dataCtx, parent, screen)
        {
        }

        public new NavigationScreen Screen { get { return (NavigationSearchScreen)base.Screen; } }

        private ICommandViewModel _ExecuteCommand = null;
        public override ICommandViewModel ExecuteCommand
        {
            get
            {
                if (_ExecuteCommand == null)
                {
                    _ExecuteCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, Name, "",
                        () => Displayer.NavigateTo(this),
                        null,
                        null);
                }
                return _ExecuteCommand;
            }
        }
    }
}
