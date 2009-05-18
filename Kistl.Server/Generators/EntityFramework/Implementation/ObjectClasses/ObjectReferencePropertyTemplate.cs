using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;
using Kistl.API;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class ObjectReferencePropertyTemplate
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList membersToSerialize,
            string propertyName, string collectionEntryAssociationName, string roleName,
            string relDataTypeString, string relDataTypeStringImpl, bool needsPositionStorage)
        {
            host.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                membersToSerialize,
                propertyName, collectionEntryAssociationName, roleName,
                relDataTypeString, relDataTypeStringImpl,
                needsPositionStorage);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            // TODO: XML Namespace
            if (list != null)
                list.Add("Implementation.ObjectClasses.SimplePropertySerialization", SerializerType.All, "http://dasz.at/Kistl", name, memberName);
        }
    }
}
