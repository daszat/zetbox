
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public abstract class BaseClientPersistenceObject : BasePersistenceObject
    {
        protected BaseClientPersistenceObject(Func<IFrozenContext> lazyCtx)
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
                if(this.Context != null) this.Context.Internals().SetModified(this);
                NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Modified);
            }
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
            if (this.Context != null) this.Context.Internals().SetModified(this);
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Deleted);
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

    public abstract class BaseClientCollectionEntry : BaseClientPersistenceObject
    {
        protected BaseClientCollectionEntry(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Always returns <value>true</value>. CollectionEntries are checked via their navigators or relations.
        /// </summary>
        /// <returns><value>true</value></returns>
        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Always returns <value>true</value>. CollectionEntries are checked via their navigators or relations.
        /// </summary>
        /// <param name="prop">is ignored</param>
        /// <returns><value>true</value></returns>
        protected override string GetPropertyError(string prop)
        {
            return String.Empty;
        }
    }

    /// <summary>
    /// local proxy
    /// </summary>
    public abstract class BaseClientCompoundObject : BaseCompoundObject
    {
        protected BaseClientCompoundObject(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        protected override void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanging(property, oldValue, newValue);
            if (ParentObject != null) ParentObject.NotifyPropertyChanging(ParentProperty, null, null);
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            if (ParentObject != null) ParentObject.NotifyPropertyChanged(ParentProperty, null, null);
        }
    }
}
