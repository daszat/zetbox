
namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    // Placeholder
    public static class ObjectListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (rel == null) { throw new ArgumentNullException("rel"); }

            // TODO: fix this template path
            host.CallTemplate("ObjectClasses.ObjectListProperty", ctx,
                serializationList,
                rel, endRole);
        }
    }
}
