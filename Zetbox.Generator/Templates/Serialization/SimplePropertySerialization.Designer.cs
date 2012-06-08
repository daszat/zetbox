using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst")]
    public partial class SimplePropertySerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberType;
		protected string memberName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.SimplePropertySerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName);
        }

        public SimplePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName)
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
#line 36 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 40 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  memberName , ");\r\n");
#line 42 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 45 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            this.",  memberName , " = ",  streamName , ".",  memberType.SerializerReadMethod() , "();\r\n");
#line 47 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        case SerializerDirection.Export:

#line 50 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 52 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 55 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                this.",  memberName , " = XmlStreamer.",  memberType.SerializerReadMethod() , "(",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 59 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}