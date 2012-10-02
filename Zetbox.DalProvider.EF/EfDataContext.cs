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
//#define EAGERLOADING

namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.Objects;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.API.Server.PerfCounter;

    /// <summary>
    /// Entityframework IZetboxContext implementation
    /// </summary>
    public sealed class EfDataContext
        : BaseZetboxDataContext, IZetboxContext, IDisposable
    {
        private static readonly object _lock = new object();

        private readonly EfObjectContext _ctx;
        private bool _connectionManuallyOpened = false;

        private readonly EfImplementationType.EfFactory _implTypeFactory;

        /// <summary>
        /// For Clean Up Session
        /// </summary>
        public override void Dispose()
        {
            try
            {
                base.Dispose();
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_ctx != null)
                {
                    if (_connectionManuallyOpened)
                    {
                        // Close manually, Connection is exposed.
                        // EF wont close it if connection was manually opened
                        _ctx.Connection.Close();
                        // Once is enough
                        _connectionManuallyOpened = false;
                    }
                    _ctx.Dispose();
                }
            }
        }

        /// <summary>
        /// Internal Constructor
        /// </summary>
        public EfDataContext(IMetaDataResolver metaDataResolver, Identity identity, ZetboxConfig config, Func<IFrozenContext> lazyCtx, InterfaceType.Factory iftFactory, EfImplementationType.EfFactory implTypeFactory, IPerfCounter perfCounter)
            : base(metaDataResolver, identity, config, lazyCtx, iftFactory)
        {
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");
            _ctx = new EfObjectContext(config);
            _implTypeFactory = implTypeFactory;
            _perfCounter = perfCounter;

            _ctx.ObjectMaterialized += new ObjectMaterializedEventHandler(_ctx_ObjectMaterialized);

            try
            {
                // First access to EF throws a null ref excetion - TODO: Investigate why
                var objectQuery = _ctx.CreateQuery<BaseServerDataObject_EntityFramework>("[" + GetEntityName(GetInterfaceType(typeof(Identity))) + "]");
                Logging.Server.DebugFormat("There are {0} identities", objectQuery.Count());
            }
            catch(Exception ex)
            {
                Logging.Server.Warn("EF throws an exception during initialization, continue. TODO: Investigate why", ex);
            }
        }

        void _ctx_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (e.Entity is IPersistenceObject)
            {
                ((IPersistenceObject)e.Entity).AttachToContext(this, lazyCtx);
            }
        }

        internal ObjectContext ObjectContext { get { return _ctx; } }
        private IPerfCounter _perfCounter;

        private class QueryCacheEntry
        {
            public QueryCacheEntry(object q)
            {
                Query = q;
            }

            public object Query = null;
        }

        /// <summary>
        /// Type/Query cache
        /// </summary>
        private Dictionary<InterfaceType, QueryCacheEntry> _table = new Dictionary<InterfaceType, QueryCacheEntry>();

        /// <summary>
        /// Returns the EntitySet baseDir of the specified InterfaceType.
        /// </summary>
        /// <param baseDir="intf">the interface to resolve</param>
        /// <returns>the baseDir of the underlying entity set</returns>
        private string GetEntityName(InterfaceType intf)
        {
            if (intf == null)
                throw new ArgumentNullException("intf");
            var rootType = intf.GetRootType();
            return rootType.Type.Name +"EfImpl";
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam baseDir="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public override IQueryable<T> GetQuery<T>()
        {
            CheckDisposed();
            return GetPersistenceObjectQuery<T>();
        }

        #region EagerLoading
#if EAGERLOADING
        private static Dictionary<Type, IList<string>> _includeCache = new Dictionary<Type, IList<string>>();

        private ObjectQuery<BaseServerDataObject_EntityFramework> AddEagerLoading<T>(ObjectQuery<BaseServerDataObject_EntityFramework> src)
        {
            lock (_lock)
            {
                if (!_includeCache.ContainsKey(typeof(T)))
                {
                    var lst = new List<string>();
                    var objClass = metaDataResolver.GetObjectClass(iftFactory(typeof(T)));
                    if (objClass == null) return src;
                    GetIncludeProperties(lst, objClass);
                    _includeCache[typeof(T)] = lst;
                }

                var includes = _includeCache[typeof(T)];
                if (Logging.Linq.IsInfoEnabled)
                {
                    Logging.Linq.InfoFormat("Including {0} to {1}", string.Join(",", includes.ToArray()), typeof(T).Name);
                }
                foreach (var incl in includes)
                {
                    src = src.Include(incl);
                }
                return src;
            }
        }

        private void GetIncludeProperties(List<string> lst, ObjectClass objClass)
        {
            foreach (var cls in objClass.GetObjectHierarchie())
            {
                foreach (var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => p.EagerLoading))
                {
                    if (prop.GetIsList()) continue;
                    lst.Add(prop.Name + Zetbox.API.Helper.ImplementationSuffix);
                }
            }
        }
