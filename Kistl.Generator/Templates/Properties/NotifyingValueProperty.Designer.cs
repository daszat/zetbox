using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst")]
    public partial class NotifyingValueProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Serialization.SerializationMembersList serializationList;
		protected string type;
		protected String name;
		protected String modulenamespace;


        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, string type, String name, String modulenamespace)
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
#line 16 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
#line 19 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyRequisitesTemplate();

    ApplyAttributesTemplate();

    string backingName = BackingMemberFromName(name);


#line 26 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("        ",  GetModifiers() , " ",  type , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // create local variable to create single point of return\r\n");
this.WriteObjects("                // for the benefit of down-stream templates\r\n");
this.WriteObjects("                var __result = ",  backingName , ";\r\n");
#line 34 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyOnGetTemplate();

#line 36 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                return __result;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 42 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyOnAllSetTemplate();

#line 44 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("                    var __newValue = value;\r\n");
#line 49 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyPreSetTemplate();

#line 51 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  backingName , " = __newValue;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
#line 55 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyPostSetTemplate();

#line 57 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  type , " ",  backingName , ";\r\n");
#line 62 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
AddSerialization(serializationList, name);

#line 64 "P:\Kistl\Kistl.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }



    }
}