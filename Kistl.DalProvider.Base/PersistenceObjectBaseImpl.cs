
namespace Kistl.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class PersistenceObjectBaseImpl : Kistl.API.BasePersistenceObject
    {
        protected PersistenceObjectBaseImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        private int _ID;
        public override int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID", _ID, value);
                    var oldVal = _ID;
                    _ID = value;
                    NotifyPropertyChanged("ID", oldVal, value);
                }
            }
        }

        private DataObjectState _ObjectState = DataObjectState.Unmodified;
        public override DataObjectState ObjectState
        {
            get
            {
                // Calc Objectstate
                if (_ObjectState != DataObjectState.Deleted)
                {
                    if (ID <= API.Helper.INVALIDID)
                    {
                        _ObjectState = DataObjectState.New;
                    }
                    else if (_ObjectState == DataObjectState.New)
                    {
                        _ObjectState = DataObjectState.Unmodified;
                    }
                }
                return _ObjectState;
            }
        }

        protected override void SetModified()
        {
            if (this.ObjectState == DataObjectState.Unmodified)
            {
                var oldValue = this._ObjectState;
                NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Modified);
                this._ObjectState = DataObjectState.Modified;
                NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Modified);
            }
            // Sadly, this is requiered on _every_ change because some ViewModels
            // rely on the Context.IsModified change event
            // TODO: Improve that!
            if (this.Context != null)
                this.Context.Internals().SetModified(this);
        }

        protected void SetUnmodified()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Unmodified);
            this._ObjectState = DataObjectState.Unmodified;
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Unmodified);
        }

        protected void SetDeleted()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Deleted);
            this._ObjectState = DataObjectState.Deleted;
            if (this.Context != null)
                this.Context.Internals().SetModified(this);
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Deleted);
        }

        public override void SetNew()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.New);
            this._ObjectState = DataObjectState.New;
            if (this.Context != null)
                this.Context.Internals().SetModified(this);
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.New);
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            if (ctx.ContainsObject(ctx.GetInterfaceType(this), this.ID) == null)
            {
                // Object is not in this Context present
                // -> Attach it. Attach will call this Method again!
                ctx.Attach(this);
            }
        }

        public override bool IsAttached
        {
            get
            {
                return Context != null;
            }
        }

        public override void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream((int)ObjectState, sw);
        }

        public override void FromStream(BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromStreamConverter(i => _ObjectState = (DataObjectState)i, sr);
        }
    }
}
