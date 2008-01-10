using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Kistl.API.Server
{
    /// <summary>
    /// Basis Datenobjekt. Attached sich automatisch an den CustomActionsManager zur Verteilung der Events
    /// </summary>
    public abstract class BaseServerDataObject : System.Data.Objects.DataClasses.EntityObject, IDataObject, ICloneable
    {
        /// <summary>
        /// Attach to Events
        /// </summary>
        public BaseServerDataObject()
        {
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
            _type = new ObjectType(this.GetType().Namespace, this.GetType().Name);
        }

        private DataObjectState _ObjectState = DataObjectState.Unmodified;
        public DataObjectState ObjectState
        {
            get
            {
                // Calc Objectstate
                if (_ObjectState != DataObjectState.Deleted)
                {
                    if (ID == Helper.INVALIDID)
                    {
                        _ObjectState = DataObjectState.New;
                    }
                    else if(_ObjectState == DataObjectState.New)
                    {
                        _ObjectState = DataObjectState.Unmodified;
                    }
                }
                return _ObjectState;
            }
            set
            {
                // Objectstate from Serializer oder Methodcall
                _ObjectState = value;
            }
        }

        protected ObjectType _type = null;
        public ObjectType Type
        {
            get
            {
                return _type;
            }
        }

        public abstract string EntitySetName { get; }

        #region IDataObject Members
        /// <summary>
        /// Jeder hat eine ID
        /// </summary>
        public abstract int ID { get; set; }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        #endregion

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geändert hat.
        /// </summary>
        public void NotifyChange()
        {
            ReportPropertyChanged(null);
        }

        public void CopyTo(BaseServerDataObject obj)
        {
            obj.ID = this.ID;
        }

        public virtual object Clone()
        {
            return null;
        }
    }
}
