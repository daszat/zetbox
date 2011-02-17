using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst")]
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
#line 21 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 25 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  isSetFlagName , ", ",  streamName , ");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 30 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 33 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            BinarySerializer.FromStream(out this.",  isSetFlagName , ", ",  streamName , ");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                ",  memberType , " tmp;\r\n");
this.WriteObjects("                BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("                this.",  memberName , " = tmp;\r\n");
this.WriteObjects("            }\r\n");
#line 40 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.ToXmlStream:

#line 43 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  isSetFlagName , ", ",  streamName , ", \"Is",  xmlname , "Set\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            }\r\n");
#line 48 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.FromXmlStream:

#line 51 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  isSetFlagName , ", ",  streamName , ", \"Is",  xmlname , "Set\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                // yuck\r\n");
this.WriteObjects("                ",  memberType , " tmp = this.",  memberName , ";\r\n");
this.WriteObjects("                XmlStreamer.FromStream(ref tmp, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("                this.",  memberName , " = tmp;\r\n");
this.WriteObjects("            }\r\n");
#line 59 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.Export:

#line 62 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            System.Diagnostics.Debug.Assert(this.",  isSetFlagName , ", \"Exported objects need to have all default values evaluated\");\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 65 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 68 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
this.WriteObjects("            // Import must have default value set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // yuck\r\n");
this.WriteObjects("                ",  memberType , " tmp = this.",  memberName , ";\r\n");
this.WriteObjects("                XmlStreamer.FromStream(ref tmp, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("                this.",  memberName , " = tmp;\r\n");
this.WriteObjects("                this.",  isSetFlagName , " = true;\r\n");
this.WriteObjects("            }\r\n");
#line 77 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/SimplePropertyWithDefaultSerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}