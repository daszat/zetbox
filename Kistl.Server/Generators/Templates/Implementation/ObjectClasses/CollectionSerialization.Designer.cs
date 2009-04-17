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
		protected string collectionName;


        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializerDirection direction, string streamName, string collectionName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.collectionName = collectionName;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
string methodName = direction.ToString().Replace("Xml", "");
    
	switch(direction)
	{
		case SerializerDirection.ToStream:
		case SerializerDirection.FromStream:

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "CollectionEntries(this.",  collectionName , ", ",  streamName , ");\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
		case SerializerDirection.ToXmlStream:
		case SerializerDirection.FromXmlStream:

#line 31 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
this.WriteObjects("            XmlStreamer.",  methodName , "CollectionEntries(this.",  collectionName , ", ",  streamName , ", \"",  collectionName.TrimStart('_') , "\", \"http://dasz.at/Kistl\");\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionSerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}