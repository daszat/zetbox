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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class RelationEntry
        : Templates.CollectionEntries.RelationEntry
    {
        public RelationEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Relation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();
            ObjectClasses.Constructors.Call(Host, ctx,
                new ObjectClasses.Constructors.CompoundInitialisationDescriptor[0],
                new string[0],
                GetCeInterface(),
                GetCeClassName(),
                null);
        }

        protected override void ApplyIdPropertyTemplate()
        {
            // inherited from Base
            //base.ApplyIdPropertyTemplate();
        }

        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
            var nav = relEnd.Navigator;

            // TODO: create/use ObjectReference*IMPLEMENTATION* instead (_fk* can already be made available)
            string moduleNamespace = rel.Module.Namespace;
            string ownInterface = moduleNamespace + "." + rel.GetRelationClassName() + ImplementationSuffix;
            string name = propertyName;
            string implName = propertyName + ImplementationPropertySuffix;
            string eventName = "On" + name;
            string fkBackingName = "_fk_" + name;
            string publicFKBackingName = "FK_" + name;
            string fkGuidBackingName = "_fk_guid_" + name;
            string referencedInterface = relEnd.Type.GetDataTypeString();
            string referencedImplementation = referencedInterface + ImplementationSuffix;
            string associationName = rel.GetAssociationName();
            string targetRoleName = otherEnd.RoleName;
            string positionPropertyName = rel.NeedsPositionStorage(endRole)
                ? name + Zetbox.API.Helper.PositionSuffix
                : null;
            string inverseNavigatorName = nav != null
                ? nav.Name
                : null;
            bool inverseNavigatorIsList = nav != null && nav.GetIsList();
            bool notifyInverseCollection = true;
            bool eagerLoading = nav != null && nav.EagerLoading;
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
                publicFKBackingName,
                fkGuidBackingName,
                referencedInterface,
                referencedImplementation,
                associationName,
                targetRoleName,
                positionPropertyName,
                inverseNavigatorName,
                inverseNavigatorIsList,
                notifyInverseCollection,
                eagerLoading,
                relDataTypeExportable,
                callGetterSetterEvents,
                false,
                false);
        }

        protected override void ApplyAIndexPropertyTemplate()
        {
            // delegate interface to actual implementation
            Templates.Properties.DelegatingProperty.Call(Host, ctx, "AIndex", "int?", "this.A" + Zetbox.API.Helper.PositionSuffix, "int?");
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            // delegate interface to actual implementation
            Templates.Properties.DelegatingProperty.Call(Host, ctx, "BIndex", "int?", "this.B" + Zetbox.API.Helper.PositionSuffix, "int?");
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string interfaceName = GetCeInterface();

            List<KeyValuePair<string, string>> typeAndNameList = new List<KeyValuePair<string, string>>(){
                new KeyValuePair<string, string>(Mappings.ObjectClassHbm.GetProxyTypeReference(rel.A.Type, this.Settings), "A"),
                new KeyValuePair<string, string>(Mappings.ObjectClassHbm.GetProxyTypeReference(rel.B.Type, this.Settings), "B"),
            };

            if (IsOrdered())
            {
                typeAndNameList.Add(new KeyValuePair<string, string>("int?", "A" + Zetbox.API.Helper.PositionSuffix));
                typeAndNameList.Add(new KeyValuePair<string, string>("int?", "B" + Zetbox.API.Helper.PositionSuffix));
            }

            if (IsExportable())
            {
                typeAndNameList.Add(new KeyValuePair<string, string>("Guid", "ExportGuid"));
            }

            RememberToDeleteTemplate.Call(Host,
                true,
                true, "A", rel.A.Navigator != null ? rel.A.Navigator.Name : null,
                true, "B", rel.B.Navigator != null ? rel.B.Navigator.Name : null);

            ObjectClasses.ProxyClass.Call(Host, ctx, interfaceName, new KeyValuePair<string, string>[0], typeAndNameList);
        }

        protected override string GetExportGuidBackingStoreReference()
        {
            return "this.Proxy.ExportGuid";
        }

        protected override void ApplyExportGuidPropertyTemplate()
        {
            Properties.ExportGuidProperty.Call(Host, ctx, this.MembersToSerialize, rel.Module.Namespace, rel.GetRelationFullName());
        }

        protected override void ApplyReloadReferenceBody()
        {
            {
                string referencedInterface = rel.A.Type.Module.Namespace + "." + rel.A.Type.Name;
                string referencedImplementation = Mappings.ObjectClassHbm.GetWrapperTypeReference(rel.A.Type, Host.Settings);
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "A", null, "_fk_A", "_fk_guid_A", IsExportable());
            }
            {
                string referencedInterface = rel.B.Type.Module.Namespace + "." + rel.B.Type.Name;
                string referencedImplementation = Mappings.ObjectClassHbm.GetWrapperTypeReference(rel.B.Type, Host.Settings);
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "B", null, "_fk_B", "_fk_guid_B", IsExportable());
            }
        }
    }
}
