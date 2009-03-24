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
	string backingName = "_" + name;
	
	string structType = prop.GetPropertyTypeString();
	string structImplementationType = structType + Kistl.API.Helper.ImplementationSuffix;


#line 23 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  structType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  backingName , "; }\r\n");
this.WriteObjects("            set { ",  backingName , " = (",  structImplementationType , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        private ",  structImplementationType , " ",  backingName , ";\r\n");
#line 32 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
AddSerialization(serializationList, backingName);

#line 33 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}