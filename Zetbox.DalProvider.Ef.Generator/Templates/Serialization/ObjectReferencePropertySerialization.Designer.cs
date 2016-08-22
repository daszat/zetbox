using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Templates.Serialization;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst")]
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
		protected string clsFullName;
		protected string assocName;
		protected string targetRoleName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string sourceMember, string targetMember, string targetGuidMember, string clsFullName, string assocName, string targetRoleName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.ObjectReferencePropertySerialization", ctx, direction, streamName, xmlnamespace, xmlname, sourceMember, targetMember, targetGuidMember, clsFullName, assocName, targetRoleName);
        }

        public ObjectReferencePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string sourceMember, string targetMember, string targetGuidMember, string clsFullName, string assocName, string targetRoleName)
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
			this.clsFullName = clsFullName;
			this.assocName = assocName;
			this.targetRoleName = targetRoleName;

        }

        public override void Generate()
        {
#line 41 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 45 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                var r = this.RelationshipManager.GetRelatedReference<",  clsFullName , ">(\"Model.",  assocName , "\", \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                var key = r.EntityKey;\r\n");
this.WriteObjects("                ",  streamName , ".Write(r.Value != null ? r.Value.ID : (key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null));\r\n");
this.WriteObjects("            }\r\n");
#line 51 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 54 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            ",  streamName , ".Read(out this.",  targetMember , ");\r\n");
#line 56 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        case SerializerDirection.Export:

#line 59 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(",  sourceMember , " != null ? ",  sourceMember , ".ExportGuid : (Guid?)null, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 61 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        case SerializerDirection.MergeImport:

#line 64 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                this.",  targetGuidMember , " = XmlStreamer.ReadNullableGuid(",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 68 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}