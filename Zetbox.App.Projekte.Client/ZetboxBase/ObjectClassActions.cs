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
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client;
    using Zetbox.Client.Presentables;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public static class ObjectClassActions
    {
        [Invocation]
        public static void NotifyCreated(ObjectClass obj)
        {
            obj.DefaultViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_DataObjectViewModel.Find(obj.Context);
        }

        [Invocation]
        public static void NotifyDeleting(ObjectClass obj)
        {
            var ctx = obj.Context;
            foreach (var ac in obj.AccessControlList.ToList())
            {
                ctx.Delete(ac);
            }

            foreach (var f in obj.FilterConfigurations.ToList())
            {
                ctx.Delete(f);
            }

            foreach (var cls in obj.SubClasses.ToList())
            {
                ctx.Delete(cls);
            }

            foreach (var rel in obj.GetRelations().ToList())
            {
                ctx.Delete(rel);
            }
        }
    }
}
