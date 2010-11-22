
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;
    using global::NHibernate.Linq;

    public class NHibernateContext
        : BaseMemoryContext, IKistlServerContext
    {
        private readonly global::NHibernate.ISession _nhSession;

        public NHibernateContext(InterfaceType.Factory iftFactory, global::NHibernate.ISession nhSession)
            : base(iftFactory)
        {
            _nhSession = nhSession;
        }

        public override IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);

            var mi = this.GetType().FindGenericMethod(
                "PrepareQueryable",
                new[] { ToImplementationType(ifType).Type },
                new Type[0]);

            return (IQueryable<IPersistenceObject>)mi.Invoke(this, new object[0]);
        }

        private IQueryable<IPersistenceObject> PrepareQueryable<T>()
        {
            return _nhSession.Query<T>().Cast<IPersistenceObject>();
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
