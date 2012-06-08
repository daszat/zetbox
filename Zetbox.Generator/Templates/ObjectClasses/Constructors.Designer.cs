using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst")]
    public partial class Constructors : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string className;
		protected IEnumerable<CompoundObjectProperty> compoundObjectProperties;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, IEnumerable<CompoundObjectProperty> compoundObjectProperties)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Constructors", ctx, className, compoundObjectProperties);
        }

        public Constructors(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, IEnumerable<CompoundObjectProperty> compoundObjectProperties)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.compoundObjectProperties = compoundObjectProperties;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("");
#line 33 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        [Obsolete]\r\n");
this.WriteObjects("        public ",  className , "()\r\n");
this.WriteObjects("            : base(null)\r\n");
this.WriteObjects("        {\r\n");
#line 37 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 38 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : base(lazyCtx)\r\n");
this.WriteObjects("        {\r\n");
#line 43 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 44 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");

        }

    }
}