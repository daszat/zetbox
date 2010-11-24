using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
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


        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, string name, string exposedCollectionInterface, string referencedInterface, string backingName, string backingCollectionType, string aSideType, string bSideType, string entryType, string providerCollectionType, Guid relId, RelationEndRole role, bool eagerLoading)
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
#line 25 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("		",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				if (",  backingName , " == null)\r\n");
this.WriteObjects("				{\r\n");
#line 33 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
// eagerly loaded relation already has the objects loaded
	if (!eagerLoading)
	{

#line 37 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("					Context.FetchRelation<",  entryType , ">(new Guid(\"",  relId , "\"), RelationEndRole.",  role , ", this);\r\n");
#line 39 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}
	else
	{

#line 43 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("					if(!",  name , "_was_eagerLoaded) Context.FetchRelation<",  entryType , ">(new Guid(\"",  relId , "\"), RelationEndRole.",  role , ", this);\r\n");
#line 45 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}

#line 47 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("					",  backingName , " \r\n");
this.WriteObjects("						= new ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , ">(\r\n");
this.WriteObjects("							this, \r\n");
this.WriteObjects("							new RelationshipFilter",  role , "SideCollection<",  entryType , ">(this.Context, this));\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				return ",  backingName , ";\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		private ",  backingCollectionType , "<",  aSideType , ", ",  bSideType , ", ",  entryType , "> ",  backingName , ";\r\n");
#line 58 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
if(eagerLoading)
	{

#line 60 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("		\r\n");
this.WriteObjects("		private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 63 "P:\Kistl\Kistl.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}
        AddSerialization(serializationList, name, eagerLoading);


        }



    }
}