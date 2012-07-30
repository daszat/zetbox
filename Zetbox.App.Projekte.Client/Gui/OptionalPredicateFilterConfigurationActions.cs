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
    public class OptionalPredicateFilterConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public OptionalPredicateFilterConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static void CreateFilterModel(Zetbox.App.GUI.OptionalPredicateFilterConfiguration obj, MethodReturnEventArgs<Zetbox.API.IFilterModel> e, Zetbox.API.IZetboxContext ctx)
        {
            var mdl = new OptionalPredicateFilterModel();
            mdl.Label = obj.Label;
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromExpression(obj.Predicate);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.RequestedKind = obj.RequestedKind;

            var valueMdl = new BoolValueModel("", "", false, false);
            valueMdl.Value = false;
            mdl.FilterArguments.Add(new FilterArgumentConfig(valueMdl, /*cfg.ArgumentViewModel ?? */ ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableBoolPropertyViewModel.Find(FrozenContext)));

            e.Result = mdl;
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.GUI.OptionalPredicateFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_OptionalPredicateFilterViewModel.Find(obj.Context);
        }
    }
}
