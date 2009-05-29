using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst")]
    public partial class ModelCsdlEntityTypeFields : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected IEnumerable<Property> properties;


        public ModelCsdlEntityTypeFields(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, IEnumerable<Property> properties)
            : base(_host)
        {
			this.ctx = ctx;
			this.properties = properties;

        }
        
        public override void Generate()
        {
#line 20 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
/*
	 * TODO: Actually, all this should die and become a bunch of polymorphic calls.
	 */

	foreach(var p in properties)
	{
		// TODO: implement IsNullable everywhere
		if (p is ObjectReferenceProperty)
		{
		    var prop = p as ObjectReferenceProperty;
		    var rel = RelationExtensions.Lookup(ctx, prop);
			var relEnd = rel.GetEnd(prop);
			var otherEnd = rel.GetOtherEnd(relEnd);
			
			if (rel.Storage == StorageType.Separate)
			{
				Debug.Assert(relEnd != null);

#line 38 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.PropertyName + Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName((RelationEndRole)relEnd.Role) , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  relEnd.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 43 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}
			else
			{

#line 47 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.PropertyName + Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  relEnd.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  otherEnd.RoleName , "\" />\r\n");
#line 53 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
if (rel.NeedsPositionStorage((RelationEndRole)relEnd.Role))
				{

#line 56 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <Property Name=\"",  p.PropertyName + Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"true\" />\r\n");
#line 58 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}
			}
		}
		else if (p is ValueTypeProperty)
		{
			var prop = (ValueTypeProperty)p;
			if (prop.IsList)
			{

#line 67 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  prop.PropertyName + Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  prop.ObjectClass.ClassName , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 72 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}
			else
			{

#line 76 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    ",  ModelCsdl.PlainPropertyDefinitionFromValueType((ValueTypeProperty)p) , "\r\n");
#line 78 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}
		}
		else if (p is StructProperty)
		{
			// Nullable Complex types are not supported by EF

#line 84 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <Property Name=\"",  p.PropertyName + Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("              Type=\"Model.",  ((StructProperty)p).StructDefinition.ClassName , "\"\r\n");
this.WriteObjects("              Nullable=\"false\" />\r\n");
#line 88 "P:\kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}	
	}


        }



    }
}