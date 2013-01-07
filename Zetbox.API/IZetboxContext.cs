// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Zetbox.API.Async;

namespace Zetbox.API
{
    [Serializable]
    public class ZetboxContextException
        : Exception
    {
        public ZetboxContextException()
            : base()
        {
        }

        public ZetboxContextException(string message)
            : base(message)
        {
        }

        public ZetboxContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ZetboxContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class ZetboxContextDisposedException
        : ZetboxContextException
    {
        public ZetboxContextDisposedException()
            : base("Context has been disposed. Reusing is not allowed.")
        {
        }

        public ZetboxContextDisposedException(string message)
            : base(message)
        {
        }

        public ZetboxContextDisposedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ZetboxContextDisposedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class WrongZetboxContextException
        : ZetboxContextException
    {
        public WrongZetboxContextException()
            : base("Operation on a Context, where the IPersistanceObject does not belong to is not allowed")
        {
        }

        public WrongZetboxContextException(string message)
            : base(message)
        {
        }

        public WrongZetboxContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected WrongZetboxContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class ConcurrencyException
        : ZetboxContextException
    {
        private const string DEFAULT_MESSAGE = "At least one object has changed between fetch and submit changes";

        [NonSerialized]
        private IEnumerable<IDataObject> objects;
        public IEnumerable<IDataObject> Objects
        {
            get
            {
                return objects;
            }
        }

        public ConcurrencyException()
            : base(DEFAULT_MESSAGE)
        {
        }

        public ConcurrencyException(string message)
            : base(message)
        {
        }

        public ConcurrencyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ConcurrencyException(Exception inner)
            : base(DEFAULT_MESSAGE, inner)
        {
        }

        protected ConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ConcurrencyException(IEnumerable<IDataObject> objects)
            : base(string.Format("{0} object(s) has changed between fetch and submit changes", objects != null ? objects.Count().ToString() : "?"))
        {
            this.objects = objects;
        }
    }

    public interface IZetboxContextDebugger
    {
        void Created(IZetboxContext ctx);
    }

    public interface IReadOnlyZetboxContext
        : IDisposable
    {
        InterfaceType GetInterfaceType(Type t);
        InterfaceType GetInterfaceType(string typeName);
        InterfaceType GetInterfaceType(IPersistenceObject obj);
        InterfaceType GetInterfaceType(ICompoundObject obj);
        ImplementationType GetImplementationType(Type t);
        ImplementationType ToImplementationType(InterfaceType t);

        Zetbox.API.AccessRights GetGroupAccessRights(InterfaceType ifType);

        /// <summary>
        /// Fired when the Context is beeing disposed.
        /// </summary>
        event GenericEventHandler<IReadOnlyZetboxContext> Disposing;

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        IQueryable<T> GetQuery<T>() where T : class, IDataObject;

        /// <summary>
        /// Returns the List referenced by the given Name.
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject;
        /// <summary>
        /// Returns the List referenced by the given Name.
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        ZbTask<List<T>> GetListOfAsync<T>(IDataObject obj, string propertyName) where T : class, IDataObject;

        /// <summary>
        /// Fetches all collection entries of a given Relation (specified by <paramref name="relationId"/>)
        /// which reference the given <paramref name="container"/> on the side <paramref name="role"/>
        /// of the relation. Mostly for internal use.
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <typeparam name="T">Type of the IRelationEntry element</typeparam>
        /// <param name="relationId">Specifies which relation to fetch</param>
        /// <param name="role">Specifies how to interpret the container</param>
        /// <param name="container">The container of the requested collection</param>
        /// <returns></returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationEntry;

        /// <summary>
        /// Fetches all collection entries of a given Relation (specified by <paramref name="relationId"/>)
        /// which reference the given <paramref name="container"/> on the side <paramref name="role"/>
        /// of the relation. Mostly for internal use.
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <typeparam name="T">Type of the IRelationEntry element</typeparam>
        /// <param name="relationId">Specifies which relation to fetch</param>
        /// <param name="role">Specifies how to interpret the container</param>
        /// <param name="container">The container of the requested collection</param>
        /// <returns></returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        ZbTask<IList<T>> FetchRelationAsync<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationEntry;

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
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        IDataObject Find(InterfaceType ifType, int ID);

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        ZbTask<IDataObject> FindAsync(InterfaceType ifType, int ID);
        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        T Find<T>(int ID) where T : class, IDataObject;

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        ZbTask<T> FindAsync<T>(int ID) where T : class, IDataObject;

        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID);
        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject;

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid);
        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject;

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids);
        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// TODO: Move to IZetboxContextInternals interface
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject;

        System.IO.Stream GetStream(int ID);
        System.IO.FileInfo GetFileInfo(int ID);
        ZbTask<System.IO.Stream> GetStreamAsync(int ID);
        ZbTask<System.IO.FileInfo> GetFileInfoAsync(int ID);

        /// <summary>
        /// IsDisposed can be used to detect whether this IZetboxContext was aborted with Dispose()
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Dictionary for storing transient state
        /// </summary>
        /// <remarks>The caller is responsible for disposing stored objects. See <see cref="Disposing"/> Event.</remarks>
        IDictionary<object, object> TransientState { get; }
    }

