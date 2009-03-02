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

	string structType = prop.GetPropertyTypeString();


#line 21 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  structType , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get;\r\n");
this.WriteObjects("            set;\r\n");
this.WriteObjects("        }\r\n");
#line 28 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
// AddSerialization(serializationList, efName);

#line 29 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\StructPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}