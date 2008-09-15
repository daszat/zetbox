using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kistl.API.Server
{
    public abstract class BaseKistlDataContext : IKistlContext, IDisposable
    {
        public virtual void Dispose()
        {
            KistlDataContext.ClearSession(this);
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        /// <summary>
        /// Is true after Dispose() was called.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Attach an IPersistenceObject. The EntityFramework guarantees the all Objects are unique. No check requiered.
        /// </summary>
        /// <param name="obj">Object to Attach</param>
        /// <returns>Object Attached</returns>
        public virtual IPersistenceObject Attach(IPersistenceObject obj)
        {
            // call Attach on Subitems
            obj.AttachToContext(this);

            return obj;
        }

        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        public virtual void Detach(IPersistenceObject obj)
        {
            obj.DetachFromContext(this);
        }

        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IPersistenceObject</param>
        public virtual void Delete(IPersistenceObject obj)
        {
            OnObjectDeleted(obj);
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public abstract IQueryable<T> GetQuery<T>() where T : IDataObject;

        /// <summary>
        /// Returns a Query by System.Type.
        /// </summary>
        /// <param name="objType">System.Type</param>
        /// <returns>IQueryable</returns>
        public abstract IQueryable<IDataObject> GetQuery(Type type);

        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public virtual List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject
        {
            return obj.GetPropertyValue<IEnumerable>(propertyName).Cast<T>().ToList();
        }

        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given Type, ID and PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="type">Type of the Object which holds the BackReferenceProperty</param>
        /// <param name="ID">ID of the Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public virtual List<T> GetListOf<T>(Type type, int ID, string propertyName) where T : IDataObject
        {
            IDataObject obj = (IDataObject)this.GetQuery(type).First(o => o.ID == ID);
            return GetListOf<T>(obj, propertyName);
        }

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        public abstract IPersistenceObject ContainsObject(Type type, int ID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<IPersistenceObject> AttachedObjects
        {
            get;
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <remarks>Das pass mir noch nicht - die Ableitung muss sich selbst um die Notifications kümmern.</remarks>
        /// <returns>Number of affected Objects</returns>
        public abstract int SubmitChanges();

        /// <summary>
        /// Creates a new IDataObject by System.Type. Note - this Method is depricated!
        /// </summary>
        /// <param name="type">System.Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        public virtual Kistl.API.IDataObject Create(Type type)
        {
            type = type.ToImplementationType();
            Kistl.API.IDataObject obj = (Kistl.API.IDataObject)Activator.CreateInstance(type);
            Attach(obj);
            OnObjectCreated(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public virtual T Create<T>() where T : Kistl.API.IDataObject
        {
            return (T)Create(typeof(T));
        }

        /// <summary>
        /// Creates a new Struct by Type
        /// </summary>
        /// <param name="type">Type of the new IDataObject</param>
        /// <returns>A new Struct</returns>
        public virtual IStruct CreateStruct(Type type)
        {
            type = type.ToImplementationType();
            Kistl.API.IStruct obj = (Kistl.API.IStruct)Activator.CreateInstance(type);
            return obj;
        }
        /// <summary>
        /// Creates a new Struct.
        /// </summary>
        /// <typeparam name="T">Type of the new Struct</typeparam>
        /// <returns>A new Struct</returns>
        public virtual T CreateStruct<T>() where T : IStruct
        {
            return (T)CreateStruct(typeof(T));
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// <remarks>Note: This Method is depricated.</remarks>
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <param name="type">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public abstract IDataObject Find(Type type, int ID);

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public abstract T Find<T>(int ID) where T : IDataObject;

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
