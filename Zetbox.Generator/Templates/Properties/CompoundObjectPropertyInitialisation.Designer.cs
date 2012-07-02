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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string implementationTypeName, string propertyName, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CompoundObjectPropertyInitialisation", ctx, implementationTypeName, propertyName, backingStoreName);
        }

        public CompoundObjectPropertyInitialisation(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string implementationTypeName, string propertyName, string backingStoreName)
            : base(_host)
        {
			this.ctx = ctx;
			this.implementationTypeName = implementationTypeName;
			this.propertyName = propertyName;
			this.backingStoreName = backingStoreName;

        }

        public override void Generate()
        {
#line 32 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyInitialisation.cst"
this.WriteObjects("            ",  backingStoreName , " = new ",  implementationTypeName , "(this, \"",  propertyName , "\");\r\n");

        }

    }
}