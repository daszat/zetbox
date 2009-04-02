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


        public EnumerationPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, EnumerationProperty prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
string interfaceName = prop.ObjectClass.ClassName;

	string name = prop.PropertyName;
	// efName needs to be == name so that member change tracking works
	string efName = name; // + Kistl.API.Helper.ImplementationSuffix;
	string backingName = "_" + name;

	string enumType = prop.ReferencedTypeAsCSharp();


#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        ",  enumType , " ",  interfaceName , ".",  name , "\r\n");
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
this.WriteObjects("					var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", __oldValue, value);\r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", __oldValue, value);\r\n");
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
this.WriteObjects("                return (int)((",  interfaceName , ")this).",  name , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ((",  interfaceName , ")this).",  name , " = (",  enumType , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
AddSerialization(serializationList);


        }



    }
}