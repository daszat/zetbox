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
            string relDataTypeString, string relDataTypeStringImpl, bool needsPositionStorage, bool relDataTypeExportable)
        {
            host.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                membersToSerialize,
                propertyName, collectionEntryAssociationName, roleName,
                relDataTypeString, relDataTypeStringImpl,
                needsPositionStorage, relDataTypeExportable);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            // TODO: XML Namespace
            if (list != null)
            {
                if(relDataTypeExportable)
                    list.Add("Implementation.ObjectClasses.ObjectReferencePropertySerialization", SerializerType.All, "http://dasz.at/Kistl", name, memberName);
                else
                    list.Add("Implementation.ObjectClasses.ObjectReferencePropertySerialization", SerializerType.All, null, null, memberName);
            }
        }
    }
}
