using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;


namespace Zetbox.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\TypeChecker.cst")]
    public partial class TypeChecker : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string shortName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("TypeChecker", ctx, shortName);
        }

        public TypeChecker(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
            : base(_host)
        {
			this.ctx = ctx;
			this.shortName = shortName;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\TypeChecker.cst"
this.WriteObjects("");
#line 27 "P:\zetbox\Zetbox.Generator\Templates\TypeChecker.cst"
this.WriteObjects("\n");
this.WriteObjects("    internal sealed class ",  shortName , "ImplementationTypeChecker\n");
this.WriteObjects("        : Zetbox.API.BaseImplementationTypeChecker, I",  shortName , "ImplementationTypeChecker\n");
this.WriteObjects("    {\n");
this.WriteObjects("        public ",  shortName , "ImplementationTypeChecker(Func<IEnumerable<IImplementationTypeChecker>> implTypeCheckersFactory)\n");
this.WriteObjects("            : base(implTypeCheckersFactory)\n");
this.WriteObjects("        {\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        protected override System.Reflection.Assembly GetAssembly()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            return typeof(",  shortName , "ImplementationTypeChecker).Assembly;\n");
this.WriteObjects("        }\n");
this.WriteObjects("    }\n");

        }

    }
}