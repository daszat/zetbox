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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// Sets the Property it's default ValueModelDescriptor.
    /// GUI Tasks - so Client Actions.
    /// Note: OnNotifyCreated should always be implemented on the client side. importing or deploying also calls this event.
    /// </summary>
    [Implementor]
    public static class CompoundObjectPropertyActions
    {
        [Invocation]
        public static void NotifyCreated(Zetbox.App.Base.CompoundObjectProperty obj)
        {
            obj.ValueModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_CompoundObjectPropertyViewModel.Find(obj.Context);
        }

        [Invocation]
        public static void postSet_CompoundObjectDefinition(CompoundObjectProperty obj, PropertyPostSetterEventArgs<CompoundObject> e)
        {
            var def = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_CompoundObjectPropertyViewModel.Find(obj.Context);
            if (obj.ValueModelDescriptor == def && e.OldValue == null && e.NewValue != null && e.NewValue.DefaultPropertyViewModelDescriptor != null)
            {
                // Only once, during initialize
                obj.ValueModelDescriptor = e.NewValue.DefaultPropertyViewModelDescriptor;
            }
        }
    }
}
