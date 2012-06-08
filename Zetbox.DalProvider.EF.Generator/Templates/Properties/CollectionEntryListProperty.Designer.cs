using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst")]
    public partial class CollectionEntryListProperty : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected Relation rel;
		protected RelationEndRole endRole;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, Relation rel, RelationEndRole endRole)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CollectionEntryListProperty", ctx, serializationList, rel, endRole);
        }

        public CollectionEntryListProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, Relation rel, RelationEndRole endRole)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.rel = rel;
			this.endRole = endRole;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("");
#line 34 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
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
            wrapperClass = "ASideListWrapper";
        }
        else if (otherEnd.GetRole() == RelationEndRole.B)
        {
            wrapperClass = "BSideListWrapper";
        }
    }
    else
    {
        if (otherEnd.GetRole() == RelationEndRole.A)
        {
            wrapperClass = "ASideCollectionWrapper";
        }
        else if (otherEnd.GetRole() == RelationEndRole.B)
        {
            wrapperClass = "BSideCollectionWrapper";
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

    // which Zetbox interface this is 
    string thisInterface = relEnd.Type.GetDataTypeString();
    // which Zetbox interface this list contains
    string referencedInterface = otherEnd.Type.GetDataTypeString();
    // the actual implementation class of the list's elements
    string referencedImplementation = otherEnd.Type.GetDataTypeString() + ImplementationSuffix;

    // whether or not the collection will be eagerly loaded
    bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;


#line 96 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\n");
this.WriteObjects("        // implement the user-visible interface\n");
this.WriteObjects("        [XmlIgnore()]\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  wrapperName , " == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  rel.A.Type.GetDataTypeString() , ", ",  rel.B.Type.GetDataTypeString() , ", ",  ceName , ", EntityCollection<",  ceName , ">>(\n");
this.WriteObjects("                            this,\n");
this.WriteObjects("                            ",  efName , ");\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return ",  wrapperName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        \n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\n");
this.WriteObjects("        public EntityCollection<",  ceName , "> ",  efName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\n");
this.WriteObjects("                    .GetRelatedCollection<",  ceName , ">(\n");
this.WriteObjects("                        \"Model.",  assocName , "\",\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\n");
this.WriteObjects("                    && !c.IsLoaded)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    c.Load();\n");
this.WriteObjects("                }\n");
this.WriteObjects("                c.ForEach(i => i.AttachToContext(Context));\n");
this.WriteObjects("                return c;\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        private ",  wrapperClass , "<",  rel.A.Type.GetDataTypeString() , ", ",  rel.B.Type.GetDataTypeString() , ", ",  ceName , ", EntityCollection<",  ceName , ">> ",  wrapperName , ";\n");
#line 133 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
if (eagerLoading) { 
#line 134 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\n");
#line 135 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
} 
#line 136 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
AddSerialization(serializationList, name, eagerLoading); 

        }

    }
}