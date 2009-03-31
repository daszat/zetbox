using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace Kistl.API
{
    public abstract class BasePersistenceObject : IPersistenceObject, INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Everyone has an ID
        /// </summary>
        public abstract int ID { get; set; }

        public bool IsReadonly { get { return _context != null ? _context.IsReadonly : false; ; } }

        private IKistlContext _context;
        public IKistlContext Context { get { return _context; } }

        public abstract bool IsAttached { get; }

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
            set
            {
                // Objectstate from Serializer or set by KistlContext
                if (_ObjectState != value)
                {
                    // only call event but do not call other infrastructure
                    // to avoid triggering provider specific business logic
                    OnPropertyChanging("ObjectState");
                    _ObjectState = value;
                    OnPropertyChanged("ObjectState");
                }
            }
        }

        protected void SetModified()
        {
            if (this.ObjectState == DataObjectState.Unmodified)
                this.ObjectState = DataObjectState.Modified;
        }

        /// <summary>
        /// Attach this Object to a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to attach this Object to.</param>
        public virtual void AttachToContext(IKistlContext ctx)
        {
            if (_context != null && _context != ctx) throw new InvalidOperationException("Object cannot be attached to a new Context while attached to another Context.");
            _context = ctx;
        }

        /// <summary>
        /// Detach this Object from a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to detach this Object from.</param>
        public virtual void DetachFromContext(IKistlContext ctx)
        {
            if (_context != ctx) throw new InvalidOperationException("Object is not attached to the given context.");
            _context = null;
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
        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        public virtual void ToStream(BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToStream(new SerializableType(this.GetInterfaceType()), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream((int)ObjectState, sw);
        }

        /// <summary>
        /// Base method for deserializing this Object.
        /// </summary>
        /// <param name="sr">Stream to serialize from</param>
        public virtual void FromStream(BinaryReader sr)
        {
            if (this.IsAttached) throw new InvalidOperationException("Deserializing attached objects is not allowed");
            if (sr == null) throw new ArgumentNullException("sr");

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            if (this.GetInterfaceType() != t.GetSystemType())
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", GetType(), t));

            BinarySerializer.FromStreamConverter(i => ID = i, sr);
            BinarySerializer.FromStreamConverter(i => ObjectState = (DataObjectState)i, sr);
        }


        public virtual void ReloadReferences() { }

        #endregion

        #region Property Change Notification

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Property is beeing changing
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanging(string property)
        {
            if (notifications == null)
            {
                OnPropertyChanging(property);
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
                SetModified();
                OnPropertyChanged(property);
            }
            else
            {
                if (!notifications.Contains(property))
                    notifications.Add(property);
            }
        }

        protected virtual void OnPropertyChanging(string property)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(property));
        }

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private List<string> notifications = null;
        public void RecordNotifications()
        {
            if (notifications == null)
            {
                notifications = new List<string>();
            }
        }

        public void PlaybackNotifications()
        {
            if (notifications == null) return;

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
}
