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
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, Property prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Interface.CollectionEntries.ValueCollectionEntry", ctx, prop);
        }

        protected Property prop { get; private set; }

        private static Module CheckNullOrReturnModule(Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return prop.Module;
        }

        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host, ctx, CheckNullOrReturnModule(prop))
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
            return prop is ValueTypeProperty ? ((ValueTypeProperty)prop).HasPersistentOrder : ((StructProperty)prop).HasPersistentOrder;
        }

        protected override string GetDescription()
        {
            return String.Format("ValueCollectionEntry for {0}", prop.Description);
        }
    }
}
