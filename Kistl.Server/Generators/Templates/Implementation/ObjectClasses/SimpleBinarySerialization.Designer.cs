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
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberName;


        public SimpleBinarySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.memberName = memberName;

        }
        
        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
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
	

#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "(",  modifier , "this.",  memberName , ", ",  streamName , ");\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
}
	else if(direction == SerializerDirection.ToXmlStream)
	{

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
}
	else if(direction == SerializerDirection.FromXmlStream)
	{

#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimpleBinarySerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}