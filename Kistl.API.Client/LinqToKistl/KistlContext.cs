using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using System.Collections.ObjectModel;

namespace Kistl.API.Client
{
    /// <summary>
    /// Linq to Kistl Context Factory
    /// </summary>
    public static class KistlContext
    {
        /// <summary>
        /// Returns a new KistContext.
        /// </summary>
        /// <returns>A new KistlContext</returns>
        public static IKistlContext GetContext()
        {
            return new KistlContextImpl();
        }
    }

    /// <summary>
    /// Linq to Kistl Context Implementation
    /// </summary>
    internal class KistlContextImpl : IKistlContext, IDisposable
    {
        public KistlContextImpl()
        {
            KistlContextDebugger.Created(this);
        }

        private bool disposed = false;
        /// <summary>
        /// Dispose this Context.
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                if (!disposed)
                {
                    KistlContextDebugger.Disposed(this);
                }
                disposed = true;
            }
            // TODO: ??? Warum? Wieso? Weshalb?
            GC.SuppressFinalize(this);
        }

        public bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        /// <summary>
        /// Throws an Exception when this Context has been disposed
        /// </summary>
        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new KistlContextDisposedExeption();
            }
        }

        /// <summary>
        /// List of Objects (IDataObject and ICollectionEntry) in this Context.
        /// </summary>
        private List<IPersistenceObject> _objects = new List<IPersistenceObject>();

        /// <summary>
        /// Returns the Root Type of a given System.Type.
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns>Root Type of the given Type</returns>
        private Type GetRootType(Type t)
        {
            // TODO: Make this better - asking for BaseTypes is not elegant
            while (t != null && t.BaseType != typeof(BaseClientDataObject) && t.BaseType != typeof(BaseClientCollectionEntry))
            {
                t = t.BaseType;
            }

            return t;
        }

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (New Object) then null is returned.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        private IPersistenceObject IsObjectInContext(Type type, int ID)
        {
            if (ID <= Helper.INVALIDID) return null;
            Type rootType = GetRootType(type);
            return _objects.SingleOrDefault(o => GetRootType(o.GetType()) == rootType && o.ID == ID);
        }

        /// <summary>
        /// Returns a Query by System.Type
        /// </summary>
        /// <param name="type">System.Type</param>
        /// <returns>IQueryable</returns>
        public IQueryable<IDataObject> GetQuery(Type type)
        {
            CheckDisposed();
            return new KistlContextQuery<IDataObject>(this, type);
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            CheckDisposed();
            return new KistlContextQuery<T>(this, typeof(T));
        }

        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject
        {
            CheckDisposed();
            return this.GetListOf<T>(obj.GetType(), obj.ID, propertyName);
        }

        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given Type, ID and PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="type">Type of the Object which holds the BackReferenceProperty</param>
        /// <param name="ID">ID of the Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public List<T> GetListOf<T>(Type type, int ID, string propertyName) where T : IDataObject
        {
            CheckDisposed();
            KistlContextQuery<T> query = new KistlContextQuery<T>(this, type);
            return ((KistlContextProvider<T>)query.Provider).GetListOf(ID, propertyName);
        }

        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public T Create<T>() where T : Kistl.API.IDataObject, new()
        {
            CheckDisposed();
            T obj = new T();
            Attach(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new IDataObject by System.Type. Note - this Method is depricated!
        /// </summary>
        /// <param name="type">System.Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        public Kistl.API.IDataObject Create(Type type)
        {
            CheckDisposed();
            Kistl.API.IDataObject obj = (Kistl.API.IDataObject)Activator.CreateInstance(type);
            Attach(obj);
            return obj;
        }

        /// <summary>
        /// Attach an IPersistenceObject. This Method checks, if the Object is already in that Context. 
        /// If so, it returns the Object in that Context.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        /// <returns>The Object in already Context or obj if not</returns>
        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) throw new ArgumentNullException("obj");

            obj = IsObjectInContext(obj.GetType(), obj.ID) ?? obj;

            obj.AttachToContext(this);
            if (!_objects.Contains(obj))
            {
                _objects.Add(obj);
                obj.ObjectState = DataObjectState.Unmodified;
                KistlContextDebugger.Changed(this, _objects);
            }

            return obj;
        }

        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        public void Detach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) throw new ArgumentNullException("obj");
            if (!_objects.Contains(obj)) throw new InvalidOperationException("This Object does not belong to this context");

            _objects.Remove(obj);
            obj.DetachFromContext(this);
            KistlContextDebugger.Changed(this, _objects);
        }

        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IPersistenceObject</param>
        public void Delete(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) throw new ArgumentNullException("obj");
            if (obj.Context != this) throw new InvalidOperationException("The Object does not belong to the current Context");
            obj.ObjectState = DataObjectState.Deleted;
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public int SubmitChanges()
        {
            CheckDisposed();
            // TODO: Add a better Cache Refresh Strategie
            CacheController<Kistl.API.IDataObject>.Current.Clear();

            List<Kistl.API.IDataObject> objectsToDetach = new List<Kistl.API.IDataObject>();
            int objectsSubmittedCount = 0;

            // Use the ToList() operator! The CopyTo Methods _may_ attach new created Objects
            // like ICollectionEntries
            // TODO: Arthur: Da hats was. Das würde ja bedeuten, dass neue Objekte, die im Context
            // drinnen sind wieder durch neue ersetzt werden & damit rausfliegen!
            // Vermutlicher Grund: Der Context kann die Objekte nur nach der ID erkennen... hmmmm
            foreach (Kistl.API.IDataObject obj in _objects.OfType<IDataObject>().ToList())
            {
                if (obj.ObjectState == DataObjectState.Deleted)
                {
                    objectsSubmittedCount++;
                    // Object was deleted
                    Proxy.Current.SetObject(obj.GetType(), obj);
                    objectsToDetach.Add(obj);
                }
                else if (obj.ObjectState.In(DataObjectState.Modified, DataObjectState.New))
                {
                    objectsSubmittedCount++;
                    // Object is temporary and will bie copied
                    Kistl.API.IDataObject newobj = Proxy.Current.SetObject(obj.GetType(), obj);
                    newobj.CopyTo(obj);

                    // Set to unmodified
                    obj.ObjectState = DataObjectState.Unmodified;
                }

                CacheController<Kistl.API.IDataObject>.Current.Set(obj.GetType(), obj.ID, obj);
            }

            objectsToDetach.ForEach(obj => this.Detach(obj));

            return objectsSubmittedCount;
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// Note: This Method is depricated.
        /// </summary>
        /// <param name="type">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public IDataObject Find(Type type, int ID)
        {
            CheckDisposed();
            return GetQuery(type).Single(o => o.ID == ID);
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public T Find<T>(int ID)
            where T: IDataObject
        {
            CheckDisposed();
            return GetQuery<T>().Single(o => o.ID == ID);
        }
    }
}
