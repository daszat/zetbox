
namespace Kistl.DalProvider.Ef.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public partial class ValueCollectionEntry
        : Templates.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host, ctx, prop)
        {
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
