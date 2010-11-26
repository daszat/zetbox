
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
        private readonly List<NHibernatePersistenceObject> _attachedObjects = new List<NHibernatePersistenceObject>();

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
        }

        private IQueryable<IPersistenceObject> PrepareQueryable<T>()
        {
            return _nhSession.Query<T>().Cast<IPersistenceObject>();
        }

        public override IQueryable<T> GetQuery<T>()
        {
            CheckDisposed();
            return GetPersistenceObjectQuery<T>();
        }

        public override IQueryable<IDataObject> GetQuery(InterfaceType ifType)
        {
            CheckDisposed();
            return GetPersistenceObjectQuery(ifType).Cast<IDataObject>();
        }

        public override IQueryable<T> GetPersistenceObjectQuery<T>()
        {
            CheckDisposed();
            // TODO: need to convert T -> TImpl
            return _nhSession.Query<T>();
        }

        public override IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            //CheckInterfaceAssembly("ifType", ifType.Type);

            var mi = this.GetType().FindGenericMethod(
                "PrepareQueryable",
                new[] { ToImplementationType(ifType).Type },
                new Type[0]);

            return (IQueryable<IPersistenceObject>)mi.Invoke(this, new object[0]);
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
            return _implTypeFactory(t.Type);
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
    }
}
