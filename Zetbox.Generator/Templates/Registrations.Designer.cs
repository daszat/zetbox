using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;


namespace Zetbox.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Registrations.cst")]
    public partial class Registrations : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string shortName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Registrations", ctx, shortName);
        }

        public Registrations(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
            : base(_host)
        {
			this.ctx = ctx;
			this.shortName = shortName;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Registrations.cst"
this.WriteObjects("");
#line 27 "P:\zetbox\Zetbox.Generator\Templates\Registrations.cst"
this.WriteObjects("            builder\n");
this.WriteObjects("                .Register<",  shortName , "ImplementationTypeChecker>(\n");
this.WriteObjects("                    c => new ",  shortName , "ImplementationTypeChecker(\n");
this.WriteObjects("                        c.Resolve<Func<IEnumerable<IImplementationTypeChecker>>>()))\n");
this.WriteObjects("                .As<I",  shortName , "ImplementationTypeChecker>()\n");
this.WriteObjects("                .As<IImplementationTypeChecker>()\n");
this.WriteObjects("                .InstancePerDependency();\n");
this.WriteObjects("                \n");
this.WriteObjects("            builder\n");
this.WriteObjects("                .Register<",  shortName , "ActionsManager>(\n");
this.WriteObjects("                    c => new ",  shortName , "ActionsManager(\n");
this.WriteObjects("                        c.Resolve<ILifetimeScope>(),\n");
this.WriteObjects("                        c.Resolve<IDeploymentRestrictor>()))\n");
this.WriteObjects("                .As<I",  shortName , "ActionsManager>()\n");
this.WriteObjects("                .InstancePerLifetimeScope();\n");

        }

    }
}