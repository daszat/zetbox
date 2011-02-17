using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst")]
    public partial class ObjectReferencePropertySerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string sourceMember;
		protected string targetMember;
		protected string targetGuidMember;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string sourceMember, string targetMember, string targetGuidMember)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.ObjectReferencePropertySerialization", ctx, direction, streamName, xmlnamespace, xmlname, sourceMember, targetMember, targetGuidMember);
        }

        public ObjectReferencePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string sourceMember, string targetMember, string targetGuidMember)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.sourceMember = sourceMember;
			this.targetMember = targetMember;
			this.targetGuidMember = targetGuidMember;

        }

        public override void Generate()
        {
#line 21 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 25 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(",  sourceMember , " != null ? ",  sourceMember , ".ID : (int?)null, ",  streamName , ");\r\n");
#line 27 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.FromStream:

#line 30 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.FromStream(out this.",  targetMember , ", ",  streamName , ");\r\n");
#line 32 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.ToXmlStream:

#line 35 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(",  sourceMember , " != null ? ",  sourceMember , ".ID : (int?)null, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 37 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.FromXmlStream:

#line 40 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  targetMember , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 42 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.Export:

#line 45 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(",  sourceMember , " != null ? ",  sourceMember , ".ExportGuid : (Guid?)null, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 47 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.MergeImport: 

#line 50 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this.",  targetGuidMember , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 52 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/ObjectReferencePropertySerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }

    }
}