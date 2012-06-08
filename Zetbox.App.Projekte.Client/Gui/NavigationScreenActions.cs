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
namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables;

    [Implementor]
    public class NavigationScreenActions
    {
        private static IViewModelFactory _modelFactory;

        public NavigationScreenActions(IViewModelFactory factory)
        {
            _modelFactory = factory;
        }

        [Invocation]
        public static void GetDefaultViewModel(Zetbox.App.GUI.NavigationScreen obj, MethodReturnEventArgs<object> e, Zetbox.API.IZetboxContext dataCtx, System.Object parent)
        {
            e.Result = _modelFactory.CreateViewModel<NavigationScreenViewModel.Factory>().Invoke(dataCtx, (ViewModel)parent, obj);
        }
    }
}
