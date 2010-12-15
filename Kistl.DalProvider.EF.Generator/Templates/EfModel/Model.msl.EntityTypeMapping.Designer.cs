using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst")]
    public partial class ModelMslEntityTypeMapping : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public ModelMslEntityTypeMapping(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  cls.Name , ")\">\r\n");
this.WriteObjects("	    <MappingFragment StoreEntitySet=\"",  cls.Name , "\">\r\n");
this.WriteObjects("	      <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 20 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
ApplyPropertyMappings();

#line 22 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("	    </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
#line 25 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
foreach(var subCls in cls.SubClasses.OrderBy(c => c.Name))
	{
		ApplyEntityTypeMapping(subCls);	
	}


        }



    }
}