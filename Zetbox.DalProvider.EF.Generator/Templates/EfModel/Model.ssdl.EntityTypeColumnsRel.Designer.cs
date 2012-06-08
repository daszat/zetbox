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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst")]
    public partial class ModelSsdlEntityTypeColumnsRel : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected ObjectClass cls;
		protected IEnumerable<Relation> relations;
		protected string prefix;
		protected ISchemaProvider schemaProvider;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, IEnumerable<Relation> relations, string prefix, ISchemaProvider schemaProvider)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelSsdlEntityTypeColumnsRel", ctx, cls, relations, prefix, schemaProvider);
        }

        public ModelSsdlEntityTypeColumnsRel(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, IEnumerable<Relation> relations, string prefix, ISchemaProvider schemaProvider)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.relations = relations;
			this.prefix = prefix;
			this.schemaProvider = schemaProvider;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
this.WriteObjects("");
#line 38 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
foreach(var rel in relations)
	{
		ProcessRelation(rel);
	}
}

/// <summary>Generate a single Property with the specified parameters</summary>
private void GenerateProperty(string propertyName, bool needPositionStorage, string positionColumnName)
{

#line 48 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
this.WriteObjects("    <Property Name=\"",  propertyName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" />\n");
#line 51 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
if (needPositionStorage)
	{

#line 54 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
this.WriteObjects("    <Property Name=\"",  positionColumnName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\n");
#line 56 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
}
	
// } last brace added by template expansion 


        }

    }
}