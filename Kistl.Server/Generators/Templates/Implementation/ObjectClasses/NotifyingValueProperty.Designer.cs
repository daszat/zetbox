using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst")]
    public partial class NotifyingValueProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializationMembersList serializationList;
		protected string type;
		protected String name;
		protected String modulenamespace;


        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializationMembersList serializationList, string type, String name, String modulenamespace)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.type = type;
			this.name = name;
			this.modulenamespace = modulenamespace;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
ApplyRequisitesTemplate();

	ApplyAttributesTemplate();

	string backingName = BackingMemberFromName(name);


#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  type , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", __oldValue, value);\r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", __oldValue, value);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  type , " ",  backingName , ";\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
AddSerialization(serializationList, name);


        }



    }
}