using System;
using System.Collections.Generic;
using System.Diagnostics;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;
using Zetbox.App.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst")]
    public partial class ValueCollectionProperty : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected Property prop;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, Property prop)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ValueCollectionProperty", ctx, serializationList, prop);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, Property prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;

        }

        public override void Generate()
        {
#line 35 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst"
Debug.Assert(prop is ValueTypeProperty ? ((ValueTypeProperty)prop).IsList : ((CompoundObjectProperty)prop).IsList);
    bool hasPersistentOrder = prop is ValueTypeProperty ? ((ValueTypeProperty)prop).HasPersistentOrder : ((CompoundObjectProperty)prop).HasPersistentOrder;

    // the name of the property to create
    string name = prop.Name;
    // the ef-visible property's name
    string efName = name + ImplementationPropertySuffix;
    // the name of the private backing store for the conversion wrapper list
    string wrapperName = "_" + name;
    // the name of the wrapper class for wrapping the EntityCollection
    string wrapperClass = (hasPersistentOrder ? "EfValueListWrapper" : "EfValueCollectionWrapper");

    // the name of the EF association
    string assocName = prop.GetAssociationName();
    string targetRoleName = "CollectionEntry";

    // which generic interface to use for the collection
    string exposedListType = hasPersistentOrder ? "IList" : "ICollection";

    // which Zetbox interface this is
    string thisInterface = prop.ObjectClass.Name;
    // which type this list contains
    string referencedType = prop.GetElementTypeString();
    // collection entries in this list
    string referencedCollectionEntry = prop.GetCollectionEntryFullName() + ImplementationSuffix;

    AddSerialization(serializationList, efName);
    var eventName = "On" + name + "_PostSetter";

#line 64 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedType , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  thisInterface , ", ",  referencedType , ", ",  referencedCollectionEntry , ", EntityCollection<",  referencedCollectionEntry , ">>(\r\n");
this.WriteObjects("                        this.Context,\r\n");
this.WriteObjects("                        this,\r\n");
this.WriteObjects("                        null, // see Get",  efName , "Collection()\r\n");
this.WriteObjects("                        ",  efName , ");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  referencedCollectionEntry , "> ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return Get",  efName , "Collection();\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        internal EntityCollection<",  referencedCollectionEntry , "> Get",  efName , "Collection()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (_",  efName , "EntityCollection == null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                _",  efName , "EntityCollection = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedCollectionEntry , ">(\r\n");
this.WriteObjects("                        \"Model.",  assocName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                // the EntityCollection has to be loaded before attaching the AssociationChanged event\r\n");
this.WriteObjects("                // because the event is triggered while relation entries are loaded from the database\r\n");
this.WriteObjects("                // although that does not require notification of the business logic.\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !_",  efName , "EntityCollection.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    _",  efName , "EntityCollection.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                _",  efName , "EntityCollection.AssociationChanged += (s, e) => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if (",  eventName , " != null && IsAttached) ",  eventName, "(this); };\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            return _",  efName , "EntityCollection;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private EntityCollection<",  referencedCollectionEntry , "> _",  efName , "EntityCollection;\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  thisInterface , ", ",  referencedType , ", ",  referencedCollectionEntry , ", EntityCollection<",  referencedCollectionEntry , ">> ",  wrapperName , ";\r\n");

        }

    }
}