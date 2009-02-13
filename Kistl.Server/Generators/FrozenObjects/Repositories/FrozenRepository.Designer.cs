using System.Collections.Generic;
using System.Linq;
using Kistl.App.Base;


namespace Kistl.Server.Generators.FrozenObjects.Repositories
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst")]
    public partial class FrozenRepository : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.API.IKistlContext ctx;
		protected Module module;


        public FrozenRepository(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Module module)
            : base(_host)
        {
			this.ctx = ctx;
			this.module = module;

        }
        
        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
string classname = "Frozen" + module.ModuleName + "Repository";


#line 14 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("using System;\r\n");
this.WriteObjects("using System.Collections.Generic;\r\n");
this.WriteObjects("using System.Linq;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("	public class ",  classname , "\r\n");
this.WriteObjects("	{\r\n");
this.WriteObjects("		public ",  classname , "(IKistlContext ctx)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			this.Context = ctx;\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		public IKistlContext Context { get; private set; }\r\n");
this.WriteObjects("		\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
foreach(var cls in module.DataTypes.OfType<ObjectClass>())
	{

#line 36 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("		/// <summary>List of all ",  cls.ClassName , "</summary>\r\n");
this.WriteObjects("		/// ",  cls.Description , "\r\n");
this.WriteObjects("		public IQueryable<",  cls.ClassName , "> ",  cls.TableName , "\r\n");
this.WriteObjects("		{ \r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				return Context.GetQuery<",  cls.ClassName , ">();\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
#line 47 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
}

#line 49 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("}");

        }



    }
}