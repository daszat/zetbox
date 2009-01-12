using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.GeneratorsOld;


namespace Kistl.Server.Generators.EntityFramework.Server.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst")]
    public partial class ModelMslCollectionEntryEntityTypeMapping : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Property listProp;


        public ModelMslCollectionEntryEntityTypeMapping(Arebis.CodeGeneration.IGenerationHost _host, Property listProp)
            : base(_host)
        {
			this.listProp = listProp;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
TypeMoniker otherType = Construct.PropertyCollectionEntryType(listProp);

#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  otherType.ClassName , ")\">\r\n");
this.WriteObjects("	    <MappingFragment StoreEntitySet=\"",  otherType.ClassName , "\">\r\n");
this.WriteObjects("	      <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 23 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
if (listProp is ValueTypeProperty)
	{

#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	      <ScalarProperty Name=\"Value\" ColumnName=\"",  listProp.PropertyName , "\" />\r\n");
#line 28 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
}

	if (listProp.NeedsPositionColumn())
	{

#line 33 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	      <ScalarProperty Name=\"ValueIndex\" ColumnName=\"",  Construct.ListPositionColumnName(listProp) , "\" />\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
}

	if (listProp.IsIndexed)
	{

#line 40 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	      <ScalarProperty Name=\"ParentIndex\" ColumnName=\"",  Construct.ListPositionColumnName(listProp.ObjectClass) , "\" />\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
}

#line 44 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	    </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>");

        }



    }
}