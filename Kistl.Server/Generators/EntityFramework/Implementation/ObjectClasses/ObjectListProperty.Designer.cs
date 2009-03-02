using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected Relation rel;
		protected RelationEndRole endRole;


        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, Relation rel, RelationEndRole endRole)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.rel = rel;
			this.endRole = endRole;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectListProperty.cst"
RelationEnd relEnd = rel.GetEnd(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

	// the name of the property to create
	string name = otherEnd.RoleName;
	// the ef-visible property's name
	string efName = name + Kistl.API.Helper.ImplementationSuffix;
	// the name of the private backing store for the conversion wrapper list
	string wrapperName = "_" + name + "Wrapper";
	// the name of the wrapper class for wrapping the EntityCollection
	string wrapperClass = otherEnd.HasPersistentOrder ? "EntityListWrapper" : "EntityCollectionWrapper";
	
	// the name of the EF association
	string assocName = rel.GetAssociationName();
	string targetRoleName = otherEnd.RoleName;

	// which generic interface to use for the collection
	string exposedListType = otherEnd.HasPersistentOrder ? "IList" : "ICollection";

	// which Kistl interface this is 
	string thisInterface = relEnd.Type.GetDataTypeString();
	// which Kistl interface this list contains
	string referencedInterface = otherEnd.Type.GetDataTypeString();
	// the actual implementation class of the list's elements
	string referencedImplementation = otherEnd.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix;

	

#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ", ",  referencedImplementation , ">(\r\n");
this.WriteObjects("                            this.Context, ",  efName , "");
#line 57 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectListProperty.cst"
// TODO: improve this!
	if (otherEnd.HasPersistentOrder)
	{
		this.WriteObjects(", \"", relEnd.RoleName, "\"");
	}
                            
#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects(");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  referencedImplementation , "> ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedImplementation , ">(\r\n");
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
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , ", ",  referencedImplementation , "> ",  wrapperName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");

        }



    }
}