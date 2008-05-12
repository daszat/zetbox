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
        private List<IPersistenceObject> _objects = new List<IPersistenceObject>();

        private Type GetRootType(ObjectType t)
        {
            Type type = Type.GetType(t.FullNameDataObject);
            return GetRootType(type);
        }

        private Type GetRootType(Type t)
        {
            while (t != null && t.BaseType != typeof(BaseClientDataObject) && t.BaseType != typeof(BaseClientCollectionEntry))
            {
                t = t.BaseType;
            }

            return t;
        }

        public IPersistenceObject IsObjectInContext(Type type, int ID)
        {
            if (ID == Helper.INVALIDID) return null;
            Type rootType = GetRootType(type);
            return _objects.SingleOrDefault(o => GetRootType(o.GetType()) == rootType && o.ID == ID);
        }

        public IQueryable<IDataObject> GetQuery(ObjectType type)
        {
            return new KistlContextQuery<IDataObject>(this, type);
        }

        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            return new KistlContextQuery<T>(this, new ObjectType(typeof(T)));
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject
        {
            return this.GetListOf<T>(obj.Type, obj.ID, propertyName);
        }

        public List<T> GetListOf<T>(ObjectType type, int ID, string propertyName) where T : IDataObject
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

        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            /*if (obj.ObjectState != DataObjectState.New && IsObjectInContext(obj.GetType(), obj.ID) != null && !_objects.Contains(obj))
            {
                throw new InvalidOperationException("Try to add same Object twice but with different references!");
            }
             * */
            obj = IsObjectInContext(obj.GetType(), obj.ID) ?? obj;

            obj.AttachToContext(this);
            if (!_objects.Contains(obj))
            {
                _objects.Add(obj);
                obj.ObjectState = DataObjectState.Unmodified;
            }

            return obj;
        }

        public void Detach(IPersistenceObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (!_objects.Contains(obj)) throw new InvalidOperationException("This Object does not belong to this context");

            _objects.Remove(obj);
            obj.DetachFromContext(this);
        }

        public void Delete(IPersistenceObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (obj.Context != this) throw new InvalidOperationException("The Object does not belong to the current Context");
            obj.ObjectState = DataObjectState.Deleted;
        }

        public int SubmitChanges()
        {
            // TODO: Add a better Cache Refresh Strategie
            CacheController<Kistl.API.IDataObject>.Current.Clear();

            List<Kistl.API.IDataObject> objectsToDetach = new List<Kistl.API.IDataObject>();
            int objectsSubmittedCount = 0;
            foreach (Kistl.API.IDataObject obj in _objects.OfType<IDataObject>())
            {
                if (obj.ObjectState == DataObjectState.Deleted)
                {
                    objectsSubmittedCount++;
                    // Object was deleted
                    Proxy.Current.SetObject(obj.Type, obj);
                    objectsToDetach.Add(obj);
                }
                else if (obj.ObjectState.In(DataObjectState.Modified, DataObjectState.New))
                {
                    objectsSubmittedCount++;
                    // Object is temporary and will bie copied
                    Kistl.API.IDataObject newobj = Proxy.Current.SetObject(obj.Type, obj);
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
