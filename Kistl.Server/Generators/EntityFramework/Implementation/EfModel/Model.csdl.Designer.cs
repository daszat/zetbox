using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst")]
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
#line 15 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
// EntitySets for all Base Classes
	foreach(var name in ctx.GetBaseClasses().Select(cls => cls.ClassName))
	{

#line 23 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  name , "\" EntityType=\"Model.",  name , "\" />\r\n");
#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
	
	// EntitySets for all CollectionEntrys and associated AssociationSets
	foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		string entityName = Construct.PropertyCollectionEntryType(prop).ClassName;

#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
var info = new AssociationInfo(prop);

#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  Construct.AssociationChildEntitySetName(prop) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

	// AssociationSets for ObjectReferenceProperties
	foreach(var prop in ctx.GetObjectReferencePropertiesWithStorage())
	{
		TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
		TypeMoniker childType = Construct.AssociationChildType(prop);
		string name = Construct.AssociationName(parentType, childType, prop.PropertyName);

#line 48 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  name , "\" Association=\"Model.",  name , "\">\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationParentRoleName(parentType) , "\" EntitySet=\"",  prop.ReferenceObjectClass.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationChildRoleName(childType) , "\" EntitySet=\"",  Construct.AssociationChildEntitySetName(prop) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 54 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityContainer>\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
// EntityTypes for all base classes
	foreach(var cls in ctx.GetBaseClasses())
	{

#line 59 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 66 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

	// EntityTypes for all other classes
	foreach(var cls in ctx.GetDerivedClasses())
	{

#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\" BaseType=\"",  cls.BaseObjectClass.ClassName , "\" >\r\n");
#line 74 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 75 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 76 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

	// EntityTypes for all CollectionEntrys
	foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		TypeMoniker collectionType = Construct.PropertyCollectionEntryType(prop);
		// TypeMoniker parentType;
		// TypeMoniker childType;

#line 84 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  collectionType.ClassName , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 91 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
{
			// Parent
			TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
			TypeMoniker childType = collectionType;

#line 96 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ParentImpl\"\r\n");
this.WriteObjects("                        RelationShip=\"Model.",  Construct.AssociationName(parentType, childType, "fk_Parent") , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  Construct.AssociationChildRoleName(childType) , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  Construct.AssociationParentRoleName(parentType) , "\" />\r\n");
#line 101 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if (prop.IsIndexed)
			{

#line 104 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ParentIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 106 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
		}
		
		// Value
		if (prop is ObjectReferenceProperty)
		{
			// ObjectReferenceProperty
			// TODO: IsNullable??
			var info = new AssociationInfo(prop);

#line 116 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ValueImpl\"\r\n");
this.WriteObjects("                        RelationShip=\"Model.",  info.AssociationName , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  info.ChildRoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  info.ParentRoleName , "\" />\r\n");
#line 121 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
		else if (prop is ValueTypeProperty)
		{
			string maxlength = "";
			if (prop is StringProperty)
				maxlength = String.Format("MaxLength=\"{0}\" ", ((StringProperty)prop).Length.ToString());

#line 128 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value\" Type=\"",  Type.GetType(prop.GetPropertyTypeString()).Name , "\" ",  maxlength , "Nullable=\"",  prop.IsNullable.ToString().ToLowerInvariant() , "\" />\r\n");
#line 130 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

		if (prop.NeedsPositionColumn())
		{

#line 135 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ValueIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 137 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 139 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 140 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

	// ComplexTypes for all structs
	foreach(var cls in ctx.GetQuery<Struct>())
	{

#line 145 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <ComplexType Name=\"",  cls.ClassName , "\" >\r\n");
#line 147 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 148 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 149 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

	// Associations for ObjectReferences
	foreach(var prop in ctx.GetObjectReferencePropertiesWithStorage().Cast<Property>().Concat(ctx.GetObjectListPropertiesWithStorage().Cast<Property>()))
	{
		var info = new AssociationInfo(prop);

#line 156 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Association Name=\"",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  info.ParentClassName , "\"\r\n");
this.WriteObjects("           Multiplicity=\"",  info.ParentMultiplicity , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  info.ChildClassName , "\"\r\n");
this.WriteObjects("           Multiplicity=\"",  info.ChildMultiplicity , "\" />\r\n");
this.WriteObjects("    </Association>\r\n");
#line 164 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 166 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("</Schema>");

        }



    }
}