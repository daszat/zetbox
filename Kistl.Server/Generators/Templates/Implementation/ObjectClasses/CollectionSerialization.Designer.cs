using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst")]
    public partial class CollectionSerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool orderByB;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByB)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Implementation.ObjectClasses.CollectionSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByB);
        }

        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByB)
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
#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
if(ShouldSerialize())
	{
		switch(direction)
		{
			case SerializerDirection.ToStream:

#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.FromStream:

#line 32 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.ToXmlStream:

#line 37 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.FromXmlStream:

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.MergeImport:

#line 47 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.MergeImportCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 49 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
		case SerializerDirection.Export:			

#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCollectionEntries(this.",  collectionName , "",  orderByB ? ".OrderBy(i => i.B)" : String.Empty , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 54 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			default:
				throw new ArgumentOutOfRangeException("direction");
		}
	}


        }

    }
}