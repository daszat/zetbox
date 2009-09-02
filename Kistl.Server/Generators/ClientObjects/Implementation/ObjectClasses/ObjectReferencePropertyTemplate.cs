using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class ObjectReferencePropertyTemplate
    {

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            ObjectReferenceProperty prop, bool callGetterSetterEvents)
        {
            Debug.Assert(!prop.IsList);

            string name = prop.PropertyName;
            string ownInterface = prop.ObjectClass.GetDataTypeString();
            string referencedInterface = prop.GetReferencedObjectClass().Module.Namespace + "." + prop.GetReferencedObjectClass().ClassName;

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
                hasInverseNavigator, rel.NeedsPositionStorage(endRole), callGetterSetterEvents);
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
            bool callGetterSetterEvents)
        {
            host.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx, serializationList,
                name, efName, fkBackingName, fkGuidBackingName, ownInterface, referencedInterface, rel, endRole, hasInverseNavigator, hasPositionStorage, callGetterSetterEvents);
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
