using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.DalProvider.EF.Generator.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.EntityTypeMapping.cst")]
    public partial class ModelMslEntityTypeMapping : Kistl.Server.Generators.KistlCodeTemplate
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
#line 17 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("      <EntityTypeMapping TypeName=\"IsTypeOf(Model.",  cls.ClassName , ")\">\r\n");
this.WriteObjects("	    <MappingFragment StoreEntitySet=\"",  cls.ClassName , "\">\r\n");
this.WriteObjects("	      <ScalarProperty Name=\"ID\" ColumnName=\"ID\" />\r\n");
#line 21 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.EntityTypeMapping.cst"
ApplyPropertyMappings();
		if(cls.HasSecurityRules(false))
		{

#line 25 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("          <ScalarProperty Name=\"CurrentIdentity",  Kistl.API.Helper.ImplementationSuffix , "\" ColumnName=\"Rights__CurrentIdentity\" />\r\n");
this.WriteObjects("          <ScalarProperty Name=\"CurrentAccessRights",  Kistl.API.Helper.ImplementationSuffix , "\" ColumnName=\"Rights__CurrentAccessRights\" />\r\n");
#line 28 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.EntityTypeMapping.cst"
}

#line 30 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.EntityTypeMapping.cst"
this.WriteObjects("	    </MappingFragment>\r\n");
this.WriteObjects("      </EntityTypeMapping>\r\n");
#line 33 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\EfModel\Model.msl.EntityTypeMapping.cst"
foreach(var subCls in cls.SubClasses.OrderBy(c => c.ClassName))
	{
		ApplyEntityTypeMapping(subCls);	
	}


        }



    }
}