    /// <summary>
    /// A marker interface to denote the context that has all "Frozen" objects in memory.
    /// </summary>
    public interface IFrozenContext
        : IReadOnlyZetboxContext
    {
    }

    /// <summary>
    /// Interface for a LinqToNNN Context.
    /// </summary>
    public interface IZetboxContext
        : IReadOnlyZetboxContext
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

        /// <summary>
        /// Fired when the AttachedObject collection has been changed.
        /// </summary>
        event GenericEventHandler<IZetboxContext> Changed;

        /// <summary>
        /// Indicates that the Zetbox Context has some modified, added or deleted items
        /// </summary>
        bool IsModified { get; }

        /// <summary>
        /// Is fires when <see cref="IsModified"/> was changed
        /// </summary>
        event EventHandler IsModifiedChanged;

        /// <summary>
        /// Gets the next sequence number of the given Sequence.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// InvalidOperationException is thrown if the Sequence is a continuous sequence.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// ArgumentOutOfRangeException is thrown if the Sequence cannot be found.
        /// </exception>
        /// <param name="sequenceGuid">Guid of the Sequence object</param>
        /// <returns>the next sequence number</returns>
        int GetSequenceNumber(Guid sequenceGuid);

        /// <summary>
        /// Gets the next sequence number of the given Sequence. The sequence is guaranteed to be continous, without any gaps.
        /// <see cref="BeginTransaction"/> has to be called bevore using this method to guarantee that the sequence is realy continuously.
        /// </summary>
        /// <remarks>
        /// <para>This exaample shows the proves of creating an invoice:</para>
        /// <example>
        /// public void CreateInvoice(Invoice obj)
        /// {
        ///     // Client has created and filled the invoice object.
        ///     
        ///     // Start a transaction
        ///     obj.Context.BeginTransaction();
        ///     
        ///     // setup invoice
        ///     obj.InvoiceNumber = obj.Context.GetContinuousSequenceNumber(...);
        ///     obj.Date = DateTime.Today;
        ///     // more setup
        ///     ... 
        ///     
        ///     // Submit &amp; commit
        ///     obj.Context.SubmitChanges();
        ///     obj.Context.CommitTransaction();
        /// }
        /// </example>
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// InvalidOperationException is thrown if no transaction is running or the Sequence is no continuous sequence.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// ArgumentOutOfRangeException is thrown if the Sequence cannot be found.
        /// </exception>
        /// <param name="sequenceGuid">Guid of the Sequence object</param>
        /// <returns>the next sequence number</returns>
        int GetContinuousSequenceNumber(Guid sequenceGuid);

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <remarks>
        /// Nested transactions are not supported.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// InvalidOperationException is thrown if a transaction is already running.
        /// </exception>
        void BeginTransaction();
        /// <summary>
        /// Commits the current transaction
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// InvalidOperationException is thrown if no transaction is currently running.
        /// </exception>
        void CommitTransaction();
        /// <summary>
        /// Rollback the transaction. The context is useless after rollback. Equivalent to calling Dispose().
        /// </summary>
        /// <remarks>
        /// If a transaction is running, Dispose() will implicitly call RollbackTransaction.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// InvalidOperationException is thrown if no transaction is currently running.
        /// </exception>
        void RollbackTransaction();

