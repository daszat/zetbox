using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst")]
    public partial class SimplePropertyWithDefaultSerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberType;
		protected string memberName;
		protected string isSetFlagName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string isSetFlagName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.SimplePropertyWithDefaultSerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName, isSetFlagName);
        }

        public SimplePropertyWithDefaultSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string isSetFlagName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.memberType = memberType;
			this.memberName = memberName;
			this.isSetFlagName = isSetFlagName;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("");
#line 37 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 41 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  isSetFlagName , ");\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\n");
this.WriteObjects("                ",  streamName , ".Write(this.",  memberName , ");\n");
this.WriteObjects("            }\n");
#line 46 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 49 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            this.",  isSetFlagName , " = ",  streamName , ".ReadBoolean();\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\n");
this.WriteObjects("                this.",  memberName , " = ",  streamName , ".",  memberType.SerializerReadMethod() , "();\n");
this.WriteObjects("            }\n");
#line 54 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.Export:

#line 57 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            System.Diagnostics.Debug.Assert(this.",  isSetFlagName , ", \"Exported objects need to have all default values evaluated\");\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\n");
#line 60 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 63 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\n");
this.WriteObjects("                // Import must have default value set\n");
this.WriteObjects("                this.",  memberName , " = XmlStreamer.",  memberType.SerializerReadMethod() , "(",  streamName , ");\n");
this.WriteObjects("                this.",  isSetFlagName , " = true;\n");
this.WriteObjects("                break;\n");
#line 69 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}