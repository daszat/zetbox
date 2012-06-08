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
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("");
#line 34 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
string interfaceName = prop.ObjectClass.Name;

	string name = prop.Name;
	string efName = name + ImplementationPropertySuffix;
	string backingName = "_" + name;

	bool isNullable = prop.Constraints.OfType<NotNullableConstraint>().Count() == 0;
	string enumType = prop.GetElementTypeString();
	string eventName = "On" + name;


#line 45 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("   		// ",  this.GetType() , "\n");
this.WriteObjects("        // implement the user-visible interface\n");
this.WriteObjects("        public ",  enumType , " ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (!CurrentAccessRights.HasReadRights()) return default(",  enumType , ");\n");
#line 53 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 56 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("				var __value = ",  backingName , ";\n");
this.WriteObjects("				if(",  eventName , "_Getter != null)\n");
this.WriteObjects("				{\n");
this.WriteObjects("					var e = new PropertyGetterEventArgs<",  enumType , ">(__value);\n");
this.WriteObjects("					",  eventName , "_Getter(this, e);\n");
this.WriteObjects("					__value = e.Result;\n");
this.WriteObjects("				}\n");
this.WriteObjects("                return __value;\n");
#line 65 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}
				else
				{

#line 69 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("				return ",  backingName , ";\n");
#line 71 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}

#line 72 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\n");
this.WriteObjects("                if (",  backingName , " != value)\n");
this.WriteObjects("                {\n");
this.WriteObjects("					var __oldValue = ",  backingName , ";\n");
this.WriteObjects("					var __newValue = value;\n");
#line 81 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
					{

#line 84 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    if(",  eventName , "_PreSetter != null)\n");
this.WriteObjects("                    {\n");
this.WriteObjects("						var e = new PropertyPreSetterEventArgs<",  enumType , ">(__oldValue, __newValue);\n");
this.WriteObjects("						",  eventName , "_PreSetter(this, e);\n");
this.WriteObjects("						__newValue = e.Result;\n");
this.WriteObjects("                    }\n");
#line 91 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}

#line 92 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("					\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\n");
this.WriteObjects("                    ",  backingName , " = value;\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\n");
#line 97 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
if(callGetterSetterEvents)
					{

#line 100 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    if(",  eventName , "_PostSetter != null)\n");
this.WriteObjects("                    {\n");
this.WriteObjects("						var e = new PropertyPostSetterEventArgs<",  enumType , ">(__oldValue, __newValue);\n");
this.WriteObjects("						",  eventName , "_PostSetter(this, e);\n");
this.WriteObjects("                    }\n");
#line 106 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
}

#line 107 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("                    \n");
this.WriteObjects("                }\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
#line 112 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
EfScalarPropHelper.ApplyBackingStoreDefinition(this, enumType, backingName, efName); 
#line 113 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
this.WriteObjects("        \n");
this.WriteObjects("        /// <summary>EF sees only this property, for ",  UglyXmlEncode(name) , "</summary>\n");
this.WriteObjects("        [XmlIgnore()]\n");
this.WriteObjects("        [EdmScalarProperty()]\n");
this.WriteObjects("        public int",  isNullable ? "?" : String.Empty , " ",  efName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                return (int",  isNullable ? "?" : String.Empty , ")this.",  name , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                this.",  name , " = (",  enumType , ")value;\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        \n");
#line 130 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumerationPropertyTemplate.cst"
AddSerialization(serializationList);


        }

    }
}