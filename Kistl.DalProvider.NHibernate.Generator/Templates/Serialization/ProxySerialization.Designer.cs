using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;
using Kistl.Generator.Templates.Serialization;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst")]
    public partial class ProxySerialization : Kistl.Generator.ResourceTemplate
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

            _host.CallTemplate("Serialization.ProxySerialization", ctx, direction, streamName, xmlnamespace, xmlname, backingStoreName);
        }

        public ProxySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName)
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
#line 20 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 24 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  backingStoreName , ", ",  streamName , ");\r\n");
#line 26 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 29 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
this.WriteObjects("            BinarySerializer.FromStreamConverter(v => this.",  backingStoreName , " = v, ",  streamName , ");\r\n");
#line 31 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
break;
        case SerializerDirection.ToXmlStream:

#line 34 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 36 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
break;
        case SerializerDirection.FromXmlStream:
        case SerializerDirection.MergeImport:

#line 40 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStreamConverter(v => this.",  backingStoreName , " = v, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 42 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
break;
        case SerializerDirection.Export:

#line 44 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
this.WriteObjects("    \r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 47 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Serialization/ProxySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}