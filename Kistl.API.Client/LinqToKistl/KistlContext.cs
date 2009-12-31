using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Kistl.API.Utils;

//using Kistl.API.Client.LinqToKistl;

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
    internal class KistlContextImpl : IDebuggingKistlContext, IDisposable
    {
        private readonly static object _lock = new object();

        public KistlContextImpl()
        {
            CreatedAt = new StackTrace(true);
            KistlContextDebuggerSingleton.Created(this);
            //_relationshipManager = new RelationshipManager(this);
        }

        //private LinqToKistl.RelationshipManager _relationshipManager;


        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification="Clarifies intent of variable")]
        private bool disposed = false;
        /// <summary>
        /// Dispose this Context.
        /// </summary>
        public void Dispose()
        {
            lock (_lock)
            {
                if (!disposed)
                {
                    DisposedAt = new StackTrace(true);
                    KistlContextDebuggerSingleton.Disposed(this);
                }
                disposed = true;
            }
            // TODO: use correct Dispose implementation pattern
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
                throw new KistlContextDisposedException();
            }
        }

        /// <summary>
        /// List of Objects (IDataObject and ICollectionEntry) in this Context.
        /// </summary>
        private ContextCache _objects = new ContextCache();

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        public IPersistenceObject ContainsObject(InterfaceType type, int ID)
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
        /// <param name="ifType">System.Type</param>
        /// <returns>IQueryable</returns>
        public IQueryable<IDataObject> GetQuery(InterfaceType ifType)
        {
            CheckDisposed();
            return new KistlContextQuery<IDataObject>(this, ifType);
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public IQueryable<T> GetQuery<T>() where T : class, IDataObject
        {
            CheckDisposed();
            return new KistlContextQuery<T>(this, new InterfaceType(typeof(T)));
        }

        /// <summary>
        /// Returns a PersistenceObject Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject
        {
            CheckDisposed();
            return new KistlContextQuery<T>(this, new InterfaceType(typeof(T)));
        }

        /// <summary>
        /// Returns a PersistenceObject Query by InterfaceType
        /// </summary>
        /// <param name="ifType">the interface to look for</param>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            return new KistlContextQuery<IPersistenceObject>(this, ifType);
        }

        /// <summary>
        /// Returns the List referenced by the given PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            CheckDisposed();
            return this.GetListOf<T>(obj.GetInterfaceType(), obj.ID, propertyName);
        }

        /// <summary>
        /// Returns the List referenced by the given Type, ID and PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="type">Type of the Object which holds the ObjectReferenceProperty</param>
        /// <param name="ID">ID of the Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public List<T> GetListOf<T>(InterfaceType type, int ID, string propertyName) where T : class, IDataObject
        {
            CheckDisposed();
            KistlContextQuery<T> query = new KistlContextQuery<T>(this, type);
            return ((KistlContextProvider)query.Provider).GetListOf(ID, propertyName).Cast<T>().ToList();
        }

        public IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationCollectionEntry
        {
            List<IStreamable> auxObjects;
            var serverList = ProxySingleton.Current.FetchRelation<T>(relationId, role, container, out auxObjects);

            foreach (IPersistenceObject obj in auxObjects)
            {
                this.Attach(obj);
            }

            var result = new List<T>();
            foreach (IPersistenceObject obj in serverList)
            {
                var localobj = this.Attach(obj);
                result.Add((T)localobj);
            }
            return result;
        }

        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public T Create<T>() where T : class, IDataObject
        {
            return (T)Create(new InterfaceType(typeof(T)));
        }

        /// <summary>
        /// Creates a new IDataObject by System.Type. Note - this Method is depricated!
        /// </summary>
        /// <param name="ifType">System.Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        public IDataObject Create(InterfaceType ifType)
        {
            return (IDataObject)CreateInternal(ifType);
        }

        /// <summary>
        /// Creates a new IRelationCollectionEntry by Type
        /// </summary>
        /// <typeparam name="T">Type of the new IRelationCollectionEntry</typeparam>
        /// <returns>A new IRelationCollectionEntry</returns>
        public T CreateRelationCollectionEntry<T>() where T : IRelationCollectionEntry
        {
            return (T)CreateRelationCollectionEntry(new InterfaceType(typeof(T)));
        }

        /// <summary>
        /// Creates a new IRelationCollectionEntry.
        /// </summary>
        /// <returns>A new IRelationCollectionEntry</returns>
        public IRelationCollectionEntry CreateRelationCollectionEntry(InterfaceType ifType)
        {
            return (IRelationCollectionEntry)CreateInternal(ifType);
        }

        /// <summary>
        /// Creates a new IValueCollectionEntry by Type
        /// </summary>
        /// <typeparam name="T">Type of the new ICollectionEntry</typeparam>
        /// <returns>A new IValueCollectionEntry</returns>
        public T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry
        {
            return (T)CreateValueCollectionEntry(new InterfaceType(typeof(T)));
        }

        /// <summary>
        /// Creates a new IValueCollectionEntry.
        /// </summary>
        /// <returns>A new IValueCollectionEntry</returns>
        public IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType)
        {
            return (IValueCollectionEntry)CreateInternal(ifType);
        }


        private IPersistenceObject CreateInternal(InterfaceType ifType)
        {
            CheckDisposed();
            IPersistenceObject obj = (IPersistenceObject)Activator.CreateInstance(ifType.ToImplementationType().Type);
            Attach(obj);
            OnObjectCreated(obj);
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyCreated();
            }
            return obj;
        }

        /// <summary>
        /// Creates a new Struct by Type
        /// </summary>
        /// <param name="ifType">Type of the new IDataObject</param>
        /// <returns>A new Struct</returns>
        public IStruct CreateStruct(InterfaceType ifType)
        {
            CheckDisposed();
            IStruct obj = (IStruct)Activator.CreateInstance(ifType.ToImplementationType().Type);
            return obj;
        }
        /// <summary>
        /// Creates a new Struct.
        /// </summary>
        /// <typeparam name="T">Type of the new Struct</typeparam>
        /// <returns>A new Struct</returns>
        public T CreateStruct<T>() where T : IStruct
        {
            return (T)CreateStruct(new InterfaceType(typeof(T)));
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
                var attachedObj = ContainsObject(obj.GetInterfaceType(), obj.ID);
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
            ((BaseClientPersistenceObject)obj).SetUnmodified();

            // Call Objects Attach Method to ensure, that every Child Object is also attached
            obj.AttachToContext(this);

            // update the debugger last 
            KistlContextDebuggerSingleton.Changed(this);

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
            KistlContextDebuggerSingleton.Changed(this);
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

            ((BaseClientPersistenceObject)obj).SetDeleted();

            OnObjectDeleted(obj);
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyDeleting();
            }
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public int SubmitChanges()
        {
            CheckDisposed();
            // TODO: Add a better Cache Refresh Strategie
            // CacheController<IDataObject>.Current.Clear();

            var objectsToSubmit = new List<BaseClientPersistenceObject>();
            var objectsToAdd = new List<BaseClientPersistenceObject>();
            var objectsToDetach = new List<BaseClientPersistenceObject>();

            // Added Objects
            foreach (var obj in _objects.OfType<BaseClientPersistenceObject>()
                .Where(o => o.ObjectState == DataObjectState.New))
            {
                objectsToSubmit.Add(obj);
                objectsToAdd.Add(obj);
            }
            // Changed objects
            foreach (var obj in _objects.OfType<BaseClientPersistenceObject>()
                .Where(o => o.ObjectState == DataObjectState.Modified))
            {
                objectsToSubmit.Add(obj);
            }
            // Deleted Objects
            foreach (var obj in _objects.OfType<BaseClientPersistenceObject>()
                .Where(o => o.ObjectState == DataObjectState.Deleted))
            {
                // Submit only persisted objects
                if (Helper.IsPersistedObject(obj))
                {
                    objectsToSubmit.Add(obj);
                }
                objectsToDetach.Add(obj);
            }

            var notifySaveList = objectsToSubmit.OfType<IDataObject>().Where(o => o.ObjectState.In(DataObjectState.New, DataObjectState.Modified));

            // Fire PreSave
            notifySaveList.ForEach(o => o.NotifyPreSave());

            // Submit to server
            var objectsFromServer = ProxySingleton.Current.SetObjects(
                objectsToSubmit
                    .Cast<IPersistenceObject>(),
                AttachedObjects
                    .ToLookup(o => o.GetInterfaceType())
                    .Select(g => new ObjectNotificationRequest() { Type = new SerializableType(g.Key), IDs = g.Select(o => o.ID).ToArray() }));

            // Apply Changes
            int counter = 0;
            var changedObjects = new List<BaseClientPersistenceObject>();
            foreach (BaseClientPersistenceObject objFromServer in objectsFromServer)
            {
                BaseClientPersistenceObject obj;

                if (counter < objectsToAdd.Count)
                {
                    obj = objectsToAdd[counter++];
                    // remove object from cache, since index by ID may change.
                    // will be re-inserted on attach later
                    _objects.Remove(obj);
                }
                else
                {
                    obj = (BaseClientPersistenceObject)this.ContainsObject(objFromServer.GetInterfaceType(), objFromServer.ID) ?? objFromServer;
                }

                obj.RecordNotifications();
                if (obj != objFromServer)
                {
                    obj.ApplyChangesFrom(objFromServer);
                }

                // reset ObjectState to new truth
                obj.SetUnmodified();

                changedObjects.Add(obj);
            }

            objectsToDetach.ForEach(obj => this.Detach(obj));
            changedObjects.ForEach(obj => this.Attach(obj));

            changedObjects.ForEach(obj => obj.PlaybackNotifications());

            // Fire PostSave
            notifySaveList.ForEach(o => o.NotifyPostSave());

            return objectsToSubmit.Count;
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <param name="ifType">Interface Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public IDataObject Find(InterfaceType ifType, int ID)
        {
            CheckDisposed();

            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            // return GetQuery(type).Single(o => o.ID == ID);

            return (IDataObject)this.GetType().FindGenericMethod("Find",
                new Type[] { ifType.Type },
                new Type[] { typeof(int) })
                .Invoke(this, new object[] { ID });
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
            where T : class, IDataObject
        {
            CheckDisposed();
            IPersistenceObject cacheHit = _objects.Lookup(new InterfaceType(typeof(T)), ID);
            if (cacheHit != null)
                return (T)cacheHit;
            else
                return GetQuery<T>().Single(o => o.ID == ID);
        }

        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();

            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            //return GetQuery(type).Single(o => o.ID == ID);

            return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject",
                new Type[] { ifType.Type },
                new Type[] { typeof(int) })
                .Invoke(this, new object[] { ID });
        }

        /// <summary>
        /// Find the Persistence Object of the given type by ID.
        /// Note: This method is not supported on the client
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject
        {
            CheckDisposed();
            IPersistenceObject cacheHit = _objects.Lookup(new InterfaceType(typeof(T)), ID);
            if (cacheHit != null)
                return (T)cacheHit;
            else
                return GetPersistenceObjectQuery<T>().Single(o => o.ID == ID);
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// Note: This method is not supported on the client yet
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            //return GetQuery(type).Single(o => o.ID == ID);

            return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject",
                new Type[] { ifType.Type },
                new Type[] { typeof(Guid) })
                .Invoke(this, new object[] { exportGuid });
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// Note: This method is not supported on the client
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject
        {
            return GetPersistenceObjectQuery<T>().Single(o => ((Kistl.App.Base.IExportable)o).ExportGuid == exportGuid);
        }

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            //return GetQuery(type).Single(o => o.ID == ID);

            return (IEnumerable<IPersistenceObject>)this.GetType().FindGenericMethod("FindPersistenceObjects",
                new Type[] { ifType.Type },
                new Type[] { typeof(IEnumerable<Guid>) })
                .Invoke(this, new object[] { exportGuids });
        }
        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject
        {
            return GetPersistenceObjectQuery<T>().Where(o => exportGuids.Contains(((Kistl.App.Base.IExportable)o).ExportGuid));
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

        #region IDebuggingKistlContext Members

        public StackTrace CreatedAt { get; private set; }

        public StackTrace DisposedAt { get; private set; }

        #endregion
    }
}
