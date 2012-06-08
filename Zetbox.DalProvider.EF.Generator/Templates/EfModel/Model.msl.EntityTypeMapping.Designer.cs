using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst")]
    public partial class ModelMslEntityTypeMapping : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected ObjectClass cls;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("EfModel.ModelMslEntityTypeMapping", ctx, cls);
        }

        public ModelMslEntityTypeMapping(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("");
#line 32 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  cls.Name , ")\">\n");
this.WriteObjects("	    <MappingFragment StoreEntitySet=\"",  cls.Name , "\">\n");
this.WriteObjects("	      <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\n");
#line 36 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
ApplyPropertyMappings();

#line 38 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("	    </MappingFragment>\n");
this.WriteObjects("      </EntityTypeMapping>\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\EfModel\Model.msl.EntityTypeMapping.cst"
foreach(var subCls in cls.SubClasses.OrderBy(c => c.Name))
	{
		ApplyEntityTypeMapping(subCls);	
	}


        }

    }
}