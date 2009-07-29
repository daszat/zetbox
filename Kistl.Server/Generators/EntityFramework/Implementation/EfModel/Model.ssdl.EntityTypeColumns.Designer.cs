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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst")]
    public partial class ModelSsdlEntityTypeColumns : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected IEnumerable<Property> properties;
		protected string prefix;


        public ModelSsdlEntityTypeColumns(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, IEnumerable<Property> properties, string prefix)
            : base(_host)
        {
			this.ctx = ctx;
			this.properties = properties;
			this.prefix = prefix;

        }
        
        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
/*
	 * TODO: Actually, all this should die and become a bunch of polymorphic calls.
	 */

	foreach(var p in properties)
	{
		// TODO: implement IsNullable everywhere
		
		if (p is StructProperty)
		{
			ApplyEntityTypeColumnDefs(
				((StructProperty)p).StructDefinition.Properties.Cast<Property>().OrderBy(prop => prop.PropertyName),
				Construct.NestedColumnName(p, prefix));
		}
		else
		{
			string propertyName = (p is ObjectReferenceProperty) 
				? Construct.ForeignKeyColumnName((ObjectReferenceProperty)p, prefix) 
				: Construct.NestedColumnName(p, prefix);
			string sqlTypeName = GetDBType(p);
			
			string maxLengthAttr = "";
			if (p is StringProperty)
			{
				// must have one space at the end
				maxLengthAttr = String.Format("MaxLength=\"{0}\" ", ((StringProperty)p).Length);
			}
			
			string nullableAttr = "";
			if (p.IsValueTypePropertySingle())
			{
				// must have one space at the end
				nullableAttr = String.Format("Nullable=\"{0}\" ", ((Property)p).IsNullable.ToString().ToLowerInvariant());
			}

#line 54 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
this.WriteObjects("    <Property Name=\"",  propertyName , "\" Type=\"",  sqlTypeName , "\" ",  maxLengthAttr , "",  nullableAttr , "/>\r\n");
#line 57 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
if (p.NeedsPositionColumn())
		    {

#line 60 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
this.WriteObjects("    <Property Name=\"",  Construct.ListPositionColumnName(p, prefix) , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
}
		}
	}


        }



    }
}