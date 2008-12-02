using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Kistl.API.Client
{
    public abstract class BaseClientPersistenceObject : IPersistenceObject, INotifyPropertyChanged, INotifyPropertyChanging
    {
        protected BaseClientPersistenceObject()
        {
            if (ApplicationContext.Current.HostType != HostType.Client) throw new InvalidOperationException("A BaseClientPersistenceObject can exist only on a client");
        }

        public int ID { get; internal set; }

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
            if (ctx.ContainsObject(this.GetType(), this.ID) == null)
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

        public virtual void ToStream(System.IO.BinaryWriter sw)
        {
        }
        public virtual void FromStream(System.IO.BinaryReader sr)
        {
            if (this.Context != null) throw new InvalidOperationException("Deserializing attached objects is not allowed");
        }

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
                if(!notifications.Contains(property))
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
    }

    public abstract class BaseClientDataObject : BaseClientPersistenceObject, IDataObject
    {
        protected BaseClientDataObject()
        {
            ApplicationContext.Current.CustomActionsManager.AttachEvents(this);
        }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        public virtual void ApplyChanges(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ((BaseClientPersistenceObject)obj).ID = this.ID;
        }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");
            base.ToStream(sw);

            BinarySerializer.ToBinary(new SerializableType(this.GetType()), sw);
            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary((int)ObjectState, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");
            base.FromStream(sr);

            SerializableType t;
            BinarySerializer.FromBinary(out t, sr);

            if (this.GetType() != t.GetSerializedType())
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", GetType(), t));

            int tmp;
            BinarySerializer.FromBinary(out tmp, sr);
            ID = tmp;

            BinarySerializer.FromBinary(out tmp, sr);
            ObjectState = (DataObjectState)tmp;
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
            throw new ArgumentOutOfRangeException("columnName", "unknown property " + prop);
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        string IDataErrorInfo.Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    public abstract class BaseClientCollectionEntry : BaseClientPersistenceObject, ICollectionEntry
    {
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");
            base.ToStream(sw);

            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary((int)ObjectState, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");
            base.FromStream(sr);

            int tmp;
            BinarySerializer.FromBinary(out tmp, sr);
            ID = tmp;

            BinarySerializer.FromBinary(out tmp, sr);
            ObjectState = (DataObjectState)tmp;
        }

        public virtual void ApplyChanges(ICollectionEntry obj)
        {
            ((BaseClientPersistenceObject)obj).ID = this.ID;
        }
    }

    public abstract class BaseClientStructObject : IStruct, INotifyPropertyChanged, INotifyPropertyChanging
    {
        public virtual void ToStream(System.IO.BinaryWriter sw)
        {
        }
        public virtual void FromStream(System.IO.BinaryReader sr)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool IsReadonly { get { return _attachedObject != null ? _attachedObject.IsReadonly : false; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Property is beeing changing
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanging(string property)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(property));

            if (_attachedObject != null)
                _attachedObject.NotifyPropertyChanging(_attachedObjectProperty);
        }

        /// <summary>
        /// Property has been changed
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            if (_attachedObject != null)
                _attachedObject.NotifyPropertyChanged(_attachedObjectProperty);
        }

        private IPersistenceObject _attachedObject;
        private string _attachedObjectProperty;
        public void AttachToObject(IPersistenceObject obj, string property)
        {
            if (_attachedObject != null && _attachedObject != obj) throw new ArgumentException("Struct is already attached to another object");

            _attachedObjectProperty = property;
            _attachedObject = obj;
        }

        public void DetachFromObject(IPersistenceObject obj, string property)
        {
            _attachedObject = null;
            _attachedObjectProperty = "";
        }
    }
}
