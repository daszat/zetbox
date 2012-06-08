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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst")]
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
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("");
#line 35 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst"
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

#line 64 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\n");
this.WriteObjects("        // implement the user-visible interface\n");
this.WriteObjects("        [XmlIgnore()]\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedType , "> ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  wrapperName , " == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  thisInterface , ", ",  referencedType , ", ",  referencedCollectionEntry , ", EntityCollection<",  referencedCollectionEntry , ">>(\n");
this.WriteObjects("						this.Context,\n");
this.WriteObjects("                        this,\n");
this.WriteObjects("              			() => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\n");
this.WriteObjects("          	            ",  efName , ");\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return ",  wrapperName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        \n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\n");
this.WriteObjects("        public EntityCollection<",  referencedCollectionEntry , "> ",  efName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedCollectionEntry , ">(\n");
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
this.WriteObjects("        private ",  wrapperClass , "<",  thisInterface , ", ",  referencedType , ", ",  referencedCollectionEntry , ", EntityCollection<",  referencedCollectionEntry , ">> ",  wrapperName , ";\n");

        }

    }
}