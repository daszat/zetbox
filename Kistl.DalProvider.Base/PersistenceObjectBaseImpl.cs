
namespace Kistl.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public abstract class PersistenceObjectBaseImpl : Kistl.API.BasePersistenceObject
    {
        private readonly IAuditable _auditable;

        protected PersistenceObjectBaseImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
            _auditable = this as IAuditable;
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
            BinarySerializer.ToStream((int)CurrentAccessRights, sw);
        }

        public override IEnumerable<IPersistenceObject> FromStream(BinaryReader sr)
        {
            var baseResult = base.FromStream(sr);
            BinarySerializer.FromStreamConverter(i => _ObjectState = (DataObjectState)i, sr);
            BinarySerializer.FromStreamConverter(i => ApplyRightsFromStream((API.AccessRights)i), sr); 
            return baseResult;
        }

        protected virtual void ApplyRightsFromStream(Kistl.API.AccessRights rights)
        {
            // cannot handle it, but some derived classes are able to
        }

        #region Auditing

        protected virtual void SaveAudits()
        {
            if (_auditable == null) return;

            if (AuditLog != null)
            {
                foreach (var msg in AuditLog.Values)
                {
                    var entry = Context.CreateCompoundObject<AuditEntry>();
                    entry.Identity = "unbekannt";
                    entry.MessageFormat = "{0} geändert von '{1}' auf '{2}'";
                    entry.PropertyName = msg.property;
                    entry.OldValue = msg.oldValue == null ? String.Empty : msg.oldValue.ToString();
                    entry.NewValue = msg.newValue == null ? String.Empty : msg.newValue.ToString();
                    _auditable.AuditJournal.Add(entry);
                }
                AuditLog.Clear();
            }
            else if (this.ObjectState == DataObjectState.New)
            {
                var entry = Context.CreateCompoundObject<AuditEntry>();
                entry.Identity = "unbekannt";
                entry.MessageFormat = "object created";
                entry.PropertyName = String.Empty;
                entry.OldValue = String.Empty;
                entry.NewValue = String.Empty;
                _auditable.AuditJournal.Add(entry);
            }
        }

        protected override void AuditPropertyChange(string property, object oldValue, object newValue)
        {
            // only audit if we can/want record the changes
            if (_auditable == null)
                return;

            // only audit on modified objects
            if (ObjectState != DataObjectState.Modified)
                return;

            // do not audit internal properties
            switch (property)
            {
                case "ID":
                case "ObjectState":
                case "ChangedOn":
                case "ChangedBy":
                case "AuditJournal":
                    return;
            }

            base.AuditPropertyChange(property, oldValue, newValue);
        }

        #endregion
    }
}
