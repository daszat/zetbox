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
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.core\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst")]
    public partial class Constructors : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string className;
		protected IEnumerable<CompoundObjectProperty> compoundObjectProperties;
		protected bool asCollectionEntry;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, IEnumerable<CompoundObjectProperty> compoundObjectProperties, bool asCollectionEntry)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Constructors", ctx, className, compoundObjectProperties, asCollectionEntry);
        }

        public Constructors(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, IEnumerable<CompoundObjectProperty> compoundObjectProperties, bool asCollectionEntry)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.compoundObjectProperties = compoundObjectProperties;
			this.asCollectionEntry = asCollectionEntry;

        }

        public override void Generate()
        {
#line 34 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        [Obsolete]\r\n");
this.WriteObjects("        public ",  className , "()\r\n");
this.WriteObjects("            : base(null)\r\n");
this.WriteObjects("        {\r\n");
#line 38 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(null); 
#line 39 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : base(lazyCtx)\r\n");
this.WriteObjects("        {\r\n");
#line 44 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers("lazyCtx"); 
#line 45 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");

        }

    }
}