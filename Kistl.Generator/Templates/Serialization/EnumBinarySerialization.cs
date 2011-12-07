
namespace Kistl.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class EnumBinarySerialization
    {
        public static void AddToSerializers(SerializationMembersList list,
            EnumerationProperty prop,
            string backingStoreName)
        {
            string xmlnamespace = prop.Module.Namespace;
            string xmlname = prop.Name;
            string enumerationType = prop.GetElementTypeString();

            AddToSerializers(list,
                SerializerType.All,
                xmlnamespace,
                xmlname,
                backingStoreName,
                enumerationType);
        }

        public static void AddToSerializers(SerializationMembersList list,
            SerializerType type,
            string xmlnamespace,
            string xmlname,
            string backingStoreName,
            string enumerationType)
        {
            if (list != null)
            {
                list.Add("Serialization.EnumBinarySerialization", type, xmlnamespace, xmlname, backingStoreName, enumerationType);
            }
        }
    }
}