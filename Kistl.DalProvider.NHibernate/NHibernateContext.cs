
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
        private readonly ContextCache _attachedObjects;

        public NHibernateContext(
            IMetaDataResolver metaDataResolver,
            Identity identity,
            KistlConfig config,
            Func<IFrozenContext> lazyCtx,
            InterfaceType.Factory iftFactory,
            NHibernateImplementationType.Factory implTypeFactory,
            global::NHibernate.ISession nhSession)
            : base(metaDataResolver, identity, config, lazyCtx, iftFactory)
        {
            _implTypeFactory = implTypeFactory;
            _nhSession = nhSession;
            _attachedObjects = new ContextCache(this);
        }

        public IQueryable<IPersistenceObject> PrepareQueryableGeneric<Tinterface, Timpl>()
        {
            var query = _nhSession.Query<Timpl>();
            return new QueryTranslator<Tinterface>(
                new NHibernateQueryTranslatorProvider<Tinterface>(
                    metaDataResolver, this.identity,
                    query, this, iftFactory))
                .Cast<IPersistenceObject>();
        }

        public IQueryable<IPersistenceObject> PrepareQueryable(InterfaceType ifType)
        {
            var mi = this.GetType().FindGenericMethod(
                "PrepareQueryableGeneric",
                new[] { ifType.Type, ToImplementationType(ifType).Type },
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
            return PrepareQueryable(ifType).OfType<Tinterface>();
        }

        public override IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);
            return PrepareQueryable(ifType);
        }

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

            FlushSession(notifySaveList);

            NotifyChanged(notifySaveList);

            return notifySaveList.Count;
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

        private List<IDataObject> GetModifiedObjects()
        {
            return _attachedObjects
                .Where(obj => obj.ObjectState != DataObjectState.Unmodified)
                    .OfType<IDataObject>()
                    .ToList();
        }

        private void FlushSession(List<IDataObject> notifySaveList)
        {
            try
            {
                foreach (var obj in notifySaveList)
                {
                    switch (obj.ObjectState)
                    {
                        case DataObjectState.New:
                            _nhSession.Save(obj);
                            break;
                        case DataObjectState.Modified:
                            _nhSession.Update(obj);
                            break;
                        case DataObjectState.Unmodified:
                            // ignore
                            break;
                        case DataObjectState.Deleted:
                            _nhSession.Delete(obj);
                            break;
                        case DataObjectState.NotDeserialized:
                            throw new InvalidOperationException("object not deserialized");
                        default:
                            throw new NotImplementedException(String.Format("unknown DataObjectState encountered: '{0}'", obj.ObjectState));
                    }
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
            return Activator.CreateInstance(ToImplementationType(ifType).Type);
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
            // TODO: T->TImpl
            return _nhSession.Load<T>(ID);
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

        public override T FindPersistenceObject<T>(int ID)
        {
            CheckDisposed();
            // TODO: T->TImpl
            return _nhSession.Load<T>(ID);
        }

        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            CheckDisposed();
            return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject", new Type[] { ifType.Type }, new Type[] { typeof(Guid) }).Invoke(this, new object[] { exportGuid });
        }

        public override T FindPersistenceObject<T>(Guid exportGuid)
        {
            CheckDisposed();
            // TODO: t->timpl
            var q = _nhSession.CreateQuery(String.Format("from {0} e where e.ExportGuid = :guid", typeof(T).FullName));
            q.SetGuid("guid", exportGuid);
            return (T)q.UniqueResult();
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
            return _implTypeFactory(Type.GetType(String.Format("{0}NHibernate{1}+{2}Interface,{3}", t.Type.FullName, Kistl.API.Helper.ImplementationSuffix, t.Type.Name, NHibernateProvider.ServerAssembly)));
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

        internal IPersistenceObject AttachAndWrap(IProxyObject proxy)
        {
            var item = _attachedObjects.Lookup(GetImplementationType(proxy.Interface).ToInterfaceType(), proxy.ID);
            if (item == null)
            {
                item = (IPersistenceObject)Activator.CreateInstance(proxy.ZBoxWrapper, null, proxy);
            }
            return item;
        }
    }
}
