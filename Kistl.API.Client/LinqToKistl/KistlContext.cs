using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using System.Collections.ObjectModel;
using System.Collections;

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

        public bool IsReadonly { get { return false; } }

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
        private ContextCache _objects = new ContextCache();

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        private int _newIDCounter = Helper.INVALIDID;

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
            return _objects.Lookup(type, ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerDisplay("Count = {_objects.Count}")]
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
        public T Create<T>() where T : Kistl.API.IDataObject
        {
            return (T)Create(typeof(T));
        }

        /// <summary>
        /// Creates a new IDataObject by System.Type. Note - this Method is depricated!
        /// </summary>
        /// <param name="type">System.Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        public Kistl.API.IDataObject Create(Type type)
        {
            CheckDisposed();
            type = type.ToImplementationType();
            Kistl.API.IDataObject obj = (Kistl.API.IDataObject)Activator.CreateInstance(type);
            Attach(obj);
            OnObjectCreated(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new Struct by Type
        /// </summary>
        /// <param name="type">Type of the new IDataObject</param>
        /// <returns>A new Struct</returns>
        public IStruct CreateStruct(Type type)
        {
            CheckDisposed();
            type = type.ToImplementationType();
            Kistl.API.IStruct obj = (Kistl.API.IStruct)Activator.CreateInstance(type);
            return obj;
        }
        /// <summary>
        /// Creates a new Struct.
        /// </summary>
        /// <typeparam name="T">Type of the new Struct</typeparam>
        /// <returns>A new Struct</returns>
        public T CreateStruct<T>() where T : IStruct
        {
            return (T)CreateStruct(typeof(T));
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
                // TODO: security: check for overflow
                ((BaseClientPersistenceObject)obj).ID = --_newIDCounter;
            }
            else
            {
                // Check if Object is already in this Context
                var attachedObj = ContainsObject(obj.GetType(), obj.ID);
                if (attachedObj != null)
                {
                    // already attached, nothing to do
                    return attachedObj;
                }

                // Check ID <-> newIDCounter
                if (obj.ID < _newIDCounter)
                {
                    _newIDCounter = obj.ID;
                }
            }

            // Attach & set Objectstate to Unmodified
            _objects.Add(obj);
            ((BaseClientPersistenceObject)obj).ObjectState = DataObjectState.Unmodified;

            // Call Objects Attach Method to ensure, that every Child Object is also attached
            obj.AttachToContext(this);

            // update the debugger last 
            KistlContextDebugger.Changed(this);

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
        /// </summary>
        /// <param name="type">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public IDataObject Find(Type type, int ID)
        {
            // TODO: check "type" for being a IDataObject
            CheckDisposed();

            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            //return GetQuery(type).Single(o => o.ID == ID);

            return (IDataObject)this.GetType().FindGenericMethod("Find", new Type[] { type }, new Type[] { typeof(int) }).Invoke(this, new object[] { ID });
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
            where T : IDataObject
        {
            CheckDisposed();
            IPersistenceObject cacheHit = _objects.Lookup(typeof(T), ID);
            if (cacheHit != null)
                return (T)cacheHit;
            else
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

        public IKistlContext GetReadonlyContext()
        {
            // TODO: actually create a ThreadStatic read-only variant of this to allow for a common cache
            // return this;
            return Kistl.API.FrozenContext.Single;
        }
    }

    /// <summary>
    /// Store IPersistenceObjects ordered by (root-)Type and ID for fast access within the KistlContextImpl
    /// </summary>
    internal class ContextCache : ICollection<IPersistenceObject>
    {

        private IDictionary<Type, IDictionary<int, IPersistenceObject>> _objects = new Dictionary<Type, IDictionary<int, IPersistenceObject>>();

        /// <summary>
        /// Returns the root implementation Type of a given IPersistenceObject.
        /// This corresponds to the ID namespace of the object
        /// </summary>
        /// <param name="obj">IPersistenceObject to inspect</param>
        /// <returns>Root Type of the given Type</returns>
        private static Type GetRootImplType(IPersistenceObject obj)
        {
            return GetRootImplType(obj.GetType());
        }

        /// <summary>
        /// Returns the root implementation Type of a given System.Type.
        /// This corresponds to the ID namespace of the object
        /// </summary>
        /// <param name="t">Type to inspect</param>
        /// <returns>Root Type of the given Type</returns>
        private static Type GetRootImplType(Type t)
        {
            Type result = t.ToImplementationType();
            // TODO: Make this better - asking for BaseTypes is not elegant
            while (result != null && result.BaseType != typeof(BaseClientDataObject) && result.BaseType != typeof(BaseClientCollectionEntry))
            {
                result = result.BaseType;
            }

            return result;
        }

        public IPersistenceObject Lookup(Type t, int id)
        {
            Type rootT = GetRootImplType(t);

            if (!_objects.ContainsKey(rootT))
                return null;

            IDictionary<int, IPersistenceObject> typeList = _objects[rootT];
            if (!typeList.ContainsKey(id))
                return null;

            return typeList[id];
        }

        #region ICollection<IPersistenceObject> Members

        public void Add(IPersistenceObject item)
        {
            Type rootT = GetRootImplType(item);

            // create per-Type dictionary on-demand
            if (!_objects.ContainsKey(rootT))
                _objects[rootT] = new Dictionary<int, IPersistenceObject>();

            _objects[rootT][item.ID] = item;
        }

        public void Clear()
        {
            _objects.Clear();
        }

        public bool Contains(IPersistenceObject item)
        {
            Type rootT = GetRootImplType(item);
            return _objects.ContainsKey(rootT) && _objects[rootT].ContainsKey(item.ID);
        }

        public void CopyTo(IPersistenceObject[] array, int arrayIndex)
        {
            foreach (IPersistenceObject item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        public int Count
        {
            get { return _objects.Values.Sum(list => list.Count); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IPersistenceObject item)
        {
            if (Contains(item))
                // should always return true
                return _objects[GetRootImplType(item)].Remove(item.ID);
            else
                return false;
        }

        #endregion

        #region IEnumerable<IPersistenceObject> Members

        public IEnumerator<IPersistenceObject> GetEnumerator()
        {
            foreach (var typeList in _objects.Values)
            {
                foreach (IPersistenceObject obj in typeList.Values)
                {
                    yield return obj;
                }
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            // reuse strongly typed enumerator
            return GetEnumerator();
        }

        #endregion

    }

}
