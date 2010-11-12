using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst")]
    public partial class ModelSsdlEntityTypeColumnsRel : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;
		protected IEnumerable<Relation> relations;
		protected string prefix;
		protected ISchemaProvider schemaProvider;


        public ModelSsdlEntityTypeColumnsRel(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, IEnumerable<Relation> relations, string prefix, ISchemaProvider schemaProvider)
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
#line 22 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
foreach(var rel in relations)
	{
		ProcessRelation(rel);
	}
}

/// <summary>Generate a single Property with the specified parameters</summary>
private void GenerateProperty(string propertyName, bool needPositionStorage, string positionColumnName)
{

#line 32 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
this.WriteObjects("    <Property Name=\"",  propertyName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" />\r\n");
#line 35 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
if (needPositionStorage)
	{

#line 38 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
this.WriteObjects("    <Property Name=\"",  positionColumnName , "\" Type=\"",  schemaProvider.DbTypeToNative(DbType.Int32) , "\" Nullable=\"true\" />\r\n");
#line 40 "P:\Kistl\Kistl.DalProvider.Ef.Generator\Templates\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
}
	
// } last brace added by template expansion 


        }



    }
}