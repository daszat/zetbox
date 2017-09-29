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
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst")]
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
#line 32 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 38 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var cls in GetBaseClasses(ctx).OrderBy(c => c.Name))
    {
        var clsName = cls.Name;

#line 42 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  clsName , "EfImpl\">\r\n");
#line 43 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 44 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 46 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (cls.NeedsRightsTable())
		{

#line 49 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 68 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    }

#line 71 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-object relations with a CollectionEntry -->\r\n");
#line 75 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {
        string fkAName = Construct.ForeignKeyColumnName(rel.A);
        string fkBName = Construct.ForeignKeyColumnName(rel.B);

#line 80 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 82 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 84 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetRelationClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  rel.GetRelationClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 90 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 93 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("		  <ScalarProperty Name=\"ExportGuid\" ColumnName=\"ExportGuid\" />\r\n");
#line 95 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
		if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 99 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"A",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkAName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 101 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

		if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 106 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkBName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 108 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 110 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 139 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 142 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSetMappings for direct object-object relations without a CollectionEntry -->\r\n");
#line 146 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var rel in GetRelationsWithoutSeparateStorage(ctx))
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

#line 164 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 166 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 168 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 181 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 184 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-value CollectionEntrys -->\r\n");
#line 187 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in GetValueTypeProperties(ctx)
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 
        var propClsName = prop.ObjectClass.Name;

#line 195 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Value\" ColumnName=\"",  prop.Name , "\" />\r\n");
#line 201 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 204 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 206 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 208 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 223 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 226 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-struct CollectionEntrys -->\r\n");
#line 229 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in GetCompoundObjectProperties(ctx)
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 
        var propClsName = prop.ObjectClass.Name;

#line 237 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          ");
#line 241 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, "Value", string.Empty); 
#line 243 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 246 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"Value",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 248 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 250 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 265 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 268 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	<FunctionImportMapping FunctionImportName=\"GetContinuousSequenceNumber\" FunctionName=\"Model.Store.GetContinuousSequenceNumber\" />\r\n");
this.WriteObjects("    <FunctionImportMapping FunctionImportName=\"GetSequenceNumber\" FunctionName=\"Model.Store.GetSequenceNumber\" />\r\n");
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }

    }
}