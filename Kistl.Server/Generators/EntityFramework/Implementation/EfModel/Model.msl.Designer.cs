using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst")]
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
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  cls.ClassName , "\">\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings for relations -->\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
foreach(var rel in NewRelation.GetAll(ctx).OrderBy(r => r.GetAssociationName()))
	{

#line 38 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <!--\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);

#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    -->\r\n");
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  rel.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
if (rel.A.HasPersistentOrder)
	{

#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"AIndex\" ColumnName=\"AIndex\" />\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

	if (rel.B.HasPersistentOrder)
	{

#line 58 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"BIndex\" ColumnName=\"BIndex\" />\r\n");
#line 60 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <!-- A to CollectionEntry -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  rel.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  rel.A.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_A\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"A\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_A\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
this.WriteObjects("    <!-- B to CollectionEntry -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  rel.GetCollectionEntryClassName() , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  rel.A.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_A\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  rel.B.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_A\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
this.WriteObjects("\r\n");
#line 91 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}
	

#line 94 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings for ValueType lists -->\r\n");
#line 97 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList))
	{ 
	#if false // TODO

#line 102 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  rel.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  rel.GetCollectionEntryClassName() , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  rel.GetCollectionEntryClassName() , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 107 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
if (rel.A.HasPersistentOrder)
	{

#line 110 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"AIndex\" ColumnName=\"AIndex\" />\r\n");
#line 112 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

	if (rel.B.HasPersistentOrder)
	{

#line 117 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"BIndex\" ColumnName=\"BIndex\" />\r\n");
#line 119 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

#line 121 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 125 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
#endif
	}
	

#line 129 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }



    }
}