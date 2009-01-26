using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


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
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("    <!-- EntitySets for all classes -->\r\n");
#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var name in ctx.GetQuery<ObjectClass>().Select(cls => cls.ClassName))
	{

#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  name , "\" EntityType=\"Model.",  name , "\" />\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets for all CollectionEntrys -->\r\n");
#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in NewRelation.GetAll(ctx).OrderBy(r => r.GetAssociationName()))
	{
		string entityName = rel.GetCollectionEntryClassName();

#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
#line 37 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets -->\r\n");
#line 43 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyAssociationSets();

#line 45 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("  <!-- EntityTypes for all base classes -->\r\n");
#line 49 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 57 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 58 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 60 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetDerivedClasses())
	{

#line 68 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\" BaseType=\"Model.",  cls.BaseObjectClass.ClassName , "\" >\r\n");
#line 69 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 70 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 74 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all CollectionEntrys -->\r\n");
#line 77 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
#if false
	foreach(var rel in NewRelation.GetAll(ctx).Where(r => r.GetPreferredStorage() == StorageHint.Separate).OrderBy(r => r.GetAssociationName()))
	{


#line 82 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!--\r\n");
this.WriteObjects("    NewRelation: ",  rel.GetAssociationName() , " \r\n");
this.WriteObjects("    Right: ",  rel.Right.Referenced.ClassName , ".",  rel.Right.Navigator != null ? rel.Right.Navigator.PropertyName : "" , "  (",  rel.Right.Multiplicity , ") (site: ",  rel.Right.DebugCreationSite , ")\r\n");
this.WriteObjects("    Left: ",  rel.Left.Referenced.ClassName , ".",  rel.Left.Navigator != null ? rel.Left.Navigator.PropertyName : "" , " (",  rel.Left.Multiplicity , ") (site: ",  rel.Left.DebugCreationSite , ")\r\n");
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  rel.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 93 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
{
			// Right

#line 96 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"RightImpl\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRightToCollectionEntryAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"Right\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.Right.RoleName , "\" />\r\n");
#line 101 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if (prop.IsIndexed)
			{

#line 104 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"ParentIndex\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 106 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
		}
		
		// Left
		if (prop is ObjectReferenceProperty)
		{
			// ObjectReferenceProperty
			// TODO: IsNullable??
			var info = AssociationInfo.CreateInfo(ctx, prop);

#line 116 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"ValueImpl\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  info.AssociationName , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  info.Child.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  info.Parent.RoleName , "\" />\r\n");
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
#endif

#line 143 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all structs -->\r\n");
#line 146 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetQuery<Struct>())
	{

#line 149 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.ClassName , "\" >\r\n");
#line 150 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 151 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 152 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 155 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations -->\r\n");
#line 158 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetAssociationPropertiesWithStorage())
	{
		var info = AssociationInfo.CreateInfo(ctx, prop);

#line 162 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <Association Name=\"",  info.AssociationName , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  info.Parent.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Parent.Type.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"",  info.Parent.ConceptualMultiplicity , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  info.Child.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Child.Type.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"",  info.Child.ConceptualMultiplicity , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 172 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
// construct reverse mapping
		if (prop is ObjectReferenceProperty && prop.IsList)
		{
			var refClass = ((ObjectReferenceProperty)prop).ReferenceObjectClass;
			var refType = new TypeMoniker(refClass.GetDataTypeString());

#line 178 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- Reverse of ",  prop.GetType().Name , ": ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <Association Name=\"",  Construct.AssociationName(refType, info.CollectionEntry, prop.PropertyName) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  Construct.AssociationParentRoleName(refClass) , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  refType.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"",  info.Child.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  info.Child.Type.ClassName , "\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 188 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}
	}

#line 191 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("</Schema>\r\n");

        }



    }
}