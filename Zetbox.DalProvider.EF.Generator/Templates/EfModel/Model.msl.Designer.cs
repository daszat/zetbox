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
#line 31 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 37 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {

#line 40 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  cls.Name , "EfImpl\">\r\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 42 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 44 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (cls.NeedsRightsTable())
		{

#line 47 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  Construct.SecurityRulesClassName(cls) , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Identity\" ColumnName=\"Identity\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Right\" ColumnName=\"Right\" />\r\n");
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  Construct.SecurityRulesFKName(cls) , "\" TypeName=\"Model.",  Construct.SecurityRulesFKName(cls) , "\" StoreEntitySet=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("      <EndProperty Name=\"",  cls.Name , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("        <ScalarProperty Name=\"Identity\" ColumnName=\"Identity\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 66 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    }

#line 69 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-object relations with a CollectionEntry -->\r\n");
#line 73 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var rel in ctx.GetRelationsWithSeparateStorage())
    {
        string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
        string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

#line 78 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 80 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 82 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  rel.GetRelationClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 88 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 91 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("		  <ScalarProperty Name=\"ExportGuid\" ColumnName=\"ExportGuid\" />\r\n");
#line 93 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
		if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 97 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"A",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkAName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 99 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

		if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 104 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkBName , "",  Zetbox.API.Helper.PositionSuffix , "\" />\r\n");
#line 106 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 108 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 137 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 140 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSetMappings for direct object-object relations without a CollectionEntry -->\r\n");
#line 144 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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

#line 162 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 164 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 166 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  dependent.Type.Name , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  principal.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_",  principal.RoleName , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  dependent.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_",  principal.RoleName , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 179 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 182 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-value CollectionEntrys -->\r\n");
#line 185 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 

#line 192 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  prop.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Value\" ColumnName=\"",  prop.Name , "\" />\r\n");
#line 198 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 201 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 203 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 205 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  prop.ObjectClass.Name , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_",  prop.ObjectClass.Name , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_",  prop.ObjectClass.Name , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 220 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 223 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-struct CollectionEntrys -->\r\n");
#line 226 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 

#line 233 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  prop.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          ");
#line 237 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, "Value", string.Empty); 
#line 239 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 242 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"Value",  Zetbox.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 244 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 246 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  prop.ObjectClass.Name , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_",  prop.ObjectClass.Name , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_",  prop.ObjectClass.Name , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 261 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 264 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	<FunctionImportMapping FunctionImportName=\"GetContinuousSequenceNumber\" FunctionName=\"Model.Store.GetContinuousSequenceNumber\" />\r\n");
this.WriteObjects("    <FunctionImportMapping FunctionImportName=\"GetSequenceNumber\" FunctionName=\"Model.Store.GetSequenceNumber\" />\r\n");
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }

    }
}