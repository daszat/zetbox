using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;


namespace Zetbox.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Module.cst")]
    public partial class Module : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string shortName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Module", ctx, shortName);
        }

        public Module(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
            : base(_host)
        {
			this.ctx = ctx;
			this.shortName = shortName;

        }

        public override void Generate()
        {
#line 27 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace Zetbox.Objects\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using System;\r\n");
this.WriteObjects("    using System.Collections.Generic;\r\n");
this.WriteObjects("    using System.Linq;\r\n");
this.WriteObjects("    using System.Text;\r\n");
this.WriteObjects("    using Autofac;\r\n");
this.WriteObjects("	using Zetbox.API;\r\n");
#line 37 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
foreach(string ns in GetAdditionalImports().OrderBy(s => s).Distinct().OrderBy(s => s)) { 
#line 38 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 39 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
} 
#line 40 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    public class ",  shortName , "Module\r\n");
this.WriteObjects("        : Autofac.Module\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        protected override void Load(ContainerBuilder builder)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.Load(builder);\r\n");
this.WriteObjects("\r\n");
#line 48 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
ApplyRegistrations(); 
#line 49 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
#line 52 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
ApplyTypeCheckerTemplate(); 
#line 53 "P:\zetbox\Zetbox.Generator\Templates\Module.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    // marker class to provide stable and correct assembly reference\r\n");
this.WriteObjects("    internal sealed class ",  shortName , "ActionsManager\r\n");
this.WriteObjects("        : BaseCustomActionsManager, I",  shortName , "ActionsManager\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        public ",  shortName , "ActionsManager(ILifetimeScope container, IDeploymentRestrictor restrictor)\r\n");
this.WriteObjects("            : base(container, restrictor, \"",  ImplementationSuffix , "\")\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}\r\n");

        }

    }
}