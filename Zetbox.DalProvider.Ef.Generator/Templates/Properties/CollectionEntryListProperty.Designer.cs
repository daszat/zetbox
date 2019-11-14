using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst")]
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
#line 34 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
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

   	string eventName = "On" + name + "_PostSetter";

#line 97 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
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
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  rel.A.Type.GetDataTypeString() , ", ",  rel.B.Type.GetDataTypeString() , ", ",  ceName , ", EntityCollection<",  ceName , ">>(\r\n");
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
this.WriteObjects("                return Get",  efName , "Collection();\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private EntityCollection<",  ceName , "> _",  efName , "EntityCollection;\r\n");
this.WriteObjects("        internal EntityCollection<",  ceName , "> Get",  efName , "Collection()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (_",  efName , "EntityCollection == null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                _",  efName , "EntityCollection\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                        .GetRelatedCollection<",  ceName , ">(\r\n");
this.WriteObjects("                            \"Model.",  assocName , "\",\r\n");
this.WriteObjects("                            \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                // the EntityCollection has to be loaded before attaching the AssociationChanged event\r\n");
this.WriteObjects("                // because the event is triggered while relation entries are loaded from the database\r\n");
this.WriteObjects("                // although that does not require notification of the business logic.\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !_",  efName , "EntityCollection.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    _",  efName , "EntityCollection.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                _",  efName , "EntityCollection.AssociationChanged += (s, e) => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); };\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            return _",  efName , "EntityCollection;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  rel.A.Type.GetDataTypeString() , ", ",  rel.B.Type.GetDataTypeString() , ", ",  ceName , ", EntityCollection<",  ceName , ">> ",  wrapperName , ";\r\n");
#line 147 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
if (eagerLoading) { 
#line 148 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 149 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
} 
#line 150 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public Zetbox.API.Async.ZbTask TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return new Zetbox.API.Async.ZbTask<",  exposedListType , "<",  referencedInterface , ">>(this.",  name , ");\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 156 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\CollectionEntryListProperty.cst"
AddSerialization(serializationList, name, eagerLoading); 

        }

    }
}