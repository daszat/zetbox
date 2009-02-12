using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
        : Templates.Implementation.CollectionEntries.ObjectCollectionEntry
    {

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, NewRelation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override string GetCeBaseClassName()
        {
            // TODO: implement full set of extension points on Templates.Implementation.CollectionEntries.ObjectCollectionEntry
            return "Kistl.DalProvider.Frozen.BaseFrozenObject";
        }

    }
}
