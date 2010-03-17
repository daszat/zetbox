
namespace Kistl.DalProvider.Frozen
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseFrozenObject
        : IPersistenceObject
    {
        /// <summary>
        /// 
        /// </summary>
        protected BaseFrozenObject()
        {
            throw new InvalidOperationException("BaseFrozenObject constructor without id called");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected BaseFrozenObject(int id)
        {
            this.ID = id;
            this.IsSealed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSealed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public void Seal()
        {
            if (IsSealed)
            {
                throw new ReadOnlyObjectException();
            }
            IsSealed = true;
        }

        #region IPersistenceObject Members
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public DataObjectState ObjectState { get { return DataObjectState.Unmodified; } }

        #region IStreamable Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sw"></param>
        public virtual void ToStream(BinaryWriter sw)
        {
            BinarySerializer.ToStream(new SerializableType(this.GetInterfaceType()), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream((int)ObjectState, sw);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="auxObjects"></param>
        public virtual void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            ToStream(sw);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sr"></param>
        public virtual void FromStream(BinaryReader sr)
        {
            throw new InvalidOperationException("Cannot deserialize to a frozen Object");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="modules"></param>
        [Obsolete]
        public virtual void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public virtual void ToStream(System.Xml.XmlWriter xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public virtual void FromStream(System.Xml.XmlReader xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        // frozen objects are already connected
        /// <summary>
        /// 
        /// </summary>
        public void ReloadReferences() { }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void NotifyPropertyChanging(string property, object oldValue, object newValue) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void NotifyPropertyChanged(string property, object oldValue, object newValue) { }

        /// <summary>
        /// 
        /// </summary>
        // TODO: actually this should return the frozencontext, but it is only a IReadOnlyKistlContext, not a full IKistlContext
        public IKistlContext Context { get { return null /* FrozenContext.Single*/; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public void AttachToContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public bool IsAttached { get { return true; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadonly { get { return IsSealed; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract InterfaceType GetInterfaceType();

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged { add { } remove { } }
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging { add { } remove { } }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue { add { } remove { } }
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue { add { } remove { } }

        #endregion


        #region ICustomTypeDescriptor Members

        protected virtual void CollectProperties(List<PropertyDescriptor> list) { }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            throw new NotImplementedException();
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            throw new NotImplementedException();
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            throw new NotImplementedException();
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            throw new NotImplementedException();
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            throw new NotImplementedException();
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            throw new NotImplementedException();
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            throw new NotImplementedException();
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            throw new NotImplementedException();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            throw new NotImplementedException();
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseFrozenDataObject : BaseFrozenObject, IDataObject
    {
        /// <summary>
        /// 
        /// </summary>
        protected BaseFrozenDataObject()
        {
            throw new InvalidOperationException("BaseFrozenDataObject constructor without id called");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected BaseFrozenDataObject(int id)
            : base(id)
        {
            // This cannot happen here, since the ActionsManager is not yet initialized.
            // ApplicationContext.Current.CustomActionsManager.AttachEvents(this);
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

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// Returnes allways Full
        /// </summary>
        public Kistl.API.AccessRights CurrentAccessRights { get { return Kistl.API.AccessRights.Full; } }


        #region IDataErrorInfo Members

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="prop">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. Returns 
        /// <value>String.Empty</value> if there is nothing to report.</returns>
        protected virtual string GetPropertyError(string prop) { return String.Empty; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get { return String.Empty; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName] { get { return GetPropertyError(columnName); } }

        #endregion


    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseFrozenCompoundObject
        : BaseFrozenObject, ICompoundObject
    {
        /// <summary>
        /// 
        /// </summary>
        protected BaseFrozenCompoundObject()
        {
            throw new InvalidOperationException("BaseFrozenCompoundObject constructor without id called");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected BaseFrozenCompoundObject(int id)
            : base(id)
        {
        }

        #region ICompoundObject Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        public void AttachToObject(IPersistenceObject obj, string property)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        public void DetachFromObject(IPersistenceObject obj, string property)
        {
        }

        #endregion

        #region ICloneable Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            // since we are immutable, returning ourself is save
            return this;
        }

        #endregion
    }
}
