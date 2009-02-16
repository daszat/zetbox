using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.App.Base;


namespace Kistl.Server.Generators.FrozenObjects.Repositories
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst")]
    public partial class FrozenContextImplementation : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.API.IKistlContext ctx;
		protected List<Module> modulesWithFrozenClasses;


        public FrozenContextImplementation(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, List<Module> modulesWithFrozenClasses)
            : base(_host)
        {
			this.ctx = ctx;
			this.modulesWithFrozenClasses = modulesWithFrozenClasses;

        }
        
        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("using System;\r\n");
this.WriteObjects("using System.Collections.Generic;\r\n");
this.WriteObjects("using System.Linq;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace Kistl.App\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("	public class FrozenContextImplementation : IKistlContext\r\n");
this.WriteObjects("	{\r\n");
this.WriteObjects("		public FrozenContextImplementation()\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		static FrozenContextImplementation()\r\n");
this.WriteObjects("		{\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{

#line 32 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("				",  module.Namespace , ".Frozen",  module.ModuleName , "Repository.CreateInstances();\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}

#line 36 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{

#line 41 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("				",  module.Namespace , ".Frozen",  module.ModuleName , "Repository.FillDataStore();\r\n");
#line 43 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}

#line 45 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		IPersistenceObject IKistlContext.Attach(IPersistenceObject obj)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		void IKistlContext.Detach(IPersistenceObject obj)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		void IKistlContext.Delete(IPersistenceObject obj)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		public IQueryable<T> GetQuery<T>()\r\n");
this.WriteObjects("			where T : IDataObject\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			return GetQuery(typeof(T)).Cast<T>();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		public IQueryable<IDataObject> GetQuery(Type t)\r\n");
this.WriteObjects("		{\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{
		foreach(var frozenCls in module.DataTypes.OfType<ObjectClass>().Where(cls => cls.IsFrozenObject || cls.GetRootClass().IsFrozenObject))
		{

#line 77 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			if (t == typeof(",  frozenCls.Module.Namespace , ".",  frozenCls.ClassName , "))\r\n");
this.WriteObjects("				return ",  frozenCls.Module.Namespace , ".",  Implementation.ObjectClasses.Template.GetClassName(frozenCls) , ".DataStore.Values.AsQueryable().Cast<IDataObject>();\r\n");
#line 80 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}
	}

#line 83 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		List<T> IKistlContext.GetListOf<T>(IDataObject obj, string propertyName)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("        List<T> IKistlContext.GetListOf<T>(Type type, int ID, string propertyName)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        IPersistenceObject IKistlContext.ContainsObject(Type type, int ID)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			return Find(type, ID);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public IEnumerable<IPersistenceObject> AttachedObjects\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				return new List<IPersistenceObject>(0)\r\n");
#line 107 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{
		foreach(var frozenCls in module.DataTypes.OfType<ObjectClass>().Where(cls => cls.IsFrozenObject || cls.GetRootClass().IsFrozenObject))
		{

#line 112 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("					.Concat(GetQuery<",  frozenCls.Module.Namespace , ".",  frozenCls.ClassName , ">().Cast<IPersistenceObject>())\r\n");
#line 114 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}
	}

#line 116 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects(";\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        int IKistlContext.SubmitChanges() { throw new NotImplementedException(); }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        bool IKistlContext.IsDisposed { get { return false; } }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        bool IKistlContext.IsReadonly { get { return true; } }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        IDataObject IKistlContext.Create(Type type) { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("        T IKistlContext.Create<T>() { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        IStruct IKistlContext.CreateStruct(Type type) { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("        T IKistlContext.CreateStruct<T>() { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public IDataObject Find(Type t, int ID)\r\n");
this.WriteObjects("		{\r\n");
#line 135 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{
		foreach(var frozenCls in module.DataTypes.OfType<ObjectClass>().Where(cls => cls.IsFrozenObject || cls.GetRootClass().IsFrozenObject))
		{

#line 140 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			if (t == typeof(",  frozenCls.Module.Namespace , ".",  frozenCls.ClassName , "))\r\n");
this.WriteObjects("				return ",  frozenCls.Module.Namespace , ".",  Implementation.ObjectClasses.Template.GetClassName(frozenCls) , ".DataStore[ID];\r\n");
#line 143 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}
	}

#line 146 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public T Find<T>(int ID)\r\n");
this.WriteObjects("			where T : IDataObject\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			return (T)Find(typeof(T), ID);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        IKistlContext IKistlContext.GetReadonlyContext() { throw new NotImplementedException(); }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectCreated\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			add { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("			remove { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Is fired when an object is deleted in this Context.\r\n");
this.WriteObjects("        /// The delted object is passed as Data.\r\n");
this.WriteObjects("        /// </summary>\r\n");
this.WriteObjects("        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectDeleted\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			add { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("			remove { throw new ReadOnlyObjectException(); }\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		public virtual void Dispose() {}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("	}\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("}");

        }



    }
}