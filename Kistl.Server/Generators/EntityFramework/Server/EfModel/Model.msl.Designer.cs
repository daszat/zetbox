using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Server.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst")]
    public partial class ModelMsl : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public ModelMsl(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Mapping Space=\"C-S\" xmlns=\"urn:schemas-microsoft-com:windows:storage:mapping:CS\">\r\n");
this.WriteObjects("  <EntityContainerMapping StorageEntityContainer=\"dbo\" CdmEntityContainer=\"Entities\">\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst"
foreach(var cls in ctx.GetBaseClasses())
	{

#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst"
this.WriteObjects("    <EntitySetMapping Name=\"",  cls.ClassName , "\">\r\n");
#line 23 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst"
ApplyEntityTypeMapping(cls); 
#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst"
this.WriteObjects("    </EntitySetMapping>\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst"
}

#line 28 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Server\EfModel\Model.msl.cst"
this.WriteObjects("  </EntityContainerMapping>\r\n");
this.WriteObjects("</Mapping>");

        }



    }
}