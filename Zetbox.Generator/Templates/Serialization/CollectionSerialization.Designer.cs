using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst")]
    public partial class CollectionSerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool orderByValue;
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CollectionSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByValue, disableExport);
        }

        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool disableExport)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;
			this.orderByValue = orderByValue;
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 37 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
if (ShouldSerialize())
    {
        switch(direction)
        {
            case SerializerDirection.ToStream:

#line 43 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            ",  streamName , ".WriteCollectionEntries(this.",  collectionName , ");\r\n");
#line 45 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.FromStream:

#line 48 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            ",  streamName , ".ReadCollectionEntries(this, this.",  collectionName , ");\r\n");
#line 50 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
        case SerializerDirection.Export:

#line 53 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCollectionEntries(this.",  collectionName , "",  orderByValue ? ".OrderBy(i => i.Value)" : String.Empty , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 55 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.MergeImport:

#line 58 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                XmlStreamer.MergeImportCollectionEntries(this, this.",  collectionName , ", ",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 62 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            default:
                throw new ArgumentOutOfRangeException("direction");
        }
    }


        }

    }
}