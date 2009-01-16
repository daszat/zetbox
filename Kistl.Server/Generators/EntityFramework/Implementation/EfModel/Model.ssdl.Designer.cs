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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst")]
    public partial class ModelSsdl : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public ModelSsdl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm/ssdl\"\r\n");
this.WriteObjects("        Namespace=\"Model.Store\"\r\n");
this.WriteObjects("        Alias=\"Self\"\r\n");
this.WriteObjects("        Provider=\"System.Data.SqlClient\"\r\n");
this.WriteObjects("        ProviderManifestToken=\"2005\" >\r\n");
this.WriteObjects("  <EntityContainer Name=\"dbo\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all Base Classes -->\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 28 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "\" EntityType=\"Model.Store.",  cls.ClassName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all derived classes and their inheritance AssociationSets -->\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses())
	{
		var info = new InheritanceStorageAssociationInfo(cls);

#line 38 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "\" EntityType=\"Model.Store.",  cls.ClassName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.Store.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  info.ParentEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  info.ChildEntitySetName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all CollectionEntrys -->\r\n");
#line 49 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		string entityName = Construct.PropertyCollectionEntryType(prop).ClassName;

#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.Store.",  entityName , "\" />\r\n");
#line 54 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 56 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets -->\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetAssociationPropertiesWithStorage())
	{
		var info = AssociationInfo.CreateInfo(ctx, prop);

#line 63 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <!-- ",  info.GetType().Name , " -->\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.Store.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ASide.RoleName , "\" EntitySet=\"",  info.ASide.StorageEntitySet , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.BSide.RoleName , "\" EntitySet=\"",  info.BSide.StorageEntitySet , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 68 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 70 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all classes -->\r\n");
#line 74 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetQuery<ObjectClass>())
	{

#line 76 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" ",  (cls.BaseObjectClass == null) ? "StoreGeneratedPattern=\"Identity\" " : "" , "/>\r\n");
#line 83 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(cls.Properties.OfType<Property>().ToList().Where(p => p.IsList == false && p.HasStorage()));

#line 85 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 86 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 88 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all CollectionEntrys -->\r\n");
#line 91 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var p in ctx.GetObjectListPropertiesWithStorage())
	{
		TypeMoniker otherType = Construct.PropertyCollectionEntryType(p);

#line 94 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  otherType.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("    <Property Name=\"",  Construct.ForeignKeyColumnNameReferencing(p.ObjectClass) , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 102 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if (p.IsIndexed)
		{

#line 105 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  Construct.ForeignListPositionColumnName(p.ObjectClass) , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 107 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}
	
		ApplyEntityTypeColumnDefs(new List<Property>() { p } );

#line 111 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 112 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}


	// define associations between entities
	

#line 118 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- First, all derived->base ObjectClass references -->\r\n");
#line 122 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses())
	{
		TypeMoniker parentType = cls.BaseObjectClass.GetTypeMoniker();
		TypeMoniker childType = cls.GetTypeMoniker();
		
		string parentRoleName = Construct.AssociationParentRoleName(parentType);
		string childRoleName = Construct.AssociationChildRoleName(childType);

#line 130 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  <Association Name=\"",  Construct.AssociationName(parentType, childType, "ID") , "\">\r\n");
this.WriteObjects("    <End Role=\"",  parentRoleName , "\" Type=\"Model.Store.",  parentType.ClassName , "\" Multiplicity=\"1\" />\r\n");
this.WriteObjects("    <End Role=\"",  childRoleName , "\" Type=\"Model.Store.",  childType.ClassName , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  parentRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  childRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 142 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 144 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- then all ObjectReferences -->\r\n");
#line 148 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var p in ctx.GetAssociationPropertiesWithStorage()) 
	{
		var info = AssociationInfo.CreateInfo(ctx, p);

#line 152 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  <!-- ",  info.GetType().Name , " -->\r\n");
this.WriteObjects("  <Association Name=\"",  info.AssociationName , "\">\r\n");
this.WriteObjects("    <End Role=\"",  info.ASide.RoleName , "\" Type=\"Model.Store.",  info.ASide.Type.ClassName , "\" Multiplicity=\"",  info.ASide.StorageMultiplicity , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  info.BSide.RoleName , "\" Type=\"Model.Store.",  info.BSide.Type.ClassName , "\" Multiplicity=\"",  info.BSide.StorageMultiplicity , "\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  info.ASide.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  info.BSide.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"",  info.ForeignKeyColumnName , "\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 165 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
} 
#line 166 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("</Schema>");

        }



    }
}