
namespace Kistl.DalProvider.Frozen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kistl.API;

    /// <summary>
    /// The basic implementation of a frozen context.
    /// </summary>
    public abstract class BaseFrozenContext : IReadOnlyKistlContext
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
        IPersistenceObject IReadOnlyKistlContext.ContainsObject(InterfaceType type, int ID)
        {
            return Find(type, ID);
        }

        /// <summary>
        /// Gets a value indicating whether or not this Context is disposed. Always false.
        /// </summary>
        bool IReadOnlyKistlContext.IsDisposed { get { return false; } }

        #region GUID Cache

        private static IDictionary<Guid, IPersistenceObject> _guidCache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
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
        IQueryable<T> IReadOnlyKistlContext.GetPersistenceObjectQuery<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IQueryable<IPersistenceObject> IReadOnlyKistlContext.GetPersistenceObjectQuery(InterfaceType ifType)
        {
            throw new NotImplementedException();
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

        /// <summary>Not implemented.</summary>
        IList<T> IReadOnlyKistlContext.FetchRelation<T>(Guid relId, RelationEndRole role, IDataObject parent)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        T IReadOnlyKistlContext.FindPersistenceObject<T>(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IPersistenceObject IReadOnlyKistlContext.FindPersistenceObject(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IEnumerable<IPersistenceObject> IReadOnlyKistlContext.FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        IEnumerable<T> IReadOnlyKistlContext.FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        /// <summary>Not implemented.</summary>
        void IDisposable.Dispose()
        {
            // nothing to dispose
        }

        #endregion

        #region IReadOnlyKistlContext Members


        public System.IO.Stream GetStream(int ID)
        {
            throw new NotSupportedException();
        }

        public System.IO.FileInfo GetFileInfo(int ID)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
