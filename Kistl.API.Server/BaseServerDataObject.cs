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
    /// Basis Datenobjekt. Attached sich automatisch an den CustomActionsManager zur Verteilung der Events
    /// </summary>
    public abstract class BaseServerDataObject : System.Data.Objects.DataClasses.EntityObject, IDataObject, ICloneable
    {
        /// <summary>
        /// Attach to Events
        /// </summary>
        protected BaseServerDataObject()
        {
            _type = new ObjectType(this.GetType().Namespace, this.GetType().Name);
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
        }

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

        private ObjectType _type = null;
        public ObjectType Type
        {
            get
            {
                return _type;
            }
        }

        #region IDataObject Members
        /// <summary>
        /// Jeder hat eine ID
        /// </summary>
        public abstract int ID { get; set; }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        #endregion

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geändert hat.
        /// </summary>
        public virtual void NotifyChange()
        {
            throw new NotImplementedException();
        }

        public virtual void NotifyPropertyChanging(string property)
        {
            base.ReportPropertyChanging(property);
        }

        public virtual void NotifyPropertyChanged(string property)
        {
            base.ReportPropertyChanged(property);
        }

        public virtual void CopyTo(IDataObject target)
        {
            if (target == null) throw new ArgumentNullException();
            target.ID = this.ID;
        }

        public virtual object Clone()
        {
            return null;
        }

        public virtual void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException();

            BinarySerializer.ToBinary(Type, sw);
            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary((int)ObjectState, sw);
        }

        public virtual void FromStream(IKistlContext ctx, System.IO.BinaryReader sr)
        {
            if (ctx == null) throw new ArgumentNullException();
            if (sr == null) throw new ArgumentNullException();

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

            ctx.Attach(this);
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
    }

    public abstract class BaseServerCollectionEntry : System.Data.Objects.DataClasses.EntityObject, ICollectionEntry, ICloneable
    {
        public abstract int ID { get; set; }

        public virtual void ToStream(System.IO.BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException();
            BinarySerializer.ToBinary(ID, sw);
        }

        public virtual void FromStream(IKistlContext ctx, System.IO.BinaryReader sr)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (sr == null) throw new ArgumentNullException("sr");

            int tmpID;
            BinarySerializer.FromBinary(out tmpID, sr);
            ID = tmpID;
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

        public virtual void NotifyPropertyChanging(string property)
        {
            base.ReportPropertyChanging(property);
        }

        public virtual void NotifyPropertyChanged(string property)
        {
            base.ReportPropertyChanged(property);
        }
    }
}
