using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst")]
    public partial class EnumBinarySerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string backingStoreName;
		protected string enumerationType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, string enumerationType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.EnumBinarySerialization", ctx, direction, streamName, xmlnamespace, xmlname, backingStoreName, enumerationType);
        }

        public EnumBinarySerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, string enumerationType)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.backingStoreName = backingStoreName;
			this.enumerationType = enumerationType;

        }

        public override void Generate()
        {
#line 37 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
////
    ////  This class always serializes as int? to avoid complicating the code
    ////

    switch(direction){
        case SerializerDirection.ToStream:

#line 44 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            ",  streamName , ".Write((int?)",  backingStoreName , ");\r\n");
#line 46 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 49 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            ",  backingStoreName , " = (",  enumerationType , ")",  streamName , ".ReadNullableInt32();\r\n");
#line 51 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        case SerializerDirection.Export:

#line 54 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream((int?)",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 56 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 59 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  enumerationType , ")XmlStreamer.ReadNullableInt32(",  streamName , ");\r\n");
this.WriteObjects("               break;\r\n");
#line 63 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}