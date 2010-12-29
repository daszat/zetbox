using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;


namespace Kistl.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\TypeChecker.cst")]
    public partial class TypeChecker : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string shortName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("TypeChecker", ctx, shortName);
        }

        public TypeChecker(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
            : base(_host)
        {
			this.ctx = ctx;
			this.shortName = shortName;

        }

        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Generator\Templates\TypeChecker.cst"
this.WriteObjects("\r\n");
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
this.WriteObjects("            return typeof(",  shortName , "ImplementationTypeChecker).Assembly;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");

        }

    }
}