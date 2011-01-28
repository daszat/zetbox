
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class ObjectReferencePropertyTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            ObjectReferenceProperty prop, bool callGetterSetterEvents,
            bool updateInverseNavigator)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string name = prop.Name;
            string ownInterface = prop.ObjectClass.GetDataTypeString();
            string referencedInterface = String.Format(
                "{0}.{1}",
                prop.GetReferencedObjectClass().Module.Namespace,
                prop.GetReferencedObjectClass().Name);

            var rel = RelationExtensions.Lookup(ctx, prop);
            var endRole = rel.GetEnd(prop).GetRole();
            var assocNameSuffix = String.Empty;

            Call(host, ctx, serializationList,
                ownInterface, name, referencedInterface, rel, endRole, callGetterSetterEvents, updateInverseNavigator, assocNameSuffix);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Serialization.SerializationMembersList serializationList,
            string ownInterface,
            string name,
            string referencedInterface,
            Relation rel,
            RelationEndRole endRole,
            bool callGetterSetterEvents,
            bool updateInverseNavigator,
            string assocNameSuffix)
        {
            // TODO: split off relation expansion in own Call() method
            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string moduleNamespace = rel.Module.Namespace;
            string implName = name + Kistl.API.Helper.ImplementationSuffix;
            string eventName = "On" + name;

            string fkBackingName = "_fk_" + name;
            string fkGuidBackingName = "_fk_guid_" + name;

            string referencedImplementation = referencedInterface
                + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;
            string associationName = rel.GetAssociationName() + assocNameSuffix;
            string targetRoleName = otherEnd.RoleName;

            string positionPropertyName = rel.NeedsPositionStorage(endRole)
                ? Construct.ListPositionPropertyName(relEnd)
                : null;

            string inverseNavigatorName = updateInverseNavigator && otherEnd.Navigator != null
                ? otherEnd.Navigator.Name
                : null;
            bool inverseNavigatorIsList = otherEnd.Navigator != null && otherEnd.Navigator.GetIsList();

            bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
            bool relDataTypeExportable = rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable();

            Call(host,
                ctx,
                serializationList,
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
                callGetterSetterEvents);
        }

        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string memberName, string fkBackingName)
        {
            if (list != null)
            {
                if (relDataTypeExportable)
                {
                    list.Add("Serialization.ObjectReferencePropertySerialization", Serialization.SerializerType.ImportExport, moduleNamespace, name, memberName);
                }
                list.Add(Serialization.SerializerType.Service, moduleNamespace, name, fkBackingName);
            }
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Public | MemberAttributes.Assembly;
        }
    }
}
