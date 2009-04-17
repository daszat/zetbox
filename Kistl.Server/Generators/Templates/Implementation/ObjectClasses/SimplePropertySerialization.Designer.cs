using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst")]
    public partial class SimplePropertySerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string memberName;


        public SimplePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.memberName = memberName;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 23 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
break;
		case SerializerDirection.FromStream:
		    // use type-inference to get right "tmp" type

#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                var tmp = this.",  memberName , ";\r\n");
this.WriteObjects("                BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("                this.",  memberName , " = tmp;\r\n");
this.WriteObjects("            }\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
break;
		case SerializerDirection.ToXmlStream:

#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  memberName.TrimStart('_') , "\", \"http://dasz.at/Kistl\");\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
break;
		case SerializerDirection.FromXmlStream:

#line 41 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
this.WriteObjects("            // TODO: Add XML Serializer here\r\n");
#line 43 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SimplePropertySerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}