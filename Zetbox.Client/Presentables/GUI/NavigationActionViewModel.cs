
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
    public class NavigationActionViewModel
        : NavigationEntryViewModel
    {
        public new delegate NavigationActionViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationAction screen);

        public NavigationActionViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, NavigationEntry screen)
            : base(dependencies, dataCtx, parent, screen)
        {
        }

        public new NavigationAction Screen { get { return (NavigationAction)base.Screen; } }

        private ICommandViewModel _ExecuteCommand = null;
        public override ICommandViewModel ExecuteCommand
        {
            get
            {
                if (_ExecuteCommand == null)
                {
                    _ExecuteCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, Name, "", 
                        Execute, 
                        CanExecute, 
                        GetReason);
                }
                return _ExecuteCommand;
            }
        }

        public virtual bool CanExecute()
        {
            return false;
        }

        public virtual void Execute()
        {
        }

        public virtual string GetReason()
        {
            return "This is the empty implementation, derive your own view model and override CanExecute/Execute/GetReason";
        }
    }
}
