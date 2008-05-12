using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Globalization;

namespace Kistl.API.Server
{
    public abstract class BaseServerPersistenceObject : System.Data.Objects.DataClasses.EntityObject, IPersistenceObject
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
                    if (ID == API.Helper.INVALIDID)
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
                _ObjectState = value;
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
            _context = null;
        }

        public abstract void ToStream(System.IO.BinaryWriter sw);
        public abstract void FromStream(System.IO.BinaryReader sr);

        public abstract void NotifyPropertyChanging(string property);
        public abstract void NotifyPropertyChanged(string property);
    }

    /// <summary>
    /// Basis Datenobjekt. Attached sich automatisch an den CustomActionsManager zur Verteilung der Events
    /// </summary>
    public abstract class BaseServerDataObject : BaseServerPersistenceObject, IDataObject, ICloneable
    {
        /// <summary>
        /// Attach to Events
        /// </summary>
        protected BaseServerDataObject()
        {
            _type = new ObjectType(this.GetType().Namespace, this.GetType().Name);
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
        }


        private ObjectType _type = null;
        public ObjectType Type
        {
            get
            {
                return _type;
            }
        }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geändert hat.
        /// </summary>
        public virtual void NotifyChange()
        {
            throw new NotImplementedException();
        }

        public override void NotifyPropertyChanging(string property)
        {
            base.ReportPropertyChanging(property);
        }

        public override void NotifyPropertyChanged(string property)
        {
            base.ReportPropertyChanged(property);
        }

        public virtual void CopyTo(IDataObject target)
        {
            if (target == null) throw new ArgumentNullException("target");
            target.ID = this.ID;
        }

        public virtual object Clone()
        {
            return null;
        }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToBinary(Type, sw);
            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary((int)ObjectState, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");

            ObjectType t;
            BinarySerializer.FromBinary(out t, sr);

            // TODO: improve Error reporting
            if (!Type.Equals(t))
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", Type, t));

            int tmp;
            BinarySerializer.FromBinary(out tmp, sr);
            ID = tmp;

            BinarySerializer.FromBinary(out tmp, sr);
            ObjectState = (DataObjectState)tmp;
        }
    }

    public abstract class BaseServerCollectionEntry : BaseServerPersistenceObject, ICollectionEntry, ICloneable
    {
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw", "No BinaryWriter specified");
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
            if (obj == null) throw new ArgumentNullException("obj");

            NotifyPropertyChanging("ID");
            obj.ID = this.ID;
            NotifyPropertyChanged("ID");
        }

        public override void NotifyPropertyChanging(string property)
        {
            base.ReportPropertyChanging(property);
        }

        public override void NotifyPropertyChanged(string property)
        {
            base.ReportPropertyChanged(property);
        }
    }
}
