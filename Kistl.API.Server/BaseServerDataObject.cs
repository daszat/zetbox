using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Globalization;

namespace Kistl.API.Server
{
    /// <summary>
    /// Abstract Base Class for a PersistenceObject on the Server Side
    /// </summary>
    public abstract class BaseServerPersistenceObject : IPersistenceObject
    {
        protected BaseServerPersistenceObject()
        {
            if (ApplicationContext.Current.HostType != HostType.Server) throw new InvalidOperationException("A BaseServerPersistenceObject can only exist on a server");
        }

        /// <summary>
        /// Everyone has an ID
        /// </summary>
        public abstract int ID { get; set; }

        public bool IsReadonly { get { return _context != null ? _context.IsReadonly : false; ; } }

        private DataObjectState _ObjectState = DataObjectState.Unmodified;

        /// <summary>
        /// Current ObjectState. Getter calculates the State, Setter is used by the Serializer.
        /// </summary>
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
            private set
            {
                // Objectstate from Serializer
                _ObjectState = value;
            }
        }

        private IKistlContext _context;
        /// <summary>
        /// Current Context.
        /// </summary>
        public IKistlContext Context { get { return _context; } }

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

        public abstract bool IsAttached { get; }

        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        public virtual void ToStream(System.IO.BinaryWriter sw)
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
        public virtual void FromStream(System.IO.BinaryReader sr)
        {
            if (this.IsAttached) throw new InvalidOperationException("Deserializing attached objects is not allowed");
            if (sr == null) throw new ArgumentNullException("sr");

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            if (this.GetInterfaceType() != t.GetSerializedType())
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", GetType(), t));

            BinarySerializer.FromStreamConverter(i => ID = i, sr);
            BinarySerializer.FromStreamConverter(i => ObjectState = (DataObjectState)i, sr);
        }

        /// <summary>
        /// Notifies that a Property is changing
        /// </summary>
        /// <param name="propertyName">Property that is changing or empty, if the Object is changing</param>
        public virtual void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies that a Property has changed
        /// </summary>
        /// <param name="propertyName">Property that has changed or empty, if the Object has changed</param>
        public virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public abstract Type GetInterfaceType();
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

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        public virtual void NotifyChange()
        {
            throw new NotImplementedException();
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

    /// <summary>
    /// Server Collection Entry Implementation. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
    public abstract class BaseServerCollectionEntry : BaseServerPersistenceObject, ICollectionEntry
    {
    }

    public abstract class BaseServerStructObject : /*ComplexObject, */ IStruct, INotifyPropertyChanged, INotifyPropertyChanging
    {

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool IsReadonly { get { return _attachedObject != null ? _attachedObject.IsReadonly : false; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Property is about to be changed
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

        #region IStruct Members

        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        public virtual void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToStream(new SerializableType(this.GetType()), sw);
        }
        /// <summary>
        /// Base method for deserializing this Object.
        /// </summary>
        /// <param name="sr">Stream to serialize from</param>
        public virtual void FromStream(System.IO.BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sw");

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            if (this.GetType() != t.GetSerializedType())
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", GetType(), t));
        }

        #endregion

        public abstract Type GetInterfaceType();
    }
}
