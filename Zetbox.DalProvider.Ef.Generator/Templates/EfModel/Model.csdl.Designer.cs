using System;
using System.Collections.Generic;
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
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst")]
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
#line 32 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("    <!-- EntitySets for all classes -->\r\n");
#line 38 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var cls in GetBaseClasses(ctx).OrderBy(c => c.Name))
    {
        var clsName = cls.Name;

#line 42 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  clsName , "EfImpl\" EntityType=\"Model.",  clsName , "EfImpl\" />\r\n");
#line 44 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (cls.NeedsRightsTable())
		{

#line 47 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  Construct.SecurityRulesClassName(cls) , "\" EntityType=\"Model.",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  Construct.SecurityRulesFKName(cls) , "\" Association=\"Model.",  Construct.SecurityRulesFKName(cls) , "\">\r\n");
this.WriteObjects("      <End Role=\"",  clsName , "\" EntitySet=\"",  clsName , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"",  Construct.SecurityRulesClassName(cls) , "\" EntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 53 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}
    }

#line 56 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-object CollectionEntrys -->\r\n");
#line 58 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in GetRelationsWithSeparateStorage(ctx))
    {
        string entityName = rel.GetRelationClassName();
        string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
        string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);
        var a = rel.A;
        var b = rel.B;

#line 66 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "EfImpl\" EntityType=\"Model.",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  a.RoleName , "\" EntitySet=\"",  a.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  b.RoleName , "\" EntitySet=\"",  b.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 76 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 78 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-value CollectionEntrys -->\r\n");
#line 81 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var prop in GetValueTypeProperties(ctx)
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();
        var cls = (ObjectClass)prop.ObjectClass;

#line 91 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "EfImpl\" EntityType=\"Model.",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" EntitySet=\"",  cls.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 97 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 99 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-struct CollectionEntrys -->\r\n");
#line 102 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var prop in GetCompoundObjectProperties(ctx)
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();
        var cls = (ObjectClass)prop.ObjectClass;

#line 112 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "EfImpl\" EntityType=\"Model.",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  cls.Name , "\" EntitySet=\"",  cls.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 118 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 120 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations without CollectionEntrys -->\r\n");
#line 123 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in GetRelationsWithoutSeparateStorage(ctx))
    {
        string assocName = rel.GetAssociationName();
        var a = rel.A;
        var b = rel.B;

#line 129 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  a.RoleName , "\" EntitySet=\"",  a.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("      <End Role=\"",  b.RoleName , "\" EntitySet=\"",  b.Type.GetRootClass().Name , "EfImpl\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 134 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 136 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 149 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var cls in GetBaseClasses(ctx).OrderBy(c => c.Name))
    {
        var clsName = cls.Name;

#line 153 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  clsName , "EfImpl\"",  GetAbstractModifier(cls) , ">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 159 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>());
		if (cls.NeedsRightsTable())
		{

#line 163 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <NavigationProperty Name=\"SecurityRightsCollection",  ImplementationPropertySuffix , "\" Relationship=\"Model.",  Construct.SecurityRulesFKName(cls) , "\" FromRole=\"",  clsName , "\" ToRole=\"",  Construct.SecurityRulesClassName(cls) , "\" />\r\n");
#line 165 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 167 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 169 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (cls.NeedsRightsTable())
		{

#line 172 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 196 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}
    }

#line 199 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 202 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var cls in GetDerivedClasses(ctx).OrderBy(c => c.Name))
    {

#line 205 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.Name , "EfImpl\" BaseType=\"Model.",  cls.BaseObjectClass.Name , "EfImpl\"",  GetAbstractModifier(cls) , ">\r\n");
#line 206 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 207 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 209 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 211 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-object CollectionEntrys -->\r\n");
#line 214 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in GetRelationsWithSeparateStorage(ctx))
    {
        var a = rel.A;
        var b = rel.B;

#line 219 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <!--\r\n");
#line 221 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 223 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  rel.GetRelationClassName() , "EfImpl\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 230 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (a.Type.ImplementsIExportable() && b.Type.ImplementsIExportable())
	{

#line 233 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"Guid\" Nullable=\"false\" />\r\n");
#line 235 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 236 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  a.RoleName , "\" />\r\n");
#line 244 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
        {

#line 247 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"A",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 249 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 251 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"B",  ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  b.RoleName , "\" />\r\n");
#line 258 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
        {

#line 261 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 263 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 265 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 284 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 286 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 290 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var prop in GetValueTypeProperties(ctx)
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        var propClsName = prop.ObjectClass.Name;

#line 298 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 313 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 316 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Value",  Zetbox.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 318 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 320 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  propClsName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  propClsName , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 330 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 332 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("<!-- EntityTypes and Associations for all object-CompoundObject CollectionEntrys -->\r\n");
#line 335 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach(var prop in GetCompoundObjectProperties(ctx)
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    {
        var propClsName = prop.ObjectClass.Name;


#line 344 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
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
#line 361 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 364 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"Index\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 366 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 368 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  propClsName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  propClsName , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 378 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 380 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntrys -->\r\n");
#line 383 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var rel in GetRelationsWithoutSeparateStorage(ctx))
    {
        var a = rel.A;
        var b = rel.B;

#line 388 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  a.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  a.Type.Name , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"",  a.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  b.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  b.Type.Name , "EfImpl\" \r\n");
this.WriteObjects("         Multiplicity=\"",  b.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 397 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}

#line 399 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all CompoundObjects -->\r\n");
#line 403 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
foreach (var cls in GetCompoundObjects(ctx).OrderBy(s => s.Name))
    {

#line 406 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.Name , "EfImpl\" >\r\n");
#line 407 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 408 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 409 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
}


#line 412 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>\r\n");

        }

    }
}