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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelCsdl", ctx);
        }

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
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
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
foreach(var rel in ctx.GetRelationsWithoutSeparateStorage())
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
#line 123 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {

#line 126 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "\"",  GetAbstractModifier(cls) , ">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 132 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>());
		if(cls.NeedsRightsTable())
		{

#line 136 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"SecurityRightsCollection",  ImplementationPropertySuffix , "\" Relationship=\"Model.",  Construct.SecurityRulesFKName(cls) , "\" FromRole=\"",  cls.Name , "\" ToRole=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
#line 138 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 140 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 142 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if(cls.NeedsRightsTable())
		{

#line 145 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 167 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}
    }

#line 170 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 173 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.Name))
    {

#line 176 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "\" BaseType=\"Model.",  cls.BaseObjectClass.Name , "\"",  GetAbstractModifier(cls) , ">\r\n");
#line 177 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 178 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 180 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 182 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-object CollectionEntrys -->\r\n");
#line 185 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
    {


#line 189 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!--\r\n");
#line 191 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 193 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  rel.GetRelationClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 200 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if(rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
	{

#line 203 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"Guid\" Nullable=\"false\" />\r\n");
#line 205 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 206 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.A.RoleName , "\" />\r\n");
#line 214 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
        {

#line 217 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"A",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 219 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 221 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"B",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.B.RoleName , "\" />\r\n");
#line 228 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
        {

#line 231 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 233 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 235 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 254 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 256 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 260 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {


#line 267 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 282 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 285 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 287 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 289 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.ObjectClass.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 299 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 301 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("<!-- EntityTypes and Associations for all object-CompoundObject CollectionEntrys -->\r\n");
#line 304 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {


#line 311 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 328 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 331 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 333 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 335 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.ObjectClass.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 345 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 347 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntrys -->\r\n");
#line 350 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var rel in ctx.GetRelationsWithoutSeparateStorage())
    {

#line 353 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.A.Type.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.B.Type.Name , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 362 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 364 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all CompoundObjects -->\r\n");
#line 368 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetQuery<CompoundObject>().OrderBy(s => s.Name))
    {

#line 371 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.Name , "\" >\r\n");
this.WriteObjects("    <Property Name=\"CompoundObject_IsNull\" Type=\"Boolean\" Nullable=\"false\" />\r\n");
#line 373 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 374 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 375 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}


#line 378 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>\r\n");

        }

    }
}