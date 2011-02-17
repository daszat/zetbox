using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst")]
    public partial class NotifyingDataPropertySerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string backingStoreName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.NotifyingDataPropertySerialization", ctx, direction, streamName, xmlnamespace, xmlname, backingStoreName);
        }

        public NotifyingDataPropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.backingStoreName = backingStoreName;

        }

        public override void Generate()
        {
#line 19 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 23 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  backingStoreName , ", ",  streamName , ");\r\n");
#line 25 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 28 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
this.WriteObjects("            BinarySerializer.FromStream(out this.",  backingStoreName , ", ",  streamName , ");\r\n");
#line 30 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
break;
        case SerializerDirection.ToXmlStream:

#line 33 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 35 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
break;
        case SerializerDirection.FromXmlStream:
        case SerializerDirection.MergeImport:

#line 39 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 41 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
break;
        case SerializerDirection.Export:

#line 43 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
this.WriteObjects("    \r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 46 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/NotifyingDataPropertySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}