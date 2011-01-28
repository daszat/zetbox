using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string name;
		protected string wrapperName;
		protected string wrapperClass;
		protected string exposedListType;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected string positionPropertyName;
		protected string otherName;
		protected string referencedInterface;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectListProperty", ctx, serializationList, name, wrapperName, wrapperClass, exposedListType, rel, endRole, positionPropertyName, otherName, referencedInterface);
        }

        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
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
#line 24 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

    // the ef-visible property's name
    string efName = name + ImplementationPropertySuffix;
    // the name of the position property as string argument
    string positionPropertyNameArgument = rel.NeedsPositionStorage(otherEnd.GetRole()) ? String.Format(@", ""{0}""", Construct.ListPositionPropertyName(otherEnd)) : String.Empty;
    
    // the name of the EF association
    string assocName = rel.GetAssociationName() + (relEnd.Multiplicity.UpperBound() > 1 ? "_" + relEnd.GetRole() : String.Empty);
    string targetRoleName = otherEnd.RoleName;

    // which Kistl interface this is    
    string thisInterface = relEnd.Type.GetDataTypeString();
    // the actual implementation class of the list's elements
    string referencedImplementation = referencedInterface + ImplementationSuffix;

    // whether or not the collection will be eagerly loaded
    bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;

    // override and ignore Base's notion of wrapper classes
    wrapperClass = rel.NeedsPositionStorage(otherEnd.GetRole()) ? "EntityListWrapper" : "EntityCollectionWrapper";

#line 47 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("           // ",  this.GetType() , "\r\n");
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
this.WriteObjects("                            this.Context, ",  efName , ",\r\n");
this.WriteObjects("                            () => this.NotifyPropertyChanging(\"",  name , "\", null, null, null),\r\n");
this.WriteObjects("                            () => this.NotifyPropertyChanged(\"",  name , "\", null, null, null),\r\n");
this.WriteObjects("                            (item) => item.NotifyPropertyChanging(\"",  otherName , "\", null, null, null),\r\n");
this.WriteObjects("                            (item) => item.NotifyPropertyChanged(\"",  otherName , "\", null, null, null)");
#line 63 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
// TODO: improve this!
    if (rel.NeedsPositionStorage(otherEnd.GetRole()))
    {
        this.WriteObjects(", \"", relEnd.RoleName, "\"");
    }
                            
#line 68 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
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
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedImplementation , ">(\r\n");
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
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , ", ",  referencedImplementation , "> ",  wrapperName , ";\r\n");
this.WriteObjects("\r\n");
#line 95 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading)
    {

#line 98 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        private List<int> ",  name , "Ids;\r\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 102 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
if (serializationList != null)
        {
            serializationList.Add("Serialization.EagerLoadingSerialization", Kistl.Generator.Templates.Serialization.SerializerType.Binary, null, null, name, true);
        }
    }

#line 108 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\r\n");

        }

    }
}