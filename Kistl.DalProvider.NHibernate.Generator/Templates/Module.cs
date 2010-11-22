
namespace Kistl.DalProvider.NHibernate.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    public class Module
        : Templates.Module
    {
        public Module(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
            : base(_host, ctx, shortName)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports()
                .Concat(new string[]{
                    "Kistl.API.Configuration",
                    "global::NHibernate",
                    "global::NHibernate.Cfg",
                    "global::NHibernate.Cfg.Loquacious",
                });
        }
    }
}
