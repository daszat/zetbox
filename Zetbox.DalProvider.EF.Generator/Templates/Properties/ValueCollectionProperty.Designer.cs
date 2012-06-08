using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;
using Kistl.App.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst")]
    public partial class ValueCollectionProperty : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected Property prop;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, Property prop)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ValueCollectionProperty", ctx, serializationList, prop);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, Property prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;

        }

        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst"
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

	// which Kistl interface this is 
	string thisInterface = prop.ObjectClass.Name;
	// which type this list contains
	string referencedType = prop.GetElementTypeString();
	// collection entries in this list
	string referencedCollectionEntry = prop.GetCollectionEntryFullName() + ImplementationSuffix;

    AddSerialization(serializationList, efName);
	var eventName = "On" + name + "_PostSetter";

#line 48 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
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
this.WriteObjects("						this.Context,\r\n");
this.WriteObjects("                        this,\r\n");
this.WriteObjects("              			() => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\r\n");
this.WriteObjects("          	            ",  efName , ");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  referencedCollectionEntry , "> ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedCollectionEntry , ">(\r\n");
this.WriteObjects("                        \"Model.",  assocName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !c.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    c.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                c.ForEach(i => i.AttachToContext(Context));\r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  thisInterface , ", ",  referencedType , ", ",  referencedCollectionEntry , ", EntityCollection<",  referencedCollectionEntry , ">> ",  wrapperName , ";\r\n");

        }

    }
}