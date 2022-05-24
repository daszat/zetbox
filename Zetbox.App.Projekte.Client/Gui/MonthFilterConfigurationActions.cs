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
    using Zetbox.App.Base;
    using Zetbox.Client;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class MonthFilterConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public MonthFilterConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static async System.Threading.Tasks.Task CreateFilterModel(Zetbox.App.GUI.MonthFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e, Zetbox.API.IZetboxContext ctx)
        {
            var mdl = MonthValueFilterModel.Create(FrozenContext, await obj.GetLabel(), FilterValueSource.FromProperty(obj.Property), obj.IsCurrentMonthDefault ?? false);
            mdl.Required = obj.Required;
            mdl.RefreshOnFilterChanged = obj.RefreshOnFilterChanged;
            e.Result = mdl;
        }

        [Invocation]
        public static System.Threading.Tasks.Task NotifyCreated(Zetbox.App.GUI.MonthFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(obj.Context);

            return System.Threading.Tasks.Task.CompletedTask;
        }

    }
}
