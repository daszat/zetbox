using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using System.Collections.ObjectModel;

namespace Kistl.API.Client
{
    public class KistlContext : IKistlContext, IDisposable
    {
        private List<BaseClientDataObject> _objects = new List<BaseClientDataObject>();

        public KistlContextQuery<BaseClientDataObject> GetQuery(ObjectType type)
        {
            return new KistlContextQuery<BaseClientDataObject>(this, type);
        }

        public KistlContextQuery<T> GetQuery<T>() where T : BaseClientDataObject
        {
            return new KistlContextQuery<T>(this, new ObjectType(typeof(T)));
        }

        public List<T> GetListOf<T>(ObjectType type, int ID, string propertyName)
        {
            KistlContextQuery<T> query = new KistlContextQuery<T>(this, type);
            return ((KistlContextProvider<T>)query.Provider).GetListOf(ID, propertyName);
        }

        public List<T> GetListOf<T>(BaseClientDataObject obj, string propertyName)
        {
            KistlContextQuery<T> query = new KistlContextQuery<T>(this, obj.Type);
            return ((KistlContextProvider<T>)query.Provider).GetListOf(obj.ID, propertyName);
        }

        public T Create<T>() where T : BaseClientDataObject, new()
        {
            T obj = new T();
            Attach(obj);
            return obj;
        }

        public BaseClientDataObject Create(Type type)
        {
            return Create(new ObjectType(type));
        }

        public BaseClientDataObject Create(ObjectType type)
        {
            BaseClientDataObject obj = (BaseClientDataObject)type.NewDataObject();
            Attach(obj);
            return obj;
        }

        public void Attach(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            BaseClientDataObject clientObj = (BaseClientDataObject)obj;
            if (_objects.Contains(clientObj)) throw new InvalidOperationException("Object is already attached to this context");
            _objects.Add(clientObj);
            clientObj.AttachToContext(this);
        }

        public void Dettach(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            BaseClientDataObject clientObj = (BaseClientDataObject)obj;
            if (!_objects.Contains(clientObj)) throw new InvalidOperationException("This Object does not belong to this context");
            _objects.Remove(clientObj);
            clientObj.DetachFromContext(this);
        }

        public void Delete(IDataObject obj)
        {
            obj.ObjectState = DataObjectState.Deleted;
        }

        public void Attach(ICollectionEntry e)
        {
            if (e == null) throw new ArgumentNullException("obj");
            BaseClientCollectionEntry entryObj = (BaseClientCollectionEntry)e;
            entryObj.AttachToContext(this);
        }

        public void Dettach(ICollectionEntry e)
        {
            if (e == null) throw new ArgumentNullException("obj");
            BaseClientCollectionEntry entryObj = (BaseClientCollectionEntry)e;
            entryObj.DetachFromContext(this);
        }

        public void Delete(ICollectionEntry e)
        {
            throw new NotSupportedException();
        }

        public int SubmitChanges()
        {
            // TODO: Add a better Cache Refresh Strategie
            CacheController<BaseClientDataObject>.Current.Clear();

            List<BaseClientDataObject> objectsToDetach = new List<BaseClientDataObject>();
            int objectsSubmittedCount = 0;
            foreach (BaseClientDataObject obj in _objects)
            {
                if (obj.ObjectState == DataObjectState.Deleted)
                {
                    objectsSubmittedCount++;
                    // Do not attach to context -> first Param is null
                    // Object was deleted, even remove that Object
                    Proxy.Current.SetObject(null, obj.Type, obj);
                    objectsToDetach.Add(obj);
                }
                else if (obj.ObjectState.In(DataObjectState.Modified, DataObjectState.New))
                {
                    objectsSubmittedCount++;
                    // Do not attach to context -> first Param is null
                    // Object is temporary and will bie copied
                    BaseClientDataObject newobj = Proxy.Current.SetObject(null, obj.Type, obj);
                    newobj.CopyTo(obj);

                    // Set to unmodified
                    obj.ObjectState = DataObjectState.Unmodified;
                }

                CacheController<BaseClientDataObject>.Current.Set(obj.Type, obj.ID, obj);
            }

            objectsToDetach.ForEach(obj => this.Dettach(obj));

            return objectsSubmittedCount;
        }

        public void DeleteObject(BaseClientDataObject obj)
        {
            if (obj.Context != this) throw new ArgumentException("The Object does not belong to the current Context", "obj");
            obj.ObjectState = DataObjectState.Deleted;
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
