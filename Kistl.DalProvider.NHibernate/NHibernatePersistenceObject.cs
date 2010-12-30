
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
        }

        public override bool IsAttached
        {
            get { return Context != null; }
        }

        private DataObjectState _objectState = DataObjectState.Unmodified;
        public override DataObjectState ObjectState
        {
            get
            {
                // Calc Objectstate
                if (_objectState != DataObjectState.Deleted)
                {
                    if (NHibernateProxy.ID == 0)
                    {
                        _objectState = DataObjectState.New;
                    }
                    else if (_objectState == DataObjectState.New)
                    {
                        _objectState = DataObjectState.Unmodified;
                    }
                }
                return _objectState;
            }
        }

        private int _ID;
        public override int ID
        {
            get
            {
                var result = _ID;
                if (this.ObjectState != DataObjectState.New)
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
                    if (this.ObjectState != DataObjectState.New)
                        NHibernateProxy.ID = newValue;

                    NotifyPropertyChanged("ID", oldValue, newValue);
                }
            }
        }

        protected override void SetModified()
        {
            _objectState = DataObjectState.Modified;
            if (this.Context != null)
                this.Context.Internals().SetModified(this);
        }

        protected NHibernateContext OurContext { get { return (NHibernateContext)Context; } }
        public abstract IProxyObject NHibernateProxy { get; }
    }
}
