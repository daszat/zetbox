
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class BaseMemoryPersistenceObject
        : BasePersistenceObject
    {
        protected BaseMemoryPersistenceObject(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
            _objectState = DataObjectState.New;
        }

        public override int ID
        {
            get;
            set;
        }

        public override bool IsAttached
        {
            get { return Context != null; }
        }

        private DataObjectState _objectState;
        public override DataObjectState ObjectState
        {
            get { return _objectState; }
        }

        protected override void SetModified()
        {
            _objectState = DataObjectState.Modified;
            if (this.Context != null) this.Context.Internals().SetModified(this);
        }

        public override void SetNew()
        {
            _objectState = DataObjectState.New;
        }

        public override void SetDeleted()
        {
            _objectState = DataObjectState.Deleted;
        }

        public override void SetUnDeleted()
        {
            _objectState = DataObjectState.Modified;
        }

        public override void ToStream(KistlStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            sw.Write((int)ObjectState);
        }

        public override IEnumerable<IPersistenceObject> FromStream(KistlStreamReader sr)
        {
            var baseResult = base.FromStream(sr);
            sr.ReadConverter(i => _objectState = (DataObjectState)i);
            return baseResult;
        }

        [Obsolete]
        public virtual void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
        }

        [Obsolete]
        public virtual IEnumerable<IPersistenceObject> FromStream(BinaryReader sr)
        {
            return null;
        }

        public override void SetUnmodified()
        {
            _objectState = DataObjectState.Unmodified;
        }

        protected override void AuditPropertyChange(string property, object oldValue, object newValue)
        {
            // do nothing. memory objects are not auditable
        }
    }
}
