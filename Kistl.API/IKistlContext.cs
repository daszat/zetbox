using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Kistl.API
{
    [Serializable]
    public class KistlContextException
        : Exception
    {
        public KistlContextException()
            : base()
        {
        }

        public KistlContextException(string message)
            : base(message)
        {
        }

        public KistlContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected KistlContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class KistlContextDisposedException
        : KistlContextException
    {
        public KistlContextDisposedException()
            : base("Context has been disposed. Reusing is not allowed.")
        {
        }

        public KistlContextDisposedException(string message)
            : base(message)
        {
        }

        public KistlContextDisposedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected KistlContextDisposedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class WrongKistlContextException
        : KistlContextException
    {
        public WrongKistlContextException()
            : base("Operation on a Context, where the IPersistanceObject does not belong to is not allowed")
        {
        }

        public WrongKistlContextException(string message)
            : base(message)
        {
        }

        public WrongKistlContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected WrongKistlContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public interface IKistlContextDebugger
    {
        void Created(IKistlContext ctx);
        void Disposed(IKistlContext ctx);
        void Changed(IKistlContext ctx);
    }

    /// <summary>
    /// A simple delegate to provide an interface for creating new contexts.
    /// </summary>
    /// <returns>A newly intialised <see cref="IKistlContext"/>.</returns>
    public delegate IKistlContext GetContext();

    public interface IReadOnlyKistlContext
        : IDisposable
    {
        InterfaceType GetInterfaceType(Type t);
        InterfaceType GetInterfaceType(string typeName);
        InterfaceType GetInterfaceType(IPersistenceObject obj);
        ImplementationType GetImplementationType(Type t);

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
        /// Returns a PersistenceObject Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject;
        /// <summary>
        /// Returns a PersistenceObject Query by InterfaceType
        /// </summary>
        /// <param name="ifType">the interface to look for</param>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType);

        /// <summary>
        /// Returns the List referenced by the given Name.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject;
        /// <summary>
        /// Returns the List referenced by the given type, ID and property name.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="ifType">Type of the Object which holds the ObjectReferenceProperty</param>
        /// <param name="ID">ID of the Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
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
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationCollectionEntry;

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPersistenceObject ContainsObject(InterfaceType type, int ID);

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IEnumerable<IPersistenceObject> AttachedObjects { get; }

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
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids);
        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject;

        System.IO.Stream GetStream(int ID);
        System.IO.FileInfo GetFileInfo(int ID);

        /// <summary>
        /// IsDisposed can be used to detect whether this IKistlContext was aborted with Dispose()
        /// </summary>
        bool IsDisposed { get; }
    }

    /// <summary>
    /// Interface for a LinqToNNN Context.
    /// </summary>
    public interface IKistlContext
        : IReadOnlyKistlContext
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
        /// Submits the changes and returns the number of affected Objects.
        /// </summary>
        /// <remarks>
        /// Only IDataObjects are counded.
        /// </remarks>
        /// <returns>Number of affected Objects</returns>
        int SubmitChanges();

        bool IsReadonly { get; }

        /// <summary>
        /// Creates a new attached IDataObject by Type
        /// </summary>
        /// <param name="ifType">Type of the new IDataObject</param>
        /// <returns>A new attached IDataObject</returns>
        IDataObject Create(InterfaceType ifType);
        /// <summary>
        /// Creates a new attached IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new attached IDataObject</returns>
        T Create<T>() where T : class, IDataObject;

        /// <summary>
        /// Creates a new unattached IPersistenceObject by Type
        /// </summary>
        /// <param name="ifType">Type of the new IPersistenceObject</param>
        /// <returns>A new unattached IPersistenceObject</returns>
        IPersistenceObject CreateUnattached(InterfaceType ifType);
        /// <summary>
        /// Creates a new unattached IPersistenceObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IPersistenceObject</typeparam>
        /// <returns>A new unattached IPersistenceObject</returns>
        T CreateUnattached<T>() where T : class, IPersistenceObject;

        /// <summary>
        /// Creates a new IRelationCollectionEntry by Type
        /// </summary>
        /// <param name="ifType">Type of the new IRelationCollectionEntry</param>
        /// <returns>A new IRelationCollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IRelationCollectionEntry CreateRelationCollectionEntry(InterfaceType ifType);
        /// <summary>
        /// Creates a new IRelationCollectionEntry.
        /// </summary>
        /// <typeparam name="T">Type of the new IRelationCollectionEntry</typeparam>
        /// <returns>A new IRelationCollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T CreateRelationCollectionEntry<T>() where T : IRelationCollectionEntry;

        /// <summary>
        /// Creates a new IValueCollectionEntry by Type
        /// </summary>
        /// <param name="ifType">Type of the new IValueCollectionEntry</param>
        /// <returns>A new IValueCollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType);
        /// <summary>
        /// Creates a new IValueCollectionEntry.
        /// </summary>
        /// <typeparam name="T">Type of the new IValueCollectionEntry</typeparam>
        /// <returns>A new IValueCollectionEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry;

        /// <summary>
        /// Creates a new CompoundObject by Type
        /// </summary>
        /// <param name="ifType">Type of the new CompoundObject</param>
        /// <returns>A new CompoundObject</returns>
        ICompoundObject CreateCompoundObject(InterfaceType ifType);
        /// <summary>
        /// Creates a new CompoundObject.
        /// </summary>
        /// <typeparam name="T">Type of the new CompoundObject</typeparam>
        /// <returns>A new CompoundObject</returns>
        T CreateCompoundObject<T>() where T : ICompoundObject;

        int CreateBlob(System.IO.Stream s, string filename, string mimetype);
        int CreateBlob(System.IO.FileInfo fi, string mimetype);

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

    public interface IKistlQueryProvider : IQueryProvider
    {
    }

    public static class KistlContextQueryableExtensions
    {
        public static IQueryable<T> WithEagerLoading<T>(this IQueryable<T> query)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (query.Provider is IKistlQueryProvider)
            {
                return query.Provider.CreateQuery<T>(
                    System.Linq.Expressions.Expression.Call(typeof(KistlContextQueryableExtensions), "WithEagerLoading", new Type[] { typeof(T) }, query.Expression));
            }
            else
            {
                return query;
            }
        }
    }
}