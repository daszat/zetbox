
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using global::NHibernate;
    using global::NHibernate.Linq;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;

    public sealed class NHibernateContext
        : BaseKistlDataContext, IKistlServerContext
    {
        private readonly NHibernateImplementationType.Factory _implTypeFactory;
        private readonly global::NHibernate.ISession _nhSession;
        private readonly INHibernateImplementationTypeChecker _implChecker;

        private readonly ContextCache<int> _attachedObjects;
        private readonly ContextCache<IProxyObject> _attachedObjectsByProxy;

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID for the ContextCache
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;


        internal NHibernateContext(
            IMetaDataResolver metaDataResolver,
            Identity identity,
            KistlConfig config,
            Func<IFrozenContext> lazyCtx,
            InterfaceType.Factory iftFactory,
            NHibernateImplementationType.Factory implTypeFactory,
            global::NHibernate.ISession nhSession,
            INHibernateImplementationTypeChecker implChecker)
            : base(metaDataResolver, identity, config, lazyCtx, iftFactory)
        {
            _implTypeFactory = implTypeFactory;
            _nhSession = nhSession;
            _implChecker = implChecker;

            _attachedObjects = new ContextCache<int>(this, item => item.ID);
            _attachedObjectsByProxy = new ContextCache<IProxyObject>(this, item => ((NHibernatePersistenceObject)item).NHibernateProxy);
        }

        public IQueryable<IPersistenceObject> PrepareQueryableGeneric<Tinterface, Timpl>()
        {
            var query = _nhSession.Query<Timpl>();
            return new QueryTranslator<Tinterface>(
                new NHibernateQueryTranslatorProvider<Tinterface>(
                    metaDataResolver, this.identity,
                    query, this, iftFactory, _implChecker))
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

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                checked
                {
                    ((NHibernatePersistenceObject)obj).ID = --_newIDCounter;
                }
            }
            else
            {
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
            }

            _attachedObjects.Add(obj);
            _attachedObjectsByProxy.Add(obj);

            return base.Attach(obj);
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

        public override IList<T> FetchRelation<T>(Guid relationId, RelationEndRole endRole, IDataObject parent)
        {
            //CheckDisposed();
            //if (parent == null)
            //{
            //    return this.GetPersistenceObjectQuery<T>().ToList();
            //}
            //else
            //{
            //    // TODO: #1571 This method expects IF Types, but Impl types are passed
            //    switch (endRole)
            //    {
            //        case RelationEndRole.A:
            //            return GetPersistenceObjectQuery(GetImplementationType(typeof(T)).ToInterfaceType()).Cast<T>().Where(i => i.AObject == parent).ToList();
            //        case RelationEndRole.B:
            //            return GetPersistenceObjectQuery(GetImplementationType(typeof(T)).ToInterfaceType()).Cast<T>().Where(i => i.BObject == parent).ToList();
            //        default:
            //            throw new NotImplementedException(String.Format("Unknown RelationEndRole [{0}]", endRole));
            //    }
            //}
            Logging.Linq.Warn("Called empty FetchRelation for NHibernate");
            return null;
        }

        public override IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            CheckDisposed();
            return AttachedObjects.Where(obj => GetInterfaceType(obj) == type && obj.ID == ID).SingleOrDefault();
        }

        public override IEnumerable<IPersistenceObject> AttachedObjects
        {
            get
            {
                CheckDisposed();
                return _attachedObjects.Cast<IPersistenceObject>();
            }
        }

        public override int SubmitChanges()
        {
            CheckDisposed();
            DebugTraceChangedObjects();

            var objects = GetModifiedObjects();
            var notifyList = objects.OfType<IDataObject>().ToList();

            NotifyChanging(notifyList);

            FlushSession(objects);

            NotifyChanged(notifyList);

            return objects.Count;
        }

        public override int SubmitRestore()
        {
            CheckDisposed();

            var objects = GetModifiedObjects();

            FlushSession(objects);

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
            try
            {
                foreach (var obj in notifySaveList)
                {
                    _attachedObjects.Remove(obj);
                    _attachedObjectsByProxy.Remove(obj);

                    obj.SaveOrUpdateTo(_nhSession);
                }
                _nhSession.Flush();
                foreach (var obj in notifySaveList)
                {
                    _attachedObjects.Add(obj);
                    _attachedObjectsByProxy.Add(obj);
                }
                Logging.Log.InfoFormat("[{0}] changes submitted.", notifySaveList.Count);
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Failed saving transaction", ex);
                throw;
            }
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            // TODO: use Autofac as BytecodeFactory and use a local autofac container to 
            //       replace this A.CI call.
            return Activator.CreateInstance(ToImplementationType(ifType).Type, lazyCtx);
        }

        public override void Delete(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            var nhObj = obj as NHibernatePersistenceObject;
            if (nhObj == null) { throw new ArgumentOutOfRangeException("obj", "should be a NHibernatePersistenceObject, but is a " + obj.GetType()); }

            nhObj.Delete();

            base.Delete(obj);
        }

        public override IDataObject Find(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            if (ID <= Kistl.API.Helper.INVALIDID) { throw new ArgumentOutOfRangeException("ID"); }

            return (IDataObject)this.GetType()
                .FindGenericMethod("Find",
                    new Type[] { ifType.Type },
                    new Type[] { typeof(int) })
                .Invoke(this, new object[] { ID });
        }

        public override T Find<T>(int ID)
        {
            CheckDisposed();
            return (T)NhFindById(ToImplementationType(GetInterfaceType(typeof(T))), ID);
        }

        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();
            if (ID <= Kistl.API.Helper.INVALIDID) { throw new ArgumentOutOfRangeException("ID"); }

            return (IPersistenceObject)this.GetType()
                .FindGenericMethod("FindPersistenceObject",
                    new Type[] { ifType.Type },
                    new Type[] { typeof(int) })
                .Invoke(this, new object[] { ID });
        }

        private IPersistenceObject NhFindById(ImplementationType implType, int ID)
        {
            if (ID <= Kistl.API.Helper.INVALIDID) { throw new ArgumentOutOfRangeException("ID"); }

            return AttachAndWrap((IProxyObject)_nhSession.Load(ToProxyType(implType).FullName, ID));
        }

        private object NhFindByExportGuid(ImplementationType implType, Guid exportGuid)
        {
            return _nhSession
                        .CreateCriteria(ToProxyType(implType).FullName)
                        .Add(global::NHibernate.Criterion.Restrictions.Eq("ExportGuid", exportGuid))
                        .UniqueResult();
        }

        public T FindPersistenceProxy<T>(int ID)
        {
            CheckDisposed();

            var ifType = GetInterfaceType(typeof(T).FullName);

            var result = _attachedObjects.Lookup(ifType, ID);

            if (result != null)
                return result.GetPrivateFieldValue<T>("Proxy");

            var implType = ToImplementationType(ifType);

            var q = NhFindById(implType, ID);
            return (T)q;
        }

        public override T FindPersistenceObject<T>(int ID)
        {
            CheckDisposed();

            var ifType = GetInterfaceType(typeof(T).FullName);

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

        public T FindPersistenceProxy<T>(Guid exportGuid)
        {
            CheckDisposed();
            var result = _attachedObjects.Lookup(exportGuid);

            if (result != null)
                return result.GetPrivateFieldValue<T>("Proxy");

            var implType = GetImplementationType(typeof(T));
            var q = NhFindByExportGuid(implType, exportGuid);

            return (T)q;
        }

        public override T FindPersistenceObject<T>(Guid exportGuid)
        {
            CheckDisposed();

            var result = _attachedObjects.Lookup(exportGuid);

            if (result != null)
                return (T)result;

            var implType = ToImplementationType(GetInterfaceType(typeof(T).FullName));
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
                yield return FindPersistenceObject<T>(guid);
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
            return _implTypeFactory(Type.GetType(String.Format("{0}NHibernate{1},{2}", t.Type.FullName, Kistl.API.Helper.ImplementationSuffix, NHibernateProvider.ServerAssembly), true));
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
            throw new NotImplementedException();
        }

        protected override int ExecGetContinuousSequenceNumber(Guid sequenceGuid)
        {
            throw new NotImplementedException();
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
            if (_transaction == null) throw new InvalidOperationException("No transaction running");
            _transaction.Rollback();
            _transaction = null;
        }

        public IPersistenceObject AttachAndWrap(IProxyObject proxy)
        {
            if (proxy == null)
                return null;

            var item = (NHibernatePersistenceObject)_attachedObjectsByProxy.Lookup(GetImplementationType(proxy.ZBoxWrapper).ToInterfaceType(), proxy);
            if (item == null)
            {
                if (proxy.ID > Kistl.API.Helper.INVALIDID)
                {
                    proxy = (IProxyObject)_nhSession.Load(proxy.ZBoxProxy, proxy.ID);
                }
                item = (NHibernatePersistenceObject)Activator.CreateInstance(proxy.ZBoxWrapper, null, proxy);
                Attach(item);
            }
            return item;
        }
    }
}
