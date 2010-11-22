
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutofacContrib.NHibernate.Bytecode;
    //using global::NHibernate.Bytecode;
    using Kistl.API;
    using Kistl.API.Server;

    public class NHibernateContext
        : BaseMemoryContext, IKistlServerContext
    {
        private static readonly object _initLock = new object();
        private bool initialized = false;

        public NHibernateContext(InterfaceType.Factory iftFactory) //, IBytecodeProvider bytecodeProvider)
            : base(iftFactory)
        {
            // TODO: setup AutoFac as IoC container in NHibernate
            //if (!initialized) // avoid lock if initialized
            //{
            //    lock (_initLock)
            //    {
            //        // recheck after lock succeeded
            //        if (!initialized)
            //        {
            //            var containerProvider = new ContainerProvider(builder.Build());

            //        global::NHibernate.   Environment.BytecodeProvider = new AutofacBytecodeProvider(
            //                containerProvider.ApplicationContainer, new ProxyFactoryFactory());

            //        }
            //    }
            //}
        }

        public override int SubmitChanges()
        {
            throw new NotImplementedException();
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public override InterfaceType GetInterfaceType(string typeName)
        {
            throw new NotImplementedException();
        }

        public override ImplementationType GetImplementationType(Type t)
        {
            throw new NotImplementedException();
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            throw new NotImplementedException();
        }

        public int SubmitRestore()
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOf<T>(InterfaceType ifType, int ID, string propertyName) where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationEntry
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetStream(int ID)
        {
            throw new NotImplementedException();
        }

        public System.IO.FileInfo GetFileInfo(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
