
namespace Kistl.DalProvider.Memory.Generator.Implementation.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Server.Generators.Extensions;
    using ClientObjects = Kistl.Server.Generators.ClientObjects;
    using Templates = Kistl.Server.Generators.Templates;

    public sealed class ValueCollectionEntry
         : Kistl.Server.Generators.Templates.Implementation.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host, ctx, prop)
        {
        }

        protected override void ApplyParentReferencePropertyTemplate(Property prop, string propertyName)
        {
            ClientObjects.Implementation.CollectionEntries.ValueCollectionEntryParentReference.Call(Host, ctx,
                MembersToSerialize, prop.ObjectClass.Name, propertyName);
        }

        protected override void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop, string propertyName)
        {
            Implementation.ObjectClasses.CompoundObjectPropertyTemplate.Call(Host, ctx, MembersToSerialize, prop, propertyName);
        }

        protected override string GetCeBaseClassName()
        {
            return "BaseMemoryCollectionEntry";
        }
    }
}
