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
            ObjectReferenceProperty prop)
        {
            Debug.Assert(!prop.IsList);

            string name = prop.PropertyName;
            string ownInterface = prop.ObjectClass.GetDataTypeString();
            string referencedInterface = prop.ReferenceObjectClass.Module.Namespace + "." + prop.ReferenceObjectClass.ClassName;

            var rel = RelationExtensions.Lookup(ctx, prop);
            var endRole = (RelationEndRole)rel.GetEnd(prop).Role;
            Call(host, ctx, serializationList,
                name, ownInterface, referencedInterface, rel, endRole);

        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name,
            string ownInterface,
            string referencedInterface,
            Relation rel,
            RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string efName = name + Kistl.API.Helper.ImplementationSuffix;
            string fkBackingName = "_fk_" + name;

            bool hasInverseNavigator = otherEnd.Navigator != null;

            Call(host, ctx, serializationList,
                name, efName, fkBackingName,
                ownInterface, referencedInterface, 
                rel, endRole,
                hasInverseNavigator, rel.NeedsPositionStorage(endRole));
        }


        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name,
            string efName,
            string fkBackingName,
            string ownInterface,
            string referencedInterface,
            Relation rel,
            RelationEndRole endRole,
            bool hasInverseNavigator,
            bool hasPositionStorage)
        {
            host.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx, serializationList,
                name, efName, fkBackingName, ownInterface, referencedInterface, rel, endRole, hasInverseNavigator, hasPositionStorage);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            // TODO: XML Namespace
            if (list != null)
                list.Add(SerializerType.All, "http://dasz.at/Kistl", name, memberName);
        }
    }
}
