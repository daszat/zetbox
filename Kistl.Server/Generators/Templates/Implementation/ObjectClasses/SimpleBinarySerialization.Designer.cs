using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


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
#line 17 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
if(direction == SerializerDirection.ToStream || direction == SerializerDirection.FromStream)
	{
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
	

#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "(",  modifier , "this.",  memberName , ", ",  streamName , ");\r\n");
#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
}
	else if(direction == SerializerDirection.ToXmlStream)
	{

#line 40 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  memberName.TrimStart('_') , "\", \"http://dasz.at/Kistl\");\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
}
	else if(direction == SerializerDirection.FromXmlStream)
	{

#line 46 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
this.WriteObjects("            // TODO: Add XML Serializer here\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}