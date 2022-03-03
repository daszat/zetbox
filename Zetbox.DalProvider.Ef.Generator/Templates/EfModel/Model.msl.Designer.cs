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
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst")]
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
#line 32 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 38 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var cls in GetBaseClasses(ctx).OrderBy(c => c.Name))
    {
        var clsName = cls.Name;

#line 42 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  clsName , "EfImpl\">\r\n");
#line 43 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 44 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 46 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (cls.NeedsRightsTable())
		{

#line 49 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 68 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    }

#line 71 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-object relations with a CollectionEntry -->\r\n");
#line 75 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var rel in GetRelationsWithSeparateStorage(ctx))
    {
        string fkAName = Construct.ForeignKeyColumnName(rel.A);
        string fkBName = Construct.ForeignKeyColumnName(rel.B);

#line 80 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 82 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 84 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetRelationClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  rel.GetRelationClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 90 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 93 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("		  <ScalarProperty Name=\"ExportGuid\" ColumnName=\"ExportGuid\" />\r\n");
#line 95 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
		if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 99 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"A",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkAName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 101 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

		if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 106 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkBName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 108 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 110 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 139 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 142 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSetMappings for direct object-object relations without a CollectionEntry -->\r\n");
#line 146 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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

#line 164 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 166 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 168 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 181 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 184 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-value CollectionEntrys -->\r\n");
#line 187 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in GetValueTypeProperties(ctx)
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 
        var propClsName = prop.ObjectClass.Name;

#line 195 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Value\" ColumnName=\"",  prop.Name , "\" />\r\n");
#line 201 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 204 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 206 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 208 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 223 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 226 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-struct CollectionEntrys -->\r\n");
#line 229 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in GetCompoundObjectProperties(ctx)
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 
        var propClsName = prop.ObjectClass.Name;

#line 237 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"Model.",  prop.GetCollectionEntryClassName() , "EfImpl\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          ");
#line 241 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, "Value", string.Empty); 
#line 243 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 246 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"Index\" ColumnName=\"",  prop.Name , "Index\" />\r\n");
#line 248 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 250 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 265 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 268 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	<FunctionImportMapping FunctionImportName=\"GetContinuousSequenceNumber\" FunctionName=\"Model.Store.GetContinuousSequenceNumber\" />\r\n");
this.WriteObjects("    <FunctionImportMapping FunctionImportName=\"GetSequenceNumber\" FunctionName=\"Model.Store.GetSequenceNumber\" />\r\n");
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }

    }
}