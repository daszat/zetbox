using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/Constructors.cst")]
    public partial class Constructors : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string className;
		protected IEnumerable<CompoundObjectProperty> compoundObjectProperties;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, IEnumerable<CompoundObjectProperty> compoundObjectProperties)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Constructors", ctx, className, compoundObjectProperties);
        }

        public Constructors(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, IEnumerable<CompoundObjectProperty> compoundObjectProperties)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.compoundObjectProperties = compoundObjectProperties;

        }

        public override void Generate()
        {
#line 17 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/Constructors.cst"
this.WriteObjects("        [Obsolete]\r\n");
this.WriteObjects("        public ",  className , "()\r\n");
this.WriteObjects("            : base(null)\r\n");
this.WriteObjects("        {\r\n");
#line 21 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 22 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : base(lazyCtx)\r\n");
this.WriteObjects("        {\r\n");
#line 27 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 28 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/Constructors.cst"
this.WriteObjects("        }\r\n");

        }

    }
}