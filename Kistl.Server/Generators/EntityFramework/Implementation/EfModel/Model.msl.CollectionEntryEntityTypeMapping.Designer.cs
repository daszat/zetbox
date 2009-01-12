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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst")]
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
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
TypeMoniker otherType = Construct.PropertyCollectionEntryType(listProp);

#line 18 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  otherType.ClassName , ")\">\r\n");
this.WriteObjects("	    <MappingFragment StoreEntitySet=\"",  otherType.ClassName , "\">\r\n");
this.WriteObjects("	      <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
if (listProp is ValueTypeProperty)
	{

#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	      <ScalarProperty Name=\"Value\" ColumnName=\"",  listProp.PropertyName , "\" />\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
}

	if (listProp.NeedsPositionColumn())
	{

#line 32 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	      <ScalarProperty Name=\"ValueIndex\" ColumnName=\"",  Construct.ListPositionColumnName(listProp) , "\" />\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
}

	if (listProp.IsIndexed)
	{

#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	      <ScalarProperty Name=\"ParentIndex\" ColumnName=\"",  Construct.ListPositionColumnName(listProp.ObjectClass) , "\" />\r\n");
#line 41 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
}

#line 43 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.CollectionEntryEntityTypeMapping.cst"
this.WriteObjects("	    </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>");

        }



    }
}