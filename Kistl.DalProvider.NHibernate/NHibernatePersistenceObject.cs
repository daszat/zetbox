
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;

    public abstract class NHibernatePersistenceObject
       : BaseServerPersistenceObject
    {
        protected NHibernatePersistenceObject(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public override bool IsAttached
        {
            get { return Context != null; }
        }

        internal void Delete()
        {
            SetDeleted();
        }

        private int _ID;
        public override int ID
        {
            get
            {
                var result = _ID;
                if (NHibernateProxy.ID != 0)
                    result = _ID = NHibernateProxy.ID;

                return result;
            }
            set
            {
                if (this.IsReadonly)
                    throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var oldValue = _ID;
                    var newValue = value;

                    NotifyPropertyChanging("ID", oldValue, newValue);

                    _ID = newValue;
                    if (!this.ObjectState.In(DataObjectState.Detached, DataObjectState.New))
                        NHibernateProxy.ID = newValue;

                    NotifyPropertyChanged("ID", oldValue, newValue);
                }
            }
        }

        protected NHibernateContext OurContext { get { return (NHibernateContext)Context; } }
        public abstract IProxyObject NHibernateProxy { get; }

        public virtual void SaveOrUpdateTo(global::NHibernate.ISession session)
        {
            if (session == null) { throw new ArgumentNullException("session"); }

            switch (this.ObjectState)
            {
                case DataObjectState.New:
                    Kistl.API.Utils.Logging.Log.DebugFormat("Save: {0}#{1}", this.GetType(), this.ID);
                    session.Save(this.NHibernateProxy);
                    break;
                case DataObjectState.Modified:
                case DataObjectState.Unmodified:
                    Kistl.API.Utils.Logging.Log.DebugFormat("SaveOrUpdate: {0}#{1}", this.GetType(), this.ID);
                    // according to NH Documentation this is not needed
                    // session.SaveOrUpdate(this.NHibernateProxy);
                    break;
                case DataObjectState.Deleted:
                    throw new InvalidOperationException("object should be deleted, not saved");
                case DataObjectState.NotDeserialized:
                    throw new InvalidOperationException("object not deserialized");
                default:
                    throw new NotImplementedException(String.Format("unknown DataObjectState encountered: '{0}'", this.ObjectState));
            }
        }

        public readonly List<NHibernatePersistenceObject> ChildrenToDelete = new List<NHibernatePersistenceObject>();
        public readonly List<NHibernatePersistenceObject> ParentsToDelete = new List<NHibernatePersistenceObject>();

        public virtual List<NHibernatePersistenceObject> GetParentsToDelete() { return ParentsToDelete.Where(c => c.ObjectState == DataObjectState.Deleted).Distinct().ToList(); }
        public virtual List<NHibernatePersistenceObject> GetChildrenToDelete() { return ChildrenToDelete.Where(c => c.ObjectState == DataObjectState.Deleted).Distinct().ToList(); }
    }
}
