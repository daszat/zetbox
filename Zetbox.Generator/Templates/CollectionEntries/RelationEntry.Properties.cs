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

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public partial class RelationEntry
    {
        protected override void ApplyAPropertyTemplate()
        {
            ApplyObjectReferenceProperty(rel, RelationEndRole.A, "A");
        }

        protected override void ApplyBPropertyTemplate()
        {
            ApplyObjectReferenceProperty(rel, RelationEndRole.B, "B");
        }

        protected override void ApplyAIndexPropertyTemplate()
        {
            ApplyIndexPropertyTemplate(rel, RelationEndRole.A);
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            ApplyIndexPropertyTemplate(rel, RelationEndRole.B);
        }

        protected virtual void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            // TODO: create/use ObjectReference*IMPLEMENTATION* instead (_fk* can already be made available)
            string moduleNamespace = rel.Module.Namespace;
            string ownInterface = "/* not member of an interface, 'ownInterface' should not be used */";
            string name = propertyName;
            string implName = propertyName + ImplementationPropertySuffix;
            string eventName = "On" + name;
            string fkBackingName = "_fk_" + name;
            string fkGuidBackingName = "_fk_guid_" + name;
            string referencedInterface = relEnd.Type.GetDataTypeString();
            string referencedImplementation = referencedInterface + ImplementationSuffix;
            string associationName = rel.GetAssociationName();
            string targetRoleName = otherEnd.RoleName;
            string positionPropertyName = rel.NeedsPositionStorage(endRole)
                ? name + Zetbox.API.Helper.PositionSuffix
                : null;
            string inverseNavigatorName = null;
            bool inverseNavigatorIsList = false;
            bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
            bool relDataTypeExportable = rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable();
            bool callGetterSetterEvents = false;

            Properties.ObjectReferencePropertyTemplate.Call(Host,
                ctx,
                MembersToSerialize,
                moduleNamespace,
                ownInterface,
                name,
                implName,
                eventName,
                fkBackingName,
                fkGuidBackingName,
                referencedInterface,
                referencedImplementation,
                associationName,
                targetRoleName,
                positionPropertyName,
                inverseNavigatorName,
                inverseNavigatorIsList,
                eagerLoading,
                relDataTypeExportable,
                callGetterSetterEvents,
                false); // ObjRef with relation cannot be calculated
        }

        protected virtual void ApplyIndexPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            if (rel.NeedsPositionStorage(endRole))
            {
                // provided by ObjectReferencePropertyTemplate
                string posBackingStore = "_" + endRole + Zetbox.API.Helper.PositionSuffix;

                // is serialized by the ObjectReferenceProperty
                //this.MembersToSerialize.Add(
                //    Serialization.SerializerType.All,
                //    relEnd.Type.Module.Namespace,
                //    endRole + Zetbox.API.Helper.PositionSuffix,
                //    posBackingStore);

                this.WriteObjects("        public int? ", endRole, "Index { get { return ",
                    posBackingStore, "; } set { ",
                    posBackingStore, " = value; } }");
                this.WriteLine();
            }
            else if (IsOrdered())
            {
                this.WriteLine("/// <summary>ignored implementation for INewListEntry</summary>");
                this.WriteObjects("public int? ", endRole, "Index { get { return null; } set { } }");
            }
        }
    }
}
