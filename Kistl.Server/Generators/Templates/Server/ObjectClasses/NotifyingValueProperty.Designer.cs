using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\NotifyingValueProperty.cst")]
    public partial class NotifyingValueProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		private Type type;
		private String name;


        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, Type type, String name)
            : base(_host)
        {
			this.type = type;
			this.name = name;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("\r\n");
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\NotifyingValueProperty.cst"
var backingName = MungeNameToBacking(name);

#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("        ");
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\NotifyingValueProperty.cst"
ApplyAttributeTemplate(); 
#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("        public ",  type.FullName , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\"); \r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\");;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  type.FullName , " ",  backingName , ";");

        }



    }
}