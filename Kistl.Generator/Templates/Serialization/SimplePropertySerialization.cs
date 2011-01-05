
namespace Kistl.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class SimplePropertySerialization
    {
        public static void AddToSerializers(SerializationMembersList list,
            SerializerType type,
            string xmlnamespace,
            string xmlname,
            string memberType,
            string memberName)
        {
            if (list != null)
            {
                list.Add(
                    "Serialization.SimplePropertySerialization", 
                    type, 
                    xmlnamespace,
                    xmlname,
                    memberType, 
                    memberName);
            }
        }
    }
}