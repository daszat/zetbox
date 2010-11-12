using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst")]
    public partial class ModelCsdl : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;


        public ModelCsdl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("    <!-- EntitySets for all classes -->\r\n");
#line 21 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {

#line 24 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.Name , "\" EntityType=\"Model.",  cls.Name , "\" />\r\n");
#line 26 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if(cls.NeedsRightsTable())
		{

#line 29 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  Construct.SecurityRulesClassName(cls) , "\" EntityType=\"Model.",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  Construct.SecurityRulesFKName(cls) , "\" Association=\"Model.",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" EntitySet=\"",  cls.Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" EntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 35 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}
    }

#line 38 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-object CollectionEntrys -->\r\n");
#line 40 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {
        string entityName = rel.GetRelationClassName();
        string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
        string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);

#line 46 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.GetRootClass().Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.GetRootClass().Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 56 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 58 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-value CollectionEntrys -->\r\n");
#line 61 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();

#line 69 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.Name , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 75 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 77 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-struct CollectionEntrys -->\r\n");
#line 80 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();

#line 88 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.Name , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 94 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 96 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations without CollectionEntrys -->\r\n");
#line 99 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithoutSeparateStorage(ctx))
    {
        string assocName = rel.GetAssociationName();

#line 103 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.GetRootClass().Name , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.GetRootClass().Name , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 108 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 110 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("  <!-- EntityTypes for all base classes -->\r\n");
#line 115 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {

#line 118 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "\"",  GetAbstractModifier(cls) , ">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 124 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>());
		if(cls.NeedsRightsTable())
		{

#line 128 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"SecurityRightsCollection",  ImplementationPropertySuffix , "\" Relationship=\"Model.",  Construct.SecurityRulesFKName(cls) , "\" FromRole=\"",  cls.Name , "\" ToRole=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
#line 130 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 132 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 134 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if(cls.NeedsRightsTable())
		{

#line 137 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
this.WriteObjects("    <End Role=\"",  cls.Name , "\" Type=\"Model.",  cls.Name , "\" Multiplicity=\"1\" />\r\n");
this.WriteObjects("    <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" Type=\"Model.",  Construct.SecurityRulesClassName(cls) , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("	  <Principal Role=\"",  cls.Name , "\">\r\n");
this.WriteObjects("	    <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("	  </Principal>\r\n");
this.WriteObjects("	  <Dependent Role=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("	    <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("	  </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 159 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}
    }

#line 162 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 165 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.Name))
    {

#line 168 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "\" BaseType=\"Model.",  cls.BaseObjectClass.Name , "\"",  GetAbstractModifier(cls) , ">\r\n");
#line 169 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 170 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 172 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 174 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-object CollectionEntrys -->\r\n");
#line 177 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {


#line 181 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!--\r\n");
#line 183 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 185 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  rel.GetRelationClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 192 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if(rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
	{

#line 195 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"Guid\" Nullable=\"false\" />\r\n");
#line 197 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 198 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.A.RoleName , "\" />\r\n");
#line 206 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
        {

#line 209 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"A",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 211 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 213 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"B",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.B.RoleName , "\" />\r\n");
#line 220 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
        {

#line 223 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 225 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 227 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.A.Type.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.GetRelationClassName() , "\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.B) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.B.Type.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.GetRelationClassName() , "\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("  \r\n");
#line 246 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 248 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 252 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {


#line 259 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.ObjectClass.Name , ".",  prop.Name , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"Parent",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  prop.ObjectClass.Name , "\" />\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    ",  PlainPropertyDefinitionFromValueType(prop, "Value", ImplementationPropertySuffix) , "\r\n");
#line 274 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 277 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 279 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 281 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.ObjectClass.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 291 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 293 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("<!-- EntityTypes and Associations for all object-CompoundObject CollectionEntrys -->\r\n");
#line 296 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {


#line 303 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.ObjectClass.Name , ".",  prop.Name , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"Parent",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  prop.ObjectClass.Name , "\" />\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <Property Name=\"Value",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("              Type=\"Model.",  prop.CompoundObjectDefinition.Name , "\"\r\n");
this.WriteObjects("              Nullable=\"false\" />\r\n");
#line 320 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 323 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 325 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 327 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.ObjectClass.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 337 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 339 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntrys -->\r\n");
#line 342 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithoutSeparateStorage(ctx))
    {

#line 345 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.A.Type.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.B.Type.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 354 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 356 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all CompoundObjects -->\r\n");
#line 360 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetQuery<CompoundObject>().OrderBy(s => s.Name))
    {

#line 363 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.Name , "\" >\r\n");
this.WriteObjects("    <Property Name=\"CompoundObject_IsNull\" Type=\"Boolean\" Nullable=\"false\" />\r\n");
#line 365 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 366 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 367 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}


#line 370 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>\r\n");

        }



    }
}