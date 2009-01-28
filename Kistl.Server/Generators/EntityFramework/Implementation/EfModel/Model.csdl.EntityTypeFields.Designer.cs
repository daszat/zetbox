using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst")]
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
#line 20 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
/*
	 * TODO: Actually, all this should die and become a bunch of polymorphic calls.
	 */

	foreach(var p in properties)
	{
		// TODO: implement IsNullable everywhere
		var rel = NewRelation.Lookup(ctx, p);
		if (rel != null)
		{
			RelationEnd end = rel.GetEnd(p);
			RelationEnd otherEnd = rel.GetOtherEnd(p);
			if (rel.GetPreferredStorage() == StorageHint.Separate)
			{
				Debug.Assert(end != null);

#line 36 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.PropertyName + Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetCollectionEntryAssociationName(end) , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  end.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 41 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}
			else
			{

#line 45 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.PropertyName + Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  end.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  otherEnd.RoleName , "\" />\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}

			if (p.NeedsPositionColumn())
			{

#line 55 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <Property Name=\"",  p.PropertyName + Kistl.API.Helper.PositionSuffix , "\" Type=\"Int32\" Nullable=\"true\" />\r\n");
#line 57 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}
		}
		else if (p is ValueTypeProperty)
		{
			// ValueTypeProperty
			string name = p.PropertyName;
			string type = p.GetPropertyTypeString();
			string maxlength = "";
			
			if (p is EnumerationProperty)
			{
				name += Kistl.API.Helper.ImplementationSuffix;
				type = "Int32";
			}
			else
			{
				// translate to short name
				type = Type.GetType(type).Name;
			}

			if (p is StringProperty)
			{
				maxlength = String.Format("MaxLength=\"{0}\" ",((StringProperty)p).Length.ToString());
			}

#line 82 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <Property Name=\"",  name , "\" Type=\"",  type , "\" Nullable=\"",  ((ValueTypeProperty)p).IsNullable.ToString().ToLowerInvariant() , "\" ",  maxlength , "/>\r\n");
#line 84 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}
		else if (p is StructProperty)
		{
			// ValueTypeProperty
			// Nullable Complex types are not supported by EF

#line 90 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
this.WriteObjects("    <Property Name=\"",  p.PropertyName + Kistl.API.Helper.ImplementationSuffix , "\"\r\n");
this.WriteObjects("              Type=\"Model.",  ((StructProperty)p).StructDefinition.ClassName , "\"\r\n");
this.WriteObjects("              Nullable=\"false\" />\r\n");
#line 94 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.EntityTypeFields.cst"
}	
	}


        }



    }
}