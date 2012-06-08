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
		protected string moduleNamespace;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string referencedInterface, string propertyName, string moduleNamespace)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.ValueCollectionEntryParentReference", ctx, serializationList, referencedInterface, propertyName, moduleNamespace);
        }

        public ValueCollectionEntryParentReference(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string referencedInterface, string propertyName, string moduleNamespace)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.referencedInterface = referencedInterface;
			this.propertyName = propertyName;
			this.moduleNamespace = moduleNamespace;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst"
this.WriteObjects("");
#line 34 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst"
string cacheName = "_" + propertyName + "Cache";
    string fkName = "fk_" + propertyName;
    string backingName = "_" + fkName;

#line 38 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst"
this.WriteObjects("        public ",  referencedInterface , " ",  propertyName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  cacheName , " != null && ",  cacheName , ".ID == ",  backingName , ")\n");
this.WriteObjects("                    return ",  cacheName , ";\n");
this.WriteObjects("\n");
this.WriteObjects("                if (",  backingName , ".HasValue)\n");
this.WriteObjects("                    ",  cacheName , " = this.Context.Find<",  referencedInterface , ">(",  backingName , ".Value);\n");
this.WriteObjects("                else\n");
this.WriteObjects("                    ",  cacheName , " = null;\n");
this.WriteObjects("\n");
this.WriteObjects("                return ",  cacheName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (value == null && !",  backingName , ".HasValue)\n");
this.WriteObjects("                    return;\n");
this.WriteObjects("                if (value != null && ",  backingName , ".HasValue && value.ID == ",  backingName , ".Value)\n");
this.WriteObjects("                    return;\n");
this.WriteObjects("\n");
this.WriteObjects("                ",  cacheName , " = value;\n");
this.WriteObjects("                if (value != null)\n");
this.WriteObjects("                    ",  fkName , " = value.ID;\n");
this.WriteObjects("                else\n");
this.WriteObjects("                    ",  fkName , " = null;\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        private ",  referencedInterface , " ",  cacheName , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        public int? ",  fkName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                return ",  backingName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  backingName , " != value)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var __oldValue = ",  backingName , ";\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, value);\n");
this.WriteObjects("                    ",  backingName , " = value;\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, value);\n");
this.WriteObjects("                }\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        // backing store for serialization\n");
this.WriteObjects("        private int? ",  backingName , ";\n");
#line 89 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ValueCollectionEntryParentReference.cst"
if (serializationList != null)
        serializationList.Add(Serialization.SerializerType.All, moduleNamespace, propertyName, "int?", backingName); // TODO: XML Namespace


        }

    }
}