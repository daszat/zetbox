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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("");
#line 41 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("		",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				if (",  backingName , " == null)\r\n");
this.WriteObjects("				{\r\n");
#line 49 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
// eagerly loaded relation already has the objects loaded
	if (!eagerLoading)
	{

#line 53 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("					Context.FetchRelation<",  entryType , ">(new Guid(\"",  relId , "\"), RelationEndRole.",  role , ", this);\r\n");
#line 55 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}
	else
	{

#line 59 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("					if (!",  name , "_was_eagerLoaded) Context.FetchRelation<",  entryType , ">(new Guid(\"",  relId , "\"), RelationEndRole.",  role , ", this);\r\n");
#line 61 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}

#line 63 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("					",  backingName , " \r\n");
this.WriteObjects("						= new ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , ", ICollection<",  entryType , ">>(\r\n");
this.WriteObjects("							this, \r\n");
this.WriteObjects("							new RelationshipFilter",  role , "SideCollection<",  entryType , ">(this.Context, this));\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				return (",  exposedCollectionInterface , "<",  referencedInterface , ">)",  backingName , ";\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		private ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , ", ICollection<",  entryType , ">> ",  backingName , ";\r\n");
#line 74 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
if (eagerLoading)
	{

#line 76 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("		\r\n");
this.WriteObjects("		private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 79 "P:\zetbox\Zetbox.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}
        AddSerialization(serializationList, name, eagerLoading);


        }

    }
}