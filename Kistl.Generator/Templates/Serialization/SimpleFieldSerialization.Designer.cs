using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst")]
    public partial class SimpleFieldSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.SimpleFieldSerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberName);
        }

        public SimpleFieldSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName)
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
#line 20 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 24 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 26 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
break;
		case SerializerDirection.FromStream:

#line 29 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
this.WriteObjects("            BinarySerializer.FromStream(out this.",  memberName , ", ",  streamName , ");\r\n");
#line 31 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
break;
		case SerializerDirection.ToXmlStream:

#line 34 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 36 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
break;
		case SerializerDirection.FromXmlStream:
		case SerializerDirection.MergeImport:

#line 40 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 42 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
break;
		case SerializerDirection.Export:

#line 44 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 47 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimpleFieldSerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }

    }
}