using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Mocks
{
    public class TestKistlContext : IKistlContext
    {
        protected ITypeTransformations typeTrans;

        public TestKistlContext(ITypeTransformations typeTrans)
        {
            this.typeTrans = typeTrans;
        }

        #region IKistlContext Members

        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            obj.AttachToContext(this);
            return obj;
        }

        public void Detach(IPersistenceObject obj)
        {
            obj.DetachFromContext(this);
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

        public IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
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

        public IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject parent) where T : class, IRelationCollectionEntry
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

        public IPersistenceObject CreateUnattached(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateUnattached<T>() where T : class, IPersistenceObject
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


        public ICompoundObject CreateCompoundObject(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateCompoundObject<T>() where T : ICompoundObject
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

        public T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public T Find<T>(int ID) where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public bool IsReadonly { get { return false; } }

        public void NotifyObjectCreated()
        {
            if (ObjectCreated != null) ObjectCreated(this, new GenericEventArgs<IPersistenceObject>());
        }

        public void NotifyObjectDeleted()
        {
            if (ObjectDeleted != null) ObjectDeleted(this, new GenericEventArgs<IPersistenceObject>());
        }

        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region IKistlContext Members


        public int CreateBlob(System.IO.Stream s, string filename, string mimetype)
        {
            throw new NotImplementedException();
        }

        public int CreateBlob(System.IO.FileInfo fi, string mimetype)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IReadOnlyKistlContext Members


        public System.IO.Stream GetStream(int ID)
        {
            throw new NotImplementedException();
        }

        public System.IO.FileInfo GetFileInfo(int ID)
        {
            throw new NotImplementedException();
        }

        public InterfaceType GetInterfaceType(Type t)
        {
            return typeTrans.AsInterfaceType(t);
        }

        public InterfaceType GetInterfaceType(string typeName)
        {
            return typeTrans.AsInterfaceType(typeName);
        }

        public InterfaceType GetInterfaceType(IPersistenceObject obj)
        {
            Type ifType;
            if (obj is TestCollectionEntry)
            {
                ifType = ((TestCollectionEntry)obj).GetImplementedInterface();
            }
            else if (obj is TestDataObject__Implementation__)
            {
                ifType = ((TestDataObject__Implementation__)obj).GetImplementedInterface();
            }
            else
            {
                throw new NotImplementedException("unable to get the implemented interfacetype of the given object");
            }
            return typeTrans.AsInterfaceType(ifType);
        }

        public ImplementationType GetImplementationType(Type t)
        {
            return typeTrans.AsImplementationType(t);
        }

        public ImplementationType ToImplementationType(InterfaceType t)
        {
            return GetImplementationType(Type.GetType(t.Type.Name + Kistl.API.Helper.ImplementationSuffix + "," + typeof(TestKistlContext).Assembly.FullName, true));
        }
        #endregion
    }
}
