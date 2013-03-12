using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst")]
    public partial class ValueCollectionEntryParentReference : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string referencedInterface;
		protected string propertyName;
		protected string fkName;
		protected string moduleNamespace;
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string referencedInterface, string propertyName, string fkName, string moduleNamespace, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.ValueCollectionEntryParentReference", ctx, serializationList, referencedInterface, propertyName, fkName, moduleNamespace, disableExport);
        }

        public ValueCollectionEntryParentReference(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string referencedInterface, string propertyName, string fkName, string moduleNamespace, bool disableExport)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.referencedInterface = referencedInterface;
			this.propertyName = propertyName;
			this.fkName = fkName;
			this.moduleNamespace = moduleNamespace;
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 36 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst"
string cacheName = "_" + propertyName + "Cache";
    string backingName = "_" + fkName;

#line 39 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst"
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
this.WriteObjects("                    ",  fkName , " = value.ID;\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                    ",  fkName , " = null;\r\n");
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
this.WriteObjects("                    var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, value);\r\n");
this.WriteObjects("                    ",  backingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, value);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // backing store for serialization\r\n");
this.WriteObjects("        private int? ",  backingName , ";\r\n");
#line 90 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst"
if (serializationList != null)
        serializationList.Add(disableExport ?  Serialization.SerializerType.Binary : Serialization.SerializerType.All, moduleNamespace, propertyName, "int?", backingName); // TODO: XML Namespace


        }

    }
}