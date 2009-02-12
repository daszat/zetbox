using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntry
        : Templates.Implementation.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ValueTypeProperty prop)
            : base(_host, ctx, prop)
        {
        }

        protected override string GetCeBaseClassName()
        {
            // TODO: implement full set of extension points on Templates.Implementation.CollectionEntries.ObjectCollectionEntry
            return "Kistl.DalProvider.Frozen.BaseFrozenObject";
        }

    }
}
