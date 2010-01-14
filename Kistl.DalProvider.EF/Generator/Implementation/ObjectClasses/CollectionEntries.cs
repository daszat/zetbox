using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public class CollectionEntries
        : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntries
    {
        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host,ctx)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.API.Server",
                "Kistl.DalProvider.EF",
                "System.Data.Objects",
                "System.Data.Objects.DataClasses"
            });
        }
    }
}
