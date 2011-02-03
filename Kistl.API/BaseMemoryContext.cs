
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Kistl.API.Utils;

    /// <summary>
    /// A temporary data context without permanent backing store.
    /// </summary>
    public abstract class BaseMemoryContext
        : IKistlContext, IZBoxContextInternals
    {
        protected readonly ContextCache<int> objects;
        private readonly InterfaceType.Factory _iftFactory;
        protected InterfaceType.Factory IftFactory { get { return _iftFactory; } }

        /// <summary>Empty stand-in for object classes without instances.</summary>
        /// <remarks>Used by GetPersistenceObjectQuery()</remarks>
        private static readonly ReadOnlyCollection<IPersistenceObject> _emptyList = new ReadOnlyCollection<IPersistenceObject>(new List<IPersistenceObject>());

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;

        protected void CheckDisposed()
        {
            if (IsDisposed) { throw new InvalidOperationException("Context already disposed"); }
        }

        /// <summary>
        /// Initializes a new instance of the BaseMemoryContext class, using the specified assemblies for interfaces and implementation.
        /// </summary>
        protected BaseMemoryContext(InterfaceType.Factory iftFactory)
        {
            this.objects = new ContextCache<int>(this, item => item.ID);
            this._iftFactory = iftFactory;
            KistlContextDebuggerSingleton.Created(this);
        }

        /// <inheritdoc />
        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            //CheckImplementationAssembly("obj", obj.GetType());

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                // TODO: security: check for overflow
                ((BasePersistenceObject)obj).ID = --_newIDCounter;
            }
            else
            {
                // Check if Object is already in this Context
                var attachedObj = ContainsObject(GetInterfaceType(obj), obj.ID);
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
            objects.Add(obj);
            // TODO: Since providers are not required to use BasePersistenceObject
            // this doesn't work. Improve IDataObject interface to contain this too?
            //((BasePersistenceObject)obj).SetUnmodified();

            // Call Objects Attach Method to ensure, that every Child Object is also attached
            obj.AttachToContext(this);

            return obj;
        }

        /// <inheritdoc />
        public void Detach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (!objects.Contains(obj)) { throw new InvalidOperationException("This object does not belong to the current context"); }

            objects.Remove(obj);
            obj.DetachFromContext(this);
        }

        /// <inheritdoc />
        public void Delete(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (obj.Context != this) { throw new InvalidOperationException("This object does not belong to the current Context"); }

            // TODO: Implement Delete on Memory Context
            //((BasePersistenceObject)obj).SetDeleted();

            OnObjectDeleted(obj);
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyDeleting();
            }
        }

        /// <inheritdoc />
        public IQueryable<T> GetQuery<T>()
            where T : class, IDataObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));
            return GetPersistenceObjectQuery(_iftFactory(typeof(T))).Cast<T>();
        }

        /// <inheritdoc />
        public IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));
            return GetPersistenceObjectQuery(_iftFactory(typeof(T))).Cast<T>();
        }

        public List<IDataObject> GetAllHack<T>()
            where T : class, IDataObject
        {
            // The query translator cannot properly handle the IDataObject cast:
            // return GetQuery<T>().Cast<IDataObject>();

            var result = new List<IDataObject>();
            foreach (var o in GetQuery<T>())
            {
                result.Add(o);
            }
            return result;
        }

        public List<IDataObject> GetAll(InterfaceType t)
        {
            var mi = this.GetType().FindGenericMethod("GetAllHack", new[] { t.Type }, new Type[0]);
            return (List<IDataObject>)mi.Invoke(this, new object[0]);
        }

        /// <summary>Retrieves a new query on top of the attached objects.</summary>
        /// <remarks>Implementors can override this method to modify queries 
        /// according to their provider's needs.</remarks>
        protected virtual IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);

            var source = objects[ifType];
            if (source != null)
            {
                // double cast to work around https://bugzilla.novell.com/show_bug.cgi?id=661462
                return source.Cast<IPersistenceObject>().AsQueryable().AddOfType(ifType.Type).Cast<IPersistenceObject>();
            }
            else
            {
                return _emptyList.AsQueryable();
            }
        }

        /// <summary>Not implemented.</summary>
        List<T> IReadOnlyKistlContext.GetListOf<T>(IDataObject obj, string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        List<T> IReadOnlyKistlContext.GetListOf<T>(InterfaceType ifType, int ID, string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Only implemented for the parent==null case.</summary>
        IList<T> IReadOnlyKistlContext.FetchRelation<T>(Guid relId, RelationEndRole role, IDataObject parent)
        {
            if (parent == null)
            {
                CheckDisposed();
                //CheckInterfaceAssembly("T", typeof(T));
                return GetPersistenceObjectQuery(_iftFactory(typeof(T))).Cast<T>().ToList();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public IPersistenceObject ContainsObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("type", ifType.Type);
            return Find(ifType, ID);
        }

        /// <inheritdoc />
        [System.Diagnostics.DebuggerDisplay("Count = {_objects.Count}")]
        public IEnumerable<IPersistenceObject> AttachedObjects
        {
            get
            {
                CheckDisposed();
                return objects;
            }
        }

        /// <inheritdoc />
        public abstract int SubmitChanges();

        /// <inheritdoc />
        public bool IsDisposed { get; private set; }

        /// <summary>This context is read/write.</summary>
        public virtual bool IsReadonly
        {
            get
            {
                CheckDisposed();
                return false;
            }
        }

        /// <inheritdoc />
        public T Create<T>() where T : class, IDataObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));
            return (T)Create(_iftFactory(typeof(T)));
        }

        /// <inheritdoc />
        public IDataObject Create(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);
            return (IDataObject)CreateInternal(ifType);
        }

        /// <inheritdoc />
        public T CreateUnattached<T>() where T : class, IPersistenceObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));
            return (T)CreateUnattachedInstance(_iftFactory(typeof(T)));
        }

        /// <inheritdoc />
        public IPersistenceObject CreateUnattached(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);
            return (IPersistenceObject)CreateUnattachedInstance(ifType);
        }

        /// <inheritdoc />
        public T CreateRelationCollectionEntry<T>() where T : IRelationEntry
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));
            return (T)CreateRelationCollectionEntry(_iftFactory(typeof(T)));
        }

        /// <inheritdoc />
        public IRelationEntry CreateRelationCollectionEntry(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);
            return (IRelationEntry)CreateInternal(ifType);
        }

        /// <inheritdoc />
        public T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));
            return (T)CreateValueCollectionEntry(_iftFactory(typeof(T)));
        }

        /// <inheritdoc />
        public IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);
            return (IValueCollectionEntry)CreateInternal(ifType);
        }

        /// <summary>
        /// Creates an unattached instance of the specified type. The implementation type is instantiated from the implementation assembly.
        /// </summary>
        /// <param name="ifType">The requested interface.</param>
        /// <returns>A newly created, unattached instance of the implementation for the specified interface.</returns>
        protected abstract object CreateUnattachedInstance(InterfaceType ifType);

        /// <summary>
        /// Creates an attached, ready-to-use instance of the specified type. The implementation type is instantiated from the implementation assembly.
        /// </summary>
        /// <param name="ifType">The requested interface.</param>
        /// <returns>A newly created, attached instance of the implementation for the specified interface.</returns>
        private IPersistenceObject CreateInternal(InterfaceType ifType)
        {
            CheckDisposed();
            IPersistenceObject obj = (IPersistenceObject)CreateUnattachedInstance(ifType);
            Attach(obj);
            OnObjectCreated(obj);
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyCreated();
            }
            return obj;
        }

        /// <inheritdoc />
        public ICompoundObject CreateCompoundObject(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);

            ICompoundObject obj = (ICompoundObject)CreateUnattachedInstance(ifType);
            return obj;
        }
        /// <inheritdoc />
        public T CreateCompoundObject<T>() where T : ICompoundObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));

            return (T)CreateCompoundObject(_iftFactory(typeof(T)));
        }

        /// <inheritdoc />
        public IDataObject Find(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);

            return (IDataObject)objects.Lookup(ifType, ID);
        }

        /// <inheritdoc />
        public T Find<T>(int ID)
            where T : class, IDataObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));

            return (T)Find(_iftFactory(typeof(T)), ID);
        }

        /// <inheritdoc />
        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));

            return (T)FindPersistenceObject(_iftFactory(typeof(T)), ID);
        }

        /// <inheritdoc />
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);

            return objects.Lookup(ifType, ID);
        }

        /// <inheritdoc />
        public T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));

            return (T)objects.Lookup(exportGuid);
        }

        /// <inheritdoc />
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);

            return objects.Lookup(exportGuid);
        }

        /// <inheritdoc />
        public IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);
            if (exportGuids == null) { throw new ArgumentNullException("exportGuids"); }

            var query = objects[ifType];
            if (query == null)
                return new List<IPersistenceObject>();
            return query.Cast<IExportableInternal>().Where(o => exportGuids.Contains(o.ExportGuid)).Cast<IPersistenceObject>().AsEnumerable();
        }

        /// <inheritdoc />
        public IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject
        {
            CheckDisposed();
            //CheckInterfaceAssembly("T", typeof(T));
            if (exportGuids == null) { throw new ArgumentNullException("exportGuids"); }

            return FindPersistenceObjects(_iftFactory(typeof(T)), exportGuids).Cast<T>();
        }

        /// <inheritdoc />
        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        /// <inheritdoc />
        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        /// <inheritdoc />
        public event GenericEventHandler<IKistlContext> Changed;


        /// <summary>
        /// Triggers the <see cref="ObjectCreated"/> event.
        /// </summary>
        /// <param name="obj">The created object.</param>
        protected virtual void OnObjectCreated(IPersistenceObject obj)
        {
            if (ObjectCreated != null)
            {
                ObjectCreated(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

        /// <summary>
        /// Triggers the <see cref="ObjectDeleted"/> event.
        /// </summary>
        /// <param name="obj">The deleted object.</param>
        protected virtual void OnObjectDeleted(IPersistenceObject obj)
        {
            if (ObjectDeleted != null)
            {
                ObjectDeleted(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

        protected virtual void OnChanged()
        {
            GenericEventHandler<IKistlContext> temp = Changed;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IKistlContext>() { Data = this });
            }
        }

        /// <summary>
        /// Fired when the Context is beeing disposed.
        /// </summary>
        public event GenericEventHandler<IReadOnlyKistlContext> Disposing;

        /// <inheritdoc />
        public virtual void Dispose()
        {
            GenericEventHandler<IReadOnlyKistlContext> temp = Disposing;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IReadOnlyKistlContext>() { Data = this });
            }
            // nothing to dispose
            IsDisposed = true;
        }

        /// <summary>Not implemented.</summary>
        int IKistlContext.CreateBlob(System.IO.Stream s, string filename, string mimetype)
        {
            throw new NotSupportedException();
        }

        /// <summary>Not implemented.</summary>
        int IKistlContext.CreateBlob(System.IO.FileInfo fi, string mimetype)
        {
            throw new NotSupportedException();
        }

        /// <summary>Not implemented.</summary>
        System.IO.Stream IReadOnlyKistlContext.GetStream(int ID)
        {
            throw new NotSupportedException();
        }

        /// <summary>Not implemented.</summary>
        System.IO.FileInfo IReadOnlyKistlContext.GetFileInfo(int ID)
        {
            throw new NotSupportedException();
        }

        public InterfaceType GetInterfaceType(IPersistenceObject obj)
        {
            return _iftFactory(((BasePersistenceObject)obj).GetImplementedInterface());
        }

        public InterfaceType GetInterfaceType(ICompoundObject obj)
        {
            return _iftFactory(((BaseCompoundObject)obj).GetImplementedInterface());
        }

        public InterfaceType GetInterfaceType(Type t)
        {
            return _iftFactory(t);
        }

        public abstract InterfaceType GetInterfaceType(string typeName);

        public abstract ImplementationType GetImplementationType(Type t);
        public abstract ImplementationType ToImplementationType(InterfaceType t);

        private IDictionary<object, object> _TransientState = null;
        /// <inheritdoc />
        public IDictionary<object, object> TransientState
        {
            get
            {
                CheckDisposed();
                if (_TransientState == null)
                {
                    _TransientState = new Dictionary<object, object>();
                }
                return _TransientState;
            }
        }

        /// <summary>
        /// Indicates that the ZBox Context has some modified, added or deleted items
        /// </summary>
        public bool IsModified { get; private set; }

        /// <summary>
        /// Is fires when <see cref="IsModified"/> was changed
        /// </summary>
        public event EventHandler IsModifiedChanged;

        #region IZBoxContextInternals Members

        void IZBoxContextInternals.SetModified(IPersistenceObject obj)
        {
            if (obj.ObjectState.In(DataObjectState.Deleted, DataObjectState.Modified, DataObjectState.New))
            {
                IsModified = true;
                EventHandler temp = IsModifiedChanged;
                if (temp != null)
                {
                    temp(this, EventArgs.Empty);
                }
            }
        }

        string IZBoxContextInternals.StoreBlobStream(System.IO.Stream s, Guid exportGuid, DateTime timestamp, string filename)
        {
            // No supprt, but do not throw an exception
            // A memory context could be loaded from a file
            // FileSystemPackageProvider support blobs
            return string.Empty;
        }

        #endregion

        #region IKistlContext Members


        public int GetSequenceNumber(Guid sequenceGuid)
        {
            throw new NotImplementedException();
        }

        public int GetContinuousSequenceNumber(Guid sequenceGuid)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
