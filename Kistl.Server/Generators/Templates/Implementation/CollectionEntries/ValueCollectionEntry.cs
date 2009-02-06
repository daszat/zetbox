using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public abstract partial class ValueCollectionEntry
        : Template
    {
        protected ValueTypeProperty prop { get; private set; }

        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ValueTypeProperty prop)
            : base(_host, ctx)
        {
            this.prop = prop;
        }

        protected override string GetCeClassName()
        {
            return prop.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;
        }

        protected override string GetCeInterface()
        {
            return String.Format("{0}<{1}, {2}>",
                IsOrdered() ? "INewListEntry" : "INewCollectionEntry",
                this.prop.ObjectClass.ClassName,
                this.prop.GetPropertyTypeString());
        }

        protected override bool IsOrdered()
        {
            return prop.IsIndexed;
        }
    }
}
