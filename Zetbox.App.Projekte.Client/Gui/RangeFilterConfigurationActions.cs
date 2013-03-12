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
    public static class RangeFilterConfigurationActions
    {
        [Invocation]
        public static void CreateFilterModel(Zetbox.App.GUI.RangeFilterConfiguration obj, MethodReturnEventArgs<Zetbox.API.IFilterModel> e, Zetbox.API.IZetboxContext ctx)
        {
            var mdl = new RangeFilterModel();
            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.RequestedKind = obj.RequestedKind;

            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(ctx, true), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(ctx, true), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            e.Result = mdl;
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.GUI.RangeFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_RangeFilterViewModel.Find(obj.Context);
        }
    }
}
