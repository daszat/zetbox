
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Kistl.API;

    public abstract class AbstractNHibernateProxy : IDataObject
    {
        public virtual int ID { get; set; }

        public void NotifyPreSave()
        {
            throw new NotImplementedException();
        }

        public void NotifyPostSave()
        {
            throw new NotImplementedException();
        }

        public void NotifyCreated()
        {
            throw new NotImplementedException();
        }

        public void NotifyDeleting()
        {
            throw new NotImplementedException();
        }

        public AccessRights CurrentAccessRights
        {
            get { throw new NotImplementedException(); }
        }

        public DataObjectState ObjectState
        {
            get { throw new NotImplementedException(); }
        }

        public IKistlContext Context
        {
            get { throw new NotImplementedException(); }
        }

        public IReadOnlyKistlContext ReadOnlyContext
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAttached
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadonly
        {
            get { throw new NotImplementedException(); }
        }

        public void NotifyPropertyChanging(string property, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }

        public void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }

        public void AttachToContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public void DetachFromContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public void ApplyChangesFrom(IPersistenceObject obj)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue
        {
            add { }
            remove { }
        }

        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue
        {
            add { }
            remove { }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { }
            remove { }
        }

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { }
            remove { }
        }

        public void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            throw new NotImplementedException();
        }

        public void FromStream(BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public void ToStream(XmlWriter xml)
        {
            throw new NotImplementedException();
        }

        public void FromStream(XmlReader xml)
        {
            throw new NotImplementedException();
        }

        public void ReloadReferences()
        {
            throw new NotImplementedException();
        }

        public AttributeCollection GetAttributes()
        {
            throw new NotImplementedException();
        }

        public string GetClassName()
        {
            throw new NotImplementedException();
        }

        public string GetComponentName()
        {
            throw new NotImplementedException();
        }

        public TypeConverter GetConverter()
        {
            throw new NotImplementedException();
        }

        public EventDescriptor GetDefaultEvent()
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            throw new NotImplementedException();
        }

        public object GetEditor(Type editorBaseType)
        {
            throw new NotImplementedException();
        }

        public System.ComponentModel.EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        public EventDescriptorCollection GetEvents()
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            throw new NotImplementedException();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            throw new NotImplementedException();
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
