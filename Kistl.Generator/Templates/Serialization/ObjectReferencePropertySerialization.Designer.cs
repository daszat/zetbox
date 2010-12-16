using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst")]
    public partial class ObjectReferencePropertySerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.ObjectReferencePropertySerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberName);
        }

        public ObjectReferencePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.memberName = memberName;

        }

        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 23 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(",  memberName , " != null ? ",  memberName , ".ID : (int?)null, ",  streamName , ");\r\n");
#line 25 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.FromStream:

#line 28 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.FromStream(out this._fk_",  memberName , ", ",  streamName , ");\r\n");
#line 30 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.ToXmlStream:

#line 33 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(",  memberName , " != null ? ",  memberName , ".ID : (int?)null, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 35 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.FromXmlStream:

#line 38 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this._fk_",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 40 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.Export:

#line 43 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(",  memberName , " != null ? ",  memberName , ".ExportGuid : (Guid?)null, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 45 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.MergeImport: 

#line 48 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this._fk_guid_",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 50 "P:\Kistl\Kistl.Generator\Templates\Serialization\ObjectReferencePropertySerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }

    }
}