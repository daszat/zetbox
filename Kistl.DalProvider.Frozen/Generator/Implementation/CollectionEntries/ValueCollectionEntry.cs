
namespace Kistl.DalProvider.Frozen.Generator.Implementation.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Server.Generators.Extensions;

    public partial class ValueCollectionEntry
        : Kistl.Server.Generators.Templates.Implementation.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host, ctx, prop)
        {
        }

        public override void Generate()
        {
            // skip
        }

        protected override string GetCeBaseClassName()
        {
            // TODO: implement full set of extension points on Templates.Implementation.CollectionEntries.ObjectCollectionEntry
            return "Kistl.DalProvider.Frozen.BaseFrozenObject";
        }
    }
}
