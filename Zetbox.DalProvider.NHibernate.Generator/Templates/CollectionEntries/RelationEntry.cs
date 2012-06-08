
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
                null,
                false);
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

            // TODO: create/use ObjectReference*IMPLEMENTATION* instead (_fk* can already be made available)
            string moduleNamespace = rel.Module.Namespace;
            string ownInterface = moduleNamespace + "." + rel.GetRelationClassName() + ImplementationSuffix;
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
            string inverseNavigatorName = null; // do not care about inverse navigator
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

            GetDeletedRelatives.Call(Host, "A", "B");

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
