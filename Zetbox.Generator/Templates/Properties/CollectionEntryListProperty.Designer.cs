using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Serialization.SerializationMembersList serializationList;
		protected string name;
		protected string exposedCollectionInterface;
		protected string referencedInterface;
		protected string backingName;
		protected string backingCollectionType;
		protected string aSideType;
		protected string bSideType;
		protected string entryType;
		protected string providerCollectionType;
		protected Guid relId;
		protected RelationEndRole role;
		protected bool eagerLoading;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string name, string exposedCollectionInterface, string referencedInterface, string backingName, string backingCollectionType, string aSideType, string bSideType, string entryType, string providerCollectionType, Guid relId, RelationEndRole role, bool eagerLoading)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CollectionEntryListProperty", ctx, serializationList, name, exposedCollectionInterface, referencedInterface, backingName, backingCollectionType, aSideType, bSideType, entryType, providerCollectionType, relId, role, eagerLoading);
        }

        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string name, string exposedCollectionInterface, string referencedInterface, string backingName, string backingCollectionType, string aSideType, string bSideType, string entryType, string providerCollectionType, Guid relId, RelationEndRole role, bool eagerLoading)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.exposedCollectionInterface = exposedCollectionInterface;
			this.referencedInterface = referencedInterface;
			this.backingName = backingName;
			this.backingCollectionType = backingCollectionType;
			this.aSideType = aSideType;
			this.bSideType = bSideType;
			this.entryType = entryType;
			this.providerCollectionType = providerCollectionType;
			this.relId = relId;
			this.role = role;
			this.eagerLoading = eagerLoading;

        }

        public override void Generate()
        {
#line 42 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
string taskName = "_triggerFetch" + name + "Task";
    string eventName = "On" + name + "_PostSetter";

#line 45 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  backingName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    TriggerFetch",  name , "Async().Wait();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return (",  exposedCollectionInterface , "<",  referencedInterface , ">)",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        Zetbox.API.Async.ZbTask ",  taskName , ";\r\n");
this.WriteObjects("        public Zetbox.API.Async.ZbTask TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (",  taskName , " != null) return ",  taskName , ";\r\n");
#line 63 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
// eagerly loaded relation already has the objects loaded
    if (!eagerLoading)
    {

#line 67 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("            ",  taskName , " = Context.FetchRelationAsync<",  entryType , ">(new Guid(\"",  relId , "\"), RelationEndRole.",  role , ", this);\r\n");
#line 69 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}
    else
    {

#line 73 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("            if (!",  name , "_was_eagerLoaded) ",  taskName , " = Context.FetchRelationAsync<",  entryType , ">(new Guid(\"",  relId , "\"), RelationEndRole.",  role , ", this);\r\n");
this.WriteObjects("            else ",  taskName , " = new Zetbox.API.Async.ZbTask(Zetbox.API.Async.ZbTask.Synchron, null);\r\n");
#line 76 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}

#line 78 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("            ",  taskName , ".OnResult(r =>\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  backingName , "\r\n");
this.WriteObjects("                    = new ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , ", ICollection<",  entryType , ">>(\r\n");
this.WriteObjects("                        this,\r\n");
this.WriteObjects("                        new RelationshipFilter",  role , "SideCollection<",  entryType , ">(this.Context, this));\r\n");
this.WriteObjects("                        ",  backingName , ".CollectionChanged += (s, e) => { On",  name , "CollectionChanged(); };\r\n");
this.WriteObjects("            });\r\n");
this.WriteObjects("            return ",  taskName , ";\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        internal void On",  name , "CollectionChanged()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            NotifyPropertyChanged(\"",  name , "\", null, null);\r\n");
this.WriteObjects("            if (",  eventName , " != null && IsAttached)\r\n");
this.WriteObjects("                ",  eventName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , ", ICollection<",  entryType , ">> ",  backingName , ";\r\n");
#line 98 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
if (eagerLoading)
    {

#line 101 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 103 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}
        AddSerialization(serializationList, name, eagerLoading);

#line 106 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}