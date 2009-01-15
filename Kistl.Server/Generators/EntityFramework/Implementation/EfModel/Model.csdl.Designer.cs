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
this.WriteObjects("    <!-- EntitySets for all Base Classes -->\r\n");
#line 20 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var name in ctx.GetBaseClasses().Select(cls => cls.ClassName))
	{

#line 23 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  name , "\" EntityType=\"Model.",  name , "\" />\r\n");
#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets for all CollectionEntrys and associated AssociationSets -->\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		string entityName = Construct.PropertyCollectionEntryType(prop).ClassName;

#line 33 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
var info = new AssociationInfo(prop);

#line 37 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  Construct.AssociationChildEntitySetName(prop) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 41 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 44 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- AssociationSets for ObjectReferenceProperties -->\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetObjectReferencePropertiesWithStorage())
	{
		TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
		TypeMoniker childType = Construct.AssociationChildType(prop);
		string name = Construct.AssociationName(parentType, childType, prop.PropertyName);

#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  name , "\" Association=\"Model.",  name , "\">\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationParentRoleName(parentType) , "\" EntitySet=\"",  prop.ReferenceObjectClass.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.AssociationChildRoleName(childType) , "\" EntitySet=\"",  Construct.AssociationChildEntitySetName(prop) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 58 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("  <!-- EntityTypes for all base classes -->\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 70 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 71 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 75 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 78 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetDerivedClasses())
	{

#line 81 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\" BaseType=\"Model.",  cls.BaseObjectClass.ClassName , "\" >\r\n");
#line 82 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 83 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 84 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 87 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all CollectionEntrys -->\r\n");
#line 90 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		TypeMoniker collectionType = Construct.PropertyCollectionEntryType(prop);
		// TypeMoniker parentType;
		// TypeMoniker childType;

#line 96 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  collectionType.ClassName , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 103 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
{
			// Parent
			TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
			TypeMoniker childType = collectionType;

#line 108 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ParentImpl\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  Construct.AssociationName(parentType, childType, "fk_Parent") , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  Construct.AssociationChildRoleName(childType) , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  Construct.AssociationParentRoleName(parentType) , "\" />\r\n");
#line 113 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if (prop.IsIndexed)
			{

#line 116 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ParentIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 118 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
		}
		
		// Value
		if (prop is ObjectReferenceProperty)
		{
			// ObjectReferenceProperty
			// TODO: IsNullable??
			var info = new AssociationInfo(prop);

#line 128 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ValueImpl\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  info.AssociationName , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  info.ChildRoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  info.ParentRoleName , "\" />\r\n");
#line 133 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
		else if (prop is ValueTypeProperty)
		{
			string maxlength = "";
			if (prop is StringProperty)
				maxlength = String.Format("MaxLength=\"{0}\" ", ((StringProperty)prop).Length.ToString());

#line 140 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value\" Type=\"",  Type.GetType(prop.GetPropertyTypeString()).Name , "\" ",  maxlength , "Nullable=\"",  prop.IsNullable.ToString().ToLowerInvariant() , "\" />\r\n");
#line 142 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

		if (prop.NeedsPositionColumn())
		{

#line 147 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ValueIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 149 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 151 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 152 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 155 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all structs -->\r\n");
#line 158 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetQuery<Struct>())
	{

#line 161 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.ClassName , "\" >\r\n");
#line 162 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 163 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 164 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 167 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations -->\r\n");
#line 170 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetObjectReferencePropertiesWithStorage().Cast<Property>()
		.Concat(ctx.GetObjectListPropertiesWithStorage().Cast<Property>())
		.Distinct()
		.OrderBy(p => p.ObjectClass.ClassName + p.PropertyName))
	{
		var info = new AssociationInfo(prop);

#line 177 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <Association Name=\"",  info.AssociationName , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  info.ParentRoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Parent.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"",  info.ParentMultiplicity , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  info.ChildRoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Child.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"",  info.ChildMultiplicity , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 187 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
// construct reverse mapping
		if (prop is ObjectReferenceProperty && prop.IsList)
		{
			var refClass = ((ObjectReferenceProperty)prop).ReferenceObjectClass;
			var refType = new TypeMoniker(refClass.GetDataTypeString());

#line 193 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- Reverse of ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <Association Name=\"",  Construct.AssociationName(refType, info.CollectionEntry, prop.PropertyName) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  Construct.AssociationParentRoleName(refClass) , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  refType.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"",  info.ChildRoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Child.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 203 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
	}

#line 206 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("</Schema>\r\n");

        }



    }
}