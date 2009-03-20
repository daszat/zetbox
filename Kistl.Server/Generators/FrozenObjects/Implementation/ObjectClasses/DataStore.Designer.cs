using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;


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
#line 15 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
InterfaceType t = cls.GetDescribedInterfaceType();
	
	// list of all instances of exactly this class
	var instanceList = ctx.GetQuery(t)
	    .ToList() // remove this, if possible
	    .Where(o => o.GetInterfaceType() == t)
	    .OrderBy(o => o.ID)
	    .ToList(); // cache sorted list, not expensive query

#line 24 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		internal ",  cls.BaseObjectClass == null ? "" : "new " , "static Dictionary<int, ",  Template.GetClassName(cls) , "> DataStore = new Dictionary<int, ",  Template.GetClassName(cls) , ">(",  instanceList.Count() , ");\r\n");
this.WriteObjects("		internal ",  cls.BaseObjectClass == null ? "" : "new " , "static void CreateInstances()\r\n");
this.WriteObjects("		{\r\n");
#line 31 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
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

#line 47 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			",  parent.Module.Namespace , ".",  Template.GetClassName(parent) , ".DataStore[",  obj.ID , "] = \r\n");
#line 49 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 51 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "] = new ",  classname , "(",  obj.ID , ");\r\n");
this.WriteObjects("\r\n");
#line 54 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 56 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		internal ",  cls.BaseObjectClass == null ? "" : "new " , "static void FillDataStore() {\r\n");
#line 60 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
foreach(var obj in instanceList) 
	{
		foreach(var baseCls in cls.GetObjectHierarchie()) {
			foreach(var prop in baseCls.Properties.Cast<Property>().OrderBy(p => p.PropertyName))
			{
				string value = GetPropertyValueAsCSharp(obj, prop);
				if (!String.IsNullOrEmpty(value))
				{

#line 69 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "].",  prop.PropertyName , " = ",  value , ";\r\n");
#line 71 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}
			}
		}

#line 75 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("			DataStore[",  obj.ID , "].Seal();\r\n");
#line 77 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
}

#line 79 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Implementation\ObjectClasses\DataStore.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("		}");

        }



    }
}