using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.DalProvider.EF.Generator.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst")]
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
#line 16 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("    <!-- EntitySets for all classes -->\r\n");
#line 22 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.ClassName))
    {

#line 25 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  cls.ClassName , "\" EntityType=\"Model.",  cls.ClassName , "\" />\r\n");
#line 27 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
if(cls.HasSecurityRules(false))
		{

#line 30 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  Construct.SecurityRulesClassName(cls) , "\" EntityType=\"Model.",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  Construct.SecurityRulesFKName(cls) , "\" Association=\"Model.",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  cls.ClassName , "\" EntitySet=\"",  cls.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" EntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 36 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}
    }

#line 39 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-object CollectionEntrys -->\r\n");
#line 41 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {
        string entityName = rel.GetRelationClassName();
        string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
        string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);

#line 47 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 57 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 59 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-value CollectionEntrys -->\r\n");
#line 62 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();

#line 70 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.ClassName , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 76 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 78 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-struct CollectionEntrys -->\r\n");
#line 81 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<StructProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();

#line 89 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.ClassName , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 95 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 97 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations without CollectionEntrys -->\r\n");
#line 100 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithoutSeparateStorage(ctx))
    {
        string assocName = rel.GetAssociationName();

#line 104 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 109 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 111 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("  <!-- EntityTypes for all base classes -->\r\n");
#line 116 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.ClassName))
    {

#line 119 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 125 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>());
		if(cls.HasSecurityRules(false))
		{

#line 129 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"SecurityRightsCollection",  Kistl.API.Helper.ImplementationSuffix, "\" Relationship=\"Model.",  Construct.SecurityRulesFKName(cls) , "\" FromRole=\"",  cls.ClassName , "\" ToRole=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
#line 131 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 133 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 135 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
if(cls.HasSecurityRules(false))
		{

#line 138 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
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
this.WriteObjects("    <End Role=\"",  cls.ClassName , "\" Type=\"Model.",  cls.ClassName , "\" Multiplicity=\"1\" />\r\n");
this.WriteObjects("    <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" Type=\"Model.",  Construct.SecurityRulesClassName(cls) , "\" Multiplicity=\"*\" />\r\n");
this.WriteObjects("    <ReferentialConstraint>\r\n");
this.WriteObjects("	  <Principal Role=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("	    <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("	  </Principal>\r\n");
this.WriteObjects("	  <Dependent Role=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("	    <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("	  </Dependent>\r\n");
this.WriteObjects("    </ReferentialConstraint>\r\n");
this.WriteObjects("  </Association>\r\n");
#line 160 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}
    }

#line 163 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 166 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.ClassName))
    {

#line 169 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\" BaseType=\"Model.",  cls.BaseObjectClass.ClassName , "\" >\r\n");
#line 170 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 171 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 173 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 175 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-object CollectionEntrys -->\r\n");
#line 178 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {


#line 182 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!--\r\n");
#line 184 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
Implementation.RelationDebugTemplate.Call(Host, ctx, rel);

#line 186 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  rel.GetRelationClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 193 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
if(rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
	{

#line 196 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"Guid\" Nullable=\"false\" />\r\n");
#line 198 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 199 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.A.RoleName , "\" />\r\n");
#line 207 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
        {

#line 210 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"A",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 212 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 214 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"B",  Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.B.RoleName , "\" />\r\n");
#line 221 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
        {

#line 224 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 226 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 228 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.A.Type.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.GetRelationClassName() , "\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("  <Association Name=\"",  rel.GetRelationAssociationName(RelationEndRole.B) , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.B.Type.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.GetRelationClassName() , "\"\r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
this.WriteObjects("  \r\n");
#line 247 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 249 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 253 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    {


#line 260 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  prop.ObjectClass.ClassName , "\" />\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    ",  PlainPropertyDefinitionFromValueType(prop, "B") , "\r\n");
#line 275 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 278 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 280 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 282 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  prop.ObjectClass.ClassName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.ObjectClass.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 292 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 294 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("<!-- EntityTypes and Associations for all object-struct CollectionEntrys -->\r\n");
#line 297 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<StructProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    {


#line 304 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!-- ",  prop.ObjectClass.ClassName , ".",  prop.PropertyName , " -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  prop.ObjectClass.ClassName , "\" />\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("              Type=\"Model.",  prop.StructDefinition.ClassName , "\"\r\n");
this.WriteObjects("              Nullable=\"false\" />\r\n");
#line 321 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 324 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 326 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 328 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  prop.ObjectClass.ClassName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.ObjectClass.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 338 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 340 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntrys -->\r\n");
#line 343 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithoutSeparateStorage(ctx))
    {

#line 346 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.A.Type.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.B.Type.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 355 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}

#line 357 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all structs -->\r\n");
#line 361 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetQuery<Struct>().OrderBy(s => s.ClassName))
    {

#line 364 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.ClassName , "\" >\r\n");
#line 365 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 366 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 367 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
}


#line 370 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>\r\n");

        }



    }
}