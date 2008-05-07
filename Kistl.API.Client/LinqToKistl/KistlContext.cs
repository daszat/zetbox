using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using System.Collections.ObjectModel;

namespace Kistl.API.Client
{
    public static class KistlContext
    {
        public static IKistlContext GetContext()
        {
            return new KistlContextImpl();
        }
    }

    internal class KistlContextImpl : IKistlContext, IDisposable
    {
        private List<Kistl.API.IDataObject> _objects = new List<Kistl.API.IDataObject>();

        public IQueryable<IDataObject> GetQuery(ObjectType type)
        {
            return new KistlContextQuery<IDataObject>(this, type);
        }

        public IQueryable<T> GetTable<T>() where T : IDataObject
        {
            return GetQuery<T>();
        }

        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            return new KistlContextQuery<T>(this, new ObjectType(typeof(T)));
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName)
        {
            return this.GetListOf<T>(obj.Type, obj.ID, propertyName);
        }

        public List<T> GetListOf<T>(ObjectType type, int ID, string propertyName)
        {
            KistlContextQuery<T> query = new KistlContextQuery<T>(this, type);
            return ((KistlContextProvider<T>)query.Provider).GetListOf(ID, propertyName);
        }

        public T Create<T>() where T : Kistl.API.IDataObject, new()
        {
            T obj = new T();
            Attach(obj);
            return obj;
        }

        public Kistl.API.IDataObject Create(Type type)
        {
            return Create(new ObjectType(type));
        }

        public Kistl.API.IDataObject Create(ObjectType type)
        {
            Kistl.API.IDataObject obj = type.NewDataObject();
            Attach(obj);
            return obj;
        }

        public void Attach(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (_objects.Contains(obj)) throw new InvalidOperationException("Object is already attached to this context");
            _objects.Add(obj);
            obj.AttachToContext(this);
        }

        public void Detach(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (!_objects.Contains(obj)) throw new InvalidOperationException("This Object does not belong to this context");
            _objects.Remove(obj);
            obj.DetachFromContext(this);
        }

        public void Delete(IDataObject obj)
        {
            if (obj.Context != this) throw new ArgumentException("The Object does not belong to the current Context", "obj");
            obj.ObjectState = DataObjectState.Deleted;
        }

        public void Attach(ICollectionEntry e)
        {
            if (e == null) throw new ArgumentNullException("obj");
            BaseClientCollectionEntry entryObj = (BaseClientCollectionEntry)e;
            entryObj.AttachToContext(this);
        }

        public void Detach(ICollectionEntry e)
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
            CacheController<Kistl.API.IDataObject>.Current.Clear();

            List<Kistl.API.IDataObject> objectsToDetach = new List<Kistl.API.IDataObject>();
            int objectsSubmittedCount = 0;
            foreach (Kistl.API.IDataObject obj in _objects)
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
                    Kistl.API.IDataObject newobj = Proxy.Current.SetObject(null, obj.Type, obj);
                    newobj.CopyTo(obj);

                    // Set to unmodified
                    obj.ObjectState = DataObjectState.Unmodified;
                }

                CacheController<Kistl.API.IDataObject>.Current.Set(obj.Type, obj.ID, obj);
            }

            objectsToDetach.ForEach(obj => this.Detach(obj));

            return objectsSubmittedCount;
        }

        // TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        // This could be moved to a common abstract IKistlContextBase
        public IDataObject Find(ObjectType type, int ID)
        {
            return GetQuery(type).Single(o => o.ID == ID);
        }

        public T Find<T>(int ID)
            where T: IDataObject
        {
            return GetQuery<T>().Single(o => o.ID == ID);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
