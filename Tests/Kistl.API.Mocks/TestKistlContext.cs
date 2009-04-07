using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Mocks
{
    public class TestKistlContext : IKistlContext
    {
        #region IKistlContext Members

        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            return obj;
        }

        public void Detach(IPersistenceObject obj)
        {

        }

        public void Delete(IPersistenceObject obj)
        {

        }

        public IQueryable<T> GetQuery<T>() where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IQueryable<IDataObject> GetQuery(InterfaceType ifType)
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

        public IList<T> FetchRelation<T>(int relationId, RelationEndRole role, IDataObject parent) where T : class, IRelationCollectionEntry
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> AttachedObjects
        {
            get { throw new NotImplementedException(); }
        }

        public int SubmitChanges()
        {
            throw new NotImplementedException();
        }

        public bool IsDisposed
        {
            get { throw new NotImplementedException(); }
        }

        public IDataObject Create(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T Create<T>() where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IRelationCollectionEntry CreateRelationCollectionEntry(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateRelationCollectionEntry<T>() where T : IRelationCollectionEntry
        {
            throw new NotImplementedException();
        }

        public IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry
        {
            throw new NotImplementedException();
        }


        public IStruct CreateStruct(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateStruct<T>() where T : IStruct
        {
            throw new NotImplementedException();
        }

        public IDataObject Find(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        public T Find<T>(int ID) where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IKistlContext GetReadonlyContext()
        {
            throw new NotImplementedException();
        }

        public bool IsReadonly { get { return false; } }


        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
