using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public class ObjectListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole)
        {
            host.CallTemplate("Implementation.ObjectClasses.ObjectListProperty", ctx,
                serializationList,
                rel, endRole);
        }
    }
}
