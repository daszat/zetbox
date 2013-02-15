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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst")]
    public partial class EnumWithDefaultBinarySerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string backingStoreName;
		protected string enumerationType;
		protected string isSetFlagName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, string enumerationType, string isSetFlagName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.EnumWithDefaultBinarySerialization", ctx, direction, streamName, xmlnamespace, xmlname, backingStoreName, enumerationType, isSetFlagName);
        }

        public EnumWithDefaultBinarySerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, string enumerationType, string isSetFlagName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.backingStoreName = backingStoreName;
			this.enumerationType = enumerationType;
			this.isSetFlagName = isSetFlagName;

        }

        public override void Generate()
        {
#line 38 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
////
    ////  This class always serializes as int? to avoid complicating the code
    ////

    switch(direction){
        case SerializerDirection.ToStream:

#line 45 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  isSetFlagName , ");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                ",  streamName , ".Write((int?)",  backingStoreName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 50 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 53 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            this.",  isSetFlagName , " = ",  streamName , ".ReadBoolean();\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  enumerationType , ")",  streamName , ".ReadNullableInt32();\r\n");
this.WriteObjects("            }\r\n");
#line 58 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        case SerializerDirection.Export:

#line 61 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            System.Diagnostics.Debug.Assert(this.",  isSetFlagName , ", \"Exported objects need to have all default values evaluated\");\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream((int?)",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 64 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 67 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                // Import must have default value set\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  enumerationType , ")XmlStreamer.ReadNullableInt32(",  streamName , ");\r\n");
this.WriteObjects("                this.",  isSetFlagName , " = true;\r\n");
this.WriteObjects("                break;\r\n");
#line 73 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}