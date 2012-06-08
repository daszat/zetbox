using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.InterfaceTemplates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst")]
    public partial class SimplePropertyListTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Property prop;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.SimplePropertyListTemplate", ctx, prop);
        }

        public SimplePropertyListTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;

        }

        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst"
this.WriteObjects("\r\n");
#line 14 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst"
this.WriteObjects("        [Kistl.API.DefinitionGuid(\"",  prop.ExportGuid , "\")]\r\n");
this.WriteObjects("        ",  GetPropertyTypeString() , " ",  GetPropertyName() , " { get; }\r\n");

        }

    }
}