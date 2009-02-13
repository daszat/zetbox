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
		protected List<Module> modules;


        public FrozenModuleRepository(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, List<Module> modules)
            : base(_host)
        {
			this.ctx = ctx;
			this.modules = modules;

        }
        
        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace Kistl.App.Frozen\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("	public class ModuleRepository\r\n");
this.WriteObjects("	{\r\n");
this.WriteObjects("		public ModuleRepository(IKistlContext ctx)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			this.Context = ctx;\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		public IKistlContext Context { get; private set; }\r\n");
this.WriteObjects("	\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
// only generate modules with frozen classes
	foreach(var module in modules.Where(m => m.DataTypes.OfType<ObjectClass>().Any(cls => cls.IsFrozenObject) ))
	{

#line 30 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("		/// <summary>Repository for ",  module.ModuleName , "</summary>\r\n");
this.WriteObjects("		/// ",  module.Description , "\r\n");
this.WriteObjects("		public ",  module.Namespace , ".",  module.ModuleName , "Repository ",  module.ModuleName , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				return new ",  module.Namespace , ".",  module.ModuleName , "Repository(Context);\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
#line 41 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
}

#line 43 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenModuleRepository.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("}");

        }



    }
}