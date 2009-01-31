using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst")]
    public partial class EnumerationPropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected EnumerationProperty prop;


        public EnumerationPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, EnumerationProperty prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
string name = prop.PropertyName;
	string efName = name + Kistl.API.Helper.ImplementationSuffix;
	string backingName = "_" + name;

	string enumType = prop.ReferencedTypeAsCSharp();


#line 23 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EnumerationPropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  enumType , " ",  name , "\r\n");
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
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\");\r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\");\r\n");
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
this.WriteObjects("                return (int)",  name , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  name , " = (",  enumType , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        \r\n");

        }



    }
}