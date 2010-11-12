using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst")]
    public partial class ModelCsdlEntityTypeFields : Kistl.Generator.ResourceTemplate
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
#line 19 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
/*
	 * TODO: Actually, all this should die and become a bunch of polymorphic calls.
	 */

	foreach(var p in properties.OrderBy(p => p.Name))
	{
		// TODO: implement IsNullable everywhere
		if (p is ObjectReferenceProperty)
		{
		    var prop = p as ObjectReferenceProperty;
		    var rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
			var relEnd = rel.GetEnd(prop);
			var otherEnd = rel.GetOtherEnd(relEnd);
			
			if (rel.Storage == StorageType.Separate)
			{
				Debug.Assert(relEnd != null);

#line 37 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(relEnd.GetRole()) , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  relEnd.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 42 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
}
			else
			{

#line 46 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  relEnd.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  otherEnd.RoleName , "\" />\r\n");
#line 52 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
if (rel.NeedsPositionStorage(relEnd.GetRole()))
				{

#line 55 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <Property Name=\"",  Construct.ListPositionPropertyName(relEnd) , "\" Type=\"Int32\" Nullable=\"true\" />\r\n");
#line 57 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
}
			}
		}
		else if (p is ValueTypeProperty)
		{
			var prop = (ValueTypeProperty)p;
			if (prop.IsList)
			{

#line 66 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  prop.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 71 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
}
			else
			{

#line 75 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    ",  ModelCsdl.PlainPropertyDefinitionFromValueType((ValueTypeProperty)p, p.Name, ImplementationPropertySuffix) , "\r\n");
#line 77 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
}
		}
		else if (p is CompoundObjectProperty)
		{
			var prop = (CompoundObjectProperty)p;
			if (prop.IsList)
			{

#line 85 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  prop.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 90 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
}
			else
			{
			// Nullable Complex types are not supported by EF

#line 95 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <Property Name=\"",  p.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("              Type=\"Model.",  prop.CompoundObjectDefinition.Name , "\"\r\n");
this.WriteObjects("              Nullable=\"false\" />\r\n");
#line 99 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.csdl.EntityTypeFields.cst"
}
		}	
	}


        }



    }
}