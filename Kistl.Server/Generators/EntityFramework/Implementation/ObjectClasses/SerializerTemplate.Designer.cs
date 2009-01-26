using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst")]
    public partial class SerializerTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected IList<string> fields;


        public SerializerTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, IList<string> fields)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.fields = fields;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst"
string methodName, argType, modifier;
	
	switch(direction){
		case SerializerDirection.ToStream:
			methodName = "ToStream";
			argType = "System.IO.BinaryWriter";
			modifier = "";
			break;
		case SerializerDirection.FromStream:
			methodName = "FromStream";
			argType = "System.IO.BinaryReader";
			modifier = "out ";
			break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}
	

#line 37 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ",  methodName , "(",  argType , " binStream)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.",  methodName , "(binStream);\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst"
foreach(string fieldName in fields)
	{

#line 45 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "(",  modifier , "this.",  fieldName , ", binStream);\r\n");
#line 47 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst"
}

#line 49 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");

        }



    }
}