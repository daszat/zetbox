
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
        protected BaseClientPersistenceObject(Func<IReadOnlyKistlContext> lazyCtx)
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
        }

        internal void SetUnmodified()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Unmodified);
            this._ObjectState = DataObjectState.Unmodified;
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Unmodified);
        }

        internal void SetDeleted()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Deleted);
            this._ObjectState = DataObjectState.Deleted;
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

    public abstract class BaseClientDataObject : BaseClientPersistenceObject, IDataObject
    {
        protected BaseClientDataObject(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Fires an Event before an Object is saved.
        /// </summary>
        public virtual void NotifyPreSave() { }
        /// <summary>
        /// Fires an Event after an Object is saved.
        /// </summary>
        public virtual void NotifyPostSave() { }

        /// <summary>
        /// Fires an Event after an Object is created.
        /// </summary>
        public virtual void NotifyCreated() { }
        /// <summary>
        /// Fires an Event before an Object is deleted.
        /// </summary>
        public virtual void NotifyDeleting() { }

        public virtual void UpdateParent(string propertyName, int? id)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// </summary>
        public AccessRights CurrentAccessRights { get; protected set; }
    }

    public abstract class BaseClientCollectionEntry : BaseClientPersistenceObject
    {
        protected BaseClientCollectionEntry(Func<IReadOnlyKistlContext> lazyCtx)
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
        protected BaseClientCompoundObject(Func<IReadOnlyKistlContext> lazyCtx)
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
