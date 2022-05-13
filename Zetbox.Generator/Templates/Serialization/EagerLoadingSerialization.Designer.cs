using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst")]
    public partial class EagerLoadingSerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;
		protected bool serializeIds;
		protected bool serializeRelationEntries;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool serializeIds, bool serializeRelationEntries)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.EagerLoadingSerialization", ctx, direction, streamName, xmlnamespace, xmlname, collectionName, serializeIds, serializeRelationEntries);
        }

        public EagerLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool serializeIds, bool serializeRelationEntries)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;
			this.serializeIds = serializeIds;
			this.serializeRelationEntries = serializeRelationEntries;

        }

        public override void Generate()
        {
#line 36 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("\r\n");
#line 38 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 42 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(eagerLoadLists);\r\n");
this.WriteObjects("            if (eagerLoadLists && auxObjects != null)\r\n");
this.WriteObjects("            {\r\n");
#line 45 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 46 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                ",  streamName , ".Write(true);\r\n");
this.WriteObjects("                ",  streamName , ".Write(",  collectionName , ".Count);\r\n");
#line 48 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 49 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                foreach(var obj in ",  collectionName , ")\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    auxObjects.Add(obj);\r\n");
#line 52 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 53 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                    ",  streamName , ".Write(obj.ID);\r\n");
#line 54 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 55 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                }\r\n");
#line 56 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeRelationEntries) { ApplyRelationEntrySerialization(); }         
#line 57 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            }\r\n");
#line 58 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 59 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            else\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  streamName , ".Write(false);\r\n");
this.WriteObjects("            }\r\n");
#line 63 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 65 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 68 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            ",  collectionName , "_was_eagerLoaded = ",  streamName , ".ReadBoolean();\r\n");
#line 69 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 70 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                bool containsList = ",  streamName , ".ReadBoolean();\r\n");
this.WriteObjects("                if (containsList)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    int numElements = ",  streamName , ".ReadInt32();\r\n");
this.WriteObjects("                    ",  collectionName , "Ids = new List<int>(numElements);\r\n");
this.WriteObjects("                    while (numElements-- > 0) \r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        int id = ",  streamName , ".ReadInt32();\r\n");
this.WriteObjects("                        ",  collectionName , "Ids.Add(id);\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
#line 83 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 85 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}