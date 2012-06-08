using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.InterfaceTemplates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst")]
    public partial class SimplePropertyListTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Property prop;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.SimplePropertyListTemplate", ctx, prop);
        }

        public SimplePropertyListTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst"
this.WriteObjects("");
#line 27 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst"
this.WriteObjects("\n");
#line 30 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst"
this.WriteObjects("        [Zetbox.API.DefinitionGuid(\"",  prop.ExportGuid , "\")]\n");
this.WriteObjects("        ",  GetPropertyTypeString() , " ",  GetPropertyName() , " { get; }\n");

        }

    }
}