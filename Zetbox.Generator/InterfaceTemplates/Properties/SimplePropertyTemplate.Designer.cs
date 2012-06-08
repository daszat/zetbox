using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.InterfaceTemplates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst")]
    public partial class SimplePropertyTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Property prop;
		protected bool isReadonly;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop, bool isReadonly)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.SimplePropertyTemplate", ctx, prop, isReadonly);
        }

        public SimplePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop, bool isReadonly)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;
			this.isReadonly = isReadonly;

        }

        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
string name = prop.Name;
    string type = prop.GetPropertyTypeString();

#line 18 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("        [Kistl.API.DefinitionGuid(\"",  prop.ExportGuid , "\")]\r\n");
this.WriteObjects("        ",  type , " ",  name , " {\r\n");
this.WriteObjects("            get;\r\n");
#line 22 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
if (!isReadonly)
    {

#line 25 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("            set;\r\n");
#line 27 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
}

#line 29 "P:\Kistl\Kistl.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("        }\r\n");

        }

    }
}