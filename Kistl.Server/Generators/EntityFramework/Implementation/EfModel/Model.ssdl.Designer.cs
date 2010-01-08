using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
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
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm/ssdl\"\r\n");
this.WriteObjects("        Namespace=\"Model.Store\"\r\n");
this.WriteObjects("        Alias=\"Self\"\r\n");
this.WriteObjects("        Provider=\"System.Data.SqlClient\"\r\n");
this.WriteObjects("        ProviderManifestToken=\"2005\" >\r\n");
this.WriteObjects("  <EntityContainer Name=\"dbo\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all Base Classes -->\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.ClassName))
	{

#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "\" EntityType=\"Model.Store.",  cls.ClassName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if(cls.HasSecurityRules(false))
		{

#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "_Rights\" EntityType=\"Model.Store.",  cls.ClassName , "_Rights\" Table=\"",  Construct.SecurityRulesTableName(cls) , "\"/>\r\n");
#line 36 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}
	}

#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all derived classes and their inheritance AssociationSets -->\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.ClassName))
	{
		var info = new InheritanceStorageAssociationInfo(cls);

#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "\" EntityType=\"Model.Store.",  cls.ClassName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
this.WriteObjects("    <!-- inherits from ",  info.ParentEntitySetName , " -->\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.Store.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  info.ParentEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  info.ChildEntitySetName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if(cls.HasSecurityRules(false))
		{

#line 56 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "_Rights\" EntityType=\"Model.Store.",  cls.ClassName , "_Rights\" Table=\"",  Construct.SecurityRulesTableName(cls) , "\"/>\r\n");
#line 58 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}
	}

#line 61 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-object CollectionEntrys -->\r\n");
#line 64 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in ModelCsdl.GetRelationsWithSeparateStorage(ctx))
	{
		string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
		string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);
		string esName = rel.GetRelationClassName();
		string esTableName = rel.GetRelationTableName();
		

#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <!-- \r\n");
#line 74 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
Implementation.RelationDebugTemplate.Call(Host, ctx, rel);

#line 76 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Table=\"",  esTableName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.Store.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.Store.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 88 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 90 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations which do not need CollectionEntrys -->\r\n");
#line 94 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in ModelCsdl.GetRelationsWithoutSeparateStorage(ctx))
	{
		string assocName = rel.GetAssociationName();
		

#line 99 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 104 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 106 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-value CollectionEntrys -->\r\n");
#line 109 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList)
		.OrderBy(p => p.ObjectClass.ClassName)
		.ThenBy(p => p.PropertyName))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();

#line 117 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Table=\"",  prop.GetCollectionEntryTable() , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.ClassName , "\" EntitySet=\"",  prop.ObjectClass.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 124 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 126 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all classes -->\r\n");
#line 132 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetQuery<ObjectClass>()
		.OrderBy(cls => cls.ClassName))
	{

#line 135 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" ",  (cls.BaseObjectClass == null) ? "StoreGeneratedPattern=\"Identity\" " : String.Empty , "/>\r\n");
#line 142 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(cls);

#line 144 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 146 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if(cls.HasSecurityRules(false))
		{

#line 149 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "_Rights\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" />\r\n");
this.WriteObjects("    <Property Name=\"Identity\" Type=\"int\" Nullable=\"false\" />\r\n");
this.WriteObjects("    <Property Name=\"Right\" Type=\"int\" Nullable=\"false\" />\r\n");
this.WriteObjects("  </EntityType>\r\n");
#line 158 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}
	}

#line 161 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all object-object CollectionEntrys with their associations -->\r\n");
#line 164 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in ModelCsdl.GetRelationsWithSeparateStorage(ctx))
	{
		string ceName = rel.GetRelationClassName();
		string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
		string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

#line 169 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  ceName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
#line 176 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if(rel.A.Type.ImplementsIExportable(ctx) && rel.B.Type.ImplementsIExportable(ctx))
		{

#line 179 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"uniqueidentifier\" Nullable=\"false\" />\r\n");
#line 181 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 183 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 185 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 188 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "",  Kistl.API.Helper.PositionSuffix , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 190 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 192 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 194 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 197 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "",  Kistl.API.Helper.PositionSuffix , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 199 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 201 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- A to CollectionEntry -->\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\" Type=\"Model.Store.",  rel.A.Type.ClassName , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\" Type=\"Model.Store.",  ceName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  rel.A.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"",  fkAName , "\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- B to CollectionEntry -->\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.B) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\" Type=\"Model.Store.",  rel.B.Type.ClassName , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\" Type=\"Model.Store.",  ceName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  rel.B.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"",  fkBName , "\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
#line 232 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 234 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntry (1:1, 1:N) -->\r\n");
#line 238 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var rel in ModelCsdl.GetRelationsWithoutSeparateStorage(ctx))
	{
		RelationEnd principal, dependent;
	
		switch(rel.Storage)
		{
			case StorageType.MergeIntoA:
				principal = rel.B;
				dependent = rel.A;
				break;
			case StorageType.MergeIntoB:
				principal = rel.A;
				dependent = rel.B;
				break;
			default:
				throw new NotImplementedException();
		}

#line 256 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\">\r\n");
this.WriteObjects("    <End Role=\"",  principal.RoleName , "\" Type=\"Model.Store.",  principal.Type.ClassName , "\" Multiplicity=\"",  principal.Multiplicity.ToSsdlMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <!-- the dependent role always has Multiplicity=\"*\" since 1:1 relations cannot be expressed by EF -->\r\n");
this.WriteObjects("    <End Role=\"",  dependent.RoleName , "\" Type=\"Model.Store.",  dependent.Type.ClassName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  principal.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  dependent.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"fk_",  principal.RoleName , "\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
#line 272 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 274 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- derived->base ObjectClass references -->\r\n");
#line 278 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.ClassName))
	{
		TypeMoniker parentType = cls.BaseObjectClass.GetTypeMoniker();
		TypeMoniker childType = cls.GetTypeMoniker();
		
		string parentRoleName = Construct.AssociationParentRoleName(parentType);
		string childRoleName = Construct.AssociationChildRoleName(childType);

#line 286 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("  <Association Name=\"",  Construct.InheritanceAssociationName(parentType, childType) , "\">\r\n");
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
#line 298 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 300 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 304 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList)
		.OrderBy(p => p.ObjectClass.ClassName)
		.ThenBy(p => p.PropertyName))
	{
		string assocName = prop.GetAssociationName();
		
		// the name of the class containing this list
		string containerTypeName = prop.ObjectClass.ClassName;
		// the name of the CollectionEntry class
		string entryTypeName = prop.GetCollectionEntryClassName();
		// the name of the contained type
		string itemTypeName = prop.ToDbType();
		
		string constraint = String.Empty;
		if (prop is StringProperty) {
			var sProp = (StringProperty)prop;
			constraint += String.Format("MaxLength=\"{0}\" ", sProp.GetMaxLength());
		}


#line 325 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"int\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"fk_",  containerTypeName , "\" Type=\"int\" Nullable=\"true\" />\r\n");
this.WriteObjects("      <Property Name=\"",  prop.PropertyName , "\" Type=\"",  itemTypeName , "\" ",  constraint , "/>\r\n");
#line 333 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
if (prop.HasPersistentOrder)
		{

#line 336 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.PropertyName , "Index\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 338 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 340 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("    </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <Association Name=\"",  prop.GetAssociationName() , "\">\r\n");
this.WriteObjects("      <End Role=\"",  containerTypeName , "\" Type=\"Model.Store.",  containerTypeName , "\" Multiplicity=\"0..1\">\r\n");
this.WriteObjects("        <OnDelete Action=\"Cascade\" />\r\n");
this.WriteObjects("      </End>\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" Type=\"Model.Store.",  entryTypeName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("      <ReferentialConstraint>\r\n");
this.WriteObjects("        <Principal Role=\"",  containerTypeName , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Principal>\r\n");
this.WriteObjects("        <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"fk_",  containerTypeName , "\" />\r\n");
this.WriteObjects("        </Dependent>\r\n");
this.WriteObjects("      </ReferentialConstraint>\r\n");
this.WriteObjects("    </Association>\r\n");
this.WriteObjects("\r\n");
#line 358 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
}

#line 360 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>");

        }



    }
}