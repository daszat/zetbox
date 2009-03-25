using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\StructPropertyTemplate.cst")]
    public partial class StructPropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected StructProperty prop;


        public StructPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, StructProperty prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\StructPropertyTemplate.cst"
string name = prop.PropertyName;
	string efName = name + Kistl.API.Helper.ImplementationSuffix;
	string backingName = "_" + name;

	string structType = prop.GetPropertyTypeString();
	string structImplementationType = structType + Kistl.API.Helper.ImplementationSuffix;


#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  structType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing store for ",  name , "</summary>\r\n");
this.WriteObjects("        private ",  structImplementationType , " ",  backingName , ";\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>EF sees only this property, for ",  name , "</summary>\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [EdmComplexProperty()]\r\n");
this.WriteObjects("        public ",  structImplementationType , " ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("					throw new ArgumentNullException(\"value\");\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("                if (IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (!object.Equals(",  backingName , ", value))\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", \"",  efName , "\");\r\n");
this.WriteObjects("                    if (",  backingName , " != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						",  backingName , ".DetachFromObject(this, \"",  name , "\");\r\n");
this.WriteObjects("					}\r\n");
this.WriteObjects("                    ",  backingName , " = (",  structImplementationType , ")value;\r\n");
this.WriteObjects("					",  backingName , ".AttachToObject(this, \"",  name , "\");\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", \"",  efName , "\");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\StructPropertyTemplate.cst"
AddSerialization(serializationList, backingName);

#line 68 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}