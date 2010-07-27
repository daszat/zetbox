
namespace Kistl.DalProvider.Memory.Generator.Implementation.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using ClientObjects = Kistl.Server.Generators.ClientObjects;
    using Templates = Kistl.Server.Generators.Templates;
    
    public class ValueCollectionEntryParentReference
        : ClientObjects.Implementation.CollectionEntries.ValueCollectionEntryParentReference
    {
        public ValueCollectionEntryParentReference(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList,
            string referencedInterface, string propertyName, string moduleNamespace)
            : base(_host, ctx, serializationList, referencedInterface, propertyName, moduleNamespace)
        {
        }
    }
}
