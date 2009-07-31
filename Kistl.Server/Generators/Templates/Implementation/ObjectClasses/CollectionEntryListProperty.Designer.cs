using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
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


        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string exposedCollectionInterface, string referencedInterface, string backingName, string backingCollectionType, string aSideType, string bSideType, string entryType, string providerCollectionType, Guid relId, RelationEndRole role, bool eagerLoading)
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
#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("		public ",  exposedCollectionInterface , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				if (",  backingName , " == null)\r\n");
this.WriteObjects("				{\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
// eagerly loaded relation already has the objects loaded
	if (!eagerLoading)
	{

#line 37 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
this.WriteObjects("					Context.FetchRelation<",  entryType , ">(new Guid(\"",  relId , "\"), RelationEndRole.",  role , ", this);\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
}

#line 41 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
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
#line 52 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
AddSerialization(serializationList, name, eagerLoading);


        }



    }
}