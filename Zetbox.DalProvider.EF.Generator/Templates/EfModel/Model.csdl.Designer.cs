using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst")]
    public partial class ModelCsdl : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelCsdl", ctx);
        }

        public ModelCsdl(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 31 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("    <!-- EntitySets for all classes -->\r\n");
#line 37 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {
        var clsName = cls.Name;

#line 41 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  clsName , "EfImpl\" EntityType=\"Model.",  clsName , "EfImpl\" />\r\n");
#line 43 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (cls.NeedsRightsTable())
		{

#line 46 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  Construct.SecurityRulesClassName(cls) , "\" EntityType=\"Model.",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  Construct.SecurityRulesFKName(cls) , "\" Association=\"Model.",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  clsName , "\" EntitySet=\"",  clsName , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" EntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 52 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}
    }

#line 55 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-object CollectionEntrys -->\r\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in ctx.GetRelationsWithSeparateStorage())
    {
        string entityName = rel.GetRelationClassName();
        string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
        string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);
        var a = rel.A;
        var b = rel.B;

#line 65 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "EfImpl\" EntityType=\"Model.",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  a.RoleName , "\" EntitySet=\"",  a.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  b.RoleName , "\" EntitySet=\"",  b.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 75 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 77 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-value CollectionEntrys -->\r\n");
#line 80 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();
        var cls = (ObjectClass)prop.ObjectClass;

#line 90 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "EfImpl\" EntityType=\"Model.",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" EntitySet=\"",  cls.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 96 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 98 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-struct CollectionEntrys -->\r\n");
#line 101 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();
        var cls = (ObjectClass)prop.ObjectClass;

#line 111 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "EfImpl\" EntityType=\"Model.",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" EntitySet=\"",  cls.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 117 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 119 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations without CollectionEntrys -->\r\n");
#line 122 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in ctx.GetRelationsWithoutSeparateStorage())
    {
        string assocName = rel.GetAssociationName();
        var a = rel.A;
        var b = rel.B;

#line 128 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  a.RoleName , "\" EntitySet=\"",  a.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"",  b.RoleName , "\" EntitySet=\"",  b.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 133 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 135 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <FunctionImport Name=\"GetContinuousSequenceNumber\" ReturnType=\"Collection(Int32)\">\r\n");
this.WriteObjects("      <Parameter Name=\"seqNumber\" Mode=\"In\" Type=\"Guid\" />\r\n");
this.WriteObjects("      <Parameter Name=\"result\" Mode=\"Out\" Type=\"Int32\" />\r\n");
this.WriteObjects("    </FunctionImport>\r\n");
this.WriteObjects("    <FunctionImport Name=\"GetSequenceNumber\" ReturnType=\"Collection(Int32)\">\r\n");
this.WriteObjects("      <Parameter Name=\"seqNumber\" Mode=\"In\" Type=\"Guid\" />\r\n");
this.WriteObjects("      <Parameter Name=\"result\" Mode=\"Out\" Type=\"Int32\" />\r\n");
this.WriteObjects("    </FunctionImport>\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("  <!-- EntityTypes for all base classes -->\r\n");
#line 148 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {
        var clsName = cls.Name;

#line 152 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  clsName , "EfImpl\"",  GetAbstractModifier(cls) , ">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 158 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>());
		if (cls.NeedsRightsTable())
		{

#line 162 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"SecurityRightsCollection",  ImplementationPropertySuffix , "\" Relationship=\"Model.",  Construct.SecurityRulesFKName(cls) , "\" FromRole=\"",  clsName , "\" ToRole=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
#line 164 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 166 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 168 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (cls.NeedsRightsTable())
		{

#line 171 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("	  <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("	  <PropertyRef Name=\"Identity\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    <Property Name=\"Identity\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    <Property Name=\"Right\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("    <End Role=\"",  clsName , "\" Type=\"Model.",  clsName , "EfImpl\" Multiplicity=\"1\">\r\n");
this.WriteObjects("      <OnDelete Action=\"Cascade\" />\r\n");
this.WriteObjects("    </End>\r\n");
this.WriteObjects("    <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" Type=\"Model.",  Construct.SecurityRulesClassName(cls) , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("	  <Principal Role=\"",  clsName , "\">\r\n");
this.WriteObjects("	    <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("	  </Principal>\r\n");
this.WriteObjects("	  <Dependent Role=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("	    <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("	  </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 195 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}
    }

#line 198 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 201 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var cls in ctx.GetDerivedClasses().OrderBy(c => c.Name))
    {

#line 204 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "EfImpl\" BaseType=\"Model.",  cls.BaseObjectClass.Name , "EfImpl\"",  GetAbstractModifier(cls) , ">\r\n");
#line 205 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 206 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 208 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 210 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-object CollectionEntrys -->\r\n");
#line 213 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in ctx.GetRelationsWithSeparateStorage())
    {
        var a = rel.A;
        var b = rel.B;

#line 218 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!--\r\n");
#line 220 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 222 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  rel.GetRelationClassName() , "EfImpl\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 229 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (a.Type.ImplementsIExportable() && b.Type.ImplementsIExportable())
	{

#line 232 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"Guid\" Nullable=\"false\" />\r\n");
#line 234 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 235 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  a.RoleName , "\" />\r\n");
#line 243 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
        {

#line 246 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"A",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 248 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 250 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"B",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  b.RoleName , "\" />\r\n");
#line 257 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
        {

#line 260 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 262 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 264 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  a.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  a.Type.Name , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.GetRelationClassName() , "EfImpl\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.B) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  b.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  b.Type.Name , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.GetRelationClassName() , "EfImpl\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("  \r\n");
#line 283 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 285 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 289 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        var propClsName = prop.ObjectClass.Name;

#line 297 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  propClsName , ".",  prop.Name , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"Parent",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  propClsName , "\" />\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    ",  PlainPropertyDefinitionFromValueType(prop, "Value", ImplementationPropertySuffix) , "\r\n");
#line 312 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 315 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 317 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 319 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  propClsName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  propClsName , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 329 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 331 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("<!-- EntityTypes and Associations for all object-CompoundObject CollectionEntrys -->\r\n");
#line 334 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        var propClsName = prop.ObjectClass.Name;


#line 343 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  propClsName , ".",  prop.Name , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"Parent",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  propClsName , "\" />\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <Property Name=\"Value",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("              Type=\"Model.",  prop.CompoundObjectDefinition.Name , "EfImpl\"\r\n");
this.WriteObjects("              Nullable=\"false\" />\r\n");
#line 360 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 363 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 365 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 367 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  propClsName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  propClsName , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 377 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 379 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntrys -->\r\n");
#line 382 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in ctx.GetRelationsWithoutSeparateStorage())
    {
        var a = rel.A;
        var b = rel.B;

#line 387 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  a.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  a.Type.Name , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"",  a.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  b.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  b.Type.Name , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"",  b.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 396 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 398 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all CompoundObjects -->\r\n");
#line 402 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var cls in ctx.GetQuery<CompoundObject>().OrderBy(s => s.Name))
    {

#line 405 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.Name , "EfImpl\" >\r\n");
#line 406 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 407 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 408 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}


#line 411 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>\r\n");

        }

    }
}