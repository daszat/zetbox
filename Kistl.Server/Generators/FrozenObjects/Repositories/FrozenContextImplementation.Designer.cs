using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.App.Base;
using Kistl.App.Extensions;


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
#line 12 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
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
#line 30 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{

#line 33 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("				",  module.Namespace , ".Frozen",  module.ModuleName , "Repository.CreateInstances();\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}

#line 37 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{

#line 42 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("				",  module.Namespace , ".Frozen",  module.ModuleName , "Repository.FillDataStore();\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}

#line 46 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
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
this.WriteObjects("			throw new ReadOnlyContextException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		public IQueryable<T> GetQuery<T>()\r\n");
this.WriteObjects("			where T : class, IDataObject\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			return GetQuery(new InterfaceType(typeof(T))).Cast<T>();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		public IQueryable<IDataObject> GetQuery(InterfaceType ifType)\r\n");
this.WriteObjects("		{\r\n");
#line 73 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{
	    // TODO: remove ToList when IsFrozenObject correctly inherits across meta-data
		foreach(var frozenCls in module.DataTypes.OfType<ObjectClass>().ToList().Where(cls => cls.IsFrozen()).OrderBy(c => c.ClassName))
		{

#line 79 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			if (ifType == typeof(",  frozenCls.Module.Namespace , ".",  frozenCls.ClassName , "))\r\n");
this.WriteObjects("				return ",  frozenCls.Module.Namespace , ".",  Implementation.ObjectClasses.Template.GetClassName(frozenCls) , ".DataStore.Values.AsQueryable().Cast<IDataObject>();\r\n");
#line 82 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}
	}

#line 85 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		List<T> IKistlContext.GetListOf<T>(IDataObject obj, string propertyName)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("        List<T> IKistlContext.GetListOf<T>(InterfaceType ifType, int ID, string propertyName)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		IList<T> IKistlContext.FetchRelation<T>(int relId, RelationEndRole role, IDataObject parent)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        IPersistenceObject IKistlContext.ContainsObject(InterfaceType type, int ID)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			return Find(type, ID);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public IEnumerable<IPersistenceObject> AttachedObjects\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				return new List<IPersistenceObject>(0)\r\n");
#line 114 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{
		// TODO: remove ToList when IsFrozenObject correctly inherits across meta-data
        foreach(var frozenCls in module.DataTypes.OfType<ObjectClass>().ToList().Where(cls => cls.IsFrozen()).OrderBy(c => c.ClassName))
		{

#line 120 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("					.Concat(GetQuery<",  frozenCls.Module.Namespace , ".",  frozenCls.ClassName , ">().Cast<IPersistenceObject>())\r\n");
#line 122 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}
	}

#line 124 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
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
this.WriteObjects("        IDataObject IKistlContext.Create(InterfaceType ifType) { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("        T IKistlContext.Create<T>() { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        IRelationCollectionEntry IKistlContext.CreateRelationCollectionEntry(InterfaceType ifType) { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("        T IKistlContext.CreateRelationCollectionEntry<T>() { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        IValueCollectionEntry IKistlContext.CreateValueCollectionEntry(InterfaceType ifType) { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("        T IKistlContext.CreateValueCollectionEntry<T>() { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("        IStruct IKistlContext.CreateStruct(InterfaceType ifType) { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("        T IKistlContext.CreateStruct<T>() { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public IDataObject Find(InterfaceType ifType, int ID)\r\n");
this.WriteObjects("		{\r\n");
#line 149 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
foreach(var module in modulesWithFrozenClasses)
	{
		// TODO: remove ToList when IsFrozenObject correctly inherits across meta-data
		foreach(var frozenCls in module.DataTypes.OfType<ObjectClass>().ToList().Where(cls => cls.IsFrozen()).OrderBy(c => c.ClassName))
		{

#line 155 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			if (ifType == typeof(",  frozenCls.Module.Namespace , ".",  frozenCls.ClassName , "))\r\n");
this.WriteObjects("				return ",  frozenCls.Module.Namespace , ".",  Implementation.ObjectClasses.Template.GetClassName(frozenCls) , ".DataStore[ID];\r\n");
#line 158 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
}
	}

#line 161 "P:\Kistl\Kistl.Server\Generators\FrozenObjects\Repositories\FrozenContextImplementation.cst"
this.WriteObjects("			throw new NotImplementedException();\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public T Find<T>(int ID)\r\n");
this.WriteObjects("			where T : class, IDataObject\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			return (T)Find(new InterfaceType(typeof(T)), ID);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            throw new NotImplementedException();\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            throw new NotImplementedException();\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        IKistlContext IKistlContext.GetReadonlyContext() { throw new NotImplementedException(); }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectCreated\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			add { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("			remove { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>\r\n");
this.WriteObjects("        /// Is fired when an object is deleted in this Context.\r\n");
this.WriteObjects("        /// The delted object is passed as Data.\r\n");
this.WriteObjects("        /// </summary>\r\n");
this.WriteObjects("        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectDeleted\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			add { throw new ReadOnlyContextException(); }\r\n");
this.WriteObjects("			remove { throw new ReadOnlyContextException(); }\r\n");
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