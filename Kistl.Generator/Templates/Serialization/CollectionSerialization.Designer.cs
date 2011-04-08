using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst")]
    public partial class CollectionSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool orderByValue;
		protected bool inPlace;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool inPlace)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CollectionSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByValue, inPlace);
        }

        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool inPlace)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;
			this.orderByValue = orderByValue;
			this.inPlace = inPlace;

        }

        public override void Generate()
        {
#line 21 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
if (ShouldSerialize())
    {
        switch(direction)
        {
            case SerializerDirection.ToStream:

#line 27 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 29 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.FromStream:

#line 32 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
this.WriteObjects("            result.AddRange(this.",  collectionName , ".Cast<IPersistenceObject>());\r\n");
#line 35 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.ToXmlStream:

#line 38 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 40 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.FromXmlStream:

#line 43 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("            result.AddRange(this.",  collectionName , ".Cast<IPersistenceObject>());\r\n");
#line 46 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.MergeImport:

#line 49 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.MergeImportCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 51 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
        case SerializerDirection.Export:

#line 54 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCollectionEntries(this.",  collectionName , "",  orderByValue ? ".OrderBy(i => i.Value)" : String.Empty , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 56 "P:\Kistl\Kistl.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            default:
                throw new ArgumentOutOfRangeException("direction");
        }
    }


        }

    }
}