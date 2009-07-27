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
		protected bool callGetterSetterEvents;
		protected String modulenamespace;


        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializationMembersList serializationList, string type, String name, bool callGetterSetterEvents, String modulenamespace)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.type = type;
			this.name = name;
			this.callGetterSetterEvents = callGetterSetterEvents;
			this.modulenamespace = modulenamespace;

        }
        
        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
ApplyRequisitesTemplate();

	ApplyAttributesTemplate();

	string backingName = BackingMemberFromName(name);
	string eventName = "On" + name;


#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  type , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
if(callGetterSetterEvents)
				{

#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("				var __value = ",  backingName , ";\r\n");
this.WriteObjects("				if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					var e = new PropertyGetterEventArgs<",  type , ">(__value);\r\n");
this.WriteObjects("					",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("					__value = e.Result;\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("                return __value;\r\n");
#line 45 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
}
				else
				{

#line 49 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("				return ",  backingName , ";\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
}

#line 53 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("					var __newValue = value;\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
if(callGetterSetterEvents)
					{

#line 65 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("                    if(",  eventName , "_PreSetter != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						var e = new PropertyPreSetterEventArgs<",  type , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("						",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("						__newValue = e.Result;\r\n");
this.WriteObjects("                    }\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
}

#line 74 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  backingName , " = __newValue;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 79 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
if(callGetterSetterEvents)
					{

#line 82 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("                    if(",  eventName , "_PostSetter != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						var e = new PropertyPostSetterEventArgs<",  type , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("						",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                    }\r\n");
#line 88 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
}

#line 90 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  type , " ",  backingName , ";\r\n");
#line 95 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingValueProperty.cst"
AddSerialization(serializationList, name);


        }



    }
}