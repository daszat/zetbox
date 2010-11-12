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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst")]
    public partial class ModelMsl : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;


        public ModelMsl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 21 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.Name))
    {

#line 24 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  cls.Name , "\">\r\n");
#line 25 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 26 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 28 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if(cls.NeedsRightsTable())
		{

#line 31 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 50 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    }

#line 53 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-object relations with a CollectionEntry -->\r\n");
#line 57 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var rel in ModelCsdl.GetRelationsWithSeparateStorage(ctx))
    {
        string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
        string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

#line 62 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 64 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 66 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  rel.GetRelationClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 72 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if(rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 75 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("		  <ScalarProperty Name=\"ExportGuid\" ColumnName=\"ExportGuid\" />\r\n");
#line 77 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
		if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 81 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"A",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkAName , "",  Kistl.API.Helper.PositionSuffix , "\" />\r\n");
#line 83 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

		if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 88 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkBName , "",  Kistl.API.Helper.PositionSuffix , "\" />\r\n");
#line 90 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 92 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 121 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 124 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSetMappings for direct object-object relations without a CollectionEntry -->\r\n");
#line 128 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var rel in ModelCsdl.GetRelationsWithoutSeparateStorage(ctx))
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

#line 146 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 148 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 150 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 163 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 166 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-value CollectionEntrys -->\r\n");
#line 169 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 

#line 175 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  prop.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"Value\" ColumnName=\"",  prop.Name , "\" />\r\n");
#line 181 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 184 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 186 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 188 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 203 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 206 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-struct CollectionEntrys -->\r\n");
#line 209 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
    { 

#line 215 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  prop.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          ");
#line 219 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, "B", string.Empty); 
#line 221 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 224 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 226 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}

#line 228 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
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
#line 243 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
}
    

#line 246 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }



    }
}