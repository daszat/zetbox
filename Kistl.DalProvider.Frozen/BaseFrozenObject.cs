using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DalProvider.Frozen
{
    public abstract class BaseFrozenObject : IPersistenceObject
    {

        protected BaseFrozenObject(FrozenContext ctx, int id)
        {
            this.Context = ctx;
            this.ID = id;
            this.IsSealed = false;
        }

        public bool IsSealed { get; private set; }

        public void Seal()
        {
            if (IsSealed)
            {
                throw new ReadOnlyObjectException();
            }
            IsSealed = true;
        }

        #region IPersistenceObject Members

        public int ID { get; private set; }

        public DataObjectState ObjectState { get { return DataObjectState.Unmodified; } }

        public virtual void ToStream(System.IO.BinaryWriter sw)
        {
            BinarySerializer.ToBinary(new SerializableType(this.GetType()), sw);
            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary((int)ObjectState, sw);
        }

        public virtual void FromStream(System.IO.BinaryReader sr)
        {
            throw new InvalidOperationException("Cannot deserialize to a frozen Object");
        }

        public void NotifyPropertyChanging(string property) { }

        public void NotifyPropertyChanged(string property) { }

        public IKistlContext Context { get; private set; }

        public void AttachToContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public void DetachFromContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public bool IsAttached { get { return true; } }

        public bool IsReadonly { get { return IsSealed; } }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged { add { } remove { } }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging { add { } remove { } }

        #endregion

    }

    public abstract class BaseFrozenDataObject : BaseFrozenObject, IDataObject
    {

        protected BaseFrozenDataObject(FrozenContext ctx, int id)
            : base(ctx, id)
        {
        }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        #region IDataObject Members

        public void NotifyChange() { }

        #endregion

        #region IDataErrorInfo Members

        public string Error { get { return ""; } }

        public string this[string columnName] { get { return ""; } }

        #endregion

    }

    public abstract class BaseFrozenStruct
        : BaseFrozenObject, IStruct
    {

        protected BaseFrozenStruct(FrozenContext ctx, int id)
            : base(ctx, id)
        {
        }

        #region IStruct Members

        public void AttachToObject(IPersistenceObject obj, string property)
        {
        }

        public void DetachFromObject(IPersistenceObject obj, string property)
        {
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            // since we are immutable, returning ourself is save
            return this;
        }

        #endregion
    }
}
