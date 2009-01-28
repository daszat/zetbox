using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected RelationEnd relEnd;


        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, RelationEnd relEnd)
            : base(_host)
        {
			this.ctx = ctx;
			this.relEnd = relEnd;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
NewRelation rel = relEnd.Container;

	// the name of the property to create
	string name = relEnd.Other.RoleName;
	// the ef-visible property's name
	string efName = name + Kistl.API.Helper.ImplementationSuffix;
	// the name of the private backing store for the conversion wrapper list
	string wrapperName = "_" + name + "Wrapper";
	// the name of the wrapper class for wrapping the other end
	string wrapperClass = Construct.EntityWrapperClassName(relEnd.Other);

	// the name of the CollectionEntry type
	string ceName = rel.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix;

	// the name of the EF association to the CollectionEntry
	string assocName = rel.GetCollectionEntryAssociationName(relEnd);
	// this class' role name in this association
	string roleName = relEnd.RoleName;

	// which generic interface to use for the collection
	string exposedListType = relEnd.Other.HasPersistentOrder ? "IList" : "ICollection";

	// which Kistl interface this is 
	string thisInterface = relEnd.Type.NameDataObject;
	// which Kistl interface this list contains
	string referencedInterface = relEnd.Other.Type.NameDataObject;
	// the actual implementation class of the list's elements
	string referencedImplementation = relEnd.Other.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix;

	

#line 48 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntryListProperty.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  rel.A.Type.NameDataObject , ", ",  rel.B.Type.NameDataObject , ", ",  ceName , ">(\r\n");
this.WriteObjects("                            this,\r\n");
this.WriteObjects("                            ",  efName , ");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  roleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  ceName , "> ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  ceName , ">(\r\n");
this.WriteObjects("                        \"Model.",  assocName , "\",\r\n");
this.WriteObjects("                        \"",  roleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !c.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    c.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  rel.A.Type.NameDataObject , ", ",  rel.B.Type.NameDataObject , ", ",  ceName , "> ",  wrapperName , ";\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("\r\n");

        }



    }
}