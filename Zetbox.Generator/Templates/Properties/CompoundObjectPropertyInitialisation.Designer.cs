using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyInitialisation.cst")]
    public partial class CompoundObjectPropertyInitialisation : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string implementationTypeName;
		protected string propertyName;
		protected string backingStoreName;
		protected bool isNull;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string implementationTypeName, string propertyName, string backingStoreName, bool isNull)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CompoundObjectPropertyInitialisation", ctx, implementationTypeName, propertyName, backingStoreName, isNull);
        }

        public CompoundObjectPropertyInitialisation(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string implementationTypeName, string propertyName, string backingStoreName, bool isNull)
            : base(_host)
        {
			this.ctx = ctx;
			this.implementationTypeName = implementationTypeName;
			this.propertyName = propertyName;
			this.backingStoreName = backingStoreName;
			this.isNull = isNull;

        }

        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyInitialisation.cst"
this.WriteObjects("            ",  backingStoreName , " = new ",  implementationTypeName , "(",  isNull ? "true": "false" , ", this, \"",  propertyName , "\");\r\n");

        }

    }
}