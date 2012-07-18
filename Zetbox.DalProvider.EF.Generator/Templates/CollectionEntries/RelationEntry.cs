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

namespace Zetbox.DalProvider.Ef.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class RelationEntry
        : Templates.CollectionEntries.RelationEntry
    {

        public RelationEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Relation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            base.ApplyClassAttributeTemplate();
            this.WriteObjects(@"    [EdmEntityType(NamespaceName=""Model"", Name=""", rel.GetRelationClassName(), @""")]");
            this.WriteLine();
        }

        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            // TODO: create/use ObjectReference*IMPLEMENTATION* instead (_fk* can already be made available)

            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string moduleNamespace = rel.Module.Namespace;
            string implName = propertyName + Zetbox.API.Helper.ImplementationSuffix;
            string eventName = "On" + propertyName;

            string fkBackingName = "_fk_" + propertyName;
            string fkGuidBackingName = "_fk_guid_" + propertyName;

            string referencedInterface = relEnd.Type.GetDataTypeString();
            string referencedImplementation = referencedInterface
                + Host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;
            string associationName = rel.GetAssociationName() + "_" + endRole.ToString();
            string targetRoleName = relEnd.RoleName;

            string positionPropertyName = rel.NeedsPositionStorage(endRole)
                ? propertyName + Zetbox.API.Helper.PositionSuffix
                : null;

            string inverseNavigatorName = null;
            bool inverseNavigatorIsList = false;

            bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
            bool relDataTypeExportable = rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable();
            bool callGetterSetterEvents = false;

            Templates.Properties.ObjectReferencePropertyTemplate.Call(Host,
                ctx,
                MembersToSerialize,
                moduleNamespace,
                "/* not member of an interface, 'ownInterface' should not be used */",
                propertyName,
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
                false, // ObjRef with relation cannot be calculated
                false);
        }
    }
}
