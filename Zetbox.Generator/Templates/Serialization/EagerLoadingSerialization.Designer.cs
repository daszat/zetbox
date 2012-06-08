using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst")]
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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("");
#line 36 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("\n");
#line 38 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 42 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(eagerLoadLists);\n");
this.WriteObjects("            if (eagerLoadLists && auxObjects != null)\n");
this.WriteObjects("            {\n");
#line 45 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 46 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                ",  streamName , ".Write(true);\n");
this.WriteObjects("                ",  streamName , ".Write(",  collectionName , ".Count);\n");
#line 48 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 49 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                foreach(var obj in ",  collectionName , ")\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    auxObjects.Add(obj);\n");
#line 52 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 53 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                    ",  streamName , ".Write(obj.ID);\n");
#line 54 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 55 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("                }\n");
#line 56 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeRelationEntries) { ApplyRelationEntrySerialization(); }         
#line 57 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            }\n");
#line 58 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 59 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            else\n");
this.WriteObjects("            {\n");
this.WriteObjects("                ",  streamName , ".Write(false);\n");
this.WriteObjects("            }\n");
#line 63 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 65 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
break;
        case SerializerDirection.FromStream:

#line 68 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            ",  collectionName , "_was_eagerLoaded = ",  streamName , ".ReadBoolean();\n");
#line 69 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
if (serializeIds) {                                                          
#line 70 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
this.WriteObjects("            {\n");
this.WriteObjects("                bool containsList = ",  streamName , ".ReadBoolean();\n");
this.WriteObjects("                if (containsList)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    int numElements = ",  streamName , ".ReadInt32();\n");
this.WriteObjects("                    ",  collectionName , "Ids = new List<int>(numElements);\n");
this.WriteObjects("                    while (numElements-- > 0) \n");
this.WriteObjects("                    {\n");
this.WriteObjects("                        int id = ",  streamName , ".ReadInt32();\n");
this.WriteObjects("                        ",  collectionName , "Ids.Add(id);\n");
this.WriteObjects("                    }\n");
this.WriteObjects("                }\n");
this.WriteObjects("            }\n");
#line 83 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
}                                                                            
#line 85 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerLoadingSerialization.cst"
break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}