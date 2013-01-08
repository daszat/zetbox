using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumns.cst")]
    public partial class ModelSsdlEntityTypeColumns : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected IEnumerable<Property> properties;
		protected string prefix;
		protected ISchemaProvider schemaProvider;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<Property> properties, string prefix, ISchemaProvider schemaProvider)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelSsdlEntityTypeColumns", ctx, properties, prefix, schemaProvider);
        }

        public ModelSsdlEntityTypeColumns(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<Property> properties, string prefix, ISchemaProvider schemaProvider)
            : base(_host)
        {
			this.ctx = ctx;
			this.properties = properties;
			this.prefix = prefix;
			this.schemaProvider = schemaProvider;

        }

        public override void Generate()
        {
#line 37 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumns.cst"
/*
	 * TODO: Actually, all this should die and become a bunch of polymorphic calls.
	 */

	foreach(var p in properties)
	{
		// TODO: implement IsNullable everywhere
		
		if (p is CompoundObjectProperty)
		{
			ApplyEntityTypeColumnDefs(
				((CompoundObjectProperty)p).CompoundObjectDefinition.Properties.Cast<Property>().OrderBy(prop => prop.Name),
				Construct.ColumnName(p, prefix),
				schemaProvider);
		}
		else if (p is ObjectReferenceProperty)
		{
			throw new ArgumentException("properties", String.Format("contains ObjectReferenceProperty {0}, but this template cannot work with that", p));
		}
		else
		{
			string columnName = Construct.ColumnName(p, prefix);
			string sqlTypeName = schemaProvider.DbTypeToNative(DbTypeMapper.GetDbTypeForProperty(p.GetType()));
			
			string maxLengthAttr = String.Empty;
			if (p is StringProperty)
			{
				int maxLength = ((StringProperty)p).GetMaxLength();
				if (maxLength != int.MaxValue)
				{
					// must have one space at the end
					maxLengthAttr = String.Format("MaxLength=\"{0}\" ", maxLength);
				}
			}
			
			string precScaleAttr = String.Empty;
			if (p is DecimalProperty)
			{
				DecimalProperty dp = (DecimalProperty)p;
				// must have one space at the end
				precScaleAttr = String.Format("Precision=\"{0}\" Scale=\"{1}\" ", dp.Precision, dp.Scale);
			}
			
			string nullableAttr = String.Empty;
			if (p.IsValueTypePropertySingle())
			{
				// must have one space at the end
				nullableAttr = String.Format("Nullable=\"{0}\" ", IsRealNullable(p).ToString().ToLowerInvariant());
			}

#line 87 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumns.cst"
this.WriteObjects("    <Property Name=\"",  columnName , "\" Type=\"",  sqlTypeName , "\" ",  maxLengthAttr , "",  precScaleAttr , "",  nullableAttr , "/>\r\n");
#line 89 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumns.cst"
}
	}


        }

    }
}