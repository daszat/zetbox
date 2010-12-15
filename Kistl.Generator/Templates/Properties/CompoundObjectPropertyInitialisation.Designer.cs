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


        public CompoundObjectPropertyInitialisation(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string implementationTypeName, string propertyName, string backingStoreName)
            : base(_host)
        {
			this.ctx = ctx;
			this.implementationTypeName = implementationTypeName;
			this.propertyName = propertyName;
			this.backingStoreName = backingStoreName;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyInitialisation.cst"
this.WriteObjects("            ",  backingStoreName , " = new ",  implementationTypeName , "(false, this, \"",  propertyName , "\");");

        }



    }
}