using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected Relation rel;
		protected RelationEndRole endRole;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, Relation rel, RelationEndRole endRole)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CollectionEntryListProperty", ctx, serializationList, rel, endRole);
        }

        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, Relation rel, RelationEndRole endRole)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.rel = rel;
			this.endRole = endRole;

        }

        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

    // the name of the property to create
    // TODO: Case #1168
    // Interface & Client Assembly generation uses the navigation property, EntityFramework Implementation uses the role name
    string name = relEnd.Navigator.Name;
    // the ef-visible property's name
    string efName = name + ImplementationPropertySuffix;
    // the name of the IDs list
    string idsListName = efName + "Ids";
    // the name of the private backing store for the conversion wrapper list
    string wrapperName = "_" + name;
    // the name of the wrapper class for wrapping the other end
    string wrapperClass = "undefined wrapper class";
    if (rel.NeedsPositionStorage(endRole))
    {
        if (otherEnd.GetRole() == RelationEndRole.A)
        {
            wrapperClass = "EntityRelationASideListWrapper";
        }
        else if (otherEnd.GetRole() == RelationEndRole.B)
        {
            wrapperClass = "EntityRelationBSideListWrapper";
        }
    }
    else
    {
        if (otherEnd.GetRole() == RelationEndRole.A)
        {
            wrapperClass = "EntityRelationASideCollectionWrapper";
        }
        else if (otherEnd.GetRole() == RelationEndRole.B)
        {
            wrapperClass = "EntityRelationBSideCollectionWrapper";
        }
    }

    // the name of the CollectionEntry type
    string ceName = rel.GetRelationFullName() + ImplementationSuffix;

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
    string referencedImplementation = otherEnd.Type.GetDataTypeString() + ImplementationSuffix;

    // whether or not the collection will be eagerly loaded
    bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;


#line 80 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\r\n");
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
this.WriteObjects("                    c.ForEach(i => i.AttachToContext(Context));\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  rel.A.Type.GetDataTypeString() , ", ",  rel.B.Type.GetDataTypeString() , ", ",  ceName , "> ",  wrapperName , ";\r\n");
this.WriteObjects("\r\n");
#line 119 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
if (eagerLoading)
    {

#line 122 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        private bool ",  efName , "_was_eagerLoaded = false;\r\n");
#line 124 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
}
        AddSerialization(serializationList, efName, eagerLoading);


        }

    }
}