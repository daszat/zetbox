using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.CollectionEntries
{
    public partial class ValueCollectionEntry
        : Template
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, ValueTypeProperty prop)
        {
            host.CallTemplate("Interface.CollectionEntries.ValueCollectionEntry", ctx, prop);
        }

        protected ValueTypeProperty prop { get; private set; }

        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ValueTypeProperty prop)
            : base(_host, ctx, prop.Module)
        {
            this.prop = prop;
        }

        protected override string GetCeClassName()
        {
            return prop.GetCollectionEntryClassName();
        }

        protected override string GetCeInterface()
        {
            return String.Format("{0}<{1}, {2}>",
                IsOrdered() ? "IValueListEntry" : "IValueCollectionEntry",
                this.prop.ObjectClass.ClassName,
                this.prop.GetPropertyTypeString());
        }

        protected override bool IsOrdered()
        {
            return prop.IsIndexed;
        }

        protected override string GetDescription()
        {
            return String.Format("ValueCollectionEntry for {0}", prop.Description);
        }
    }
}
