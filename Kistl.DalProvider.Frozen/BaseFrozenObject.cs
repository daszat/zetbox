
namespace Kistl.DalProvider.Frozen
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public abstract class BaseFrozenObject
        : IPersistenceObject
    {
        protected BaseFrozenObject()
        {
            throw new InvalidOperationException("BaseFrozenObject constructor without id called");
        }

        protected BaseFrozenObject(int id)
        {
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

        #region IStreamable Members

        public virtual void ToStream(BinaryWriter sw)
        {
            BinarySerializer.ToStream(new SerializableType(this.GetInterfaceType()), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream((int)ObjectState, sw);
        }

        public virtual void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            ToStream(sw);
        }

        public virtual void FromStream(BinaryReader sr)
        {
            throw new InvalidOperationException("Cannot deserialize to a frozen Object");
        }

        [Obsolete]
        public virtual void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        public virtual void ToStream(System.Xml.XmlWriter xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        public virtual void FromStream(System.Xml.XmlReader xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        // frozen objects are already connected
        public void ReloadReferences() { }

        #endregion

        public void NotifyPropertyChanging(string property, object oldValue, object newValue) { }
        public void NotifyPropertyChanged(string property, object oldValue, object newValue) { }

        public IKistlContext Context { get { return FrozenContext.Single; } }

        public void AttachToContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public void DetachFromContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void ApplyChangesFrom(IPersistenceObject obj)
        {
            throw new NotImplementedException();
        }

        public bool IsAttached { get { return true; } }

        public bool IsReadonly { get { return IsSealed; } }

        public abstract InterfaceType GetInterfaceType();

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged { add { } remove { } }
        public event PropertyChangingEventHandler PropertyChanging { add { } remove { } }

        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue { add { } remove { } }
        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue { add { } remove { } }

        #endregion

    }

    public abstract class BaseFrozenDataObject : BaseFrozenObject, IDataObject
    {
        protected BaseFrozenDataObject()
        {
            throw new InvalidOperationException("BaseFrozenDataObject constructor without id called");
        }

        protected BaseFrozenDataObject(int id)
            : base(id)
        {
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
        /// Fires an Event after an Object is created.
        /// </summary>
        public virtual void NotifyCreated() { }
        /// <summary>
        /// Fires an Event before an Object is deleted.
        /// </summary>
        public virtual void NotifyDeleting() { }

        #region IDataErrorInfo Members

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="prop">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. Returns 
        /// <value>String.Empty</value> if there is nothing to report.</returns>
        protected virtual string GetPropertyError(string prop) { return String.Empty; }

        public string Error { get { return String.Empty; } }

        public string this[string columnName] { get { return GetPropertyError(columnName); } }

        #endregion


    }

    public abstract class BaseFrozenStruct
        : BaseFrozenObject, IStruct
    {
        protected BaseFrozenStruct()
        {
            throw new InvalidOperationException("BaseFrozenStruct constructor without id called");
        }

        protected BaseFrozenStruct(int id)
            : base(id)
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
