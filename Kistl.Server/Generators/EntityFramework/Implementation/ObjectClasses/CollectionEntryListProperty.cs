using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class CollectionEntryListProperty
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Templates.Implementation.SerializationMembersList list, Relation rel, RelationEndRole endRole)
        {
            host.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty", ctx,
                list,
                rel, endRole);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            //if (list != null)
            //    list.Add("Implementation.ObjectClasses.CollectionSerialization", SerializerType.Xml, memberName);
        }
    }
}
