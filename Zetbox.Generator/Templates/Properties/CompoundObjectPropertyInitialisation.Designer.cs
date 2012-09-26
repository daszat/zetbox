using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyInitialisation.cst")]
    public partial class CompoundObjectPropertyInitialisation : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string implementationTypeName;
		protected string propertyName;
		protected string backingStoreName;
		protected string lazyCtxParam;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string implementationTypeName, string propertyName, string backingStoreName, string lazyCtxParam)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CompoundObjectPropertyInitialisation", ctx, implementationTypeName, propertyName, backingStoreName, lazyCtxParam);
        }

        public CompoundObjectPropertyInitialisation(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string implementationTypeName, string propertyName, string backingStoreName, string lazyCtxParam)
            : base(_host)
        {
			this.ctx = ctx;
			this.implementationTypeName = implementationTypeName;
			this.propertyName = propertyName;
			this.backingStoreName = backingStoreName;
			this.lazyCtxParam = lazyCtxParam;

        }

        public override void Generate()
        {
#line 33 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyInitialisation.cst"
this.WriteObjects("            ",  backingStoreName , " = new ",  implementationTypeName , "(",  lazyCtxParam , ", this, \"",  propertyName , "\");\r\n");

        }

    }
}