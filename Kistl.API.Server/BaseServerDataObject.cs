using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    /// <summary>
    /// Abstract Base Class for a PersistenceObject on the Server Side
    /// </summary>
    public abstract class BaseServerPersistenceObject : BasePersistenceObject
    {
        protected BaseServerPersistenceObject()
        {
            if (ApplicationContext.Current.HostType != HostType.Server) throw new InvalidOperationException("A BaseServerPersistenceObject can only exist on a server");
            ClientObjectState = DataObjectState.NotDeserialized;
        }

        public DataObjectState ClientObjectState { get; set; }

        public override void ToStream(BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToStream((int)DataObjectState.Unmodified, sw);
        }

        public override void FromStream(BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromStreamConverter(i => ClientObjectState = (DataObjectState)i, sr);
        }
    }

    /// <summary>
    /// Abstract Base Class for Server Objects
    /// </summary>
    public abstract class BaseServerDataObject : BaseServerPersistenceObject, IDataObject
    {
        /// <summary>
        /// Attach to Events
        /// </summary>
        protected BaseServerDataObject()
        {
            ApplicationContext.Current.CustomActionsManager.AttachEvents(this);
        }

        /// <summary>
        /// Fires an Event before an Object is saved.
        /// </summary>
        public virtual void NotifyPreSave() { }
        /// <summary>
        /// Fires an Event after an Object is saved.
        /// </summary>
        public virtual void NotifyPostSave() { }

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
            throw new ArgumentOutOfRangeException("columnName", "unknown property " + prop);
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        string IDataErrorInfo.Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    /// <summary>
    /// Server Collection Entry Implementation. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
    public abstract class BaseServerCollectionEntry : BaseServerPersistenceObject, ICollectionEntry { }

    /// <summary>
    /// local proxy
    /// </summary>
    public abstract class BaseServerStructObject : BaseStructObject { }
}
