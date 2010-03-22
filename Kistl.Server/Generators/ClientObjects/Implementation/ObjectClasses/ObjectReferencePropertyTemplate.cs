
namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators.Templates.Implementation;

    public partial class ObjectReferencePropertyTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            ObjectReferenceProperty prop, bool callGetterSetterEvents)
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
            Call(host, ctx, serializationList,
                name, ownInterface, referencedInterface, rel, endRole, callGetterSetterEvents);

        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name,
            string ownInterface,
            string referencedInterface,
            Relation rel,
            RelationEndRole endRole, bool callGetterSetterEvents)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }

            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string efName = name + Kistl.API.Helper.ImplementationSuffix;
            string fkBackingName = "_fk_" + name;
            string fkGuidBackingName = "_fk_guid_" + name;

            bool hasInverseNavigator = otherEnd.Navigator != null;

            Call(host, ctx, serializationList,
                name, efName, fkBackingName, fkGuidBackingName,
                ownInterface, referencedInterface,
                rel, endRole,
                hasInverseNavigator,
                rel.NeedsPositionStorage(endRole),
                Construct.ListPositionPropertyName(relEnd),
                callGetterSetterEvents);
        }


        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name,
            string efName,
            string fkBackingName,
            string fkGuidBackingName,
            string ownInterface,
            string referencedInterface,
            Relation rel,
            RelationEndRole endRole,
            bool hasInverseNavigator,
            bool hasPositionStorage,
            string positionPropertyName,
            bool callGetterSetterEvents)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (rel == null) { throw new ArgumentNullException("rel"); }

            host.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx, serializationList,
                name, efName, fkBackingName, fkGuidBackingName, ownInterface, referencedInterface, rel, endRole,
                hasInverseNavigator, hasPositionStorage, positionPropertyName, callGetterSetterEvents);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            // TODO: XML Namespace
            if (list != null)
            {
                //if (relDataTypeExportable)
                //{
                // list.Add("Implementation.ObjectClasses.ObjectReferencePropertySerialization", SerializerType.ImportExport, "http://dasz.at/Kistl", name, memberName);
                //}
                list.Add(SerializerType.Service, "http://dasz.at/Kistl", name, memberName);
            }
        }
    }
}
