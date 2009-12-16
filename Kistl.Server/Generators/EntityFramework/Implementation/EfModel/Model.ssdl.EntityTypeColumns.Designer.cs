using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
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
#line 20 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
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
		else if (p is ObjectReferenceProperty)
		{
			throw new ArgumentException("properties", String.Format("contains ObjectReferenceProperty {0}, but this template cannot work with that", p));
		}
		else
		{
			string propertyName = Construct.NestedColumnName(p, prefix);
			string sqlTypeName = GetDBType(p);
			
			string maxLengthAttr = "";
			if (p is StringProperty)
			{
				// must have one space at the end
				maxLengthAttr = String.Format("MaxLength=\"{0}\" ", ((StringProperty)p).GetMaxLength());
			}
			
			string nullableAttr = "";
			if (p.IsValueTypePropertySingle())
			{
				// must have one space at the end
				nullableAttr = String.Format("Nullable=\"{0}\" ", ((Property)p).IsNullable().ToString().ToLowerInvariant());
			}

#line 57 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
this.WriteObjects("    <Property Name=\"",  propertyName , "\" Type=\"",  sqlTypeName , "\" ",  maxLengthAttr , "",  nullableAttr , "/>\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumns.cst"
}
	}


        }



    }
}