using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst")]
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
#line 16 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
string name = prop.PropertyName;
	string backingPropertyName = "_" + name;
	string backingStoreName = "_" + name + "Store";
	
	string structType = prop.GetPropertyTypeString();
	string structImplementationType = structType + Kistl.API.Helper.ImplementationSuffix;


#line 24 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  structType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  backingPropertyName , "; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing store for ",  name , "</summary>\r\n");
this.WriteObjects("        private ",  structImplementationType , " ",  backingStoreName , ";\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing property for ",  name , ", takes care of attaching/detaching the values</summary>\r\n");
this.WriteObjects("        private ",  structImplementationType , " ",  backingPropertyName , " {\r\n");
this.WriteObjects("            get { return ",  backingStoreName , "; }\r\n");
this.WriteObjects("            set {\r\n");
this.WriteObjects("				if (",  backingStoreName , " != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("	                ",  backingStoreName , ".DetachFromObject(this, \"",  name , "\");\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  structImplementationType , ")value;\r\n");
this.WriteObjects("                ",  backingStoreName , ".AttachToObject(this, \"",  name , "\");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("		}\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
AddSerialization(serializationList, backingPropertyName);

#line 47 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}