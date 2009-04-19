using System;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\CollectionEntries\ValueCollectionEntryParentReference.cst")]
    public partial class ValueCollectionEntryParentReference : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected string referencedInterface;
		protected string propertyName;


        public ValueCollectionEntryParentReference(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string referencedInterface, string propertyName)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.referencedInterface = referencedInterface;
			this.propertyName = propertyName;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\CollectionEntries\ValueCollectionEntryParentReference.cst"
string cacheName = "_" + propertyName + "Cache";
	string fkName = "fk_" + propertyName;
	string backingName = "_" + fkName;

#line 22 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\CollectionEntries\ValueCollectionEntryParentReference.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public ",  referencedInterface , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  cacheName , " != null && ",  cacheName , ".ID == ",  backingName , ")\r\n");
this.WriteObjects("                    return ",  cacheName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (",  backingName , ".HasValue)\r\n");
this.WriteObjects("                    ",  cacheName , " = this.Context.Find<",  referencedInterface , ">(",  backingName , ".Value);\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                    ",  cacheName , " = null;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                return ",  cacheName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (value == null && !",  backingName , ".HasValue)\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("                if (value != null && ",  backingName , ".HasValue && value.ID == ",  backingName , ".Value)\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                ",  cacheName , " = value;\r\n");
this.WriteObjects("                if (value != null)\r\n");
this.WriteObjects("					",  fkName , " = value.ID;\r\n");
this.WriteObjects("				else\r\n");
this.WriteObjects("					",  fkName , " = null;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  referencedInterface , " ",  cacheName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public int? ",  fkName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, value);\r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, value);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // backing store for serialization\r\n");
this.WriteObjects("        private int? ",  backingName , ";\r\n");
this.WriteObjects("        \r\n");
#line 75 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\CollectionEntries\ValueCollectionEntryParentReference.cst"
if (serializationList != null)
		serializationList.Add(Kistl.Server.Generators.Templates.Implementation.SerializerType.All, "http://dasz.at/Kistl", propertyName,backingName); // TODO: XML Namespace


        }



    }
}