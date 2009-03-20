using System.Collections.Generic;
using System.Linq;
using Kistl.App.Base;
using Kistl.App.Extensions;


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
#line 12 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
string classname = "Frozen" + module.ModuleName + "Repository";
    // TODO: remove ToList when IsFrozenObject correctly inherits across meta-data
	var frozenClasses = module.DataTypes.OfType<ObjectClass>().ToList().Where(cls => cls.IsFrozen()).OrderBy(c => c.ClassName);

#line 16 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("using System;\r\n");
this.WriteObjects("using System.Collections.Generic;\r\n");
this.WriteObjects("using System.Linq;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("	public static class ",  classname , "\r\n");
this.WriteObjects("	{\r\n");
this.WriteObjects("		\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
foreach(var cls in frozenClasses)
	{
		string implementationName = Implementation.ObjectClasses.Template.GetClassName(cls);

#line 33 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("		/// <summary>Frozen List of all ",  cls.ClassName , "</summary>\r\n");
this.WriteObjects("		/// ",  cls.Description , "\r\n");
this.WriteObjects("		public static IQueryable<",  cls.ClassName , "> ",  cls.TableName , "\r\n");
this.WriteObjects("		{ \r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				return ",  implementationName , ".DataStore.Values.AsQueryable().Cast<",  cls.ClassName , ">();\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
}

#line 46 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		internal static void CreateInstances()\r\n");
this.WriteObjects("		{\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
foreach(var cls in frozenClasses)
	{
		string implementationName = Implementation.ObjectClasses.Template.GetClassName(cls);

#line 54 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("				",  implementationName , ".CreateInstances();\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
}

#line 58 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		internal static void FillDataStore()\r\n");
this.WriteObjects("		{\r\n");
#line 64 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
foreach(var cls in frozenClasses)
	{
		string implementationName = Implementation.ObjectClasses.Template.GetClassName(cls);

#line 68 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("				",  implementationName , ".FillDataStore();\r\n");
#line 70 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
}

#line 72 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenRepository.cst"
this.WriteObjects("		}\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("}");

        }



    }
}