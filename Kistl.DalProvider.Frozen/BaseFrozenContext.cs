
namespace Kistl.DalProvider.Frozen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kistl.API;

    /// <summary>
    /// The basic implementation of a frozen context.
    /// </summary>
    public abstract class BaseFrozenContext : IKistlContext
    {

        /// <inheritdoc/>
        public abstract IQueryable<T> GetQuery<T>() where T : class, IDataObject;

        /// <inheritdoc/>
        public abstract IQueryable<IDataObject> GetQuery(InterfaceType ifType);

        /// <inheritdoc/>
        public abstract IEnumerable<IPersistenceObject> AttachedObjects { get; }

        /// <inheritdoc/>
        public abstract IDataObject Find(InterfaceType ifType, int ID);

        /// <inheritdoc/>
        public abstract T Find<T>(int ID) where T : class, IDataObject;

        /// <inheritdoc/>
        public T FindPersistenceObject<T>(Guid exportGuid)
            where T : class, IPersistenceObject
        {
            if (_guidCache.ContainsKey(exportGuid))
            {
                return (T)_guidCache[exportGuid];
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            if (_guidCache.ContainsKey(exportGuid))
            {
                return _guidCache[exportGuid];
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        IPersistenceObject IKistlContext.ContainsObject(InterfaceType type, int ID)
        {
            return Find(type, ID);
        }

        /// <summary>
        /// Gets a value indicating whether or not this Context is disposed. Always false.
        /// </summary>
        bool IKistlContext.IsDisposed { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether or not this Context is read only. Always true.
        /// </summary>
        bool IKistlContext.IsReadonly { get { return true; } }

        #region GUID Cache

        private static IDictionary<Guid, IPersistenceObject> _guidCache;
        protected static void InitialiseGuidCache(IEnumerable<IPersistenceObject> objs)
        {
            // Do the casting dance to do all the work on initialisation
            // then the lookup/find doesn't have to cast
            _guidCache = objs.OfType<IExportableInternal>()
                .Cast<IPersistenceObject>()
                .ToDictionary(ex => ((IExportableInternal)ex).ExportGuid);
        }

        #endregion

        #region not implemented stuff

        /// <summary>Not implemented.</summary>
        IPersistenceObject IKistlContext.Attach(IPersistenceObject obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        void IKistlContext.Detach(IPersistenceObject obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        void IKistlContext.Delete(IPersistenceObject obj)
        {
            throw new ReadOnlyContextException();
        }

        /// <summary>Not implemented.</summary>
        IQueryable<T> IKistlContext.GetPersistenceObjectQuery<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IQueryable<IPersistenceObject> IKistlContext.GetPersistenceObjectQuery(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        List<T> IKistlContext.GetListOf<T>(IDataObject obj, string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        List<T> IKistlContext.GetListOf<T>(InterfaceType ifType, int ID, string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IList<T> IKistlContext.FetchRelation<T>(Guid relId, RelationEndRole role, IDataObject parent)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        int IKistlContext.SubmitChanges() { throw new NotImplementedException(); }

        /// <summary>Not implemented.</summary>
        IDataObject IKistlContext.Create(InterfaceType ifType) { throw new ReadOnlyContextException(); }
        /// <summary>Not implemented.</summary>
        T IKistlContext.Create<T>() { throw new ReadOnlyContextException(); }

        /// <summary>Not implemented.</summary>
        IRelationCollectionEntry IKistlContext.CreateRelationCollectionEntry(InterfaceType ifType) { throw new ReadOnlyContextException(); }
        /// <summary>Not implemented.</summary>
        T IKistlContext.CreateRelationCollectionEntry<T>() { throw new ReadOnlyContextException(); }

        /// <summary>Not implemented.</summary>
        IValueCollectionEntry IKistlContext.CreateValueCollectionEntry(InterfaceType ifType) { throw new ReadOnlyContextException(); }
        /// <summary>Not implemented.</summary>
        T IKistlContext.CreateValueCollectionEntry<T>() { throw new ReadOnlyContextException(); }

        /// <summary>Not implemented.</summary>
        IStruct IKistlContext.CreateStruct(InterfaceType ifType) { throw new ReadOnlyContextException(); }
        /// <summary>Not implemented.</summary>
        T IKistlContext.CreateStruct<T>() { throw new ReadOnlyContextException(); }

        /// <summary>Not implemented.</summary>
        T IKistlContext.FindPersistenceObject<T>(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IPersistenceObject IKistlContext.FindPersistenceObject(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IEnumerable<IPersistenceObject> IKistlContext.FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IEnumerable<T> IKistlContext.FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectCreated
        {
            add { throw new ReadOnlyContextException(); }
            remove { throw new ReadOnlyContextException(); }
        }

        /// <summary>Not implemented.</summary>
        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectDeleted
        {
            add { throw new ReadOnlyContextException(); }
            remove { throw new ReadOnlyContextException(); }
        }

        /// <summary>Not implemented.</summary>
        void IDisposable.Dispose() { }

        #endregion
    }
}
