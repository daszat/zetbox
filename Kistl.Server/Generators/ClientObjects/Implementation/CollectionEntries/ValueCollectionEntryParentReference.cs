using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntryParentReference
    {
        public static void Call(IGenerationHost host, IKistlContext ctx,
           Templates.Implementation.SerializationMembersList membersToSerialize,
           string className, string propertyName)
        {
            host.CallTemplate("Implementation.CollectionEntries.ValueCollectionEntryParentReference",
                ctx, membersToSerialize, className, propertyName);
        }
    }
}