        /// <summary>
        /// Elevates your priviliges to the highest level (equivalent to iddqd).
        /// </summary>
        /// <remarks>
        /// Has no effect on the server because you have already the highest priviliges.
        /// </remarks>
        /// <param name="elevatedMode">true, to get highest priviliges</param>
        void SetElevatedMode(bool elevatedMode);
        /// <summary>
        /// Indicates if the context is in a elevated mode
        /// </summary>
        bool IsElevatedMode { get; }
        /// <summary>
        /// Inidcates that the context has entered or leaded the elevated mode
        /// </summary>
        event EventHandler IsElevatedModeChanged;
    }

    public interface IZetboxContextInternals
    {
        void SetModified(IPersistenceObject obj);

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
        /// Attaches the specified object and sets its ObjectState to new.
        /// This functionality is needed when deserializing from external sources (e.g.: import, facade)
        /// </summary>
        /// <param name="obj">the "new" object</param>
        void AttachAsNew(IPersistenceObject obj);

        /// <summary>
        /// Stores a blob stream
        /// </summary>
        /// <param name="s">Stream to store</param>
        /// <param name="exportGuid">The export guid of the blob to store</param>
        /// <param name="timestamp">The created on timestamp of the blob to store</param>
        /// <param name="filename">optional filename</param>
        /// <returns>The relative storage path. Must be stored in Blob.StoragePath</returns>
        string StoreBlobStream(System.IO.Stream s, Guid exportGuid, DateTime timestamp, string filename);

        /// <summary>
        /// Creates a new IRelationEntry by Type
        /// </summary>
        /// <param name="ifType">Type of the new IRelationEntry</param>
        /// <returns>A new IRelationEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IRelationEntry CreateRelationCollectionEntry(InterfaceType ifType);
        /// <summary>
        /// Creates a new IRelationEntry.
        /// </summary>
        /// <typeparam name="T">Type of the new IRelationEntry</typeparam>
        /// <returns>A new IRelationEntry</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        T CreateRelationCollectionEntry<T>() where T : IRelationEntry;

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
        /// Returns a PersistenceObject Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject;

        /// <summary>
        /// Returns a list of all objects of the specified type. This method is marked as internal, because it is used only for very specific use-cases in the guts of the product.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        List<IDataObject> GetAll(InterfaceType t);

        int IdentityID { get; }
    }

    public interface IDebuggingZetboxContext : IZetboxContext
    {
        StackTrace CreatedAt { get; }
        StackTrace DisposedAt { get; }
    }

    public interface IZetboxQueryProvider : IQueryProvider
    {
    }

    /// <summary>
    /// TODO: Remove that class when Case #1763 is solved
    /// </summary>
    public static class ZetboxContextExtensions
    {
        public static IZetboxContextInternals Internals(this IReadOnlyZetboxContext ctx)
        {
            return (IZetboxContextInternals)ctx;
        }
    }

    public static class ZetboxContextQueryableExtensions
    {
        public static IQueryable<T> WithEagerLoading<T>(this IQueryable<T> query)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (query.Provider is IZetboxQueryProvider)
            {
                return query.Provider.CreateQuery<T>(
                    System.Linq.Expressions.Expression.Call(typeof(ZetboxContextQueryableExtensions), "WithEagerLoading", new Type[] { typeof(T) }, query.Expression));
            }
            else
            {
                return query;
            }
        }
    }

    public static class ContextTransientStateExtensions
    {
        public static TCacheItem TransientState<TCacheItem, TKey>(this IReadOnlyZetboxContext ctx, string transientCacheKey, TKey key, Func<TCacheItem> getter)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (string.IsNullOrEmpty(transientCacheKey)) throw new ArgumentNullException("transientCacheKey");
            if (getter == null) throw new ArgumentNullException("getter");

            // Ensure cache
            // Ensure transient cache
            if (!ctx.TransientState.ContainsKey(transientCacheKey))
            {
                ctx.TransientState[transientCacheKey] = new Dictionary<TKey, TCacheItem>();
            }
            Dictionary<TKey, TCacheItem> cache = (Dictionary<TKey, TCacheItem>)ctx.TransientState[transientCacheKey];

            TCacheItem result;
            if (!cache.TryGetValue(key, out result))
            {
                result = getter();
                cache[key] = result;
                return result;
            }
            else
            {
                // Cachehit
                // Maybe count it here
                return result;
            }
        }

        public static TCacheItem TransientState<TCacheItem>(this IReadOnlyZetboxContext ctx, string transientCacheKey, Func<TCacheItem> getter)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (string.IsNullOrEmpty(transientCacheKey)) throw new ArgumentNullException("transientCacheKey");
            if (getter == null) throw new ArgumentNullException("getter");

            if (!ctx.TransientState.ContainsKey(transientCacheKey))
            {
                var result = getter();
                ctx.TransientState[transientCacheKey] = result;
                return result;
            }
            else
            {
                // Cachehit
                // Maybe count it here
                return (TCacheItem)ctx.TransientState[transientCacheKey];
            }
        }
    }
}