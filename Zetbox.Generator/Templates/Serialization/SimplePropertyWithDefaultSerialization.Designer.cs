using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst")]
    public partial class SimplePropertyWithDefaultSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberType;
		protected string memberName;
		protected string isSetFlagName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string isSetFlagName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.SimplePropertyWithDefaultSerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName, isSetFlagName);
        }

        public SimplePropertyWithDefaultSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string isSetFlagName)
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
#line 21 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 25 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  isSetFlagName , ");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                ",  streamName , ".Write(this.",  memberName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 30 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 33 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            this.",  isSetFlagName , " = ",  streamName , ".ReadBoolean();\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                this.",  memberName , " = ",  streamName , ".",  memberType.SerializerReadMethod() , "();\r\n");
this.WriteObjects("            }\r\n");
#line 38 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.Export:

#line 41 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            System.Diagnostics.Debug.Assert(this.",  isSetFlagName , ", \"Exported objects need to have all default values evaluated\");\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 44 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 47 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                // Import must have default value set\r\n");
this.WriteObjects("                this.",  memberName , " = XmlStreamer.",  memberType.SerializerReadMethod() , "(",  streamName , ");\r\n");
this.WriteObjects("                this.",  isSetFlagName , " = true;\r\n");
this.WriteObjects("                break;\r\n");
#line 53 "P:\Kistl\Kistl.Generator\Templates\Serialization\SimplePropertyWithDefaultSerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}