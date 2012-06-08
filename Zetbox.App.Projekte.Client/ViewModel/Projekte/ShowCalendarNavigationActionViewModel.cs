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
namespace Zetbox.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class ShowCalendarNavigationActionViewModel : NavigationActionViewModel
    {
        public new delegate ShowCalendarNavigationActionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationAction screen);

        private readonly Func<IZetboxContext> _ctxFactory;

        public ShowCalendarNavigationActionViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, NavigationAction screen)
            : base(appCtx, dataCtx, parent, screen)
        {
            _ctxFactory = ctxFactory;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            ViewModelFactory.ShowModel(ViewModelFactory.CreateViewModel<CalendarWorkspaceViewModel.Factory>().Invoke(_ctxFactory(), null), true);
        }
    }
}
