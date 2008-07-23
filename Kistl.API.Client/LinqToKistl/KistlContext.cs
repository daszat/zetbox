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
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        private int _newIDCounter = Helper.INVALIDID;

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
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        public IPersistenceObject ContainsObject(Type type, int ID)
        {
            if (ID == Helper.INVALIDID) throw new ArgumentException("ID cannot be invalid", "ID");
            Type rootType = GetRootType(type);
            return _objects.SingleOrDefault(o => GetRootType(o.GetType()) == rootType && o.ID == ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPersistenceObject> AttachedObjects
        {
            get
            {
                return _objects;
            }
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
            return ((KistlContextProvider)query.Provider).GetListOf(ID, propertyName).Cast<T>().ToList();
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
            OnObjectCreated(obj);
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
            OnObjectCreated(obj);
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

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                obj.ID = --_newIDCounter;
            }
            else
            {
                // Check if Object is already in this Context
                obj = ContainsObject(obj.GetType(), obj.ID) ?? obj;

                // Check ID <-> newIDCounter
                if (obj.ID < _newIDCounter)
                {
                    _newIDCounter = obj.ID;
                }
            }

            // Attach & set Objectstate to Unmodified
            if (!_objects.Contains(obj))
            {
                _objects.Add(obj);
                ((BaseClientPersistenceObject)obj).ObjectState = DataObjectState.Unmodified;
                KistlContextDebugger.Changed(this);
            }

            // Call Objects Attach Method to ensure, that every Child Object is also attached
            obj.AttachToContext(this);

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
            KistlContextDebugger.Changed(this);
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
            ((BaseClientPersistenceObject)obj).ObjectState = DataObjectState.Deleted;
            OnObjectDeleted(obj);
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public int SubmitChanges()
        {
            CheckDisposed();
            // TODO: Add a better Cache Refresh Strategie
            // CacheController<Kistl.API.IDataObject>.Current.Clear();

            List<Kistl.API.IDataObject> objectsToSubmit = new List<Kistl.API.IDataObject>();
            List<Kistl.API.IDataObject> objectsToAdd = new List<Kistl.API.IDataObject>();
            List<Kistl.API.IDataObject> objectsToDetach = new List<Kistl.API.IDataObject>();

            // Added Objects
            foreach (Kistl.API.IDataObject obj in _objects.OfType<IDataObject>()
                .Where(o => o.ObjectState == DataObjectState.New))
            {
                objectsToSubmit.Add(obj);
                objectsToAdd.Add(obj);
            }
            // Changed objects
            foreach (Kistl.API.IDataObject obj in _objects.OfType<IDataObject>()
                .Where(o => o.ObjectState == DataObjectState.Modified))
            {
                objectsToSubmit.Add(obj);
            }
            // Deleted Objects
            foreach (Kistl.API.IDataObject obj in _objects.OfType<IDataObject>()
                .Where(o => o.ObjectState == DataObjectState.Deleted))
            {
                // Submit only persisted objects
                if (Helper.IsPersistedObject(obj))
                {
                    objectsToSubmit.Add(obj);
                }
                objectsToDetach.Add(obj);
            }

            // Submit to server
            var newObjects = Proxy.Current.SetObjects(objectsToSubmit);

            // Apply Changes
            int counter = 0;
            List<Kistl.API.IDataObject> changedObjects = new List<Kistl.API.IDataObject>();
            foreach (IDataObject newobj in newObjects)
            {
                IDataObject obj;

                if (counter < objectsToAdd.Count)
                {
                    obj = objectsToAdd[counter++];
                }
                else
                {
                    obj = (IDataObject)this.ContainsObject(newobj.GetType(), newobj.ID) ?? newobj;
                }

                ((BaseClientDataObject)obj).RecordNotifications();
                if (obj != newobj)
                {
                    ((BaseClientDataObject)newobj).ApplyChanges(obj);
                }

                // Set to unmodified
                ((BaseClientPersistenceObject)obj).ObjectState = DataObjectState.Unmodified;

                changedObjects.Add(obj);
            }

            objectsToDetach.ForEach(obj => this.Detach(obj));
            changedObjects.ForEach(obj => this.Attach(obj));

            changedObjects.ForEach<BaseClientDataObject>(obj => obj.PlaybackNotifications());

            return objectsToSubmit.Count;
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

        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        protected virtual void OnObjectCreated(IPersistenceObject obj)
        {
            if (ObjectCreated != null)
            {
                ObjectCreated(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        protected virtual void OnObjectDeleted(IPersistenceObject obj)
        {
            if (ObjectDeleted != null)
            {
                ObjectDeleted(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

    }
}
