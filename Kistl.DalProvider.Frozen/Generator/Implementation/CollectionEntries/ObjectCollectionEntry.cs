
namespace Kistl.DalProvider.Frozen.Generator.Implementation.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Server.Generators.Extensions;

    public partial class ObjectCollectionEntry
        : Kistl.Server.Generators.Templates.Implementation.CollectionEntries.ObjectCollectionEntry
    {

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, rel)
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
