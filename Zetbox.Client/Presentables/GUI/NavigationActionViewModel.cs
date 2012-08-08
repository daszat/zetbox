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
    using Zetbox.API.Common;
    using Zetbox.App.GUI;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.App.Base;

    [ViewModelDescriptor]
    public class NavigationActionViewModel
        : NavigationEntryViewModel
    {
        public new delegate NavigationActionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationAction screen);

        public NavigationActionViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, NavigationEntry screen)
            : base(dependencies, dataCtx, parent, screen)
        {
        }

        public new NavigationAction Screen { get { return (NavigationAction)base.Screen; } }

        public override bool IsScreen
        {
            get { return false; }
        }

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
