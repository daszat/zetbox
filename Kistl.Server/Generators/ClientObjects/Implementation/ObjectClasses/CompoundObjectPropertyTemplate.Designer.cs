using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst")]
    public partial class CompoundObjectPropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected CompoundObjectProperty prop;
		protected string name;


        public CompoundObjectPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, CompoundObjectProperty prop, string name)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;
			this.name = name;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
string backingPropertyName = name + Kistl.API.Helper.ImplementationSuffix;
	string backingStoreName = "_" + name + "Store";
	
	string coType = prop.GetPropertyTypeString();
	string coImplementationType = coType + Kistl.API.Helper.ImplementationSuffix;


#line 25 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("		// ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  coType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  backingPropertyName , "; }\r\n");
this.WriteObjects("            set { ",  backingPropertyName , " = (",  coImplementationType , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing store for ",  name , "</summary>\r\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingStoreName , ";\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing property for ",  name , ", takes care of attaching/detaching the values</summary>\r\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingPropertyName , " {\r\n");
this.WriteObjects("            get { return ",  backingStoreName , "; }\r\n");
this.WriteObjects("            set {\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
if(!prop.IsNullable())
				{

#line 42 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("				            \r\n");
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("					throw new ArgumentNullException(\"value\");\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
}                

#line 48 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                var __oldValue = ",  backingStoreName , ";\r\n");
this.WriteObjects("                var __newValue = value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("				if (",  backingStoreName , " != null) ",  backingStoreName , ".DetachFromObject(this, \"",  name , "\");\r\n");
this.WriteObjects("                ",  backingStoreName , " = value != null ? (",  coImplementationType , ")value.Clone() : null;\r\n");
this.WriteObjects("				if (",  backingStoreName , " != null) ",  backingStoreName , ".AttachToObject(this, \"",  name , "\");\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("		}\r\n");
#line 61 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, name);

#line 62 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}