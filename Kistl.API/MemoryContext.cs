using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Utils;

namespace Kistl.API
{
    public class MemoryContext
        : IKistlContext
    {
        public MemoryContext(Assembly interfaces, Assembly implementations)
        {
            if (interfaces == null) { throw new ArgumentNullException("interfaces"); }
            if (implementations == null) { throw new ArgumentNullException("implementations"); }

            InterfaceAssembly = interfaces;
            ImplementationAssembly = implementations;
        }

        private ContextCache _objects = new ContextCache();

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;

        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (!ImplementationAssembly.Equals(obj.GetType().Assembly))
            {
                var message = String.Format(CultureInfo.InvariantCulture, "Not from the ImplementationAssembly [{0}]!", ImplementationAssembly);
                throw new ArgumentOutOfRangeException("obj", message);
            }

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
            _objects.Add(obj);
            //((BasePersistenceObject)obj).SetUnmodified();

            // Call Objects Attach Method to ensure, that every Child Object is also attached
            obj.AttachToContext(this);

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
            //((BasePersistenceObject)obj).SetDeleted();
            // TODO: Implement Delete on Memory Context
            OnObjectDeleted(obj);
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyDeleting();
            }
        }

        public IQueryable<T> GetQuery<T>()
            where T : class, IDataObject
        {
            return GetPersistenceObjectQuery(new InterfaceType(typeof(T))).Cast<T>();
        }

        public IQueryable<IDataObject> GetQuery(InterfaceType ifType)
        {
            return GetPersistenceObjectQuery(ifType).Cast<IDataObject>();
        }

        public IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject
        {
            return GetPersistenceObjectQuery(new InterfaceType(typeof(T))).Cast<T>();
        }

        private List<IPersistenceObject> _emptyList = new List<IPersistenceObject>();
        public IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            return (_objects[ifType] ?? _emptyList).AsQueryable().AddOfType(ifType.Type).Cast<IPersistenceObject>();
        }

        List<T> IKistlContext.GetListOf<T>(IDataObject obj, string propertyName)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOf<T>(InterfaceType ifType, int ID, string propertyName) where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IList<T> FetchRelation<T>(Guid relId, RelationEndRole role, IDataObject parent) where T : class, IRelationCollectionEntry
        {
            if (parent == null)
            {
                return GetPersistenceObjectQuery(new InterfaceType(typeof(T))).Cast<T>().ToList();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            return Find(type, ID);
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

        public int SubmitChanges() { throw new NotSupportedException(); }

        public bool IsDisposed { get { return false; } }

        public bool IsReadonly { get { return false; } }

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

        public IDataObject Find(InterfaceType ifType, int ID)
        {
            return (IDataObject)_objects.Lookup(ifType, ID);
        }

        public T Find<T>(int ID)
            where T : class, IDataObject
        {
            return (T)Find(new InterfaceType(typeof(T)), ID);
        }

        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject
        {
            return (T)FindPersistenceObject(new InterfaceType(typeof(T)), ID);
        }

        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            return _objects.Lookup(ifType, ID);
        }

        public T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject
        {
            return (T)FindPersistenceObject(new InterfaceType(typeof(T)), exportGuid);
        }

        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            var query = _objects[ifType];
            if (query == null) return null;
            return (IPersistenceObject)query.Cast<IExportableInternal>().FirstOrDefault(o => o.ExportGuid == exportGuid);
        }

        public IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            var query = _objects[ifType];
            if (query == null) return new List<IPersistenceObject>();
            return query.Cast<IExportableInternal>().Where(o => exportGuids.Contains(o.ExportGuid)).Cast<IPersistenceObject>().AsEnumerable();
        }

        public IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject
        {
            return FindPersistenceObjects(new InterfaceType(typeof(T)), exportGuids).Cast<T>();
        }

        public event GenericEventHandler<IPersistenceObject> ObjectCreated;
        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        protected virtual void OnObjectCreated(IPersistenceObject obj)
        {
            if (ObjectCreated != null)
            {
                ObjectCreated(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

        protected virtual void OnObjectDeleted(IPersistenceObject obj)
        {
            if (ObjectDeleted != null)
            {
                ObjectDeleted(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

        public Assembly InterfaceAssembly
        {
            get;
            private set;
        }

        public Assembly ImplementationAssembly
        {
            get;
            private set;
        }

        public virtual void Dispose()
        {
            // nothing to dispose
        }
    }
}
