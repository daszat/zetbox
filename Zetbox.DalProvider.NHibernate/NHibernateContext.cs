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

namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using global::NHibernate;
    using global::NHibernate.Linq;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.API.Async;

    public sealed class NHibernateContext
        : BaseZetboxDataContext, IZetboxServerContext
    {
        private readonly NHibernateImplementationType.Factory _implTypeFactory;
        private readonly global::NHibernate.ISession _nhSession;
        private readonly INHibernateImplementationTypeChecker _implChecker;

        private readonly ContextCache<int> _attachedObjects;
        private readonly ContextCache<IProxyObject> _attachedObjectsByProxy;

        private IPerfCounter _perfCounter;

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID for the ContextCache
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;

        internal NHibernateContext(
            IMetaDataResolver metaDataResolver,
            Identity identity,
            ZetboxConfig config,
            Func<IFrozenContext> lazyCtx,
            InterfaceType.Factory iftFactory,
            NHibernateImplementationType.Factory implTypeFactory,
            global::NHibernate.ISessionFactory nhSessionFactory,
            INHibernateImplementationTypeChecker implChecker,
            IPerfCounter perfCounter)
            : base(metaDataResolver, identity, config, lazyCtx, iftFactory)
        {
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");
            _implTypeFactory = implTypeFactory;
            _implChecker = implChecker;

            _attachedObjects = new ContextCache<int>(this, item => item.ID);
            _attachedObjectsByProxy = new ContextCache<IProxyObject>(this, item => ((NHibernatePersistenceObject)item).NHibernateProxy);

            _perfCounter = perfCounter;

            _nhSession = nhSessionFactory.OpenSession(new NHInterceptor(this, lazyCtx));
        }

        public IQueryable<IPersistenceObject> PrepareQueryableGeneric<Tinterface, Tproxy>()
        {
            var query = _nhSession.Query<Tproxy>();
            return new QueryTranslator<Tinterface>(
                new NHibernateQueryTranslatorProvider<Tinterface>(
                    metaDataResolver, this.identityStore,
                    query, this, iftFactory, _implChecker, _perfCounter))
                .Cast<IPersistenceObject>();
        }

        public IQueryable<IPersistenceObject> PrepareQueryable(InterfaceType ifType)
        {
            var proxyType = ToProxyType(ifType);

            var mi = this.GetType().FindGenericMethod(
                "PrepareQueryableGeneric",
                new[] { ifType.Type, proxyType },
                null);
            return (IQueryable<IPersistenceObject>)mi.Invoke(this, new IPersistenceObject[0]);
        }

        public override IPersistenceObject Attach(IPersistenceObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (obj.Context != null && obj.Context != this) { throw new WrongZetboxContextException("Nh.Attach"); }
            if (obj.ID == Helper.INVALIDID) { throw new ArgumentException("NHibernate: cannot attach object without valid ID", "obj"); }

            // already attached?
            if (obj.Context == this) return obj;

            // Check if Object is already in this Context
            var attachedObj = ContainsObject(GetInterfaceType(obj), obj.ID);
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

            _attachedObjects.Add(obj);
            _attachedObjectsByProxy.Add(obj);

            return base.Attach(obj);
        }

        protected override void AttachAsNew(IPersistenceObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (obj.Context != null && obj.Context != this) { throw new WrongZetboxContextException("Nh.Attach"); }
            if (obj.ID > Helper.INVALIDID) { throw new ArgumentException(String.Format("cannot attach object as new with valid ID ({0}#{1})", obj.GetType().FullName, obj.ID), "obj"); }

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                checked
                {
                    ((BasePersistenceObject)obj).ID = --_newIDCounter;
                }
            }
            else if (obj.ID < _newIDCounter)// Check ID <-> newIDCounter
            {
                _newIDCounter = obj.ID;
            }

            _attachedObjects.Add(obj);
            _attachedObjectsByProxy.Add(obj);

            base.AttachAsNew(obj);
        }

        public override IQueryable<Tinterface> GetQuery<Tinterface>()
        {
            CheckDisposed();
            return GetPersistenceObjectQuery<Tinterface>();
        }

        public override IQueryable<Tinterface> GetPersistenceObjectQuery<Tinterface>()
        {
            CheckDisposed();

            var ifType = GetInterfaceType(typeof(Tinterface));
            return PrepareQueryable(ifType).Cast<Tinterface>();
        }

        public override ZbTask<IList<T>> FetchRelationAsync<T>(Guid relationId, RelationEndRole endRole, IDataObject parent)
        {
            return new ZbTask<IList<T>>(ZbTask.Synchron, () =>
            {
                CheckDisposed();
                if (parent == null)
                {
                    return this.GetPersistenceObjectQuery<T>().ToList();
                }
                else
                {
                    switch (endRole)
                    {
                        case RelationEndRole.A:
                            return GetPersistenceObjectQuery<T>().Where(i => i.AObject == parent).ToList();
                        case RelationEndRole.B:
                            return GetPersistenceObjectQuery<T>().Where(i => i.BObject == parent).ToList();
                        default:
                            throw new NotImplementedException(String.Format("Unknown RelationEndRole [{0}]", endRole));
                    }
                }
            });
        }

        public override IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            CheckDisposed();
            return _attachedObjects.Lookup(type, ID);
        }

        public override IEnumerable<IPersistenceObject> AttachedObjects
        {
            get
            {
                CheckDisposed();
                return _attachedObjects.Cast<IPersistenceObject>();
            }
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
                    case DataObjectState.Deleted:
                        _attachedObjects.Remove(o);
                        _attachedObjectsByProxy.Remove(o);
                        break;
                    default:
                        Logging.Log.WarnFormat("found [{3}] object after SubmitChanges: {0}#{1}", o.GetType().AssemblyQualifiedName, o.ID, o.ObjectState);
                        break;
                }
            }
        }

        public override int SubmitChanges()
        {
            CheckDisposed();
            DebugTraceChangedObjects();

            var objects = GetModifiedObjects().Distinct().ToList();
            var notifyList = objects.OfType<IDataObject>().ToList();

            NotifyChanging(notifyList);

            FlushSession(objects);

            NotifyChanged(notifyList);

            UpdateObjectState();

            return objects.Count;
        }

        public override int SubmitRestore()
        {
            CheckDisposed();

            var objects = GetModifiedObjects().Distinct().ToList();

            FlushSession(objects);

            UpdateObjectState();

            return objects.Count;
        }

        private void DebugTraceChangedObjects()
        {
            if (Logging.Log.IsDebugEnabled)
            {
                if (_attachedObjects
                    .Any(obj => obj.ObjectState == DataObjectState.New
                        || obj.ObjectState == DataObjectState.Modified
                        || obj.ObjectState == DataObjectState.Deleted))
                {
                    Logging.Log.Debug("************************* >>>> Submit Changes ******************************");
                    foreach (DataObjectState state in Enum.GetValues(typeof(DataObjectState)))
                    {
                        Logging.Log.DebugFormat(
                            "  {0}: {1}",
                            state,
                            _attachedObjects.Where(obj => obj.ObjectState == state).Count());
                    }
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

        private List<NHibernatePersistenceObject> GetModifiedObjects()
        {
            return _attachedObjects
                .Where(obj => obj.ObjectState != DataObjectState.Unmodified)
                    .OfType<NHibernatePersistenceObject>()
                    .ToList();
        }

        private void FlushSession(List<NHibernatePersistenceObject> notifySaveList)
        {
            var ticks = _perfCounter.IncrementSubmitChanges();
            bool isItMyTransaction = !IsTransactionRunning;
            if (isItMyTransaction)
                BeginTransaction();
            try
            {
                foreach (var obj in RelationTopoSort(notifySaveList.Where(obj => obj.ObjectState == DataObjectState.Deleted)))
                {
                    _attachedObjects.Remove(obj);
                    _attachedObjectsByProxy.Remove(obj);

                    _nhSession.Delete(obj.NHibernateProxy);
                }

                var saveOrUpdateList = notifySaveList.Where(obj => obj.ObjectState != DataObjectState.Deleted).ToList();
                // remove objects from internal caches as the hashkey is about to change
                foreach (var obj in saveOrUpdateList)
                {
                    _attachedObjects.Remove(obj);
                    _attachedObjectsByProxy.Remove(obj);
                }

                // this will change IDs all over the place, depending on NHibernate's opinions on efficient updating
                foreach (var obj in saveOrUpdateList)
                {
                    obj.SaveOrUpdateTo(_nhSession);
                }

                // force outstanding changes into DB
                _nhSession.Flush();

                foreach (var obj in saveOrUpdateList)
                {
                    _attachedObjects.Add(obj);
                    _attachedObjectsByProxy.Add(obj);
                }

                if (isItMyTransaction)
                    CommitTransaction();
                Logging.Log.InfoFormat("[{0}] changes submitted.", notifySaveList.Count);
            }
            catch (StaleObjectStateException ex)
            {
                if (isItMyTransaction)
                    RollbackTransaction();
                var error = string.Format("Failed saving transaction: Concurrent modification on {0}#{1}", ex.EntityName, ex.Identifier);
                throw new ConcurrencyException(error, ex);
            }
            catch (Exception ex)
            {
                if (isItMyTransaction)
                    RollbackTransaction();
                Logging.Log.Error("Failed saving transaction", ex);
                throw;
            }
            finally
            {
                _perfCounter.DecrementSubmitChanges(notifySaveList.Count, ticks);
            }
        }

        /// <summary>
        /// Orders the specified input topologically by required relations. The output ordering can be used for deleting objects without violating FKs.
        /// </summary>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Topological_ordering#CITEREFKahn1962
        /// </remarks>
        private IEnumerable<NHibernatePersistenceObject> RelationTopoSort(IEnumerable<NHibernatePersistenceObject> input)
        {
            List<NHibernatePersistenceObject> result = new List<NHibernatePersistenceObject>();
            var edges = input.ToDictionary(i => i, i => i.GetChildrenToDelete());

            // > L ← Empty list that will contain the sorted elements
            // will be yielded directly

            // > S ← Set of all nodes with no incoming edges
            var S = new Stack<NHibernatePersistenceObject>(edges.Where(kvp => kvp.Value.Count == 0).Select(kvp => kvp.Key));

            // > while S is non-empty do
            while (S.Count > 0)
            {
                // > remove a node n from S
                var n = S.Pop();

                // > insert n into L
                result.Add(n);

                // > for each node m with an edge e from n to m do
                foreach (var m in n.GetParentsToDelete())
                {
                    // > remove edge e from the graph
                    edges[m].Remove(n);

                    // > if m has no other incoming edges then
                    if (edges[m].Count == 0)
                    {
                        // > insert m into S
                        S.Push(m);
                    }
                }
            }

            // > if graph has edges then
            if (edges.Any(e => e.Value.Count > 0))
            {
                // > output error message (graph has at least one cycle)
                var cycle = String.Join(", ",
                    edges.Where(kvp => kvp.Value.Count > 0)
                        .Select(kvp => string.Format("{0}#{1} => < {2} >",
                            kvp.Key.GetType(),
                            kvp.Key.ID,
                            string.Join(", ", kvp.Value.Select(v => string.Format("{0}#{1}", v.GetType(), v.ID)).ToArray()))).ToArray());
                throw new InvalidOperationException("deletion cycle detected: " + cycle);
            }
            // > else 
            // >     output message (proposed topologically sorted order: L)
            // already done by yielding

            return result;
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            // TODO: use Autofac as BytecodeFactory and use a local autofac container to 
            //       replace this A.CI call.
            return Activator.CreateInstance(ToImplementationType(ifType).Type, lazyCtx);
        }

        protected override void DoDeleteObject(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            var nhObj = obj as NHibernatePersistenceObject;
            if (nhObj == null) { throw new ArgumentOutOfRangeException("obj", "should be a NHibernatePersistenceObject, but is a " + obj.GetType()); }

            base.DoDeleteObject(obj);

            nhObj.Delete();
        }

        public override ZbTask<IDataObject> FindAsync(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            return new ZbTask<IDataObject>(ZbTask.Synchron, () =>
            {
                try
                {
                    return (IDataObject)this.GetType()
                        .FindGenericMethod("Find",
                            new Type[] { ifType.Type },
                            new Type[] { typeof(int) })
                        .Invoke(this, new object[] { ID });
                }
                catch (System.Reflection.TargetInvocationException ex)
                {
                    if (ex.InnerException is ArgumentOutOfRangeException)
                    {
                        // wrap in new AOORE, to preserve all stack traces
                        throw new ArgumentOutOfRangeException("ID", ex);
                    }
                    else
                    {
                        // huhu, something bad happened
                        throw;
                    }
                }
            });
        }

        public override ZbTask<T> FindAsync<T>(int ID)
        {
            CheckDisposed();

            return new ZbTask<T>(ZbTask.Synchron, () =>
            {
                var result = FindPersistenceObject<T>(ID);
                if (result == null) { throw new ArgumentOutOfRangeException("ID", String.Format("no object of type {0} with ID={1}", typeof(T).FullName, ID)); }
                return result;
            });
        }

        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            try
            {
                return (IPersistenceObject)this.GetType()
                    .FindGenericMethod("FindPersistenceObject",
                        new Type[] { ifType.Type },
                        new Type[] { typeof(int) })
                    .Invoke(this, new object[] { ID });
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                if (ex.InnerException is ArgumentOutOfRangeException)
                {
                    // wrap in new AOORE, to preserve all stack traces
                    throw new ArgumentOutOfRangeException("ID", ex);
                }
                else
                {
                    // huhu, something bad happened
                    throw;
                }
            }
        }

        private IProxyObject NhFindById(ImplementationType implType, int ID)
        {
            if (ID <= Zetbox.API.Helper.INVALIDID) { throw new ArgumentOutOfRangeException("ID", ID, "Cannot ask NHibernate for INVALIDID"); }

            return (IProxyObject)_nhSession
                        .CreateCriteria(ToProxyType(implType))
                        .Add(global::NHibernate.Criterion.Restrictions.Eq("ID", ID))
                        .UniqueResult();
        }

        private IProxyObject NhFindByExportGuid(ImplementationType implType, Guid exportGuid)
        {
            return (IProxyObject)_nhSession
                        .CreateCriteria(ToProxyType(implType))
                        .Add(global::NHibernate.Criterion.Restrictions.Eq("ExportGuid", exportGuid))
                        .UniqueResult();
        }

        public override T FindPersistenceObject<T>(int ID)
        {
            CheckDisposed();

            var ifType = GetInterfaceType(typeof(T));

            var result = _attachedObjects.Lookup(ifType, ID);

            if (result != null)
                return (T)result;

            var implType = ToImplementationType(ifType);

            var q = NhFindById(implType, ID);
            return (T)AttachAndWrap((IProxyObject)q);
        }

        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            CheckDisposed();
            return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject", new Type[] { ifType.Type }, new Type[] { typeof(Guid) }).Invoke(this, new object[] { exportGuid });
        }

        public override T FindPersistenceObject<T>(Guid exportGuid)
        {
            CheckDisposed();

            var result = _attachedObjects.Lookup(exportGuid);

            if (result != null)
                return (T)result;

            var implType = ToImplementationType(GetInterfaceType(typeof(T)));
            var q = NhFindByExportGuid(implType, exportGuid);
            return (T)AttachAndWrap((IProxyObject)q);
        }

        public override IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            CheckDisposed();
            return ((System.Collections.IEnumerable)this.GetType()
                .FindGenericMethod("FindPersistenceObjects",
                    new Type[] { ifType.Type },
                    new Type[] { typeof(IEnumerable<Guid>) })
                .Invoke(this, new object[] { exportGuids }))
                .Cast<IPersistenceObject>();
        }

        public override IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids)
        {
            CheckDisposed();
            // TODO: do batching here
            foreach (var guid in exportGuids)
            {
                var result = FindPersistenceObject<T>(guid);
                if (result != null)
                    yield return result;
            }
        }

        public override ImplementationType GetImplementationType(Type t)
        {
            CheckDisposed();
            return _implTypeFactory(t);
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            CheckDisposed();
            return _implTypeFactory(Type.GetType(String.Format("{0}NHibernate{1},{2}", t.Type.FullName, Zetbox.API.Helper.ImplementationSuffix, NHibernateProvider.ServerAssembly), true));
        }

        internal Type ToProxyType(ImplementationType implType)
        {
            var proxyType = implType.Type.GetNestedTypes().Where(cls => cls.Name.EndsWith("Proxy")).Single();
            return proxyType;
        }

        internal Type ToProxyType(InterfaceType ifType)
        {
            return ToProxyType(ToImplementationType(ifType));
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
            using (var cmd = _nhSession.Connection.CreateCommand())
            {
                if (_transaction != null)
                    _transaction.Enlist(cmd);
                cmd.CommandText = "dbo.\"" + procName + "\"";
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

        private ITransaction _transaction;
        protected override bool IsTransactionRunning
        {
            get { return _transaction != null; }
        }

        public override void BeginTransaction()
        {
            if (_transaction != null) throw new InvalidOperationException("A transaction is already running. Nested transaction are not supported");
            _transaction = _nhSession.BeginTransaction();
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

        /// <summary>
        /// Given an NH-proxy, load a full Impl-object. This may force going to the database to find the concrete type.
        /// </summary>
        public IPersistenceObject AttachAndWrap(IProxyObject proxy)
        {
            if (proxy == null)
                return null;

            var implType = GetImplType(proxy);
            var ift = GetImplementationType(implType).ToInterfaceType();
            var item = (NHibernatePersistenceObject)_attachedObjectsByProxy.Lookup(ift, proxy);
            if (item == null)
            {
                // re-load proxy to avoid aliasing issues from unloaded proxies, but only if there is the possibility, that this might be a _different_ sub-class

                if (proxy.ID > Zetbox.API.Helper.INVALIDID)
                {
                    var objClass = metaDataResolver.GetObjectClass(ift);
                    if (objClass != null && metaDataResolver.GetObjectClass(ift).SubClasses.Count > 0)
                    {
                        proxy = (IProxyObject)_nhSession.Load(proxy.ZetboxProxy, proxy.ID);
                        item = (NHibernatePersistenceObject)ContainsObject(ift, proxy.ID);
                    }
                }

                if (item == null)
                {
                    item = (NHibernatePersistenceObject)Activator.CreateInstance(proxy.ZetboxWrapper, lazyCtx, proxy);

                    if (proxy.ID == Zetbox.API.Helper.INVALIDID)
                    {
                        AttachAsNew(item);
                    }
                    else
                    {
                        item = (NHibernatePersistenceObject)Attach(item);
                    }
                }
            }
            return item;
        }

        /// <summary>
        /// Get only the underlying ID from a proxy. This avoids going to the database when an object is not loaded yet.
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public int GetIdFromProxy(IProxyObject proxy)
        {
            if (proxy == null) throw new ArgumentNullException("proxy");

            // existing object
            if (proxy.ID > Zetbox.API.Helper.INVALIDID)
                return proxy.ID;

            // new object
            var implType = GetImplType(proxy);
            var ift = GetImplementationType(implType).ToInterfaceType();
            var item = (NHibernatePersistenceObject)_attachedObjectsByProxy.Lookup(ift, proxy);
            if (item == null)
            {
                // ... without wrapper
                item = (NHibernatePersistenceObject)Activator.CreateInstance(proxy.ZetboxWrapper, lazyCtx, proxy);
                AttachAsNew(item);
            }
            return item.ID;
        }

        private static Type GetImplType(IProxyObject proxy)
        {
            // search for "our" implementation
            var t = proxy.GetType();
            while (!t.IsNested && t != typeof(object))
            {
                t = t.BaseType;
            }
            var implType = t.DeclaringType;
            return implType;
        }
    }
}
