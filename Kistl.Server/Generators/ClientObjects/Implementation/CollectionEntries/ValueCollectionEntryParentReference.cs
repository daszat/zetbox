
namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public partial class ValueCollectionEntryParentReference
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx,
           Templates.Implementation.SerializationMembersList membersToSerialize,
           string className, string propertyName, string moduleName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.CollectionEntries.ValueCollectionEntryParentReference",
                ctx, membersToSerialize, className, propertyName, moduleName);
        }
    }
}
