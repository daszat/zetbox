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


        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;

        }
        
        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
if(ShouldSerialize())
	{
		switch(direction)
		{
			case SerializerDirection.ToStream:

#line 26 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 28 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.FromStream:

#line 31 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.ToXmlStream:

#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.ToStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.FromXmlStream:

#line 41 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.FromStreamCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 43 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			case SerializerDirection.MergeImport:

#line 46 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.MergeImportCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
		case SerializerDirection.Export:

#line 50 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
			default:
				throw new ArgumentOutOfRangeException("direction");
		}
	}


        }



    }
}