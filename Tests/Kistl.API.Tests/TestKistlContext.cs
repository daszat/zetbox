using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
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

        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            throw new NotImplementedException();
        }

        public IQueryable<IDataObject> GetQuery(Type type)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOf<T>(Type type, int ID, string propertyName) where T : IDataObject
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject ContainsObject(Type type, int ID)
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

        public IDataObject Create(Type type)
        {
            throw new NotImplementedException();
        }

        public T Create<T>() where T : IDataObject, new()
        {
            throw new NotImplementedException();
        }

        public IDataObject Find(Type type, int ID)
        {
            throw new NotImplementedException();
        }

        public T Find<T>(int ID) where T : IDataObject
        {
            throw new NotImplementedException();
        }

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
