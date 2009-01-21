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
this.WriteObjects("  \r\n");
this.WriteObjects("    <!-- EntitySets for all classes -->\r\n");
#line 21 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var name in ctx.GetQuery<ObjectClass>().Select(cls => cls.ClassName))
	{

#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  name , "\" EntityType=\"Model.",  name , "\" />\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 28 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets for all CollectionEntrys -->\r\n");
#line 30 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		string entityName = Construct.PropertyCollectionEntryType(prop).ClassName;

#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 38 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets -->\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyAssociationSets();

#line 44 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("  <!-- EntityTypes for all base classes -->\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 57 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 58 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 61 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 64 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetDerivedClasses())
	{

#line 67 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\" BaseType=\"Model.",  cls.BaseObjectClass.ClassName , "\" >\r\n");
#line 68 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 69 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 70 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 73 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all CollectionEntrys -->\r\n");
#line 76 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		TypeMoniker collectionType = Construct.PropertyCollectionEntryType(prop);
		// TypeMoniker parentType;
		// TypeMoniker childType;

#line 82 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  collectionType.ClassName , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 89 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
{
			// Parent
			TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
			TypeMoniker childType = collectionType;

#line 94 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ParentImpl\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  Construct.AssociationName(parentType, childType, "fk_Parent") , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  Construct.AssociationChildRoleName(childType) , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  Construct.AssociationParentRoleName(parentType) , "\" />\r\n");
#line 99 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if (prop.IsIndexed)
			{

#line 102 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ParentIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 104 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
		}
		
		// Value
		if (prop is ObjectReferenceProperty)
		{
			// ObjectReferenceProperty
			// TODO: IsNullable??
			var info = AssociationInfo.CreateInfo(ctx, prop);

#line 114 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ValueImpl\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  info.AssociationName , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  info.Child.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  info.Parent.RoleName , "\" />\r\n");
#line 119 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
		else if (prop is ValueTypeProperty)
		{
			string maxlength = "";
			if (prop is StringProperty)
				maxlength = String.Format("MaxLength=\"{0}\" ", ((StringProperty)prop).Length.ToString());

#line 126 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value\" Type=\"",  Type.GetType(prop.GetPropertyTypeString()).Name , "\" ",  maxlength , "Nullable=\"",  prop.IsNullable.ToString().ToLowerInvariant() , "\" />\r\n");
#line 128 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

		if (prop.NeedsPositionColumn())
		{

#line 133 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ValueIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 135 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 137 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 138 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 141 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all structs -->\r\n");
#line 144 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetQuery<Struct>())
	{

#line 147 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.ClassName , "\" >\r\n");
#line 148 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 149 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 150 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 153 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations -->\r\n");
#line 156 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetAssociationPropertiesWithStorage())
	{
		var info = AssociationInfo.CreateInfo(ctx, prop);

#line 160 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <Association Name=\"",  info.AssociationName , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  info.Parent.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Parent.Type.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"",  info.Parent.ConceptualMultiplicity , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  info.Child.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Child.Type.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"",  info.Child.ConceptualMultiplicity , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 170 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
// construct reverse mapping
		if (prop is ObjectReferenceProperty && prop.IsList)
		{
			var refClass = ((ObjectReferenceProperty)prop).ReferenceObjectClass;
			var refType = new TypeMoniker(refClass.GetDataTypeString());

#line 176 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- Reverse of ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <Association Name=\"",  Construct.AssociationName(refType, info.CollectionEntry, prop.PropertyName) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  Construct.AssociationParentRoleName(refClass) , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  refType.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"",  info.Child.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Child.Type.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 186 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
	}

#line 189 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("</Schema>\r\n");

        }



    }
}