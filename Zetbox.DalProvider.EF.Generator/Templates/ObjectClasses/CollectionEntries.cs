using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public class CollectionEntries
        : Zetbox.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntries
    {
        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host,ctx)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Zetbox.API.Server",
                "Zetbox.DalProvider.EF",
                "System.Data.Objects",
                "System.Data.Objects.DataClasses"
            });
        }
    }
}
