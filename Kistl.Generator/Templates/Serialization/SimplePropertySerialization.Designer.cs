using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst")]
    public partial class SimplePropertySerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberType;
		protected string memberName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.SimplePropertySerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName);
        }

        public SimplePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.memberType = memberType;
			this.memberName = memberName;

        }

        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 24 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 26 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 29 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  memberType , " tmp;\r\n");
this.WriteObjects("                BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("                this.",  memberName , " = tmp;\r\n");
this.WriteObjects("            }\r\n");
#line 35 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        case SerializerDirection.ToXmlStream:

#line 38 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 40 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        case SerializerDirection.FromXmlStream:
        case SerializerDirection.MergeImport:

#line 44 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                // yuck\r\n");
this.WriteObjects("                ",  memberType , " tmp = this.",  memberName , ";\r\n");
this.WriteObjects("                XmlStreamer.FromStream(ref tmp, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("                this.",  memberName , " = tmp;\r\n");
this.WriteObjects("            }\r\n");
#line 51 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        case SerializerDirection.Export:

#line 54 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 56 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}