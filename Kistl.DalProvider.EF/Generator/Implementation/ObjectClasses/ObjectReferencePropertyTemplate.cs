using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public partial class ObjectReferencePropertyTemplate
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Kistl.Server.Generators.Templates.Implementation.SerializationMembersList membersToSerialize,
            string propertyName, string collectionEntryAssociationName, string roleName,
            string relDataTypeString, string relDataTypeStringImpl, bool needsPositionStorage, string positionPropertyName,
            bool relDataTypeExportable, string moduleNamespace,
            bool eagerLoading, bool callGetterSetterEvents, bool isReloadable)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                membersToSerialize,
                propertyName, collectionEntryAssociationName, roleName,
                relDataTypeString, relDataTypeStringImpl,
                needsPositionStorage, positionPropertyName, relDataTypeExportable, moduleNamespace,
                eagerLoading, callGetterSetterEvents, isReloadable);
        }

        protected virtual void AddSerialization(Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, string memberName)
        {
            if (list != null)
            {
                if (relDataTypeExportable)
                {
                    list.Add("Implementation.ObjectClasses.ObjectReferencePropertySerialization", SerializerType.ImportExport, moduleNamespace, name, memberName);
                }
                list.Add("Implementation.ObjectClasses.ObjectReferencePropertySerialization", SerializerType.Service, moduleNamespace, name, memberName);
                if (eagerLoading)
                {
                    list.Add("Implementation.ObjectClasses.EagerObjectLoadingSerialization", SerializerType.Binary, moduleNamespace, name, memberName);
                }
            }
        }
    }
}
