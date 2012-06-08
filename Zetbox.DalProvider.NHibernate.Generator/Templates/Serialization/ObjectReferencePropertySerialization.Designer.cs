using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;
using Zetbox.Generator.Templates.Serialization;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst")]
    public partial class ObjectReferencePropertySerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string sourceMember;
		protected string targetMember;
		protected string targetGuidMember;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string sourceMember, string targetMember, string targetGuidMember)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.ObjectReferencePropertySerialization", ctx, direction, streamName, xmlnamespace, xmlname, sourceMember, targetMember, targetGuidMember);
        }

        public ObjectReferencePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string sourceMember, string targetMember, string targetGuidMember)
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("");
#line 38 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(",  sourceMember , " != null ? OurContext.GetIdFromProxy(",  sourceMember , ") : (int?)null);\r\n");
#line 44 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 47 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            ",  streamName , ".Read(out this.",  targetMember , ");\r\n");
#line 49 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        case SerializerDirection.Export:

#line 52 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(",  sourceMember , " != null ? ",  sourceMember , ".ExportGuid : (Guid?)null, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                this.",  targetGuidMember , " = XmlStreamer.ReadNullableGuid(",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 61 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}