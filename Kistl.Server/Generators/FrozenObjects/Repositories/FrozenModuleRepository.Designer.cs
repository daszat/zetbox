using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.App.Base;


namespace Kistl.Server.Generators.FrozenObjects.Repositories
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst")]
    public partial class FrozenModuleRepository : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.API.IKistlContext ctx;
		protected List<Module> modulesWithFrozenClasses;


        public FrozenModuleRepository(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, List<Module> modulesWithFrozenClasses)
            : base(_host)
        {
			this.ctx = ctx;
			this.modulesWithFrozenClasses = modulesWithFrozenClasses;

        }
        
        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace Kistl.App\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("	public class FrozenModuleRepository\r\n");
this.WriteObjects("	{\r\n");
this.WriteObjects("		public FrozenModuleRepository()\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
#line 24 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
foreach(var module in modulesWithFrozenClasses)
	{

#line 27 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("		/// <summary>Frozen Repository for ",  module.ModuleName , "</summary>\r\n");
this.WriteObjects("		/// ",  module.Description , "\r\n");
this.WriteObjects("		public ",  module.Namespace , ".Frozen",  module.ModuleName , "Repository ",  module.ModuleName , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				return new ",  module.Namespace , ".Frozen",  module.ModuleName , "Repository();\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
}

#line 40 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		static FrozenModuleRepository()\r\n");
this.WriteObjects("		{\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
// only fill data stores with frozen classes
	foreach(var module in modulesWithFrozenClasses)
	{

#line 48 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("				",  module.Namespace , ".Frozen",  module.ModuleName , "Repository.FillDataStore();\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
}

#line 52 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("		}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("}");

        }



    }
}