using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;


namespace Kistl.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Module.cst")]
    public partial class Module : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string shortName;


        public Module(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
            : base(_host)
        {
			this.ctx = ctx;
			this.shortName = shortName;

        }
        
        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace Kistl.Objects\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using System;\r\n");
this.WriteObjects("    using System.Collections.Generic;\r\n");
this.WriteObjects("    using System.Linq;\r\n");
this.WriteObjects("    using System.Text;\r\n");
this.WriteObjects("    using Autofac;\r\n");
this.WriteObjects("	using Kistl.API;\r\n");
#line 21 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
foreach(string ns in GetAdditionalImports().OrderBy(s => s).Distinct().OrderBy(s => s)) { 
#line 22 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 23 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
} 
#line 24 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    public class ",  shortName , "Module\r\n");
this.WriteObjects("        : Autofac.Module\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        protected override void Load(ContainerBuilder builder)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.Load(builder);\r\n");
this.WriteObjects("\r\n");
#line 32 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
ApplyRegistrations(); 
#line 33 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
#line 36 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
ApplyTypeCheckerTemplate(); 
#line 37 "P:\Kistl\Kistl.Generator\Templates\Module.cst"
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