using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
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
#line 17 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
string backingPropertyName = "_" + name;
	string backingStoreName = "_" + name + "Store";
	
	string coType = prop.GetPropertyTypeString();
	string coImplementationType = coType + Kistl.API.Helper.ImplementationSuffix;


#line 24 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
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
this.WriteObjects("				if (",  backingStoreName , " != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("	                ",  backingStoreName , ".DetachFromObject(this, \"",  name , "\");\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  coImplementationType , ")value;\r\n");
this.WriteObjects("                ",  backingStoreName , ".AttachToObject(this, \"",  name , "\");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("		}\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, backingPropertyName);

#line 49 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}