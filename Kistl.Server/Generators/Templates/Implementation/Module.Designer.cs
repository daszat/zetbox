using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;


namespace Kistl.Server.Generators.Templates.Implementation
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\Module.cst")]
    public partial class Module : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected string shortName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Implementation.Module", ctx, shortName);
        }

        public Module(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
            : base(_host)
        {
			this.ctx = ctx;
			this.shortName = shortName;

        }

        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\Module.cst"
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
#line 22 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\Module.cst"
foreach(string ns in GetAdditionalImports().Distinct().OrderBy(s => s))
    {

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\Module.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\Module.cst"
}

#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\Module.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    public class ",  shortName , "Module\r\n");
this.WriteObjects("        : Autofac.Module\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        protected override void Load(ContainerBuilder builder)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.Load(builder);\r\n");
this.WriteObjects("\r\n");
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
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    // marker class to provide stable and correct assembly reference\r\n");
this.WriteObjects("    internal sealed class ",  shortName , "ImplementationTypeChecker\r\n");
this.WriteObjects("        : Kistl.API.BaseImplementationTypeChecker, I",  shortName , "ImplementationTypeChecker\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        public ",  shortName , "ImplementationTypeChecker(Func<IEnumerable<IImplementationTypeChecker>> implTypeCheckersFactory)\r\n");
this.WriteObjects("            : base(implTypeCheckersFactory)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        protected override System.Reflection.Assembly GetAssembly()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return this.GetType().Assembly;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
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