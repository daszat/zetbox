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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst")]
    public partial class SimpleBinarySerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string memberName;


        public SimpleBinarySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.memberName = memberName;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
string methodName = direction.ToString();
	string modifier;
	
	switch(direction){
		case SerializerDirection.ToStream:
			modifier = "";
			break;
		case SerializerDirection.FromStream:
			modifier = "out ";
			break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}
	

#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "(",  modifier , "this.",  memberName , ", ",  streamName , ");\r\n");

        }



    }
}