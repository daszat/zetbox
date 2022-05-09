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
    [Arebis.CodeGeneration.TemplateInfo(@"C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst")]
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
#line 38 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
////
    ////  This class always serializes as int? to avoid complicating the code
    ////

    switch(direction){
        case SerializerDirection.ToStream:

#line 45 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  isSetFlagName , ");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                ",  streamName , ".Write((int?)",  backingStoreName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 50 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 53 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            this.",  isSetFlagName , " = ",  streamName , ".ReadBoolean();\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  enumerationType , ")",  streamName , ".ReadNullableInt32();\r\n");
this.WriteObjects("            }\r\n");
#line 58 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        case SerializerDirection.Export:

#line 61 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream((int?)",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 63 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 66 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                // Import must have default value set\r\n");
this.WriteObjects("                ",  backingStoreName , " = (",  enumerationType , ")XmlStreamer.ReadNullableInt32(",  streamName , ");\r\n");
this.WriteObjects("                this.",  isSetFlagName , " = true;\r\n");
this.WriteObjects("                break;\r\n");
#line 72 "C:\projects\zetbox\Zetbox.Generator\Templates\Serialization\EnumWithDefaultBinarySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}