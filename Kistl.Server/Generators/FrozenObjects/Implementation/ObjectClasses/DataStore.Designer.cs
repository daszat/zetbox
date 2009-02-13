using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
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
#line 14 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
Type t = Type.GetType(cls.Module.Namespace + "." + cls.ClassName + ", Kistl.Objects", true);
	Console.WriteLine("Generating " + t.AssemblyQualifiedName);
	
	var testList = ctx.GetQuery(typeof(ObjectClass)).ToList();
	
	// list of all instances of exactly this class
	// var instanceList = new List<ObjectClass>();
	var instanceList = ctx.GetQuery(t)
	    .ToList()
	    .Where(o => o.GetObjectClass(ctx) == cls);
	/*
	*/

#line 27 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		internal static Dictionary<int, ",  cls.ClassName , "> DataStore = new Dictionary<int, ",  cls.ClassName , ">(",  instanceList.Count() , ");\r\n");
this.WriteObjects("		static ",  cls.ClassName , "",  Kistl.API.Helper.ImplementationSuffix , "Frozen()\r\n");
this.WriteObjects("		{\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
string classname = Template.GetClassName(cls);
	foreach(var obj in instanceList) 
	{

#line 38 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "] = new ",  classname , "(null, ",  obj.ID , ");\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 42 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		internal static void FillDataStore() {\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
foreach(var obj in instanceList) 
	{
		foreach(var prop in cls.Properties)
		{

#line 51 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "].",  prop.PropertyName , " = ",  "null"/* obj.GetPropertyValue<int>(prop.PropertyName).ToCSharpValue() */ , ";\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}
	}

#line 56 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("		}");

        }



    }
}