using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


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
#line 15 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    <!-- EntitySetMappings for classes -->\r\n");
#line 21 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  cls.ClassName , "\">\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 28 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

#line 30 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- EntitySetMappings for collections -->\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetObjectListPropertiesWithStorage())
	{
		var info = AssociationInfo.CreateInfo(ctx, prop);

#line 38 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  info.BSide.Type.ClassName , "\">\r\n");
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  info.BSide.Type.ClassName , ")\">\r\n");
this.WriteObjects("        <MappingFragment StoreEntitySet=\"",  info.BSide.Type.ClassName , "\">\r\n");
this.WriteObjects("          <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 43 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
if (prop is ValueTypeProperty)
	{

#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"Value\" ColumnName=\"",  prop.PropertyName , "\" />\r\n");
#line 47 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

	if (prop.NeedsPositionColumn())
	{

#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"ValueIndex\" ColumnName=\"",  Construct.ListPositionColumnName(prop, "") , "\" />\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

	if (prop.IsIndexed)
	{

#line 58 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("          <ScalarProperty Name=\"ValueIndex\" ColumnName=\"",  Construct.ListPositionColumnName(prop.ObjectClass) , "\" />\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

#line 61 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("        </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}
	

#line 68 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    <!-- AssociationSetMappings -->\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
foreach(var prop in ctx.GetAssociationPropertiesWithStorage())
	{
		var info = AssociationInfo.CreateInfo(ctx, prop);
		var colName = prop.IsList ? prop.ObjectClass.ClassName : prop.PropertyName;

#line 77 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <!-- ",  info.GetType().Name , " -->\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  info.AssociationName , "\" TypeName=\"Model.",  info.AssociationName , "\" StoreEntitySet=\"",  info.BSide.StorageEntitySet , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  info.ASide.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"fk_",  colName , "\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  info.BSide.RoleName , "\">\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\"/>\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"fk_",  colName , "\" IsNull=\"false\"/>\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 87 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

#line 89 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }



    }
}