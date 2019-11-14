using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
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
		protected bool serializeRelationEntries;
		protected string entryProxyType;
		protected string inverseNavigatorName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string exposedCollectionInterface, string referencedInterface, string backingName, string backingCollectionType, string aSideType, string bSideType, string entryType, string providerCollectionType, Guid relId, RelationEndRole role, bool eagerLoading, bool serializeRelationEntries, string entryProxyType, string inverseNavigatorName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CollectionEntryListProperty", ctx, serializationList, name, exposedCollectionInterface, referencedInterface, backingName, backingCollectionType, aSideType, bSideType, entryType, providerCollectionType, relId, role, eagerLoading, serializeRelationEntries, entryProxyType, inverseNavigatorName);
        }

        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string exposedCollectionInterface, string referencedInterface, string backingName, string backingCollectionType, string aSideType, string bSideType, string entryType, string providerCollectionType, Guid relId, RelationEndRole role, bool eagerLoading, bool serializeRelationEntries, string entryProxyType, string inverseNavigatorName)
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
			this.serializeRelationEntries = serializeRelationEntries;
			this.entryProxyType = entryProxyType;
			this.inverseNavigatorName = inverseNavigatorName;

        }

        public override void Generate()
        {
#line 45 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CollectionEntryListProperty.cst"
string eventName = "On" + name + "_PostSetter";

#line 47 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("		",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				if (",  backingName , " == null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					",  backingName , " \r\n");
this.WriteObjects("						= new ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , ">(\r\n");
this.WriteObjects("							this, \r\n");
this.WriteObjects("							new ProjectedCollection<",  entryProxyType , ", ",  entryType , ">(\r\n");
this.WriteObjects("                                () => this.Proxy.",  name , ",\r\n");
this.WriteObjects("                                p => (",  entryType , ")OurContext.AttachAndWrap(p),\r\n");
this.WriteObjects("                                ce => (",  entryProxyType , ")((NHibernatePersistenceObject)ce).NHibernateProxy));\r\n");
this.WriteObjects("                    ",  backingName , ".CollectionChanged += (s, e) => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); };\r\n");
this.WriteObjects("                    if (",  name , "_was_eagerLoaded) { ",  name , "_was_eagerLoaded = false; }\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				return (",  exposedCollectionInterface , "<",  referencedInterface , ">)",  backingName , ";\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		private ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , "> ",  backingName , ";\r\n");
this.WriteObjects("		// ignored, but required for Serialization\r\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public Zetbox.API.Async.ZbTask TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return new Zetbox.API.Async.ZbTask<",  exposedCollectionInterface , "<",  referencedInterface , ">>(this.",  name , ");\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 77 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CollectionEntryListProperty.cst"
AddSerialization(serializationList, name, eagerLoading); 

        }

    }
}