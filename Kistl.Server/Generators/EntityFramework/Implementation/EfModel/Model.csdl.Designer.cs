using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst")]
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
#line 15 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Schema Namespace=\"Model\" Alias=\"Self\" xmlns=\"http://schemas.microsoft.com/ado/2006/04/edm\">\r\n");
this.WriteObjects("  <EntityContainer Name=\"Entities\">\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("    <!-- EntitySets for all classes -->\r\n");
#line 21 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var name in ctx.GetBaseClasses().Select(cls => cls.ClassName).OrderBy(s => s))
    {

#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  name , "\" EntityType=\"Model.",  name , "\" />\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 28 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-object CollectionEntrys -->\r\n");
#line 30 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {
        string entityName = rel.GetRelationClassName();
        string assocNameA = rel.GetRelationAssociationName(RelationEndRole.A);
        string assocNameB = rel.GetRelationAssociationName(RelationEndRole.B);

#line 36 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 48 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySets and AssociationSets for all object-value CollectionEntrys -->\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    {
        string assocName = prop.GetAssociationName();
        string entityName = prop.GetCollectionEntryClassName();

#line 59 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <EntitySet Name=\"",  entityName , "\" EntityType=\"Model.",  entityName , "\" />\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  prop.ObjectClass.ClassName , "\" EntitySet=\"",  ((ObjectClass)prop.ObjectClass).GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  entityName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 67 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSets for all object-object relations without CollectionEntrys -->\r\n");
#line 70 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithoutSeparateStorage(ctx))
    {
        string assocName = rel.GetAssociationName();

#line 74 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.GetRootClass().ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 79 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 81 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainer>\r\n");
this.WriteObjects("  \r\n");
this.WriteObjects("  <!-- EntityTypes for all base classes -->\r\n");
#line 86 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.ClassName))
    {

#line 89 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 95 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>());
		if(cls.HasSecurityRules(false))
		{

#line 99 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"CurrentIdentity",  Kistl.API.Helper.ImplementationSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
this.WriteObjects("    <Property Name=\"CurrentAccessRights",  Kistl.API.Helper.ImplementationSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 102 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 104 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 106 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 108 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes for all other classes -->\r\n");
#line 111 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetDerivedClasses().OrderBy(c => c.ClassName))
    {

#line 114 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <EntityType Name=\"",  cls.ClassName , "\" BaseType=\"Model.",  cls.BaseObjectClass.ClassName , "\" >\r\n");
#line 115 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 116 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
#line 118 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 120 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-object CollectionEntrys -->\r\n");
#line 123 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {


#line 127 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <!--\r\n");
#line 129 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
Implementation.RelationDebugTemplate.Call(Host, ctx, rel);

#line 131 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <EntityType Name=\"",  rel.GetRelationClassName() , "\" >\r\n");
this.WriteObjects("    <Key>\r\n");
this.WriteObjects("      <PropertyRef Name=\"ID\" />\r\n");
this.WriteObjects("    </Key>\r\n");
this.WriteObjects("    <Property Name=\"ID\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 138 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if(rel.A.Type.ImplementsIExportable(ctx) && rel.B.Type.ImplementsIExportable(ctx))
	{

#line 141 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	<Property Name=\"ExportGuid\" Type=\"Guid\" Nullable=\"false\" />\r\n");
#line 143 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 144 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- A -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"A",  Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.A.RoleName , "\" />\r\n");
#line 152 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.A))
        {

#line 155 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"A",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 157 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 159 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- B -->\r\n");
this.WriteObjects("    <NavigationProperty Name=\"B",  Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                        FromRole=\"CollectionEntry\"\r\n");
this.WriteObjects("                        ToRole=\"",  rel.B.RoleName , "\" />\r\n");
#line 166 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if (rel.NeedsPositionStorage(RelationEndRole.B))
        {

#line 169 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 171 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 173 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
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
#line 192 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 194 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- EntityTypes and Associations for all object-value CollectionEntrys -->\r\n");
#line 198 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    {


#line 205 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
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
#line 220 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
if (prop.HasPersistentOrder)
        {

#line 223 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("    <Property Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"false\" />\r\n");
#line 225 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 227 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </EntityType>\r\n");
this.WriteObjects("  <Association Name=\"",  prop.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  prop.ObjectClass.ClassName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.ObjectClass.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("    <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("         Type=\"Model.",  prop.GetCollectionEntryClassName() , "\" \r\n");
this.WriteObjects("         Multiplicity=\"*\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 237 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 239 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Associations for all object-object relations without CollectionEntrys -->\r\n");
#line 242 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var rel in GetRelationsWithoutSeparateStorage(ctx))
    {

#line 245 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <Association Name=\"",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("    <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.A.Type.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("         Type=\"Model.",  rel.B.Type.ClassName , "\" \r\n");
this.WriteObjects("         Multiplicity=\"",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("  </Association>\r\n");
#line 254 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}

#line 256 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- ComplexTypes for all structs -->\r\n");
#line 260 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
foreach(var cls in ctx.GetQuery<Struct>().OrderBy(s => s.ClassName))
    {

#line 263 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  <ComplexType Name=\"",  cls.ClassName , "\" >\r\n");
#line 264 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
ApplyEntityTypeFieldDefs(cls.Properties.Cast<Property>()); 
#line 265 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("  </ComplexType>\r\n");
#line 266 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
}


#line 269 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("</Schema>\r\n");

        }



    }
}