using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.InterfaceTemplates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst")]
    public partial class SimplePropertyTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Property prop;
		protected bool isReadonly;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop, bool isReadonly)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.SimplePropertyTemplate", ctx, prop, isReadonly);
        }

        public SimplePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop, bool isReadonly)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;
			this.isReadonly = isReadonly;

        }

        public override void Generate()
        {
#line 31 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
string name = prop.Name;
    string type = prop.GetPropertyTypeString();

#line 34 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("        [Zetbox.API.DefinitionGuid(\"",  prop.ExportGuid , "\")]\r\n");
#line 35 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
if(IsObjectReference) { 
#line 36 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("		[System.Runtime.Serialization.IgnoreDataMember]\r\n");
#line 37 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
} 
#line 38 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("        ",  type , " ",  name , " {\r\n");
this.WriteObjects("            get;\r\n");
#line 41 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
if (!isReadonly)
    {

#line 44 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("            set;\r\n");
#line 46 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
}

#line 48 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 50 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
if(IsObjectReference) { 
#line 51 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
this.WriteObjects("		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]\r\n");
this.WriteObjects("		int? FK_",  name , " \r\n");
this.WriteObjects("		{ \r\n");
this.WriteObjects("			get; \r\n");
this.WriteObjects("			set;\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        System.Threading.Tasks.Task<",  type , "> GetProp_",  name , "();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        System.Threading.Tasks.Task SetProp_",  name , "(",  type , " newValue);\r\n");
#line 61 "D:\Projects\zetbox\Zetbox.Generator\InterfaceTemplates\Properties\SimplePropertyTemplate.cst"
} 

        }

    }
}