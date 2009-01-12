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
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
// map entities
	foreach(var cls in ctx.GetBaseClasses())
	{

#line 23 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  cls.ClassName , "\">\r\n");
#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

	// map collection entries
	foreach(var listProp in ctx.GetObjectListPropertiesWithStorage())
	{
		TypeMoniker parentType = listProp.ObjectClass.GetTypeMoniker();
		TypeMoniker childType = Construct.PropertyCollectionEntryType(listProp);
		string associationName = Construct.AssociationName(parentType, childType, "fk_Parent");
		string fkColumnName = Construct.ForeignKeyColumnNameReferencing(listProp.ObjectClass);

#line 37 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  childType.ClassName , "\">\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
ApplyCollectionEntryEntityTypeMapping(listProp); 
#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
this.WriteObjects("    <AssociationSetMapping Name=\"",  associationName , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  associationName , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  childType.ClassName , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  Construct.AssociationParentRoleName(parentType) , "\" >\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"",  fkColumnName , "\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  Construct.AssociationChildRoleName(childType) , "\" >\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"",  fkColumnName , "\" IsNull=\"false\" />\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

	// map object references
	foreach(var refProp in ctx.GetObjectReferencePropertiesWithStorage())
	{
		TypeMoniker parentType = new TypeMoniker(refProp.GetPropertyTypeString());
		TypeMoniker childType = Construct.AssociationChildType(refProp);
		string associationName = Construct.AssociationName(parentType, childType, refProp.PropertyName);
		string fkColumnName = Construct.ForeignKeyColumnName(refProp);

#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("    <AssociationSetMapping Name=\"",  associationName , "\"\r\n");
this.WriteObjects("                           TypeName=\"Model.",  associationName , "\"\r\n");
this.WriteObjects("                           StoreEntitySet=\"",  childType.ClassName , "\" >\r\n");
this.WriteObjects("      <EndProperty Name=\"",  Construct.AssociationParentRoleName(parentType) , "\" >\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"",  fkColumnName , "\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <EndProperty Name=\"",  Construct.AssociationChildRoleName(childType) , "\" >\r\n");
this.WriteObjects("        <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
this.WriteObjects("      </EndProperty>\r\n");
this.WriteObjects("      <Condition ColumnName=\"",  fkColumnName , "\" IsNull=\"false\" />\r\n");
this.WriteObjects("    </AssociationSetMapping>\r\n");
#line 74 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
}

#line 76 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.msl.cst"
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }



    }
}