
namespace Kistl.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public partial class CollectionSerialization
    {
        public static void Add(SerializationMembersList list, IKistlContext ctx, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool inPlace)
        {
            list.Add("Serialization.CollectionSerialization", Serialization.SerializerType.All, xmlnamespace, xmlname, collectionName, orderByValue, inPlace);
        }

        public virtual bool ShouldSerialize()
        {
            return true;
        }
    }
}
