using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst")]
    public partial class ModelSsdl : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ISchemaProvider schemaProvider;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ISchemaProvider schemaProvider)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelSsdl", ctx, schemaProvider);
        }

        public ModelSsdl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ISchemaProvider schemaProvider)
            : base(_host)
        {
			this.ctx = ctx;
			this.schemaProvider = schemaProvider;

        }

        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm/ssdl\"\r\n");
this.WriteObjects("        Namespace=\"Model.Store\"\r\n");
this.WriteObjects("        Alias=\"Self\"\r\n");
this.WriteObjects("        Provider=\"",  schemaProvider.AdoNetProvider , "\"\r\n");
this.WriteObjects("        ProviderManifestToken=\"",  schemaProvider.ManifestToken , "\" >\r\n");
this.WriteObjects("  <EntityContainer Name=\"dbo\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all Base Classes -->\r\n");
#line 27 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
	{

#line 30 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.Name , "\" EntityType=\"Model.Store.",  cls.Name , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
#line 32 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (cls.NeedsRightsTable())
		{

#line 35 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  Construct.SecurityRulesClassName(cls) , "\" EntityType=\"Model.Store.",  Construct.SecurityRulesClassName(cls) , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  Construct.SecurityRulesTableName(cls) , "\"/>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  Construct.SecurityRulesFKName(cls) , "\" Association=\"Model.Store.",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" EntitySet=\"",  cls.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" EntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 41 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}
	}

#line 44 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all derived classes and their inheritance AssociationSets -->\r\n");
#line 47 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.Name))
	{
		var info = new InheritanceStorageAssociationInfo(cls);

#line 51 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.Name , "\" EntityType=\"Model.Store.",  cls.Name , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
this.WriteObjects("    <!-- inherits from ",  info.ParentEntitySetName , " -->\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.Store.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  info.ParentEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  info.ChildEntitySetName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 58 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 60 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-object CollectionEntrys -->\r\n");
#line 63 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
	{
		string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
		string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);
		string esName = rel.GetRelationClassName();
		string esSchemaName = rel.Module.SchemaName;
		string esTableName = rel.GetRelationTableName();
		

#line 72 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <!-- \r\n");
#line 74 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 76 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  esSchemaName , "\" Table=\"",  esTableName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.Store.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.Store.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.Name , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 88 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 90 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations which do not need CollectionEntrys -->\r\n");
#line 94 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach (var rel in ctx.GetRelationsWithoutSeparateStorage())
	{
		string assocName = rel.GetAssociationName();
		

#line 99 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.Name , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 104 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 106 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-value CollectionEntrys -->\r\n");
#line 109 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();

#line 118 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  prop.Module.SchemaName , "\" Table=\"",  prop.GetCollectionEntryTable() , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.Name , "\" EntitySet=\"",  prop.ObjectClass.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 125 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 127 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-struct CollectionEntrys -->\r\n");
#line 130 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();

#line 139 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  prop.Module.SchemaName , "\" Table=\"",  prop.GetCollectionEntryTable() , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.Name , "\" EntitySet=\"",  prop.ObjectClass.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 146 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 148 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all classes -->\r\n");
#line 154 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetQuery<ObjectClass>()
		.OrderBy(cls => cls.Name))
	{

#line 157 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" ",  (cls.BaseObjectClass == null) ? "StoreGeneratedPattern=\"Identity\" " : String.Empty , "/>\r\n");
#line 164 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(cls);

#line 166 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 168 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (cls.NeedsRightsTable())
		{

#line 171 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        <PropertyRef Name=\"Identity\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" />\r\n");
this.WriteObjects("      <Property Name=\"Identity\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" />\r\n");
this.WriteObjects("      <Property Name=\"Right\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" />\r\n");
this.WriteObjects("    </EntityType>\r\n");
this.WriteObjects("    <Association Name=\"",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" Type=\"Model.Store.",  cls.Name , "\" Multiplicity=\"1\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" Type=\"Model.Store.",  Construct.SecurityRulesClassName(cls) , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("      <ReferentialConstraint>\r\n");
this.WriteObjects("        <Principal Role=\"",  cls.Name , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Principal>\r\n");
this.WriteObjects("        <Dependent Role=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Dependent>\r\n");
this.WriteObjects("      </ReferentialConstraint>\r\n");
this.WriteObjects("    </Association>\r\n");
#line 193 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

	}

#line 197 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all object-object CollectionEntrys with their associations -->\r\n");
#line 200 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
	{
		string ceName = rel.GetRelationClassName();
		string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
		string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

#line 205 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  ceName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
#line 212 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 215 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Guid) , "\" Nullable=\"false\" />\r\n");
#line 217 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 219 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 221 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 224 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "",  Kistl.API.Helper.PositionSuffix , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 226 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 228 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 230 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 233 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "",  Kistl.API.Helper.PositionSuffix , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 235 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 237 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- A to CollectionEntry -->\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\" Type=\"Model.Store.",  rel.A.Type.Name , "\" Multiplicity=\"0..1\" />\r\n");
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
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\" Type=\"Model.Store.",  rel.B.Type.Name , "\" Multiplicity=\"0..1\" />\r\n");
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
#line 268 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 270 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntry (1:1, 1:N) -->\r\n");
#line 274 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var rel in ctx.GetRelationsWithoutSeparateStorage())
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

#line 292 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\">\r\n");
this.WriteObjects("    <End Role=\"",  principal.RoleName , "\" Type=\"Model.Store.",  principal.Type.Name , "\" Multiplicity=\"",  principal.Multiplicity.ToSsdlMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  dependent.RoleName , "\" Type=\"Model.Store.",  dependent.Type.Name , "\" Multiplicity=\"*\" />\r\n");
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
#line 307 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 309 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- derived->base ObjectClass references -->\r\n");
#line 313 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.Name))
	{
		var parentType = cls.BaseObjectClass;
		var childType = cls;
		
		string parentRoleName = Construct.AssociationParentRoleName(parentType);
		string childRoleName = Construct.AssociationChildRoleName(childType);

#line 321 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("  <Association Name=\"",  Construct.InheritanceAssociationName(parentType, childType) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  parentRoleName , "\" Type=\"Model.Store.",  parentType.Name , "\" Multiplicity=\"1\" />\r\n");
this.WriteObjects("    <End Role=\"",  childRoleName , "\" Type=\"Model.Store.",  childType.Name , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  parentRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  childRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 333 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 335 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 339 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		
		// the name of the class containing this list
		string containerTypeName = prop.ObjectClass.Name;
		// the name of the CollectionEntry class
		string entryTypeName = prop.GetCollectionEntryClassName();
		// the name of the contained type
		string itemTypeName = schemaProvider.DbTypeToNative(DbTypeMapper.GetDbTypeForProperty(prop.GetType()));
		
		string constraint = String.Empty;
		if (prop is StringProperty) {
			var sProp = (StringProperty)prop;
			constraint += String.Format("MaxLength=\"{0}\" ", sProp.GetMaxLength());
		}
		if (prop is DecimalProperty) {
			var dProp = (DecimalProperty)prop;
			constraint += String.Format("Precision=\"{0}\" Scale=\"{1}\" ", dProp.Precision, dProp.Scale);
		}


#line 365 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"fk_",  containerTypeName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
this.WriteObjects("      <Property Name=\"",  prop.Name , "\" Type=\"",  itemTypeName , "\" ",  constraint , "/>\r\n");
#line 373 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (prop.HasPersistentOrder)
		{

#line 376 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.Name , "Index\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 378 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 380 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 398 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 400 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-struct CollectionEntrys -->\r\n");
#line 403 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
		.Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		
		// the name of the class containing this list
		string containerTypeName = prop.ObjectClass.Name;
		// the name of the CollectionEntry class
		string entryTypeName = prop.GetCollectionEntryClassName();

#line 416 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"fk_",  containerTypeName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 423 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(prop);

#line 426 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (prop.HasPersistentOrder)
		{

#line 429 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.Name , "Index\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 431 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 433 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 451 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 453 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	<Function Name=\"GetContinuousSequenceNumber\" Aggregate=\"false\" BuiltIn=\"false\" NiladicFunction=\"false\" IsComposable=\"false\" ParameterTypeSemantics=\"AllowImplicitConversion\" Schema=\"dbo\">\r\n");
this.WriteObjects("          <Parameter Name=\"seqNumber\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Guid) , "\" Mode=\"In\" />\r\n");
this.WriteObjects("		  <Parameter Name=\"result\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Mode=\"Out\" />\r\n");
this.WriteObjects("    </Function>\r\n");
this.WriteObjects("    <Function Name=\"GetSequenceNumber\" Aggregate=\"false\" BuiltIn=\"false\" NiladicFunction=\"false\" IsComposable=\"false\" ParameterTypeSemantics=\"AllowImplicitConversion\" Schema=\"dbo\">\r\n");
this.WriteObjects("        <Parameter Name=\"seqNumber\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Guid) , "\" Mode=\"In\" />\r\n");
this.WriteObjects("		<Parameter Name=\"result\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Mode=\"Out\" />\r\n");
this.WriteObjects("    </Function>\r\n");
this.WriteObjects("</Schema>");

        }

    }
}