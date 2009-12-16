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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumnsRel.cst")]
    public partial class ModelSsdlEntityTypeColumnsRel : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;
		protected IEnumerable<Relation> relations;
		protected string prefix;


        public ModelSsdlEntityTypeColumnsRel(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, IEnumerable<Relation> relations, string prefix)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.relations = relations;
			this.prefix = prefix;

        }
        
        public override void Generate()
        {
#line 21 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
foreach(var rel in relations)
	{
		ProcessRelation(rel);
	}
}

/// <summary>Generate a single Property with the specified parameters</summary>
private void GenerateProperty(string propertyName, bool needPositionStorage, string positionColumnName)
{

#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
this.WriteObjects("    <Property Name=\"",  propertyName , "\" Type=\"int\" />\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
if (needPositionStorage)
	{

#line 37 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
this.WriteObjects("    <Property Name=\"",  positionColumnName , "\" Type=\"int\" Nullable=\"true\" />\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.ssdl.EntityTypeColumnsRel.cst"
}
	
// } last brace added by template expansion 


        }



    }
}