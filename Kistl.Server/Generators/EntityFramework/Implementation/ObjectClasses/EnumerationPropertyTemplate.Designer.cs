using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst")]
    public partial class EnumerationPropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected EnumerationProperty prop;
		protected bool callGetterSetterEvents;


        public EnumerationPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, EnumerationProperty prop, bool callGetterSetterEvents)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;
			this.callGetterSetterEvents = callGetterSetterEvents;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
string interfaceName = prop.ObjectClass.ClassName;

	string name = prop.PropertyName;
	string efName = name + Kistl.API.Helper.ImplementationSuffix;
	string backingName = "_" + name;

	string enumType = prop.ReferencedTypeAsCSharp();
	string eventName = "On" + name;


#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  enumType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 37 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("				var __value = ",  backingName , ";\r\n");
this.WriteObjects("				if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					var e = new PropertyGetterEventArgs<",  enumType , ">(__value);\r\n");
this.WriteObjects("					",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("					__value = e.Result;\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("                return __value;\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
}
				else
				{

#line 50 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("				return ",  backingName , ";\r\n");
#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
}

#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("					var __newValue = value;\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
					{

#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    if(",  eventName , "_PreSetter != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						var e = new PropertyPreSetterEventArgs<",  enumType , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("						",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("						__newValue = e.Result;\r\n");
this.WriteObjects("                    }\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
}

#line 73 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("					\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", \"",  efName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", \"",  efName , "\", __oldValue, __newValue);\r\n");
#line 78 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
					{

#line 81 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    if(",  eventName , "_PostSetter != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						var e = new PropertyPostSetterEventArgs<",  enumType , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("						",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                    }\r\n");
#line 87 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
}

#line 88 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    \r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing store for ",  name , "</summary>\r\n");
this.WriteObjects("        private ",  enumType , " ",  backingName , ";\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>EF sees only this property, for ",  name , "</summary>\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [EdmScalarProperty()]\r\n");
this.WriteObjects("        public int ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return (int)this.",  name , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                this.",  name , " = (",  enumType , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
#line 112 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
AddSerialization(serializationList);


        }



    }
}