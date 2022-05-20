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
    public static class SinglePropertyFilterConfigurationActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task CreateFilterModel(Zetbox.App.GUI.SinglePropertyFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e, Zetbox.API.IZetboxContext ctx)
        {
            var mdl = new SingleValueFilterModel();
            var prop = obj.Property;

            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.RefreshOnFilterChanged = obj.RefreshOnFilterChanged;
            mdl.ValueSource = FilterValueSource.FromProperty(prop);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.RequestedKind = obj.RequestedKind;

            mdl.FilterArguments.Add(new FilterArgumentConfig(prop.GetDetachedValueModel(ctx, true), /*cfg.ArgumentViewModel ?? */ prop.ValueModelDescriptor));
            if (prop is StringProperty)
            {
                mdl.Operator = FilterOperators.Contains;
            }
            else if (prop is EnumerationProperty)
            {
                mdl.RefreshOnFilterChanged = true;
            }
            else if (prop is ObjectReferenceProperty)
            {
                mdl.RefreshOnFilterChanged = true;
            }
            e.Result = mdl;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task NotifyCreated(Zetbox.App.GUI.SinglePropertyFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(obj.Context);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