#endif
        #endregion

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam baseDir="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public override IQueryable<T> GetPersistenceObjectQuery<T>()
        {
            CheckDisposed();
            var interfaceType = iftFactory(typeof(T));
            PrimeQueryCache<T>(interfaceType, ToImplementationType(interfaceType));
            // OfType<T>() at the end adds the security filters
            return ((IQueryable)_table[interfaceType].Query).OfType<T>();
        }

        public System.Collections.IList GetListHack<T>()
            where T : class, IPersistenceObject
        {
            CheckDisposed();
            var interfaceType = iftFactory(typeof(T));
            PrimeQueryCache<T>(interfaceType, ToImplementationType(interfaceType));

            // OfType<T>() at the end adds the security filters
            return ((IQueryable)_table[interfaceType].Query).OfType<T>().ToList();
        }

        private void PrimeQueryCache<T>(InterfaceType interfaceType, ImplementationType implementationType)
            where T : class, IPersistenceObject
        {
            if (!_table.ContainsKey(interfaceType))
            {
                var objectQuery = _ctx.CreateQuery<BaseServerDataObject_EntityFramework>("[" + GetEntityName(interfaceType) + "]");
#if EAGERLOADING
                objectQuery = AddEagerLoading<T>(objectQuery);
#endif

                // The reason is that "GetEntityName" returns a Query to the baseobject 
                // but maybe a derived object is requested. OfType will filter this.
                // This filter has to be added first, so the QueryTranslator can ignore this
                MethodInfo ofType = typeof(ObjectQuery<BaseServerDataObject_EntityFramework>).GetMethod("OfType").MakeGenericMethod(implementationType.Type);
                var query = (IQueryable)ofType.Invoke(objectQuery, new object[] { });

                _table[interfaceType] = new QueryCacheEntry(new QueryTranslator<T>(
                    new EfQueryTranslatorProvider<T>(
                        metaDataResolver, this.identityStore,
                        query, this, iftFactory, _perfCounter)));
            }
        }

        // TODO: Create new override
        public override IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject parent)
        {
            CheckDisposed();
            if (parent == null)
            {
                return this.GetPersistenceObjectQuery<T>().ToList();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param baseDir="type">Type of Object</param>
        /// <param baseDir="ID">ID</param>
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        public override IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            CheckDisposed();
            return AttachedObjects.Where(obj => GetInterfaceType(obj) == type && obj.ID == ID).SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistenceObject> AttachedObjects
        {
            get
            {
                CheckDisposed();
                // Must use OfType -> ObjectStateManager also contains RelationshipEntities
                var result = _ctx.ObjectStateManager
                    .GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted | EntityState.Unchanged)
                    .Select(e => e.Entity)
                    .OfType<IPersistenceObject>()
                    .ToList();
                return result;
            }
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public override int SubmitChanges()
        {
            CheckDisposed();
            int result = 0;
            var ticks = _perfCounter.IncrementSubmitChanges();
            try
            {
                DebugTraceChangedObjects();

                var notifySaveList = _ctx.ObjectStateManager
                    .GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted)
                    .Select(e => e.Entity)
                    .OfType<IDataObject>()
                    .ToList();

                NotifyChanging(notifySaveList);

                try
                {
                    result = _ctx.SaveChanges();
                    Logging.Log.InfoFormat("[{0}] changes submitted.", result);
                }
                catch (OptimisticConcurrencyException cex)
                {
                    Logging.Log.Error("OptimisticConcurrencyException during SubmitChanges", cex);
                    throw new ConcurrencyException(cex);
                }
                catch (UpdateException updex)
                {
                    Logging.Log.Error("UpdateException during SubmitChanges", updex);
                    if (updex.InnerException == null)
                        throw;
                    throw updex.InnerException;
                }

                NotifyChanged(notifySaveList);

                UpdateObjectState();
            }
            finally
            {
                _perfCounter.DecrementSubmitChanges(result, ticks);
            }
            return result;
        }

        private void UpdateObjectState()
        {

            foreach (var o in AttachedObjects.Cast<BaseServerPersistenceObject>().ToList())
            {
                switch (o.ObjectState)
                {
                    case DataObjectState.New:
                    case DataObjectState.Modified:
                        o.SetUnmodified();
                        break;
                    case DataObjectState.Unmodified:
                        // ignore
                        break;
                    default:
                        Logging.Log.WarnFormat("found [{3}] object after SubmitChanges: {0}#{1}", o.GetType().AssemblyQualifiedName, o.ID, o.ObjectState);
                        break;
                }
            }
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects.
        /// This method does not fire any events or methods on added/changed objects. 
        /// It also does not change any IChanged property.
        /// </summary>
        /// <remarks>
        /// Only IDataObjects are counded.
        /// </remarks>
        /// <returns>Number of affected Objects</returns>
        public override int SubmitRestore()
        {
            CheckDisposed();
            DebugTraceChangedObjects();

            try
            {
                var result = _ctx.SaveChanges();
                Logging.Log.InfoFormat("[{0}] changes submitted without Notifications.", result);
                UpdateObjectState();
                return result;
            }
            catch (UpdateException updex)
            {
                Logging.Log.Error("Error during SubmitChanges", updex);
                throw updex.InnerException;
            }
        }

        private void DebugTraceChangedObjects()
        {
            if (Logging.Log.IsDebugEnabled)
            {
                if (_ctx.ObjectStateManager
                    .GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted)
                    .Count() > 0)
                {
                    Logging.Log.Debug("************************* >>>> Submit Changes ******************************");

                    #region Details
                    //foreach (var msgPair in new[]{
                    //    new{State = EntityState.Added,      Label="Insert" },
                    //    new{State = EntityState.Modified,   Label="Update" },
                    //    new{State = EntityState.Deleted,    Label="Delete" },
                    //    // new{State = EntityState.Unchanged,  Label="Unchanged" },
                    //})
                    //{
                    //    var debugList = _ctx.ObjectStateManager
                    //        .GetObjectStateEntries(msgPair.State)
                    //        .Where(e => e.Entity != null)
                    //        .Select(e => e.Entity);
                    //    foreach (var i in debugList)
                    //    {
                    //        Logging.Log.InfoFormat("{0}: {1} -> {2}", msgPair.Label, i.GetType(), i.ToString());
                    //    }
                    //}
                    // Logging.Log.Info(String.Empty);
                    #endregion

                    Logging.Log.Debug("  Added: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Count().ToString());
                    Logging.Log.Debug("  Modified: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Count().ToString());
                    Logging.Log.Debug("  Deleted: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted).Count().ToString());
                    Logging.Log.Debug("  Unchanged: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Unchanged).Count().ToString());

                    Logging.Log.Debug("************************* Submit Changes <<<< ******************************");
                }
                else
                {
                    Logging.Log.Debug("**** >>>> Empty Submit Changes <<<< ****");
                }
            }
            else
            {
                Logging.Log.Info("Submitting changes");
            }
        }

        private int _newIDCounter = Helper.INVALIDID;
        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            var obj = Activator.CreateInstance(ToImplementationType(ifType).Type, lazyCtx);
            var bpo = obj as BaseServerPersistenceObject;
            if (bpo != null)
            {
                checked
                {
                    // Set a temporary ID
                    bpo.ID = --_newIDCounter;
                }
            }
            return obj;
        }

        /// <summary>
        /// Attach an IPersistenceObject. The EntityFramework guarantees the all Objects are unique. No check required.
        /// </summary>
        /// <param baseDir="obj">Object to Attach</param>
        /// <returns>Object Attached</returns>
        public override IPersistenceObject Attach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (obj.Context != null && obj.Context != this) { throw new WrongZetboxContextException("Ef.Attach"); }

            // already attached?
            if (obj.Context == this) return obj;

            var serverObj = (BaseServerPersistenceObject)obj;
            string entityName = GetEntityName(GetInterfaceType(obj));

            _ctx.AttachTo(entityName, obj);
                        
            if (obj.ID < _newIDCounter)// Check ID <-> newIDCounter
            {
                _newIDCounter = obj.ID;
            }

            return base.Attach(obj);
        }

        protected override void AttachAsNew(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (obj.Context != null) { throw new WrongZetboxContextException("Ef.Attach"); }

            var serverObj = (BaseServerPersistenceObject)obj;
            string entityName = GetEntityName(GetInterfaceType(obj));

            _ctx.AddObject(entityName, obj);

            if (obj.ID < _newIDCounter)// Check ID <-> newIDCounter
            {
                _newIDCounter = obj.ID;
            }
            base.AttachAsNew(obj);
        }

        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param baseDir="obj">IDataObject</param>
        public override void Detach(IPersistenceObject obj)
        {
            CheckDisposed();
            _ctx.Detach(obj);
            base.Detach(obj);
        }

        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param baseDir="obj">IPersistenceObject</param>
        protected override void DoDeleteObject(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }

            base.DoDeleteObject(obj);

            _ctx.DeleteObject(obj);
            ((BaseServerPersistenceObject)obj).SetDeleted();
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <param baseDir="ifType">Object Type of the Object to find.</param>
        /// <param baseDir="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override IDataObject Find(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            try
            {
                // See Case 552
                return (IDataObject)this.GetType().FindGenericMethod("Find", new Type[] { ifType.Type }, new Type[] { typeof(int) }).Invoke(this, new object[] { ID });
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam baseDir="T">Object Type of the Object to find.</typeparam>
        /// <param baseDir="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override T Find<T>(int ID)
        {
            CheckDisposed();
            try
            {
                return AttachedObjects.OfType<T>().SingleOrDefault(o => o.ID == ID)
                    ?? GetQuery<T>().First(o => o.ID == ID);
            }
            catch (InvalidOperationException)
            {
                // if the IOEx happened because there is no such object, we 
                // want to report this with an ArgumentOutOfRangeException, 
                // else, we just want to pass the exception on.
                // Since we do not want to rely on the exception string, 
                // we have to check whether there is _any_ object with that ID
                if (GetQuery<T>().Count(o => o.ID == ID) == 0)
                {
                    throw new ArgumentOutOfRangeException("ID", ID, "No such object");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <param baseDir="ifType">Object Type of the Object to find.</param>
        /// <param baseDir="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            try
            {
                // See Case 552
                return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject", new Type[] { ifType.Type }, new Type[] { typeof(int) }).Invoke(this, new object[] { ID });
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam baseDir="T">Object Type of the Object to find.</typeparam>
        /// <param baseDir="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override T FindPersistenceObject<T>(int ID)
        {
            CheckDisposed();
            try
            {
                return AttachedObjects.OfType<T>().SingleOrDefault(o => o.ID == ID)
                    ?? GetPersistenceObjectQuery<T>().First(o => o.ID == ID);
            }
            catch (InvalidOperationException)
            {
                // if the IOEx happened because there is no such object, we 
                // want to report this with an ArgumentOutOfRangeException, 
                // else, we just want to pass the exception on.
                // Since we do not want to rely on the exception string, 
                // we have to check whether there is _any_ object with that ID
                if (!GetPersistenceObjectQuery<T>().Any(o => o.ID == ID))
                {
                    throw new ArgumentOutOfRangeException("ID", ID, "No such object");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <param baseDir="ifType">Object Type of the Object to find.</param>
        /// <param baseDir="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            CheckDisposed();
            try
            {
                // See Case 552
                return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject", new Type[] { ifType.Type }, new Type[] { typeof(Guid) }).Invoke(this, new object[] { exportGuid });
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <typeparam baseDir="T">Object Type of the Object to find.</typeparam>
        /// <param baseDir="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        public override T FindPersistenceObject<T>(Guid exportGuid)
        {
            CheckDisposed();
            T result = (T)AttachedObjects.OfType<T>().OfType<IExportableInternal>().SingleOrDefault(o => o.ExportGuid == exportGuid);
            if (result == null)
            {
                // TODO: Case #1174
                var tmp = GetPersistenceObjectQuery<Zetbox.App.Base.ObjectClass>().FirstOrDefault();
                string sql = string.Format("SELECT VALUE e FROM Entities.[{0}] AS e WHERE e.[ExportGuid] = @guid", GetEntityName(iftFactory(typeof(T))));
                result = _ctx.CreateQuery<T>(sql, new System.Data.Objects.ObjectParameter("guid", exportGuid)).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <param baseDir="ifType">Object Type of the Object to find.</param>
        /// <param baseDir="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public override IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            CheckDisposed();
            try
            {
                // See Case 552
                return ((IEnumerable)this.GetType().FindGenericMethod("FindPersistenceObjects", new Type[] { ifType.Type }, new Type[] { typeof(IEnumerable<Guid>) })
                    .Invoke(this, new object[] { exportGuids })).Cast<IPersistenceObject>();
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <typeparam baseDir="T">Object Type of the Object to find.</typeparam>
        /// <param baseDir="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public override IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids)
        {
            CheckDisposed();
            var guidStrings = exportGuids.Select(g => g.ToString()).ToList();

            if (guidStrings.Count == 0)
                return new List<T>();

            IEnumerable<T> result = null;
            int offset = 0;

            // EF dies from stack overflow exception when trying to fetch 2000+ elements
            // therefore this splits the export guids into batches of 100
            while (offset < guidStrings.Count)
            {
                var tmp = SelectByExportGuid<T>(guidStrings.Skip(offset).Take(100).ToArray());
                result = result == null ? tmp : result.Concat(tmp);
                offset += 100;
            }

            return result;
        }

        private List<T> SelectByExportGuid<T>(string[] cache)
            where T : class, IPersistenceObject
        {
            if (cache == null)
                throw new ArgumentNullException("cache");
            if (cache.Length > 100)
                throw new ArgumentOutOfRangeException("cache", "cache too big");

            if (cache.Length == 0)
                return new List<T>(0);

            // It looks like, that EF loads meta-data only during a CreateQuery<T>("[<<type>>]")
            var interfaceType = iftFactory(typeof(T));
            PrimeQueryCache<T>(interfaceType, ToImplementationType(interfaceType));

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT VALUE e FROM Entities.[{0}] AS e WHERE e.ExportGuid IN {{Guid'", GetEntityName(iftFactory(typeof(T))));
            sql.Append(String.Join("',Guid'", cache));
            sql.Append("'}");
            return _ctx.CreateQuery<T>(sql.ToString()).ToList();
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            CheckDisposed();
            return _implTypeFactory(Type.GetType(t.Type.FullName + "Ef" + Zetbox.API.Helper.ImplementationSuffix + "," + EfProvider.ServerAssembly));
        }

        public override ImplementationType GetImplementationType(Type t)
        {
            CheckDisposed();
            return _implTypeFactory(t);
        }

        protected override int ExecGetSequenceNumber(Guid sequenceGuid)
        {
            return CallGetSequenceNumber(sequenceGuid, "GetSequenceNumber");
        }

        protected override int ExecGetContinuousSequenceNumber(Guid sequenceGuid)
        {
            return CallGetSequenceNumber(sequenceGuid, "GetContinuousSequenceNumber");
        }

        private int CallGetSequenceNumber(Guid sequenceGuid, string procName)
        {
            using (var cmd = _ctx.Connection.CreateCommand())
            {
                cmd.Transaction = _transaction;
                cmd.CommandText = "Entities." + procName;
                cmd.CommandType = CommandType.StoredProcedure;

                var pIn = cmd.CreateParameter();
                pIn.ParameterName = "seqNumber";
                pIn.Value = sequenceGuid;
                pIn.DbType = DbType.Guid;
                pIn.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pIn);

                var pOut = cmd.CreateParameter();
                pOut.ParameterName = "result";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);

                cmd.ExecuteNonQuery();

                return (int)pOut.Value;
            }
        }

        private void OpenEntityConnection()
        {
            if (this._ctx.Connection.State == ConnectionState.Closed)
            {
                _connectionManuallyOpened = true;
                this._ctx.Connection.Open();
            }
        }

        private DbTransaction _transaction;
        protected override bool IsTransactionRunning
        {
            get { return _transaction != null; }
        }

        public override void BeginTransaction()
        {
            if (_transaction != null) throw new InvalidOperationException("A transaction is already running. Nested transaction are not supported");
            OpenEntityConnection();
            _transaction = _ctx.Connection.BeginTransaction();
        }

        public override void CommitTransaction()
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction running");
            _transaction.Commit();
            _transaction = null;
        }

        public override void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }
    }
}
