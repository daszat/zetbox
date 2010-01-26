using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst")]
    public partial class CompoundObjectPropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Server.Generators.Templates.Implementation.SerializationMembersList serializationList;
		protected CompoundObjectProperty prop;
		protected string name;


        public CompoundObjectPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Server.Generators.Templates.Implementation.SerializationMembersList serializationList, CompoundObjectProperty prop, string name)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;
			this.name = name;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
string efName = name + Kistl.API.Helper.ImplementationSuffix;
	string backingName = "_" + name;

	string structType = prop.GetPropertyTypeString();
	string structImplementationType = structType + Kistl.API.Helper.ImplementationSuffix;


#line 25 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  structType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  efName , ".CompoundObject_IsNull ? null : ",  efName , "; }\r\n");
this.WriteObjects("            set { ",  efName , " = (",  structImplementationType , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing store for ",  name , "</summary>\r\n");
this.WriteObjects("        private ",  structImplementationType , " ",  backingName , ";\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing property for ",  name , ", takes care of attaching/detaching the values, mapped via EF</summary>\r\n");
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
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 48 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
if(!prop.IsNullable())
				{

#line 50 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("				            \r\n");
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("					throw new ArgumentNullException(\"value\");\r\n");
#line 54 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
}                

#line 56 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (!object.Equals(",  backingName , ", value))\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", \"",  efName , "\", __oldValue, value);\r\n");
this.WriteObjects("                    if (",  backingName , " != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						",  backingName , ".DetachFromObject(this, \"",  name , "\");\r\n");
this.WriteObjects("					}\r\n");
this.WriteObjects("                    if(value == null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						",  backingName , " = new ",  structImplementationType , "(true, this, \"",  name , "\");\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                    else\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("						",  backingName , " = (",  structImplementationType , ")value.Clone();\r\n");
this.WriteObjects("						",  backingName , ".AttachToObject(this, \"",  name , "\");\r\n");
this.WriteObjects("					}\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", \"",  efName , "\", __oldValue, value);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
#line 80 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, name);

#line 81 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}