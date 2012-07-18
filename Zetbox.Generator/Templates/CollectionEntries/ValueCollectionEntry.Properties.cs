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

namespace Zetbox.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;

    public partial class ValueCollectionEntry
    {
        protected override void ApplyAPropertyTemplate()
        {
            var interfaceType = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;

            CollectionEntries.ValueCollectionEntryParentReference.Call(Host, ctx,
                MembersToSerialize, interfaceType, "Parent", prop.Module.Namespace, prop.DisableExport == true);

            Properties.DelegatingProperty.Call(
                Host, ctx,
                "ParentObject", "Zetbox.API.IDataObject",
                "Parent", interfaceType + ImplementationSuffix);
        }

        protected override void ApplyBPropertyTemplate()
        {
            string interfaceType = prop.GetElementTypeString();
            string implementationType = interfaceType;

            var cop = prop as CompoundObjectProperty;
            if (cop != null)
            {
                Properties.CompoundObjectPropertyTemplate.Call(
                    Host, ctx, MembersToSerialize,
                    prop as CompoundObjectProperty, "Value",
                    false, false);
                implementationType = interfaceType + ImplementationSuffix;
            }
            var vtp = prop as ValueTypeProperty;
            if (vtp != null)
            {
                Properties.NotifyingValueProperty.Call(
                    Host, ctx, MembersToSerialize,
                    interfaceType, "Value", vtp.Module.Namespace, vtp.IsCalculated, vtp.DisableExport == true);
            }

            Properties.DelegatingProperty.Call(Host, ctx, "ValueObject", "object", "Value", implementationType);
        }

        protected override sealed void ApplyAIndexPropertyTemplate()
        {
            // never used
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            Properties.NotifyingValueProperty.Call(
                Host, ctx, MembersToSerialize,
                "int?", "Index", prop.GetCollectionEntryNamespace(), false, prop.DisableExport == true);
        }
    }
}
