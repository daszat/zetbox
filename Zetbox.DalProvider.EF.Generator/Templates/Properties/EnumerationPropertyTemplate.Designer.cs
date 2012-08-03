using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst")]
    public partial class EnumerationPropertyTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected EnumerationProperty prop;
		protected bool callGetterSetterEvents;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, EnumerationProperty prop, bool callGetterSetterEvents)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.EnumerationPropertyTemplate", ctx, serializationList, prop, callGetterSetterEvents);
        }

        public EnumerationPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, EnumerationProperty prop, bool callGetterSetterEvents)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;
			this.callGetterSetterEvents = callGetterSetterEvents;

        }

        public override void Generate()
        {
#line 34 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
string interfaceName = prop.ObjectClass.Name;

	string name = prop.Name;
	string efName = name + ImplementationPropertySuffix;
	string backingName = "_" + name;

	bool isNullable = prop.Constraints.OfType<NotNullableConstraint>().Count() == 0;
	string enumType = prop.GetElementTypeString();
	string eventName = "On" + name;


#line 45 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  enumType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
#line 52 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 55 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("				var __value = ",  backingName , ";\r\n");
this.WriteObjects("				if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					var e = new PropertyGetterEventArgs<",  enumType , ">(__value);\r\n");
this.WriteObjects("					",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("					__value = e.Result;\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("                return __value;\r\n");
#line 64 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}
				else
				{

#line 68 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("				return ",  backingName , ";\r\n");
#line 70 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}

#line 71 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("					var __newValue = value;\r\n");
#line 80 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
					{

#line 83 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    if(",  eventName , "_PreSetter != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						var e = new PropertyPreSetterEventArgs<",  enumType , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("						",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("						__newValue = e.Result;\r\n");
this.WriteObjects("                    }\r\n");
#line 90 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}

#line 91 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("					\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    UpdateChangedInfo = true;\r\n");
#line 97 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
					{

#line 100 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    if(",  eventName , "_PostSetter != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						var e = new PropertyPostSetterEventArgs<",  enumType , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("						",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                    }\r\n");
#line 106 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}

#line 107 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    \r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 112 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
EfScalarPropHelper.ApplyBackingStoreDefinition(this, enumType, backingName, efName); 
#line 113 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>EF sees only this property, for ",  UglyXmlEncode(name) , "</summary>\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [EdmScalarProperty()]\r\n");
this.WriteObjects("        public int",  isNullable ? "?" : String.Empty , " ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return (int",  isNullable ? "?" : String.Empty , ")this.",  name , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                this.",  name , " = (",  enumType , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
#line 130 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
AddSerialization(serializationList);


        }

    }
}