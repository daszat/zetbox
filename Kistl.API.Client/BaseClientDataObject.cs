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
        public abstract int ID { get; set; }

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

        public abstract void ToStream(System.IO.BinaryWriter sw);
        public abstract void FromStream(System.IO.BinaryReader sr);

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

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
            else
            {
                if(!notifications.Contains(property))
                    notifications.Add(property);
            }
        }

        private List<string> notifications = null;
        public void RecordNotifications()
        {
            if (notifications != null) throw new InvalidOperationException("Notifications are recording");
            notifications = new List<string>();
        }

        public void PlaybackNotifications()
        {
            if (notifications == null) throw new InvalidOperationException("No notifications where recorded");
            if (PropertyChanged != null)
            {
                notifications.ForEach(p => PropertyChanged(this, new PropertyChangedEventArgs(p)));
            }
            notifications = null;
        }
    }

    public abstract class BaseClientDataObject : BaseClientPersistenceObject, IDataObject
    {
        protected BaseClientDataObject()
        {
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
        }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        public virtual void ApplyChanges(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.ID = this.ID;
        }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToBinary(new SerializableType(this.GetType()), sw);
            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary((int)ObjectState, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");

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

    }

    public abstract class BaseClientCollectionEntry : BaseClientPersistenceObject, ICollectionEntry
    {
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary((int)ObjectState, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");

            int tmp;
            BinarySerializer.FromBinary(out tmp, sr);
            ID = tmp;

            BinarySerializer.FromBinary(out tmp, sr);
            ObjectState = (DataObjectState)tmp;
        }

        public virtual void ApplyChanges(ICollectionEntry obj)
        {
            obj.ID = this.ID;
        }
    }
}