using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.GeneratorsOld;


namespace Kistl.Server.Generators.EntityFramework.Server.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst")]
    public partial class ModelCsdl : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public ModelCsdl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
#line 20 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
// EntitySets for all Base Classes
	foreach(var name in GetBaseClasses().Select(cls => cls.ClassName))
	{

#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  name , "\" EntityType=\"Model.",  name , "\" />\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}
	
	// AssociationSets for ObjectReferenceProperties
	foreach(var prop in GetObjectReferencePropertiesWithStorage())
	{
		TypeMoniker parentType = Construct.AssociationParentType(prop);
		TypeMoniker childType = Construct.AssociationChildType(prop);
		string name = Construct.AssociationName(parentType, childType, prop.PropertyName);

#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  name , "\" Association=\"Model.",  name , "\">\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationParentRoleName(parentType) , "\" EntitySet=\"",  prop.ReferenceObjectClass.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationChildRoleName(childType) , "\" EntitySet=\"",  GetAssociationChildEntitySetName(prop) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

	// EntitySets for all CollectionEntrys and associated AssociationSets
	foreach(var prop in GetObjectListPropertiesWithStorage())
	{
		string entityName = Construct.PropertyCollectionEntryType(prop).ClassName;

#line 45 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
#line 47 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
		TypeMoniker childType = Construct.PropertyCollectionEntryType(prop);
		string associationName = Construct.AssociationName(parentType, childType, "fk_Parent");

#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  associationName , "\" Association=\"Model.",  associationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationParentRoleName(parentType) , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationChildRoleName(childType) , "\" EntitySet=\"",  GetAssociationChildEntitySetName(prop) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 55 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

#line 57 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityContainer>\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
// EntityTypes for all base classes
	foreach(var cls in GetBaseClasses())
	{

#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 68 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties); 
#line 69 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 70 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

	// EntityTypes for all other classes
	foreach(var cls in GetDerivedClasses())
	{

#line 75 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\" BaseType=\"",  cls.BaseObjectClass.ClassName , "\" >\r\n");
#line 77 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties); 
#line 78 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 79 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

	// EntityTypes for all CollectionEntrys
	foreach(var prop in GetObjectListPropertiesWithStorage())
	{
		TypeMoniker collectionType = Construct.PropertyCollectionEntryType(prop);
		// TypeMoniker parentType;
		// TypeMoniker childType;

#line 87 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  collectionType.ClassName , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 94 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
{
			// Parent
			TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
			TypeMoniker childType = collectionType;

#line 99 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ParentImpl\"\r\n");
this.WriteObjects("                        RelationShip=\"Model.",  Construct.AssociationName(parentType, childType, "fk_Parent") , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  Construct.AssociationChildRoleName(childType) , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  Construct.AssociationParentRoleName(parentType) , "\" />\r\n");
#line 104 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
if (prop.IsIndexed)
			{

#line 107 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ParentIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 109 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}
		}
		
		// Value
		if (prop is ObjectReferenceProperty)
		{
			// ObjectReferenceProperty
			// TODO: IsNullable??
			TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
			TypeMoniker childType = collectionType;

#line 120 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ValueImpl\"\r\n");
this.WriteObjects("                        RelationShip=\"Model.",  Construct.AssociationName(parentType, childType, prop.PropertyName) , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  Construct.AssociationChildRoleName(childType) , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  Construct.AssociationParentRoleName(parentType) , "\" />\r\n");
#line 125 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}
		else if (prop is ValueTypeProperty)
		{
			string maxlength = "";
			if (prop is StringProperty)
				maxlength = String.Format("MaxLength=\"{0}\" ", ((StringProperty)prop).Length.ToString());

#line 132 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value\" Type=\"",  Type.GetType(prop.GetPropertyTypeString()).Name , "\" ",  maxlength , "Nullable=\"",  prop.IsNullable.ToString().ToLowerInvariant() , "\" />\r\n");
#line 134 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

		if (prop.NeedsPositionColumn())
		{

#line 139 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ValueIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 141 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

#line 143 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 144 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

	// ComplexTypes for all structs
	foreach(var cls in ctx.GetQuery<Struct>())
	{

#line 149 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <ComplexType Name=\"",  cls.ClassName , "\" >\r\n");
#line 151 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties); 
#line 152 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 153 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

	// Associations for ObjectReferences
	foreach(var prop in GetObjectReferencePropertiesWithStorage())
	{
		TypeMoniker parentType = Construct.AssociationParentType(prop);
		TypeMoniker childType = Construct.AssociationChildType(prop);

#line 161 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <Association Name=\"",  Construct.AssociationName(parentType, childType, prop.PropertyName) , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationParentRoleName(parentType) , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  parentType.ClassName , "\"\r\n");
this.WriteObjects("           Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationChildRoleName(childType) , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  childType.ClassName , "\"\r\n");
this.WriteObjects("           Multiplicity=\"",  prop.GetRelationType() == RelationType.one_one ? "0..1" : "*" , "\" />\r\n");
this.WriteObjects("    </Association>\r\n");
#line 169 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

	// Associations for ObjectLists
	foreach(var prop in GetObjectListPropertiesWithStorage())
	{
		TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
		TypeMoniker childType = Construct.PropertyCollectionEntryType(prop);

#line 177 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("    <Association Name=\"",  Construct.AssociationName(parentType, childType, "fk_Parent") , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationParentRoleName(parentType) , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  parentType.ClassName , "\"\r\n");
this.WriteObjects("           Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationChildRoleName(childType) , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  childType.ClassName , "\"\r\n");
this.WriteObjects("           Multiplicity=\"*\" />\r\n");
this.WriteObjects("    </Association>\r\n");
#line 185 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
}

#line 187 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.csdl.cst"
this.WriteObjects("</Schema>");

        }



    }
}