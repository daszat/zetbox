using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    public partial class ValueCollectionEntry
        : Templates.Implementation.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ValueTypeProperty prop)
            : base(_host, ctx, prop)
        {
        }

        protected override void ApplyIdPropertyTemplate()
        {
            base.ApplyIdPropertyTemplate();
        }

        protected override string GetCeBaseClassName()
        {
            return "BaseClientCollectionEntry";
        }

    }
}
