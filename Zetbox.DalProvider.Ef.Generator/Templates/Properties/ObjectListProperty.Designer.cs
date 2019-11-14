using System;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string name;
		protected string wrapperName;
		protected string wrapperClass;
		protected string exposedListType;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected string positionPropertyName;
		protected string otherName;
		protected string referencedInterface;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectListProperty", ctx, serializationList, name, wrapperName, wrapperClass, exposedListType, rel, endRole, positionPropertyName, otherName, referencedInterface);
        }

        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.wrapperName = wrapperName;
			this.wrapperClass = wrapperClass;
			this.exposedListType = exposedListType;
			this.rel = rel;
			this.endRole = endRole;
			this.positionPropertyName = positionPropertyName;
			this.otherName = otherName;
			this.referencedInterface = referencedInterface;

        }

        public override void Generate()
        {
#line 41 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

    // the ef-visible property's name
    string efName = name + ImplementationPropertySuffix;
    // the name of the position property as string argument
    string positionPropertyNameArgument = rel.NeedsPositionStorage(otherEnd.GetRole()) ? String.Format(@", ""{0}""", Construct.ListPositionPropertyName(otherEnd)) : String.Empty;
    
    // the name of the EF association
    string assocName = rel.GetAssociationName() + (relEnd.Multiplicity.UpperBound() > 1 ? "_" + relEnd.GetRole().ToString() : String.Empty);
    string targetRoleName = otherEnd.RoleName;

    // which Zetbox interface this is    
    string thisInterface = relEnd.Type.GetDataTypeString();
    // the actual implementation class of the list's elements
    string referencedImplementation = referencedInterface + ImplementationSuffix;

    // whether or not the collection will be eagerly loaded
    bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;

    // override and ignore Base's notion of wrapper classes
    wrapperClass = rel.NeedsPositionStorage(otherEnd.GetRole()) ? "EntityListWrapper" : "EntityCollectionWrapper";

	var eventName = "On" + name + "_PostSetter";

#line 66 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("		[System.Runtime.Serialization.IgnoreDataMember]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ", ",  referencedImplementation , ">(\r\n");
this.WriteObjects("                            this.Context, ",  efName , ",\r\n");
this.WriteObjects("                            () => this.NotifyPropertyChanging(\"",  name , "\", null, null),\r\n");
this.WriteObjects("                            null, // see Get",  efName , "Collection()\r\n");
this.WriteObjects("                            (item) => item.NotifyPropertyChanging(\"",  otherName , "\", null, null),\r\n");
this.WriteObjects("                            (item) => item.NotifyPropertyChanged(\"",  otherName , "\", null, null)");
#line 83 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
// TODO: improve this!
    if (rel.NeedsPositionStorage(otherEnd.GetRole()))
    {
        this.WriteObjects(", \"", relEnd.RoleName, "\"");
    }
                            
#line 88 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("",  positionPropertyNameArgument , ");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  referencedImplementation , "> ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return Get",  efName , "Collection();\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , ", ",  referencedImplementation , "> ",  wrapperName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private EntityCollection<",  referencedImplementation , "> _",  efName , "EntityCollection;\r\n");
this.WriteObjects("        internal EntityCollection<",  referencedImplementation , "> Get",  efName , "Collection()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (_",  efName , "EntityCollection == null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                _",  efName , "EntityCollection = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedImplementation , ">(\r\n");
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
this.WriteObjects("\r\n");
this.WriteObjects("        public Zetbox.API.Async.ZbTask TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return new Zetbox.API.Async.ZbTask<",  exposedListType , "<",  referencedInterface , ">>(this.",  name , ");\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 131 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading) { 
#line 132 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        private List<int> ",  name , "Ids;\r\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 135 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
if (serializationList != null)
        {
            serializationList.Add("Serialization.EagerLoadingSerialization", Zetbox.Generator.Templates.Serialization.SerializerType.Binary, null, null, name, true, false, null);
        }
    }

#line 141 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}