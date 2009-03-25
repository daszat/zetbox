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
    public abstract class BaseClientPersistenceObject : IPersistenceObject, INotifyPropertyChanged, INotifyPropertyChanging
    {
        protected BaseClientPersistenceObject()
        {
            if (ApplicationContext.Current.HostType != HostType.Client)
                throw new InvalidOperationException("A BaseClientPersistenceObject can exist only on a client");
        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            internal set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        public bool IsReadonly { get { return _context != null ? _context.IsReadonly : false; ; } }

        private DataObjectState _ObjectState = DataObjectState.Unmodified;
        public DataObjectState ObjectState
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
            internal set
            {
                // Objectstate from Serializer or set by KistlContext
                if (_ObjectState != value)
                {
                    // only call event but do not call other infrastructure
                    // to avoid triggering provider specific business logic
                    if (PropertyChanging != null)
                        PropertyChanging(this, new PropertyChangingEventArgs("ObjectState"));
                    _ObjectState = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ObjectState"));
                }
            }
        }

        private IKistlContext _context;
        public IKistlContext Context { get { return _context; } }

        public virtual void AttachToContext(IKistlContext ctx)
        {
            if (_context != null && _context != ctx) throw new InvalidOperationException("Object cannot be attached to a new Context while attached to another Context.");
            if (ctx.ContainsObject(this.GetInterfaceType(), this.ID) == null)
            {
                // Object is not in this Context present
                // -> Attach it. Attach will call this Method again!
                ctx.Attach(this);
            }
            else
            {
                _context = ctx;
            }
        }

        public virtual void DetachFromContext(IKistlContext ctx)
        {
            if (_context != ctx) throw new InvalidOperationException("Object is not attached to the given context.");
            _context = null;
        }

        public bool IsAttached
        {
            get
            {
                return _context != null;
            }
        }

        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void ApplyChangesFrom(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj.GetType().ToInterfaceType() != this.GetType().ToInterfaceType())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }

        #region IStreamable Members

        public virtual void ToStream(BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToStream(new SerializableType(this.GetInterfaceType()), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream((int)ObjectState, sw);
        }

        public virtual void FromStream(BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");
            if (this.IsAttached) throw new InvalidOperationException("Deserializing into attached objects is not allowed");

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            if (this.GetInterfaceType() != t.GetInterfaceType())
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", GetType(), t));

            int tmp;
            BinarySerializer.FromStream(out tmp, sr);
            ID = tmp;

            BinarySerializer.FromStream(out tmp, sr);
            ObjectState = (DataObjectState)tmp;
        }

        public virtual void ReloadReferences() { }

        #endregion

        #region Property Change Notification

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Object has been changed
        /// </summary>
        public virtual void NotifyChange()
        {
            if (notifications == null)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
            }
        }

        /// <summary>
        /// Property is beeing changing
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanging(string property)
        {
            if (notifications == null)
            {
                if (PropertyChanging != null)
                    PropertyChanging(this, new PropertyChangingEventArgs(property));
            }
        }

        /// <summary>
        /// Property has been changed
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanged(string property)
        {
            if (notifications == null)
            {
                if (this.ObjectState == DataObjectState.Unmodified)
                    this.ObjectState = DataObjectState.Modified;

                OnPropertyChanged(property);
            }
            else
            {
                if (!notifications.Contains(property))
                    notifications.Add(property);
            }
        }

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private List<string> notifications = null;
        public void RecordNotifications()
        {
            if (notifications != null) throw new InvalidOperationException("Notifications are already recording");
            notifications = new List<string>();
        }

        public void PlaybackNotifications()
        {
            if (notifications == null) throw new InvalidOperationException("Notification recording was not enabled");

            // enable normal notifications before playing back the old
            // this is neccessary to allow handlers to cause normal events
            var localCopy = notifications;
            notifications = null;

            if (PropertyChanged != null)
            {
                localCopy.ForEach(p => OnPropertyChanged(p));
            }
        }

        #endregion

        public abstract InterfaceType GetInterfaceType();

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
