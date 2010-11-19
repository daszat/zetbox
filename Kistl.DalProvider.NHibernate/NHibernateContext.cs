
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;

    public class NHibernateContext
        : BaseMemoryContext, IKistlServerContext
    {
        public NHibernateContext(InterfaceType.Factory iftFactory)
            : base(iftFactory)
        {
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
