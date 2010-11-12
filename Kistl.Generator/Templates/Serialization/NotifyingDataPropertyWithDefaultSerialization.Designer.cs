using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst")]
    public partial class NotifyingDataPropertyWithDefaultSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string backingStoreName;
		protected string isSetFlagName;


        public NotifyingDataPropertyWithDefaultSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, string isSetFlagName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.backingStoreName = backingStoreName;
			this.isSetFlagName = isSetFlagName;

        }
        
        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 24 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  isSetFlagName , ", ",  streamName , ");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                BinarySerializer.ToStream(this.",  backingStoreName , ", ",  streamName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 29 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 32 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
this.WriteObjects("            BinarySerializer.FromStream(out this.",  isSetFlagName , ", ",  streamName , ");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                BinarySerializer.FromStream(out this.",  backingStoreName , ", ",  streamName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 37 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.ToXmlStream:

#line 40 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  isSetFlagName , ", ",  streamName , ", \"Is",  xmlname , "Set\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            }\r\n");
#line 45 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.FromXmlStream:

#line 48 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  isSetFlagName , ", ",  streamName , ", \"Is",  xmlname , "Set\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            if (this.",  isSetFlagName , ") {\r\n");
this.WriteObjects("                XmlStreamer.FromStream(ref this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            }\r\n");
#line 53 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.Export:

#line 55 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
this.WriteObjects("    \r\n");
this.WriteObjects("            System.Diagnostics.Debug.Assert(this.",  isSetFlagName , ", \"Exported objects need to have all default values evaluated\");\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 59 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 62 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            this.",  isSetFlagName , " = true;\r\n");
#line 65 "P:\Kistl\Kistl.Generator\Templates\Serialization\NotifyingDataPropertyWithDefaultSerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }



    }
}