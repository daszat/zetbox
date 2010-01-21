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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst")]
    public partial class ModelMsl : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public ModelMsl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 22 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
foreach(var cls in ctx.GetBaseClasses().OrderBy(c => c.ClassName))
    {

#line 25 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  cls.ClassName , "\">\r\n");
#line 26 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 27 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 29 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
if(cls.HasSecurityRules(false))
		{

#line 32 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
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
this.WriteObjects("      <EndProperty Name=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  Construct.SecurityRulesClassName(cls) , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("        <ScalarProperty Name=\"Identity\" ColumnName=\"Identity\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 51 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}
    }

#line 54 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-object relations with a CollectionEntry -->\r\n");
#line 58 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
foreach(var rel in ModelCsdl.GetRelationsWithSeparateStorage(ctx))
    {
        string fkAName = rel.GetRelationFkColumnName(RelationEndRole.A);
        string fkBName = rel.GetRelationFkColumnName(RelationEndRole.B);

#line 63 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 65 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
Implementation.RelationDebugTemplate.Call(Host, ctx, rel);

#line 67 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  rel.GetRelationClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetRelationClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 73 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
if(rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable())
		{

#line 76 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("		  <ScalarProperty Name=\"ExportGuid\" ColumnName=\"ExportGuid\" />\r\n");
#line 78 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}
		if (rel.NeedsPositionStorage(RelationEndRole.A))
		{

#line 82 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"A",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkAName , "",  Kistl.API.Helper.PositionSuffix , "\" />\r\n");
#line 84 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}

		if (rel.NeedsPositionStorage(RelationEndRole.B))
		{

#line 89 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"",  fkBName , "",  Kistl.API.Helper.PositionSuffix , "\" />\r\n");
#line 91 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}

#line 93 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
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
#line 122 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}
    

#line 125 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSetMappings for direct object-object relations without a CollectionEntry -->\r\n");
#line 129 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
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

#line 147 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 149 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
Implementation.RelationDebugTemplate.Call(Host, ctx, rel);

#line 151 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  dependent.Type.ClassName , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  principal.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_",  principal.RoleName , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  dependent.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_",  principal.RoleName , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 164 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}
    

#line 167 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-value CollectionEntrys -->\r\n");
#line 170 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    { 

#line 176 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  prop.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"B\" ColumnName=\"",  prop.PropertyName , "\" />\r\n");
#line 182 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 185 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 187 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}

#line 189 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  prop.ObjectClass.ClassName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_",  prop.ObjectClass.ClassName , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_",  prop.ObjectClass.ClassName , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 204 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}
    

#line 207 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings and AssociationSetMappings for object-struct CollectionEntrys -->\r\n");
#line 210 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .ThenBy(p => p.PropertyName))
    { 

#line 216 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  prop.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("          ");
#line 220 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, "B", string.Empty); 
#line 222 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
if (prop.HasPersistentOrder)
		{

#line 225 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"B",  Kistl.API.Helper.PositionSuffix , "\" ColumnName=\"BIndex\" />\r\n");
#line 227 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}

#line 229 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  prop.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  prop.ObjectClass.ClassName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_",  prop.ObjectClass.ClassName , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"CollectionEntry\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_",  prop.ObjectClass.ClassName , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 244 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
}
    

#line 247 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }



    }
}