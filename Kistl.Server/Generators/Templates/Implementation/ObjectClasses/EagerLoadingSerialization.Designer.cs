using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst")]
    public partial class EagerLoadingSerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool serializeIds;


        public EagerLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool serializeIds)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;
			this.serializeIds = serializeIds;

        }
        
        public override void Generate()
        {
#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
this.WriteObjects("			{\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
if (serializeIds) { 
#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
this.WriteObjects("				BinarySerializer.ToStream(",  collectionName , ".Count, ",  streamName , ");\r\n");
#line 28 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
} 
#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
this.WriteObjects("				foreach(var obj in ",  collectionName , ")\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					auxObjects.Add(obj);\r\n");
#line 32 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
if (serializeIds) { 
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
this.WriteObjects("					BinarySerializer.ToStream(obj.ID, ",  streamName , ");\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
} 
#line 35 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("			}\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
break;
		case SerializerDirection.FromStream:
			if (serializeIds)
			{ 

#line 43 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("				int numElements;\r\n");
this.WriteObjects("				BinarySerializer.FromStream(out numElements, ",  streamName , ");\r\n");
this.WriteObjects("				",  collectionName , "Ids = new List<int>(numElements);\r\n");
this.WriteObjects("				while (numElements-- > 0) \r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					int id;\r\n");
this.WriteObjects("					BinarySerializer.FromStream(out id, ",  streamName , ");\r\n");
this.WriteObjects("					",  collectionName , "Ids.Add(id);\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
}
			break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}