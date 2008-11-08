using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public class ReadOnlyContextException : NotSupportedException
    {
        public ReadOnlyContextException()
            : base("This context is readonly")
        {
        }
    }

    public abstract class FrozenContext : IKistlContext
    {

        private static FrozenContext _Single = null;
        public static FrozenContext Single 
        { 
            get 
            {
                if (_Single == null)
                {
                    Type t = Type.GetType(ApplicationContext.Current.ImplementationAssembly + ".FrozenContextImplementation, " + ApplicationContext.Current.ImplementationAssembly, true);
                    _Single = (FrozenContext)Activator.CreateInstance(t);

                    if (_Single == null) throw new InvalidOperationException("Unable to create frozen context");
                }
                return _Single; 
            } 
        }

        /// <summary>
        /// List of Objects (IDataObject and ICollectionEntry) in this Context.
        /// </summary>
        private HashSet<IPersistenceObject> _objects = new HashSet<IPersistenceObject>();

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        private int _newIDCounter = Helper.INVALIDID;

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            // OfType -> Filter Class hierarchy
            return GetQuery(typeof(T)).OfType<T>();
        }
        /// <summary>
        /// Returns a Query by Type
        /// </summary>
        /// <param name="type">System.Type</param>
        /// <returns>IQueryable</returns>
        public virtual IQueryable<IDataObject> GetQuery(Type type)
        {
            throw new ArgumentException(string.Format("Type '{0}' is not a frozen Object", type.FullName));
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
            return GetQuery(type).Single(obj => obj.ID == ID);
        }
        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public T Find<T>(int ID) where T : IDataObject
        {
            return (T)Find(typeof(T), ID);
        }

        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                obj.SetPrivatePropertyValue<int>("ID", --_newIDCounter);
            }
            else if (obj.ID < _newIDCounter)
            {
                // Check ID <-> newIDCounter
                _newIDCounter = obj.ID;
            }
            
            // Attach & set Objectstate to Unmodified
            if (!_objects.Contains(obj))
            {
                _objects.Add(obj);
            }
            else
            {
                throw new InvalidOperationException("Object is already attached to this context");
            }

            obj.AttachToContext(this);
            return obj;
        }

        public void Detach(IPersistenceObject obj)
        {
            throw new NotSupportedException();
        }

        public void Delete(IPersistenceObject obj)
        {
            throw new ReadOnlyContextException();
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");
            // Return empty list - Object Initializer will fill the Collection
            return new List<T>();
        }

        public List<T> GetListOf<T>(Type type, int ID, string propertyName) where T : IDataObject
        {
            IDataObject obj = this.Find(type, ID);
            return GetListOf<T>(obj, propertyName);
        }

        public IPersistenceObject ContainsObject(Type type, int ID)
        {
            if (ID == Helper.INVALIDID) throw new ArgumentException("ID cannot be invalid", "ID");
            return _objects.FirstOrDefault<IPersistenceObject>(obj => obj.GetType() == type && obj.ID == ID);
        }

        public IEnumerable<IPersistenceObject> AttachedObjects
        {
            get { return _objects; }
        }

        public int SubmitChanges()
        {
            throw new ReadOnlyContextException();
        }

        public bool IsDisposed
        {
            get { return false; }
        }

        public IDataObject Create(Type type)
        {
            throw new ReadOnlyContextException();
        }

        public T Create<T>() where T : IDataObject
        {
            throw new ReadOnlyContextException();
        }

        public IStruct CreateStruct(Type type)
        {
            throw new ReadOnlyContextException();
        }

        public T CreateStruct<T>() where T : IStruct
        {
            throw new ReadOnlyContextException();
        }

        public IKistlContext GetReadonlyContext()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Will never fire
        /// </summary>
        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        /// <summary>
        /// Will never fire
        /// </summary>
        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        public void Dispose()
        {
        }
    }
}
