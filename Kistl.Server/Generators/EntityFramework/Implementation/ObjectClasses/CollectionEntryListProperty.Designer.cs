using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected Relation rel;
		protected RelationEndRole endRole;


        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, Relation rel, RelationEndRole endRole)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.rel = rel;
			this.endRole = endRole;

        }
        
        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
RelationEnd relEnd = rel.GetEnd(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

	// the name of the property to create
	// TODO: Case #1168
	// Interface & Client Assembly generation uses the navigation property, EntityFramework Implementation uses the role name
	string name = otherEnd.RoleName;
	// the ef-visible property's name
	string efName = name + Kistl.API.Helper.ImplementationSuffix;
	// the name of the IDs list
	string idsListName = efName + "Ids";
	// the name of the private backing store for the conversion wrapper list
	string wrapperName = "_" + name + "Wrapper";
	// the name of the wrapper class for wrapping the other end
	string wrapperClass = "undefined wrapper class";
    if (rel.NeedsPositionStorage(endRole))
	{
	    if ((RelationEndRole)otherEnd.Role == RelationEndRole.A)
	    {
	        wrapperClass = "EntityRelationASideListWrapper";
	    }
	    else if ((RelationEndRole)otherEnd.Role == RelationEndRole.B)
	    {
	        wrapperClass = "EntityRelationBSideListWrapper";
	    }
	}
	else
	{
	    if ((RelationEndRole)otherEnd.Role == RelationEndRole.A)
	    {
	        wrapperClass = "EntityRelationASideCollectionWrapper";
	    }
	    else if ((RelationEndRole)otherEnd.Role == RelationEndRole.B)
	    {
	        wrapperClass = "EntityRelationBSideCollectionWrapper";
	    }
	}

	// the name of the CollectionEntry type
	string ceName = rel.GetRelationFullName() + Kistl.API.Helper.ImplementationSuffix;

	// the name of the EF association to the CollectionEntry
	string assocName = rel.GetRelationAssociationName(endRole);
	// this class' role name in this association
	string roleName = relEnd.RoleName;
	// this targeted role name 
	string targetRoleName = "CollectionEntry";

	// which generic interface to use for the collection
	string exposedListType = rel.NeedsPositionStorage(endRole) ? "IList" : "ICollection";

	// which Kistl interface this is 
	string thisInterface = relEnd.Type.GetDataTypeString();
	// which Kistl interface this list contains
	string referencedInterface = otherEnd.Type.GetDataTypeString();
	// the actual implementation class of the list's elements
	string referencedImplementation = otherEnd.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix;

	// whether or not the collection will be eagerly loaded
	bool eagerLoading = (relEnd.Navigator != null && relEnd.Navigator.EagerLoading)
        || (otherEnd.Navigator != null && otherEnd.Navigator.EagerLoading);


#line 82 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
this.WriteObjects("		// ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  rel.A.Type.GetDataTypeString() , ", ",  rel.B.Type.GetDataTypeString() , ", ",  ceName , ">(\r\n");
this.WriteObjects("                            this,\r\n");
this.WriteObjects("                            ",  efName , ");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  ceName , "> ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  ceName , ">(\r\n");
this.WriteObjects("                        \"Model.",  assocName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !c.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    c.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  rel.A.Type.GetDataTypeString() , ", ",  rel.B.Type.GetDataTypeString() , ", ",  ceName , "> ",  wrapperName , ";\r\n");
this.WriteObjects("\r\n");
#line 120 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
AddSerialization(serializationList, efName, eagerLoading);


        }



    }
}