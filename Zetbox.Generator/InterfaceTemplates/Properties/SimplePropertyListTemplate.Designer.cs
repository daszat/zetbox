using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.InterfaceTemplates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst")]
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
#line 27 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst"
this.WriteObjects("\r\n");
#line 30 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyListTemplate.cst"
this.WriteObjects("        [Zetbox.API.DefinitionGuid(\"",  prop.ExportGuid , "\")]\r\n");
this.WriteObjects("        [System.Runtime.Serialization.IgnoreDataMember]\r\n");
this.WriteObjects("        ",  GetPropertyTypeString() , " ",  GetPropertyName() , " { get; }\r\n");

        }

    }
}