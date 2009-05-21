using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public class KistlContextDisposedExeption : Exception
    {
        public KistlContextDisposedExeption()
            : base("Context has been disposed. Reusing is not allowed.")
        {
        }
    }

    public interface IKistlContextDebugger : IDisposable
    {
        void Created(IKistlContext ctx);
        void Disposed(IKistlContext ctx);
        void Changed(IKistlContext ctx);
    }

    /// <summary>
    /// Interface for a LinqToNNN Context.
    /// </summary>
    public interface IKistlContext : IDisposable
    {
        /// <summary>
        /// Attach an IPersistenceObject. This Method checks, if the Object is already in that Context. 
        /// If so, it returns the Object in that Context.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        /// <returns>The Object in already Context or obj if not</returns>
        IPersistenceObject Attach(IPersistenceObject obj);
        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Detach(IPersistenceObject obj);
        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Delete(IPersistenceObject obj);

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        IQueryable<T> GetQuery<T>() where T : class, IDataObject;
        /// <summary>
        /// Returns a Query by InterfaceType
        /// </summary>
        /// <param name="ifType">the interface to look for</param>
        /// <returns>IQueryable</returns>
        IQueryable<IDataObject> GetQuery(InterfaceType ifType);

        /// <summary>
        /// Returns the List referenced by the given PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject;
        /// <summary>
        /// Returns the List referenced by the given Type, ID and PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="ifType">Type of the Object which holds the ObjectReferenceProperty</param>
        /// <param name="ID">ID of the Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        List<T> GetListOf<T>(InterfaceType ifType, int ID, string propertyName) where T : class, IDataObject;

        /// <summary>
        /// Fetches all collection entries of a given Relation (specified by <paramref name="ID"/>)
        /// which reference the given <paramref name="container"/> on the side <paramref name="role"/>
        /// of the relation. Mostly for internal use.
        /// </summary>
        /// <typeparam name="T">Type of the IRelationCollectionEntry element</typeparam>
        /// <param name="relationId">Specifies which relation to fetch</param>
        /// <param name="role">Specifies how to interpret the container</param>
        /// <param name="container">The container of the requested collection</param>
        /// <returns></returns>
        IList<T> FetchRelation<T>(int relationId, RelationEndRole role, IDataObject container) where T : class, IRelationCollectionEntry;

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        IPersistenceObject ContainsObject(InterfaceType type, int ID);

        IEnumerable<IPersistenceObject> AttachedObjects { get; }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counded.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        int SubmitChanges();

        /// <summary>
        /// IsDisposed can be used to detect whether this IKistlContext was aborted with Dispose()
        /// </summary>
        bool IsDisposed { get; }

        bool IsReadonly { get; }

        /// <summary>
        /// Creates a new IDataObject by Type
        /// </summary>
        /// <param name="ifType">Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        IDataObject Create(InterfaceType ifType);
        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        T Create<T>() where T : class, IDataObject;

        /// <summary>
        /// Creates a new ICollectionEntry by Type
        /// </summary>
        /// <param name="ifType">Type of the new ICollectionEntry</param>
        /// <returns>A new ICollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IRelationCollectionEntry CreateRelationCollectionEntry(InterfaceType ifType);
        /// <summary>
        /// Creates a new ICollectionEntry.
        /// </summary>
        /// <typeparam name="T">Type of the new ICollectionEntry</typeparam>
        /// <returns>A new ICollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T CreateRelationCollectionEntry<T>() where T : IRelationCollectionEntry;

        /// <summary>
        /// Creates a new ICollectionEntry by Type
        /// </summary>
        /// <param name="ifType">Type of the new ICollectionEntry</param>
        /// <returns>A new ICollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType);
        /// <summary>
        /// Creates a new ICollectionEntry.
        /// </summary>
        /// <typeparam name="T">Type of the new ICollectionEntry</typeparam>
        /// <returns>A new ICollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry;

        /// <summary>
        /// Creates a new Struct by Type
        /// </summary>
        /// <param name="ifType">Type of the new Struct</param>
        /// <returns>A new Struct</returns>
        IStruct CreateStruct(InterfaceType ifType);
        /// <summary>
        /// Creates a new Struct.
        /// </summary>
        /// <typeparam name="T">Type of the new Struct</typeparam>
        /// <returns>A new Struct</returns>
        T CreateStruct<T>() where T : IStruct;

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        IDataObject Find(InterfaceType ifType, int ID);
        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        T Find<T>(int ID) where T : class, IDataObject;

        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID);
        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject;

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid);
        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject;

        /// <summary>
        /// Creates a read-only context connected to the same data source as this IKistlContext.
        /// </summary>
        /// <returns>a read-only context of the same source as this context</returns>
        /// 
        /// Objects fetched from this context cannot be modfied. 
        /// 
        /// Implementations are explicitly allowed to re-use one read-only context for all calls
        /// to this function for performance reasons.
        /// Implementations are required to return the same read-only context on successive calls 
        /// of GetReadonlyContext() on a single IKistlContext.
        IKistlContext GetReadonlyContext();

        /// <summary>
        /// Is fired when an object is created in this Context.
        /// The newly created object is passed as Data.
        /// </summary>
        event GenericEventHandler<IPersistenceObject> ObjectCreated;

        /// <summary>
        /// Is fired when an object is deleted in this Context.
        /// The delted object is passed as Data.
        /// </summary>
        event GenericEventHandler<IPersistenceObject> ObjectDeleted;

    }

    public interface IDebuggingKistlContext : IKistlContext
    {
        StackTrace CreatedAt { get; }
        StackTrace DisposedAt { get; }
    }
}
