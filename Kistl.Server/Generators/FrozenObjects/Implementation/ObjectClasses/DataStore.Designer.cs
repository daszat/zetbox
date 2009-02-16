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
	
	var dataType = cls.Module.Namespace + "." + cls.ClassName;
		
	// list of all instances of exactly this class
	var instanceList = ctx.GetQuery(t)
	    .ToList()
	    .Where(o => o.GetInterfaceType().FullName == dataType);

#line 26 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		internal ",  cls.BaseObjectClass == null ? "" : "new " , "static Dictionary<int, ",  Template.GetClassName(cls) , "> DataStore = new Dictionary<int, ",  Template.GetClassName(cls) , ">(",  instanceList.Count() , ");\r\n");
this.WriteObjects("		static ",  Template.GetClassName(cls) , "()\r\n");
this.WriteObjects("		{\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
string classname = Template.GetClassName(cls);
	foreach(var obj in instanceList) 
	{
		// store inherited objects also in base class' DataStore to short-circuit polymorphic queries.
		var baseCls = cls.BaseObjectClass;
		var parents = new List<ObjectClass>();
		while (baseCls != null)
		{
			parents.Add(baseCls);
			baseCls = baseCls.BaseObjectClass;
		}
		// to get "right" type order, the root class has to be the "left-most", i.e. first, assigned one
		parents.Reverse();
		foreach(var parent in parents)
		{

#line 49 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			",  parent.Module.Namespace , ".",  Template.GetClassName(parent) , ".DataStore[",  obj.ID , "] = \r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 53 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "] = new ",  classname , "(null, ",  obj.ID , ");\r\n");
this.WriteObjects("\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 58 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		internal ",  cls.BaseObjectClass == null ? "" : "new " , "static void FillDataStore() {\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
foreach(var obj in instanceList) 
	{
		foreach(var baseCls in cls.GetObjectHierarchie()) {
			foreach(var prop in baseCls.Properties.Cast<Property>())
			{
				string value = GetPropertyValueAsCSharp(obj, prop);
				if (!String.IsNullOrEmpty(value))
				{

#line 71 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "].",  prop.PropertyName , " = ",  value , ";\r\n");
#line 73 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}
			}
		}

#line 77 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "].Seal();\r\n");
#line 79 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 81 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("		}");

        }



    }
}