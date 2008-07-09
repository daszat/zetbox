using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Kistl.API.Client
{
    public abstract class BaseClientPersistenceObject : IPersistenceObject, INotifyPropertyChanged
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
            set
            {
                // Objectstate from Serializer oder Methodcall
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
            _context = ctx;
        }

        public virtual void DetachFromContext(IKistlContext ctx)
        {
            if (_context != ctx) throw new InvalidOperationException("Object is not attached to the given context.");
            _context = null;
        }

        public abstract void ToStream(System.IO.BinaryWriter sw);
        public abstract void FromStream(System.IO.BinaryReader sr);

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Object has been changed
        /// </summary>
        public virtual void NotifyChange()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        /// <summary>
        /// Property is beeing changing
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanging(string property)
        {
        }

        /// <summary>
        /// Property has been changed
        /// </summary>
        /// <param name="property"></param>
        public virtual void NotifyPropertyChanged(string property)
        {
            if (this.ObjectState == DataObjectState.Unmodified)
                this.ObjectState = DataObjectState.Modified;

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

    public abstract class BaseClientDataObject : BaseClientPersistenceObject, IDataObject, ICloneable
    {
        protected BaseClientDataObject()
        {
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
        }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        public virtual void CopyTo(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.ID = this.ID;
        }

        public virtual object Clone()
        {
            return null;
        }

        private static int BadHackInvalidIdCounter = Helper.INVALIDID;

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToBinary(new SerializableType(this.GetType()), sw);
            if (ID == Helper.INVALIDID)
                BinarySerializer.ToBinary(BadHackInvalidIdCounter--, sw);
            else
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

    public abstract class BaseClientCollectionEntry : BaseClientPersistenceObject, ICollectionEntry, ICloneable
    {
        
        private static int BadHackInvalidIdCounter = Helper.INVALIDID;

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            if (ID == Helper.INVALIDID)
                BinarySerializer.ToBinary(BadHackInvalidIdCounter--, sw);
            else
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual void CopyTo(ICollectionEntry obj)
        {
            obj.ID = this.ID;
        }
    }

}