
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class NHibernatePersistenceObject
       : BasePersistenceObject
    {
        protected NHibernatePersistenceObject(Func<IFrozenContext> lazyCtx)
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
            if (this.Context != null)
                this.Context.Internals().SetModified(this);
        }
    }
}
