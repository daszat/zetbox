
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
        : IKistlContext
    {
        protected readonly ContextCache objects = new ContextCache();

        private readonly IInterfaceTypeFilter _ifFilter;
        private readonly Assembly _interfaces;
        private readonly Assembly _implementations;

        /// <summary>Empty stand-in for object classes without instances.</summary>
        /// <remarks>Used by GetPersistenceObjectQuery()</remarks>
        private static readonly ReadOnlyCollection<IPersistenceObject> _emptyList = new ReadOnlyCollection<IPersistenceObject>(new List<IPersistenceObject>());

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;

        /// <summary>
        /// Check whether the specified type is from the interface assembly. Throws an ArgumentOutOfRangeException if not.
        /// </summary>
        /// <param name="paramName">the paramName to use for the exception</param>
        /// <param name="t">the Type to check.</param>
        private void CheckInterfaceAssembly(string paramName, Type t)
        {
            if (!InterfaceAssembly.Equals(t.Assembly))
            {
                var message = String.Format(CultureInfo.InvariantCulture, "[{0}] is not from the interface Assembly [{1}]!", t.FullName, InterfaceAssembly);
                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }

        /// <summary>
        /// Check whether the specified type is from the implementation assembly. Throws an ArgumentOutOfRangeException if not.
        /// </summary>
        /// <param name="paramName">the paramName to use for the exception</param>
        /// <param name="t">the Type to check.</param>
        private void CheckImplementationAssembly(string paramName, Type t)
        {
            if (!ImplementationAssembly.Equals(t.Assembly))
            {
                var message = String.Format(CultureInfo.InvariantCulture, "[{0}] is not from the implementation Assembly [{1}]!", t.FullName, ImplementationAssembly);
                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }

        private void CheckDisposed()
        {
            if (IsDisposed) { throw new InvalidOperationException("Context already disposed"); }
        }

        /// <summary>
        /// Initializes a new instance of the BaseMemoryContext class, using the specified assemblies for interfaces and implementation.
        /// </summary>
        /// <param name="ifFilter">An interface type filter, passed in from the container</param>
        /// <param name="interfaces">The assembly containing the interfaces available in this context. MUST not be null.</param>
        /// <param name="implementations">The assembly containing the classes implementing the interfaces in this context. MUST not be null.</param>
        protected BaseMemoryContext(IInterfaceTypeFilter ifFilter, Assembly interfaces, Assembly implementations)
        {
            if (interfaces == null) { throw new ArgumentNullException("interfaces"); }
            if (implementations == null) { throw new ArgumentNullException("implementations"); }

            _ifFilter = ifFilter;
            _interfaces = interfaces;
            _implementations = implementations;
        }

        /// <inheritdoc />
        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            CheckImplementationAssembly("obj", obj.GetType());

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                // TODO: security: check for overflow
                ((BasePersistenceObject)obj).ID = --_newIDCounter;
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
            CheckInterfaceAssembly("T", typeof(T));
            return GetPersistenceObjectQuery(_ifFilter.AsInterfaceType(typeof(T))).Cast<T>();
        }

        /// <inheritdoc />
        public IQueryable<IDataObject> GetQuery(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);
            return GetPersistenceObjectQuery(ifType).Cast<IDataObject>();
        }

        /// <inheritdoc />
        public IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));
            return GetPersistenceObjectQuery(_ifFilter.AsInterfaceType(typeof(T))).Cast<T>();
        }

        /// <summary>Retrieves a new query on top of the attached objects.</summary>
        /// <remarks>Implementors can override this method to modify queries 
        /// according to their provider's needs.</remarks>
        public virtual IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);
            return (objects[ifType] ?? _emptyList).AsQueryable().AddOfType(ifType.Type).Cast<IPersistenceObject>();
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
                CheckInterfaceAssembly("T", typeof(T));
                return GetPersistenceObjectQuery(_ifFilter.AsInterfaceType(typeof(T))).Cast<T>().ToList();
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
            CheckInterfaceAssembly("type", ifType.Type);
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
        public bool IsReadonly
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
            CheckInterfaceAssembly("T", typeof(T));
            return (T)Create(_ifFilter.AsInterfaceType(typeof(T)));
        }

        /// <inheritdoc />
        public IDataObject Create(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);
            return (IDataObject)CreateInternal(ifType);
        }

        /// <inheritdoc />
        public T CreateUnattached<T>() where T : class, IPersistenceObject
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));
            return (T)CreateUnattachedInstance(_ifFilter.AsInterfaceType(typeof(T)));
        }

        /// <inheritdoc />
        public IPersistenceObject CreateUnattached(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);
            return (IPersistenceObject)CreateUnattachedInstance(ifType);
        }

        /// <inheritdoc />
        public T CreateRelationCollectionEntry<T>() where T : IRelationCollectionEntry
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));
            return (T)CreateRelationCollectionEntry(_ifFilter.AsInterfaceType(typeof(T)));
        }

        /// <inheritdoc />
        public IRelationCollectionEntry CreateRelationCollectionEntry(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);
            return (IRelationCollectionEntry)CreateInternal(ifType);
        }

        /// <inheritdoc />
        public T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));
            return (T)CreateValueCollectionEntry(_ifFilter.AsInterfaceType(typeof(T)));
        }

        /// <inheritdoc />
        public IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);
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
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);

            ICompoundObject obj = (ICompoundObject)CreateUnattachedInstance(ifType);
            return obj;
        }
        /// <inheritdoc />
        public T CreateCompoundObject<T>() where T : ICompoundObject
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));

            return (T)CreateCompoundObject(_ifFilter.AsInterfaceType(typeof(T)));
        }

        /// <inheritdoc />
        public IDataObject Find(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);

            return (IDataObject)objects.Lookup(ifType, ID);
        }

        /// <inheritdoc />
        public T Find<T>(int ID)
            where T : class, IDataObject
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));

            return (T)Find(_ifFilter.AsInterfaceType(typeof(T)), ID);
        }

        /// <inheritdoc />
        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));

            return (T)FindPersistenceObject(_ifFilter.AsInterfaceType(typeof(T)), ID);
        }

        /// <inheritdoc />
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);

            return objects.Lookup(ifType, ID);
        }

        /// <inheritdoc />
        public T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));

            return (T)objects.Lookup(exportGuid);
        }

        /// <inheritdoc />
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);

            return objects.Lookup(exportGuid);
        }

        /// <inheritdoc />
        public IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            CheckDisposed();
            if (ifType == null) { throw new ArgumentNullException("ifType"); }
            CheckInterfaceAssembly("ifType", ifType.Type);
            if (exportGuids == null) { throw new ArgumentNullException("exportGuids"); }

            var query = objects[ifType];
            if (query == null) return new List<IPersistenceObject>();
            return query.Cast<IExportableInternal>().Where(o => exportGuids.Contains(o.ExportGuid)).Cast<IPersistenceObject>().AsEnumerable();
        }

        /// <inheritdoc />
        public IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject
        {
            CheckDisposed();
            CheckInterfaceAssembly("T", typeof(T));
            if (exportGuids == null) { throw new ArgumentNullException("exportGuids"); }

            return FindPersistenceObjects(_ifFilter.AsInterfaceType(typeof(T)), exportGuids).Cast<T>();
        }

        /// <inheritdoc />
        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        /// <inheritdoc />
        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

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

        /// <summary>
        /// The assembly containing the interfaces available in this context.
        /// </summary>
        public Assembly InterfaceAssembly
        {
            get { return _interfaces; }
        }

        /// <summary>
        /// The assembly containing the implementations of the interfaces available in this context.
        /// </summary>
        public Assembly ImplementationAssembly
        {
            get { return _implementations; }
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
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
    }
}
