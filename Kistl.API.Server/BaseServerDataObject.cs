
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;

    /// <summary>
    /// Abstract Base Class for a PersistenceObject on the Server Side
    /// </summary>
    public abstract class BaseServerPersistenceObject : BasePersistenceObject
    {
        private readonly IAuditable _auditable;

        protected BaseServerPersistenceObject(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
            _auditable = this as IAuditable;
            ClientObjectState = DataObjectState.NotDeserialized;
#if DEBUG
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
#endif
        }

#if DEBUG
        /// <summary>
        /// A private field storing the stracktrace where the object was constructed.
        /// </summary>
        private string _constructTrace;

        /// <summary>
        /// The stack trace where this object was created
        /// </summary>
        public string ConstructionTrace
        {
            get { return _constructTrace; }
        }
#endif

        public DataObjectState ClientObjectState { get; set; }

        private DataObjectState _ObjectState = DataObjectState.Detached;
        public override sealed DataObjectState ObjectState
        {
            get { return _ObjectState; }
        }

        private void SetObjectState(DataObjectState newState)
        {
            var oldValue = this._ObjectState;
            NotifyPropertyChanging("ObjectState", oldValue, newState);
            this._ObjectState = newState;
            if (this.Context != null && newState.In(DataObjectState.Modified, DataObjectState.Deleted))
                this.Context.Internals().SetModified(this);
            NotifyPropertyChanged("ObjectState", oldValue, newState);
        }

        protected override sealed void SetModified()
        {
            if (this.ObjectState == DataObjectState.Unmodified)
            {
                SetObjectState(DataObjectState.Modified);
            }
        }

        public override void SetNew()
        {
            if (_ObjectState != DataObjectState.Detached) { throw new InvalidOperationException(String.Format("ObjectState is {0}, but should be Detached", _ObjectState)); }
            SetObjectState(DataObjectState.New);
        }

        public void SetUnmodified()
        {
            if (this.Context == null) throw new InvalidOperationException("Cannot set object to Unmodified when object has no Context");

            if (!_ObjectState.In(DataObjectState.Detached, DataObjectState.New, DataObjectState.Modified, DataObjectState.Unmodified))
            {
                throw new InvalidOperationException("Cannot set object to Unmodified when in State " + _ObjectState.ToString());
            }

            SetObjectState(DataObjectState.Unmodified);
        }

        public void SetDeleted()
        {
            if (!_ObjectState.In(DataObjectState.New, DataObjectState.Modified, DataObjectState.Unmodified, DataObjectState.Deleted))
            {
                throw new InvalidOperationException("Cannot delete object when in State " + _ObjectState.ToString());
            }

            SetObjectState(DataObjectState.Deleted);
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            // avoid double-attaches
            if (this.Context != null && this.Context == ctx)
            {
                return;
            }
            else if (this.Context != null && this.Context != ctx)
            {
                throw new WrongKistlContextException("Object cannot be attached to a new Context while attached to another Context.");
            }

            if (!ObjectState.In(DataObjectState.Detached, DataObjectState.New))
            {
                throw new InvalidOperationException(String.Format("Cannot attach object unless it is New or Detached. obj.ObjectState == {0}", ObjectState));
            }

            if (ObjectState == DataObjectState.Detached)
                SetObjectState(DataObjectState.Unmodified);

            base.AttachToContext(ctx);
        }

        public override void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream((int)DataObjectState.Unmodified, sw);
        }

        public override IEnumerable<IPersistenceObject> FromStream(BinaryReader sr)
        {
            var baseResult = base.FromStream(sr);
            BinarySerializer.FromStreamConverter(i => ClientObjectState = (DataObjectState)i, sr);
            return baseResult;
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
                    entry.Identity = GetIdentity(Context as IKistlServerContext);
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
                entry.Identity = GetIdentity(Context as IKistlServerContext);
                entry.MessageFormat = "object created";
                entry.PropertyName = String.Empty;
                entry.OldValue = String.Empty;
                entry.NewValue = String.Empty;
                _auditable.AuditJournal.Add(entry);
            }
        }

        private static string GetIdentity(IKistlServerContext serverCtx)
        {
            return serverCtx != null && serverCtx.Identity != null
                ? string.IsNullOrEmpty(serverCtx.Identity.DisplayName)
                    ? serverCtx.Identity.UserName
                    : serverCtx.Identity.DisplayName
                : "System";
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

    /// <summary>
    /// Abstract Base Class for Server Objects
    /// </summary>
    public abstract class BaseServerDataObject : BaseServerPersistenceObject, IDataObject
    {
        /// <summary>
        /// Attach to Events
        /// </summary>
        protected BaseServerDataObject(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Fires an Event before an Object is saved.
        /// </summary>
        public virtual void NotifyPreSave()
        {
            LogAudits();
            SaveAudits();
        }

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
        /// Base implementations returnes always Full
        /// </summary>
        public virtual Kistl.API.AccessRights CurrentAccessRights { get { return Kistl.API.AccessRights.Full; } }

        public abstract void UpdateParent(string propertyName, int? id);
    }

    /// <summary>
    /// Server Collection Entry Implementation. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
    public abstract class BaseServerCollectionEntry
        : BaseServerPersistenceObject
    {
        protected BaseServerCollectionEntry(Func<IFrozenContext> lazyCtx)
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
    public abstract class BaseServerCompoundObject : BaseCompoundObject
    {
        protected BaseServerCompoundObject(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        protected override void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanging(property, oldValue, newValue);
            // Notifing parent is done in provider implementation
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            // Notifing parent is done in provider implementation
        }
    }
}
