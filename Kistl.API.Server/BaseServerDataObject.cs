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
            var trace = new System.Diagnostics.StackTrace(true);
            _constructTrace = String.Join("\n", 
                trace
                .GetFrames()
                .Select(frm => String.Format(
                    "  at {0}.{1} ({2}:{3})",
                    frm.GetMethod().ReflectedType != null ? frm.GetMethod().ReflectedType.FullName : "<not a type>",
                    frm.GetMethod().Name,
                    frm.GetFileName(),
                    frm.GetFileLineNumber()))
                .ToArray());
        }

        /// <summary>
        /// A private field storing the stracktrace where the object was constructed.
        /// </summary>
        private string _constructTrace;


        public DataObjectState ClientObjectState { get; set; }

        public override void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            base.ToStream(sw, auxObjects);
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

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// Base implementations returnes allways Full
        /// </summary>
        public virtual AccessRights CurrentAccessRights { get { return AccessRights.Full; } }
    }

    /// <summary>
    /// Server Collection Entry Implementation. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
    public abstract class BaseServerCollectionEntry
        : BaseServerPersistenceObject
    {
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
    public abstract class BaseServerStructObject : BaseStructObject { }
}
