
namespace Zetbox.DalProvider.NHibernate.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public class Module
        : Templates.Module
    {
        public Module(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
            : base(_host, ctx, shortName)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports()
                .Concat(new string[]{
                    "Zetbox.API.Configuration",
                    "global::NHibernate",
                    "global::NHibernate.Cfg",
                });
        }
    }
}
