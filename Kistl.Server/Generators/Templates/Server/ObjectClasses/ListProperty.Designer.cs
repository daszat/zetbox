using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\ListProperty.cst")]
    public partial class ListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Type type;
		protected String name;
		protected Property property;


        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, Type type, String name, Property property)
            : base(_host)
        {
			this.type = type;
			this.name = name;
			this.property = property;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\ListProperty.cst"
this.WriteObjects("\r\n");
#line 17 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\ListProperty.cst"
var backingName = MungeNameToBacking(name);

	ApplyAttributeTemplate();

#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\ListProperty.cst"
this.WriteObjects("        ",  GetModifiers() , " ",  GetPropertyTypeString() , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  GetPropertyTypeString() , " ",  backingName , ";");

        }



    }
}