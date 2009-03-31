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
                    _ID = value;
                    OnPropertyChanged("ID");
                }
            }
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

    public abstract class BaseClientCollectionEntry : BaseClientPersistenceObject, ICollectionEntry { }

    /// <summary>
    /// local proxy
    /// </summary>
    public abstract class BaseClientStructObject : BaseStructObject { }

}
