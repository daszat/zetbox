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

namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.SchemaManagement;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public partial class ObjectReferencePropertyTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
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
            IZetboxContext ctx,
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
            string implName = name + Zetbox.API.Helper.ImplementationSuffix;
            string eventName = "On" + name;

            string fkBackingName = "_fk_" + name;
            string publicFKBackingName = "FK_" + name;
            string fkGuidBackingName = "_fk_guid_" + name;

            string referencedImplementation = referencedInterface
                + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;
            string associationName = rel.GetAssociationName() + assocNameSuffix;
            string targetRoleName = otherEnd.RoleName;

            string positionPropertyName = rel.NeedsPositionStorage(endRole)
                ? Construct.ListPositionPropertyName(relEnd)
                : null;

            string inverseNavigatorName = updateInverseNavigator && otherEnd.Navigator != null
                ? otherEnd.Navigator.Name
                : null;
            bool inverseNavigatorIsList = otherEnd.Navigator != null && otherEnd.Navigator.GetIsList();
            bool notifyInverseCollection = false;

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
                false, // ObjRef with relation cannot be calculated
                false);
        }

        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string sourceMember, string targetMember, string targetGuidMember)
        {
            if (list != null)
            {
                if (relDataTypeExportable && !disableExport)
                {
                    list.Add("Serialization.ObjectReferencePropertySerialization", Serialization.SerializerType.ImportExport, moduleNamespace, name, sourceMember, targetMember, targetGuidMember);
                }
                list.Add("Serialization.ObjectReferencePropertySerialization",
                    Templates.Serialization.SerializerType.Service, moduleNamespace, name, sourceMember, targetMember, targetGuidMember);
            }
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Public | MemberAttributes.Assembly;
        }
    }
}
