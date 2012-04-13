using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst")]
    public partial class EnumBinarySerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string backingStoreName;
		protected string enumerationType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, string enumerationType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.EnumBinarySerialization", ctx, direction, streamName, xmlnamespace, xmlname, backingStoreName, enumerationType);
        }

        public EnumBinarySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, string enumerationType)
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
#line 21 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
////
    ////  This class always serializes as int? to avoid complicating the code
    ////

    switch(direction){
        case SerializerDirection.ToStream:

#line 28 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            ",  streamName , ".Write((int?)",  backingStoreName , ");\r\n");
#line 30 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 33 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                int? baseValue;\r\n");
this.WriteObjects("                ",  streamName , ".Read(out baseValue);\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  enumerationType , ")baseValue;\r\n");
this.WriteObjects("            }\r\n");
#line 39 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        case SerializerDirection.ToXmlStream:

#line 42 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream((int?)",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 44 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        case SerializerDirection.FromXmlStream:
        case SerializerDirection.MergeImport:

#line 48 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStreamConverter(v => ",  backingStoreName , " = (",  enumerationType , ")v, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 50 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        case SerializerDirection.Export:

#line 53 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream((int?)",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 55 "P:\Kistl\Kistl.Generator\Templates\Serialization\EnumBinarySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}