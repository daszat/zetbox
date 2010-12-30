
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        private readonly ContextCache _attachedObjects;
        
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

            _attachedObjects = new ContextCache(this);
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
            _attachedObjects.Add(obj);
            return base.Attach(obj);
        }

        public override IQueryable<Tinterface> GetQuery<Tinterface>()
        {
            CheckDisposed();
            return GetPersistenceObjectQuery<Tinterface>();
        }

        public override IQueryable<IDataObject> GetQuery(InterfaceType ifType)
        {
            CheckDisposed();
            return GetPersistenceObjectQuery(ifType).Cast<IDataObject>();
        }

        public override IQueryable<Tinterface> GetPersistenceObjectQuery<Tinterface>()
        {
            CheckDisposed();

            var ifType = GetInterfaceType(typeof(Tinterface));
            return PrepareQueryable(ifType).Cast<Tinterface>();
        }

        public override IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);
            return PrepareQueryable(ifType);
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
            Logging.Linq.Warn("Called useless FetchRelation for NHibernate");
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

            var notifySaveList = GetModifiedObjects();

            NotifyChanging(notifySaveList);

            // TODO: refactor this to always talk about DataObjectNHibernateImpl
            FlushSession(notifySaveList.Cast<DataObjectNHibernateImpl>().ToList());

            NotifyChanged(notifySaveList);

            return notifySaveList.Count;
        }

        public override int SubmitRestore()
        {
            CheckDisposed();

            var objects = GetModifiedObjects();

            // TODO: refactor this to always talk about DataObjectNHibernateImpl
            FlushSession(objects.Cast<DataObjectNHibernateImpl>().ToList());

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

        private List<IDataObject> GetModifiedObjects()
        {
            return _attachedObjects
                .Where(obj => obj.ObjectState != DataObjectState.Unmodified)
                    .OfType<IDataObject>()
                    .ToList();
        }

        private void FlushSession(List<DataObjectNHibernateImpl> notifySaveList)
        {
            try
            {
                foreach (var obj in notifySaveList)
                {
                    obj.SaveOrUpdateTo(_nhSession);
                }
                _nhSession.Flush();
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

        public override IDataObject Find(InterfaceType ifType, int ID)
        {
            CheckDisposed();
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
            return (IPersistenceObject)this.GetType()
                .FindGenericMethod("FindPersistenceObject",
                    new Type[] { ifType.Type },
                    new Type[] { typeof(int) })
                .Invoke(this, new object[] { ID });
        }


        private object NhFindById(ImplementationType implType, int id)
        {
            return _nhSession.Load(ToProxyType(implType).FullName, id);
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
            return _implTypeFactory(Type.GetType(String.Format("{0}NHibernate{1},{2}", t.Type.FullName, Kistl.API.Helper.ImplementationSuffix, NHibernateProvider.ServerAssembly)));
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

        protected override bool IsTransactionRunning
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public override void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public override void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject AttachAndWrap(IProxyObject proxy)
        {
            if (proxy == null)
                return null;

            var proxyType = proxy.GetType();
            // dereference NHibernate proxies
            if (proxyType.DeclaringType == null)
                proxyType = proxyType.BaseType;

            var item = (NHibernatePersistenceObject)_attachedObjects.Lookup(GetImplementationType(proxyType.DeclaringType).ToInterfaceType(), proxy.ID);
            if (item == null)
            {
                item = (NHibernatePersistenceObject)Activator.CreateInstance(proxy.ZBoxWrapper, null, proxy);
                item.AttachToContext(this);
            }
            return item;
        }
    }
}
