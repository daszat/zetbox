
namespace Kistl.DalProvider.Memory.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    public class CollectionEntries
        : Templates.ObjectClasses.CollectionEntries
    {
        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host, ctx)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.DalProvider.Memory",
            });
        }
    }
}
