using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Arebis.CodeGeneration;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class CollectionEntryListProperty
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Templates.Implementation.SerializationMembersList list, Relation rel, RelationEndRole endRole)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty", ctx,
                list,
                rel, endRole);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            //if (list != null)
            //    list.Add("Implementation.ObjectClasses.CollectionSerialization", SerializerType.Xml, memberName);
            if (list != null && eagerLoading)
            {
                list.Add("Implementation.ObjectClasses.EagerLoadingSerialization", Templates.Implementation.SerializerType.Binary, null, null, memberName, false);
            }
        }
    }
}
