using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
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
#line 34 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm/ssdl\"\r\n");
this.WriteObjects("        Namespace=\"Model.Store\"\r\n");
this.WriteObjects("        Alias=\"Self\"\r\n");
this.WriteObjects("        Provider=\"",  schemaProvider.AdoNetProvider , "\"\r\n");
this.WriteObjects("        ProviderManifestToken=\"",  schemaProvider.ManifestToken , "\" >\r\n");
this.WriteObjects("  <EntityContainer Name=\"dbo\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all Base Classes -->\r\n");
#line 44 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
	{
        var clsName = cls.Name;

#line 48 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  clsName , "\" EntityType=\"Model.Store.",  clsName , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
#line 50 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (cls.NeedsRightsTable())
		{

#line 53 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  Construct.SecurityRulesClassName(cls) , "\" EntityType=\"Model.Store.",  Construct.SecurityRulesClassName(cls) , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  Construct.SecurityRulesTableName(cls) , "\"/>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  Construct.SecurityRulesFKName(cls) , "\" Association=\"Model.Store.",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  clsName , "\" EntitySet=\"",  clsName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" EntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 59 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}
	}

#line 62 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets for all derived classes and their inheritance AssociationSets -->\r\n");
#line 65 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedTPTClasses().OrderBy(c => c.Name))
	{
		var info = new InheritanceStorageAssociationInfo(cls);
        var clsName = cls.Name;

#line 70 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  clsName , "\" EntityType=\"Model.Store.",  clsName , "\" Schema=\"",  cls.Module.SchemaName , "\" Table=\"",  cls.TableName , "\"/>\r\n");
this.WriteObjects("    <!-- inherits from ",  info.ParentEntitySetName , " -->\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.Store.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ParentRoleName , "\" EntitySet=\"",  info.ParentEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.ChildRoleName , "\" EntitySet=\"",  info.ChildEntitySetName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 77 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 79 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-object CollectionEntrys -->\r\n");
#line 82 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
	{
		string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
		string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);
		string esName = rel.GetRelationClassName();
		string esSchemaName = rel.Module.SchemaName;
		string esTableName = rel.GetRelationTableName();
        var a = rel.A;
        var b = rel.B;
		

#line 93 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <!-- \r\n");
#line 95 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 97 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  esSchemaName , "\" Table=\"",  esTableName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.Store.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  a.RoleName , "\" EntitySet=\"",  a.Type.GetEntitySetName() , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.Store.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  b.RoleName , "\" EntitySet=\"",  b.Type.GetEntitySetName() , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 109 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 111 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations which do not need CollectionEntrys -->\r\n");
#line 115 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach (var rel in ctx.GetRelationsWithoutSeparateStorage())
	{
		string assocName = rel.GetAssociationName();
		var a = rel.A;
        var b = rel.B;

#line 121 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  a.RoleName , "\" EntitySet=\"",  a.Type.GetEntitySetName() , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  b.RoleName , "\" EntitySet=\"",  b.Type.GetEntitySetName() , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 126 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 128 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-value CollectionEntrys -->\r\n");
#line 131 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();
        string propClsName = prop.ObjectClass.Name;
        string propEntitySetName = prop.ObjectClass.GetEntitySetName();

#line 142 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  prop.Module.SchemaName , "\" Table=\"",  prop.GetCollectionEntryTable() , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  propClsName , "\" EntitySet=\"",  propEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 149 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 151 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSet for all object-struct CollectionEntrys -->\r\n");
#line 154 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		string esName = prop.GetCollectionEntryClassName();
        string propClsName = prop.ObjectClass.Name;
        string propEntitySetName = prop.ObjectClass.GetEntitySetName();

#line 165 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  esName , "\" EntityType=\"Model.Store.",  esName , "\" Schema=\"",  prop.Module.SchemaName , "\" Table=\"",  prop.GetCollectionEntryTable() , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.Store.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  propClsName , "\" EntitySet=\"",  propEntitySetName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  esName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    \r\n");
#line 172 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 174 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all classes -->\r\n");
#line 180 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in GetEntityTypes())
	{
        var clsName = cls.Name;

#line 183 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  clsName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"",  int32DbType , "\" Nullable=\"false\" ",  (cls.BaseObjectClass == null) ? "StoreGeneratedPattern=\"Identity\" " : String.Empty , "/>\r\n");
#line 189 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if(cls.GetTableMapping() == TableMapping.TPH) { 
#line 190 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  Zetbox.API.Server.TableMapper.DiscriminatorColumnName , "\" Type=\"",  nvarcharDbType , "\" Nullable=\"false\" />\r\n");
#line 191 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
} 
#line 193 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(cls);

#line 195 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 197 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (cls.NeedsRightsTable())
		{

#line 200 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        <PropertyRef Name=\"Identity\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  int32DbType , "\" Nullable=\"false\" />\r\n");
this.WriteObjects("      <Property Name=\"Identity\" Type=\"",  int32DbType , "\" Nullable=\"false\" />\r\n");
this.WriteObjects("      <Property Name=\"Right\" Type=\"",  int32DbType , "\" Nullable=\"false\" />\r\n");
this.WriteObjects("    </EntityType>\r\n");
this.WriteObjects("    <Association Name=\"",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  clsName , "\" Type=\"Model.Store.",  cls.GetEntitySetName() , "\" Multiplicity=\"1\">\r\n");
this.WriteObjects("        <OnDelete Action=\"Cascade\" />\r\n");
this.WriteObjects("      </End>\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" Type=\"Model.Store.",  Construct.SecurityRulesClassName(cls) , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("      <ReferentialConstraint>\r\n");
this.WriteObjects("        <Principal Role=\"",  clsName , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Principal>\r\n");
this.WriteObjects("        <Dependent Role=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Dependent>\r\n");
this.WriteObjects("      </ReferentialConstraint>\r\n");
this.WriteObjects("    </Association>\r\n");
#line 224 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

	}

#line 228 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all object-object CollectionEntrys with their associations -->\r\n");
#line 231 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
	{
		string ceName = rel.GetRelationClassName();
		string fkAName = Construct.ForeignKeyColumnName(rel.A);
		string fkBName = Construct.ForeignKeyColumnName(rel.B);
        var a = rel.A;
        var b = rel.B;

#line 238 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("  <EntityType Name=\"",  ceName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"",  int32DbType , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
#line 245 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (a.Type.ImplementsIExportable() && b.Type.ImplementsIExportable())
		{

#line 248 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"",  guidDbType , "\" Nullable=\"false\" />\r\n");
#line 250 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 252 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
#line 254 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 257 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkAName , "",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
#line 259 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 261 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
#line 263 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 266 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  fkBName , "",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
#line 268 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 270 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- A to CollectionEntry -->\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  a.RoleName , "\" Type=\"Model.Store.",  a.Type.GetEntitySetName() , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\" Type=\"Model.Store.",  ceName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  a.RoleName , "\">\r\n");
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
this.WriteObjects("    <End Role=\"",  b.RoleName , "\" Type=\"Model.Store.",  b.Type.GetEntitySetName() , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\" Type=\"Model.Store.",  ceName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  b.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"",  fkBName , "\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
#line 301 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 303 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntry (1:1, 1:N) -->\r\n");
#line 307 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
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

#line 325 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\">\r\n");
this.WriteObjects("    <End Role=\"",  principal.RoleName , "\" Type=\"Model.Store.",  principal.Type.GetEntitySetName() , "\" Multiplicity=\"",  principal.Multiplicity.ToSsdlMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  dependent.RoleName , "\" Type=\"Model.Store.",  dependent.Type.GetEntitySetName() , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  principal.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  dependent.RoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"",  Construct.ForeignKeyColumnName(principal) , "\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("\r\n");
#line 340 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 342 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- derived->base ObjectClass references -->\r\n");
#line 346 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var cls in ctx.GetDerivedTPTClasses().OrderBy(c => c.Name))
	{
		var parentType = cls.BaseObjectClass;
		var childType = cls;
		
		string parentRoleName = Construct.AssociationParentRoleName(parentType);
		string childRoleName = Construct.AssociationChildRoleName(childType);

#line 354 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("  <Association Name=\"",  Construct.InheritanceAssociationName(parentType, childType) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  parentRoleName , "\" Type=\"Model.Store.",  parentType.GetEntitySetName() , "\" Multiplicity=\"1\" />\r\n");
this.WriteObjects("    <End Role=\"",  childRoleName , "\" Type=\"Model.Store.",  childType.GetEntitySetName() , "\" Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("      <Principal Role=\"",  parentRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Principal>\r\n");
this.WriteObjects("      <Dependent Role=\"",  childRoleName , "\">\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 366 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 368 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 372 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		
		// the name of the class containing this list
		string containerTypeName = prop.ObjectClass.Name;
		string containerEntitySetName = prop.ObjectClass.GetEntitySetName();
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


#line 399 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  int32DbType , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"",  Construct.ForeignKeyColumnName(prop) , "\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
this.WriteObjects("      <Property Name=\"",  prop.Name , "\" Type=\"",  itemTypeName , "\" ",  constraint , "/>\r\n");
#line 407 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (prop.HasPersistentOrder)
		{

#line 410 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.Name , "Index\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
#line 412 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 414 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <Association Name=\"",  prop.GetAssociationName() , "\">\r\n");
this.WriteObjects("      <End Role=\"",  containerTypeName , "\" Type=\"Model.Store.",  containerEntitySetName , "\" Multiplicity=\"0..1\">\r\n");
this.WriteObjects("        <OnDelete Action=\"Cascade\" />\r\n");
this.WriteObjects("      </End>\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" Type=\"Model.Store.",  entryTypeName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("      <ReferentialConstraint>\r\n");
this.WriteObjects("        <Principal Role=\"",  containerTypeName , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Principal>\r\n");
this.WriteObjects("        <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"",  Construct.ForeignKeyColumnName(prop) , "\" />\r\n");
this.WriteObjects("        </Dependent>\r\n");
this.WriteObjects("      </ReferentialConstraint>\r\n");
this.WriteObjects("    </Association>\r\n");
this.WriteObjects("\r\n");
#line 432 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 434 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntityTypes and Associations for all object-struct CollectionEntrys -->\r\n");
#line 437 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
		.Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{
		string assocName = prop.GetAssociationName();
		
		// the name of the class containing this list
		string containerTypeName = prop.ObjectClass.Name;
		string containerEntitySetName = prop.ObjectClass.GetEntitySetName();
		// the name of the CollectionEntry class
		string entryTypeName = prop.GetCollectionEntryClassName();

#line 451 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <EntityType Name=\"",  entryTypeName , "\" >\r\n");
this.WriteObjects("      <Key>\r\n");
this.WriteObjects("        <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("      </Key>\r\n");
this.WriteObjects("      <Property Name=\"ID\" Type=\"",  int32DbType , "\" Nullable=\"false\" StoreGeneratedPattern=\"Identity\" />\r\n");
this.WriteObjects("      <Property Name=\"",  Construct.ForeignKeyColumnName(prop) , "\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
#line 458 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
ApplyEntityTypeColumnDefs(prop);

#line 461 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
if (prop.HasPersistentOrder)
		{

#line 464 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    <Property Name=\"",  prop.Name , "Index\" Type=\"",  int32DbType , "\" Nullable=\"true\" />\r\n");
#line 466 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 468 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("    </EntityType>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <Association Name=\"",  prop.GetAssociationName() , "\">\r\n");
this.WriteObjects("      <End Role=\"",  containerTypeName , "\" Type=\"Model.Store.",  containerEntitySetName , "\" Multiplicity=\"0..1\">\r\n");
this.WriteObjects("        <OnDelete Action=\"Cascade\" />\r\n");
this.WriteObjects("      </End>\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" Type=\"Model.Store.",  entryTypeName , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("      <ReferentialConstraint>\r\n");
this.WriteObjects("        <Principal Role=\"",  containerTypeName , "\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("        </Principal>\r\n");
this.WriteObjects("        <Dependent Role=\"CollectionEntry\">\r\n");
this.WriteObjects("          <PropertyRef Name=\"",  Construct.ForeignKeyColumnName(prop) , "\" />\r\n");
this.WriteObjects("        </Dependent>\r\n");
this.WriteObjects("      </ReferentialConstraint>\r\n");
this.WriteObjects("    </Association>\r\n");
this.WriteObjects("\r\n");
#line 486 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
}

#line 488 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.cst"
this.WriteObjects("	<Function Name=\"GetContinuousSequenceNumber\" Aggregate=\"false\" BuiltIn=\"false\" NiladicFunction=\"false\" IsComposable=\"false\" ParameterTypeSemantics=\"AllowImplicitConversion\" Schema=\"dbo\">\r\n");
this.WriteObjects("          <Parameter Name=\"seqNumber\" Type=\"",  guidDbType , "\" Mode=\"In\" />\r\n");
this.WriteObjects("		  <Parameter Name=\"result\" Type=\"",  int32DbType , "\" Mode=\"Out\" />\r\n");
this.WriteObjects("    </Function>\r\n");
this.WriteObjects("    <Function Name=\"GetSequenceNumber\" Aggregate=\"false\" BuiltIn=\"false\" NiladicFunction=\"false\" IsComposable=\"false\" ParameterTypeSemantics=\"AllowImplicitConversion\" Schema=\"dbo\">\r\n");
this.WriteObjects("        <Parameter Name=\"seqNumber\" Type=\"",  guidDbType , "\" Mode=\"In\" />\r\n");
this.WriteObjects("		<Parameter Name=\"result\" Type=\"",  int32DbType , "\" Mode=\"Out\" />\r\n");
this.WriteObjects("    </Function>\r\n");
this.WriteObjects("</Schema>");

        }

    }
}