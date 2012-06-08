
namespace Zetbox.DalProvider.Memory.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public class CollectionEntries
        : Templates.ObjectClasses.CollectionEntries
    {
        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host, ctx)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Zetbox.DalProvider.Memory",
            });
        }
    }
}
