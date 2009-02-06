using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst")]
    public partial class SerializerTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected SerializationMembersList fields;


        public SerializerTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, SerializationMembersList fields)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.fields = fields;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("\r\n");
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
string methodName = direction.ToString();
	string argType;
	
	switch(direction){
		case SerializerDirection.ToStream:
			argType = "System.IO.BinaryWriter";
			break;
		case SerializerDirection.FromStream:
			argType = "System.IO.BinaryReader";
			break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}
	

#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        public override void ",  methodName , "(",  argType , " binStream)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.",  methodName , "(binStream);\r\n");
#line 37 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
foreach(var serMember in fields)
	{
	    ApplySerializer(direction, serMember, "binStream");
	}

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");

        }



    }
}