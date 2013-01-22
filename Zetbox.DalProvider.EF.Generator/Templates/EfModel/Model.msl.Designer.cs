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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst")]
    public partial class ModelMsl : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelMsl", ctx);
        }

        public ModelMsl(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 32 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 38 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {
        var clsName = cls.Name;

#line 42 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  clsName , "EfImpl\">\r\n");
#line 43 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 44 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 46 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (cls.NeedsRightsTable())
		{

#line 49 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Identity\" ColumnName=\"Identity\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Right\" ColumnName=\"Right\" />\r\n");
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  Construct.SecurityRulesFKName(cls) , "\" TypeName=\"Model.",  Construct.SecurityRulesFKName(cls) , "\" StoreEntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("      <EndProperty Name=\"",  clsName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("        <ScalarProperty Name=\"Identity\" ColumnName=\"Identity\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 68 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    }

#line 71 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-object relations with a CollectionEntry -->\r\n");
#line 75 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
    {
        string fkAName = Construct.ForeignKeyColumnName(rel.A);
        string fkBName = Construct.ForeignKeyColumnName(rel.B);

#line 80 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 82 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 84 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetRelationClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  rel.GetRelationClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 90 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 93 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("		  <ScalarProperty Name=\"ExportGuid\" ColumnName=\"ExportGuid\" />\r\n");
#line 95 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
		if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 99 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"A",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkAName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 101 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

		if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 106 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkBName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 108 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 110 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <!-- A to CollectionEntry -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.A) , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  rel.GetRelationClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  rel.A.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"",  fkAName , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"",  fkAName , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
this.WriteObjects("    <!-- B to CollectionEntry -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  rel.GetRelationAssociationName(RelationEndRole.B) , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  rel.GetRelationClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  rel.B.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"",  fkBName , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"",  fkBName , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
this.WriteObjects("\r\n");
#line 139 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 142 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSetMappings for direct object-object relations without a CollectionEntry -->\r\n");
#line 146 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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

#line 164 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 166 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 168 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  dependent.Type.GetEntitySetName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  principal.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"",  Construct.ForeignKeyColumnName(principal) , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  dependent.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"",  Construct.ForeignKeyColumnName(principal) , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 181 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 184 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-value CollectionEntrys -->\r\n");
#line 187 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 
        var propClsName = prop.ObjectClass.Name;

#line 195 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Value\" ColumnName=\"",  prop.Name , "\" />\r\n");
#line 201 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 204 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 206 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 208 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  prop.ObjectClass.Name , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"",  Construct.ForeignKeyColumnName(prop) , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"",  Construct.ForeignKeyColumnName(prop)  , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 223 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 226 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-struct CollectionEntrys -->\r\n");
#line 229 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 
        var propClsName = prop.ObjectClass.Name;

#line 237 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          ");
#line 241 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, "Value", string.Empty); 
#line 243 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 246 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"Value",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 248 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 250 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  prop.ObjectClass.Name , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"",  Construct.ForeignKeyColumnName(prop) , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"",  Construct.ForeignKeyColumnName(prop) , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 265 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 268 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	<FunctionImportMapping FunctionImportName=\"GetContinuousSequenceNumber\" FunctionName=\"Model.Store.GetContinuousSequenceNumber\" />\r\n");
this.WriteObjects("    <FunctionImportMapping FunctionImportName=\"GetSequenceNumber\" FunctionName=\"Model.Store.GetSequenceNumber\" />\r\n");
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }

    }
}