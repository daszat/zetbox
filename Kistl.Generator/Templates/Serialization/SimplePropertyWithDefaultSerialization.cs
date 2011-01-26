
namespace Kistl.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class SimplePropertyWithDefaultSerialization
    {
        public static void AddToSerializers(SerializationMembersList list,
            SerializerType type,
            string xmlnamespace,
            string xmlname,
            string memberType,
            string memberName,
            string isSetFlagName)
        {
            if (list != null)
            {
                list.Add(
                    "Serialization.SimplePropertyWithDefaultSerialization", 
                    type, 
                    xmlnamespace,
                    xmlname,
                    memberType, 
                    memberName,
                    isSetFlagName);
            }
        }
    }
}