using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst")]
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
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("");
#line 40 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
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

#line 65 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("           // ",  this.GetType() , "\n");
this.WriteObjects("        // implement the user-visible interface\n");
this.WriteObjects("        [XmlIgnore()]\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  wrapperName , " == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ", ",  referencedImplementation , ">(\n");
this.WriteObjects("                            this.Context, ",  efName , ",\n");
this.WriteObjects("                            () => this.NotifyPropertyChanging(\"",  name , "\", null, null),\n");
this.WriteObjects("                            () => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\n");
this.WriteObjects("                            (item) => item.NotifyPropertyChanging(\"",  otherName , "\", null, null),\n");
this.WriteObjects("                            (item) => item.NotifyPropertyChanged(\"",  otherName , "\", null, null)");
#line 81 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
// TODO: improve this!
    if (rel.NeedsPositionStorage(otherEnd.GetRole()))
    {
        this.WriteObjects(", \"", relEnd.RoleName, "\"");
    }
                            
#line 86 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("",  positionPropertyNameArgument , ");\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return ",  wrapperName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("    \n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\n");
this.WriteObjects("        public EntityCollection<",  referencedImplementation , "> ",  efName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedImplementation , ">(\n");
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
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , ", ",  referencedImplementation , "> ",  wrapperName , ";\n");
this.WriteObjects("\n");
#line 112 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading) { 
#line 113 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        private List<int> ",  name , "Ids;\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\n");
#line 116 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
if (serializationList != null)
        {
            serializationList.Add("Serialization.EagerLoadingSerialization", Zetbox.Generator.Templates.Serialization.SerializerType.Binary, null, null, name, true, false, null);
        }
    }

#line 122 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\n");

        }

    }
}