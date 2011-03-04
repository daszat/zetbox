
namespace Kistl.DalProvider.Ef.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public partial class RelationEntry
        : Templates.CollectionEntries.RelationEntry
    {

        public RelationEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            base.ApplyClassAttributeTemplate();
            this.WriteObjects(@"    [EdmEntityType(NamespaceName=""Model"", Name=""", rel.GetRelationClassName(), @""")]");
            this.WriteLine();
        }

        protected override void ApplyChangesFromBody()
        {
            base.ApplyChangesFromBody();
            this.WriteLine("            me._fk_A = other._fk_A;");
            this.WriteLine("            me._fk_B = other._fk_B;");
        }

        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            // TODO: create/use ObjectReference*IMPLEMENTATION* instead (_fk* can already be made available)

            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string moduleNamespace = rel.Module.Namespace;
            string implName = propertyName + Kistl.API.Helper.ImplementationSuffix;
            string eventName = "On" + propertyName;

            string fkBackingName = "_fk_" + propertyName;
            string fkGuidBackingName = "_fk_guid_" + propertyName;

            string referencedInterface = relEnd.Type.GetDataTypeString();
            string referencedImplementation = referencedInterface
                + Host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;
            string associationName = rel.GetAssociationName() + "_" + endRole.ToString();
            string targetRoleName = relEnd.RoleName;

            string positionPropertyName = rel.NeedsPositionStorage(endRole)
                ? propertyName + Kistl.API.Helper.PositionSuffix
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
                callGetterSetterEvents);
        }
    }
}
