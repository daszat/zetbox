using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst")]
    public partial class CollectionSerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool orderByValue;
		protected bool inPlace;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool inPlace)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CollectionSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByValue, inPlace);
        }

        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool inPlace)
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
#line 21 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
if (ShouldSerialize())
    {
        switch(direction)
        {
            case SerializerDirection.ToStream:

#line 27 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            ",  streamName , ".WriteCollectionEntries(this.",  collectionName , ");\r\n");
#line 29 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.FromStream:

#line 32 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            ",  streamName , ".ReadCollectionEntries(this, this.",  collectionName , ");\r\n");
#line 34 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
        case SerializerDirection.Export:

#line 37 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCollectionEntries(this.",  collectionName , "",  orderByValue ? ".OrderBy(i => i.Value)" : String.Empty , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 39 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            case SerializerDirection.MergeImport:

#line 42 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                XmlStreamer.MergeImportCollectionEntries(this, this.",  collectionName , ", ",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 46 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\CollectionSerialization.cst"
break;
            default:
                throw new ArgumentOutOfRangeException("direction");
        }
    }


        }

    }
}