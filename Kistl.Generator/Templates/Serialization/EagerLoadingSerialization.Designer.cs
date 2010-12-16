using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst")]
    public partial class EagerLoadingSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool serializeIds;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool serializeIds)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.EagerLoadingSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, serializeIds);
        }

        public EagerLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool serializeIds)
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
#line 19 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("\r\n");
#line 22 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 26 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("			BinarySerializer.ToStream(eagerLoadLists, ",  streamName , ");\r\n");
this.WriteObjects("			if(eagerLoadLists)\r\n");
this.WriteObjects("			{\r\n");
#line 29 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) { 
#line 30 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("				BinarySerializer.ToStream(true, ",  streamName , ");\r\n");
this.WriteObjects("				BinarySerializer.ToStream(",  collectionName , ".Count, ",  streamName , ");\r\n");
#line 32 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
} 
#line 33 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("				foreach(var obj in ",  collectionName , ")\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					if (auxObjects != null) {\r\n");
this.WriteObjects("						auxObjects.Add(obj);\r\n");
this.WriteObjects("					}\r\n");
#line 38 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) { 
#line 39 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("					BinarySerializer.ToStream(obj.ID, ",  streamName , ");\r\n");
#line 40 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
} 
#line 41 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("			}\r\n");
#line 43 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) { 
#line 44 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("			else\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				BinarySerializer.ToStream(false, ",  streamName , ");\r\n");
this.WriteObjects("			}\r\n");
#line 48 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
} 
#line 50 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
break;
		case SerializerDirection.FromStream:

#line 53 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("			BinarySerializer.FromStream(out ",  collectionName , "_was_eagerLoaded, ",  streamName , ");\r\n");
#line 54 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) { 
#line 55 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("				bool containsList;\r\n");
this.WriteObjects("				BinarySerializer.FromStream(out containsList, ",  streamName , ");\r\n");
this.WriteObjects("				if(containsList)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					int numElements;\r\n");
this.WriteObjects("					BinarySerializer.FromStream(out numElements, ",  streamName , ");\r\n");
this.WriteObjects("					",  collectionName , "Ids = new List<int>(numElements);\r\n");
this.WriteObjects("					while (numElements-- > 0) \r\n");
this.WriteObjects("					{\r\n");
this.WriteObjects("						int id;\r\n");
this.WriteObjects("						BinarySerializer.FromStream(out id, ",  streamName , ");\r\n");
this.WriteObjects("						",  collectionName , "Ids.Add(id);\r\n");
this.WriteObjects("					}\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("			}\r\n");
#line 71 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
} 
#line 73 "P:\Kistl\Kistl.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }

    }
}