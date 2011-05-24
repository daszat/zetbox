
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    public partial class ObjectReferencePropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string sourceMember, string targetMember, string targetGuidMember, string clsFullName, string assocName, string targetRoleName)
        {
            if (list != null)
            {
                if (relDataTypeExportable)
                {
                    list.Add("Serialization.ObjectReferencePropertySerialization",
                        Templates.Serialization.SerializerType.ImportExport, moduleNamespace, name, sourceMember, targetMember, targetGuidMember, clsFullName, assocName, targetRoleName);
                }
                list.Add("Serialization.ObjectReferencePropertySerialization",
                    Templates.Serialization.SerializerType.Service, moduleNamespace, name, sourceMember, targetMember, targetGuidMember, clsFullName, assocName, targetRoleName);
                if (eagerLoading)
                {
                    list.Add("Serialization.EagerObjectLoadingSerialization",
                        Templates.Serialization.SerializerType.Binary, moduleNamespace, name, sourceMember);
                }
            }
        }
    }
}
