using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.App.Base;


namespace Kistl.Server.Generators.FrozenObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst")]
    public partial class DataStore : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.API.IKistlContext ctx;
		protected ObjectClass cls;


        public DataStore(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
// TODO: find a correct incantation here
	// list of all instances of exactly this class
	var instanceList = new List<ObjectClass>(); //ctx.GetQuery(cls.GetDataType()).ToList().Where(o => o.GetObjectClass(ctx) == cls);

#line 17 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("		internal Dictionary<int, ",  cls.ClassName , "> DataStore = new Dictionary<int, ",  cls.ClassName , ">(",  instanceList.Count() , ");\r\n");
this.WriteObjects("		static ",  cls.ClassName , "",  Kistl.API.Helper.ImplementationSuffix , "()\r\n");
this.WriteObjects("		{\r\n");
#line 22 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
foreach(var obj in instanceList) 
	{

#line 25 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "] = new ",  cls.ClassName , "(null, ",  obj.ID , ");\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 29 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("		}");

        }



    }
}