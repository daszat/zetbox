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
		protected DataType containingType;
		protected Type type;
		protected String name;
		protected Property property;


        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, DataType containingType, Type type, String name, Property property)
            : base(_host)
        {
			this.containingType = containingType;
			this.type = type;
			this.name = name;
			this.property = property;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\ListProperty.cst"
this.WriteObjects("\r\n");
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\ListProperty.cst"
var backingName = MungeNameToBacking(name);

	ApplyRequisitesTemplate();
	
	ApplyAttributeTemplate();

#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\ListProperty.cst"
this.WriteObjects("        ",  GetModifiers() , " ",  GetPropertyTypeString() , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				if (",  backingName , " == null)\r\n");
this.WriteObjects("					",  backingName , " = ",  GetInitialisationExpression() , ";\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  GetBackingTypeString() , " ",  backingName , ";");

        }



    }
}