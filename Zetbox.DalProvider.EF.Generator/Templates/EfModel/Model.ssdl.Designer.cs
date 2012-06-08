using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst")]
    public partial class ModelSsdl : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected ISchemaProvider schemaProvider;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ISchemaProvider schemaProvider)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelSsdl", ctx, schemaProvider);
        }

        public ModelSsdl(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ISchemaProvider schemaProvider)
            : base(_host)
        {
			this.ctx = ctx;
			this.schemaProvider = schemaProvider;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("");
#line 33 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm/ssdl\"\r\n");
this.WriteObjects("        Namespace=\"Model.Store\"\r\n");
this.WriteObjects("        Alias=\"Self\"\r\n");
this.WriteObjects("        Provider=\"",  schemaProvider.AdoNetProvider , "\"\r\n");
this.WriteObjects("        ProviderManifestToken=\"",  schemaProvider.ManifestToken , "\" >\r\n");
this.WriteObjects("  <EntityContainer Name=\"dbo\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all Base Classes -->\r\n");
#line 43 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
	{

#line 46 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.Name , "\" EntityType=\"Model.Store.",  cls.Name , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
#line 48 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (cls.NeedsRightsTable())
		{

#line 51 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  Construct.SecurityRulesClassName(cls) , "\" EntityType=\"Model.Store.",  Construct.SecurityRulesClassName(cls) , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  Construct.SecurityRulesTableName(cls) , "\"/>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  Construct.SecurityRulesFKName(cls) , "\" Association=\"Model.Store.",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" EntitySet=\"",  cls.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" EntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}
	}

#line 60 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all derived classes and their inheritance AssociationSets -->\r\n");
#line 63 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.Name))
	{
		var info = new InheritanceStorageAssociationInfo(cls);

#line 67 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.Name , "\" EntityType=\"Model.Store.",  cls.Name , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
this.WriteObjects("    <!-- inherits from ",  info.ParentEntitySetName , " -->\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.Store.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  info.ParentEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  info.ChildEntitySetName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 74 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 76 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-object CollectionEntrys -->\r\n");
#line 79 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
	{
		string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
		string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);
		string esName = rel.GetRelationClassName();
		string esSchemaName = rel.Module.SchemaName;
		string esTableName = rel.GetRelationTableName();
		

#line 88 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <!-- \r\n");
#line 90 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 92 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 104 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 106 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations which do not need CollectionEntrys -->\r\n");
#line 110 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach (var rel in ctx.GetRelationsWithoutSeparateStorage())
	{
		string assocName = rel.GetAssociationName();
		

#line 115 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.Name , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 120 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 122 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-value CollectionEntrys -->\r\n");
#line 125 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();

#line 134 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  prop.Module.SchemaName , "\" Table=\"",  prop.GetCollectionEntryTable() , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.Name , "\" EntitySet=\"",  prop.ObjectClass.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 141 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 143 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-struct CollectionEntrys -->\r\n");
#line 146 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();

#line 155 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  prop.Module.SchemaName , "\" Table=\"",  prop.GetCollectionEntryTable() , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.Name , "\" EntitySet=\"",  prop.ObjectClass.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 162 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 164 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all classes -->\r\n");
#line 170 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetQuery<ObjectClass>()
		.OrderBy(cls => cls.Name))
	{

#line 173 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" ",  (cls.BaseObjectClass == null) ? "StoreGeneratedPattern=\"Identity\" " : String.Empty , "/>\r\n");
#line 180 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(cls);

#line 182 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 184 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (cls.NeedsRightsTable())
		{

#line 187 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 209 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

	}

#line 213 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all object-object CollectionEntrys with their associations -->\r\n");
#line 216 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
	{
		string ceName = rel.GetRelationClassName();
		string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
		string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

#line 221 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  ceName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
#line 228 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 231 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Guid) , "\" Nullable=\"false\" />\r\n");
#line 233 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 235 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 237 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 240 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 242 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 244 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 246 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 249 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 251 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 253 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 284 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 286 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntry (1:1, 1:N) -->\r\n");
#line 290 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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

#line 308 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 323 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 325 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- derived->base ObjectClass references -->\r\n");
#line 329 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.Name))
	{
		var parentType = cls.BaseObjectClass;
		var childType = cls;
		
		string parentRoleName = Construct.AssociationParentRoleName(parentType);
		string childRoleName = Construct.AssociationChildRoleName(childType);

#line 337 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 349 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 351 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 355 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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


#line 381 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"fk_",  containerTypeName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
this.WriteObjects("      <Property Name=\"",  prop.Name , "\" Type=\"",  itemTypeName , "\" ",  constraint , "/>\r\n");
#line 389 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (prop.HasPersistentOrder)
		{

#line 392 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.Name , "Index\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 394 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 396 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 414 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 416 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-struct CollectionEntrys -->\r\n");
#line 419 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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

#line 432 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"fk_",  containerTypeName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 439 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(prop);

#line 442 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (prop.HasPersistentOrder)
		{

#line 445 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.Name , "Index\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 447 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 449 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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
#line 467 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 469 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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