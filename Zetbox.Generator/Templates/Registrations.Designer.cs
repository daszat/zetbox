using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;


namespace Kistl.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Registrations.cst")]
    public partial class Registrations : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string shortName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Registrations", ctx, shortName);
        }

        public Registrations(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
            : base(_host)
        {
			this.ctx = ctx;
			this.shortName = shortName;

        }

        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Generator\Templates\Registrations.cst"
this.WriteObjects("            builder\r\n");
this.WriteObjects("                .Register<",  shortName , "ImplementationTypeChecker>(\r\n");
this.WriteObjects("                    c => new ",  shortName , "ImplementationTypeChecker(\r\n");
this.WriteObjects("                        c.Resolve<Func<IEnumerable<IImplementationTypeChecker>>>()))\r\n");
this.WriteObjects("                .As<I",  shortName , "ImplementationTypeChecker>()\r\n");
this.WriteObjects("                .As<IImplementationTypeChecker>()\r\n");
this.WriteObjects("                .InstancePerDependency();\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("            builder\r\n");
this.WriteObjects("                .Register<",  shortName , "ActionsManager>(\r\n");
this.WriteObjects("                    c => new ",  shortName , "ActionsManager(\r\n");
this.WriteObjects("                        c.Resolve<ILifetimeScope>(),\r\n");
this.WriteObjects("                        c.Resolve<IDeploymentRestrictor>()))\r\n");
this.WriteObjects("                .As<I",  shortName , "ActionsManager>()\r\n");
this.WriteObjects("                .InstancePerLifetimeScope();\r\n");

        }

    }
}