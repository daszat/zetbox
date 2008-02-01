using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kistl.API.Client
{
    public abstract class BaseClientDataObject : IDataObject, ICloneable, INotifyPropertyChanged
    {
        public BaseClientDataObject()
        {
            _type = new ObjectType(this.GetType());
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
        }

        protected ObjectType _type = null;

        public ObjectType Type
        {
            get
            {
                return _type;
            }
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

        private KistlContext _context;
        public KistlContext Context { get { return _context; } }
        internal void AttachToContext(KistlContext ctx)
        {
            _context = ctx;
        }

        internal void DetachFromContext(KistlContext ctx)
        {
            if (_context != ctx) throw new InvalidOperationException("Object is not attached to the given context.");
            _context = null;
        }

        #region IDataObject Members

        public abstract int ID { get; set; }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        #endregion

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geändert hat.
        /// </summary>
        public void NotifyChange()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        public void CopyTo(BaseClientDataObject obj)
        {
            obj.ID = this.ID;
        }

        public virtual object Clone()
        {
            return null;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public virtual void ToStream(System.IO.BinaryWriter sw)
        {
            BinarySerializer.ToBinary(Type, sw);
            BinarySerializer.ToBinary(ID, sw);
        }

        public virtual void FromStream(IKistlContext ctx, System.IO.BinaryReader sr)
        {
            ObjectType t;
            BinarySerializer.FromBinary(out t, sr);

            if (!Type.Equals(t))
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", Type, t));

            int tmpID;
            BinarySerializer.FromBinary(out tmpID, sr);
            ID = tmpID;

            ctx.Attach(this);
        }
    }
}