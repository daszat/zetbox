// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    public abstract class PersistenceObjectBaseImpl : Zetbox.API.BasePersistenceObject
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

        public override void SetUnmodified()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Unmodified);
            this._ObjectState = DataObjectState.Unmodified;
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Unmodified);
        }

        public override void SetDeleted()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Deleted);
            this._ObjectState = DataObjectState.Deleted;
            if (this.Context != null)
                this.Context.Internals().SetModified(this);
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Deleted);
        }

        public override void SetUnDeleted()
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, DataObjectState.Modified);
            this._ObjectState = DataObjectState.Modified;
            if (this.Context != null)
                this.Context.Internals().SetModified(this);
            NotifyPropertyChanged("ObjectState", oldValue, DataObjectState.Modified);
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

        public override void AttachToContext(IZetboxContext ctx)
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

        public override void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            sw.Write((int)ObjectState);
            sw.Write((int)CurrentAccessRights);
        }

        public override IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            var baseResult = base.FromStream(sr);
            sr.ReadConverter(i => _ObjectState = (DataObjectState)i);
            sr.ReadConverter(i => ApplyRightsFromStream((API.AccessRights)i)); 
            return baseResult;
        }

        protected virtual void ApplyRightsFromStream(Zetbox.API.AccessRights rights)
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
