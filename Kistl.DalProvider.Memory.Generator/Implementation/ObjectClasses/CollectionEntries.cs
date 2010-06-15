
namespace Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Implementation = Kistl.Server.Generators.Templates.Implementation;

    public class CollectionEntries
        : Implementation.ObjectClasses.CollectionEntries
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
