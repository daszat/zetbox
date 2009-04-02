using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kistl.API.Client
{
    public abstract class BaseClientPersistenceObject : BasePersistenceObject
    {
        protected BaseClientPersistenceObject()
        {
            if (ApplicationContext.Current.HostType != HostType.Client)
                throw new InvalidOperationException("A BaseClientPersistenceObject can exist only on a client");
        }

        private int _ID;
        public override int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    OnPropertyChanged("ID", _ID, value);
                    _ID = value;
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
                OnPropertyChanging("ObjectState", oldValue, DataObjectState.Modified);
                this._ObjectState = DataObjectState.Modified;
                OnPropertyChanged("ObjectState", oldValue, DataObjectState.Modified);
            }
        }

        internal void SetUnmodified()
        {
            var oldValue = this._ObjectState;
            OnPropertyChanging("ObjectState", oldValue, DataObjectState.Unmodified);
            this._ObjectState = DataObjectState.Unmodified;
            OnPropertyChanged("ObjectState", oldValue, DataObjectState.Unmodified);
        }

        internal void SetDeleted()
        {
            var oldValue = this._ObjectState;
            OnPropertyChanging("ObjectState", oldValue, DataObjectState.Deleted);
            this._ObjectState = DataObjectState.Deleted;
            OnPropertyChanged("ObjectState", oldValue, DataObjectState.Deleted);
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            if (ctx.ContainsObject(this.GetInterfaceType(), this.ID) == null)
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

        public override void ToStream(BinaryWriter sw)
        {
            base.ToStream(sw);
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
        protected BaseClientDataObject()
        {
            ApplicationContext.Current.CustomActionsManager.AttachEvents(this);
        }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        public virtual void UpdateParent(string propertyName, int? id)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetInterfaceType().Type.FullName));
        }

        #region IDataErrorInfo Members

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return GetPropertyError(columnName);
            }
        }

        protected virtual string GetPropertyError(string prop)
        {
            // TODO: Wieder einbauen oder anders warnen
            // throw new ArgumentOutOfRangeException("columnName", "unknown property " + prop);
            return "";
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        string IDataErrorInfo.Error
        {
            get { return ""; /* throw new NotImplementedException();*/ }
        }

        #endregion
    }

    public abstract class BaseClientCollectionEntry : BaseClientPersistenceObject, ICollectionEntry 
    {
        public abstract int RelationID { get; }
    }

    /// <summary>
    /// local proxy
    /// </summary>
    public abstract class BaseClientStructObject : BaseStructObject { }

}
