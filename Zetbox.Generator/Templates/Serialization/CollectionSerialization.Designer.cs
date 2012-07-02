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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CollectionSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByValue);
        }

        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;
			this.orderByValue = orderByValue;

        }

        public override void Generate()
        {
#line 36 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
if (ShouldSerialize())
    {
        switch(direction)
        {
            case SerializerDirection.ToStream:

#line 42 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            ",  streamName , ".WriteCollectionEntries(this.",  collectionName , ");\r\n");
#line 44 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.FromStream:

#line 47 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            ",  streamName , ".ReadCollectionEntries(this, this.",  collectionName , ");\r\n");
#line 49 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
        case SerializerDirection.Export:

#line 52 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCollectionEntries(this.",  collectionName , "",  orderByValue ? ".OrderBy(i => i.Value)" : String.Empty , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 54 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.MergeImport:

#line 57 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                XmlStreamer.MergeImportCollectionEntries(this, this.",  collectionName , ", ",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 61 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            default:
                throw new ArgumentOutOfRangeException("direction");
        }
    }


        }

    }
}