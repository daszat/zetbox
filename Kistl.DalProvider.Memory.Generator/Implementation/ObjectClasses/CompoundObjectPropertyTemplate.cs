
namespace Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using ClientObjects = Kistl.Server.Generators.ClientObjects;
    using Templates = Kistl.Server.Generators.Templates;

    public class CompoundObjectPropertyTemplate
        : ClientObjects.Implementation.ObjectClasses.CompoundObjectPropertyTemplate
    {
        public CompoundObjectPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList,
            CompoundObjectProperty prop, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType)
            : base(_host, ctx, serializationList, prop, propName, backingPropertyName + MemoryGenerator.Suffix, backingStoreName, coType, coImplementationType + MemoryGenerator.Suffix)
        {
        }
    }
}
