
namespace Zetbox.DalProvider.Ef.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class ValueCollectionEntry
        : Templates.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop)
            : base(_host, ctx, prop)
        {
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();

            this.WriteObjects("[Obsolete]");
            this.WriteLine();
            this.WriteObjects("public ", GetCeClassName(), "()");
            this.WriteLine();
            this.WriteObjects(": base(null)");
            this.WriteLine();
            this.WriteObjects("{");
            this.WriteLine();

            if (this.prop is CompoundObjectProperty)
            {
                Templates.Properties.CompoundObjectPropertyInitialisation.Call(Host, ctx, prop.GetElementTypeString() + ImplementationSuffix, "Value", "_Value", false);
            }

            this.WriteObjects("}");
            this.WriteLine();

            this.WriteObjects("public ", GetCeClassName() ,"(Func<IFrozenContext> lazyCtx)");
            this.WriteLine();
            this.WriteObjects("    : base(lazyCtx)");
            this.WriteLine();
            this.WriteObjects("{");
            this.WriteLine();
            
            if (this.prop is CompoundObjectProperty)
            {
                Templates.Properties.CompoundObjectPropertyInitialisation.Call(Host, ctx, prop.GetElementTypeString() + ImplementationSuffix, "Value", "_Value", false);
            }

            this.WriteObjects("}");
            this.WriteLine();

        }

        protected override void ApplyClassAttributeTemplate()
        {
            base.ApplyClassAttributeTemplate();
            this.WriteObjects(@"    [EdmEntityType(NamespaceName=""Model"", Name=""",
                prop.GetCollectionEntryClassName(), @""")]");
            this.WriteLine();
        }
    }
}
