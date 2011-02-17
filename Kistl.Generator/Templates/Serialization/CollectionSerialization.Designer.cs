using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst")]
    public partial class CollectionSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool orderByB;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByB)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CollectionSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByB);
        }

        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByB)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;
			this.orderByB = orderByB;

        }

        public override void Generate()
        {
#line 20 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
if (ShouldSerialize())
	{
		switch(direction)
		{
			case SerializerDirection.ToStream:

#line 26 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 28 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
break;
			case SerializerDirection.FromStream:

#line 31 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 33 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
break;
			case SerializerDirection.ToXmlStream:

#line 36 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 38 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
break;
			case SerializerDirection.FromXmlStream:

#line 41 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 43 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
break;
			case SerializerDirection.MergeImport:

#line 46 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.MergeImportCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 48 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
break;
		case SerializerDirection.Export:			

#line 50 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCollectionEntries(this.",  collectionName , "",  orderByB ? ".OrderBy(i => i.B)" : String.Empty , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 53 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/CollectionSerialization.cst"
break;
			default:
				throw new ArgumentOutOfRangeException("direction");
		}
	}


        }

    }
}