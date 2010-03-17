using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server.Mocks
{
    public class ObjectClassMock : Kistl.App.Base.ObjectClass
    {
        #region ObjectClass Members

        public ICollection<Kistl.App.Base.AccessControl> AccessControlList
        {
            get { return new List<Kistl.App.Base.AccessControl>(); }
        }

        public Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get; set;
        }

        public ICollection<Kistl.App.Base.InstanceConstraint> Constraints
        {
            get { throw new NotImplementedException(); }
        }

        public Kistl.App.GUI.PresentableModelDescriptor DefaultPresentableModelDescriptor
        {
            get;
            set;
        }

        public ICollection<Kistl.App.Base.Interface> ImplementsInterfaces
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFrozenObject
        {
            get;
            set;
        }

        public bool IsSimpleObject
        {
            get;
            set;
        }

        public ICollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get { throw new NotImplementedException(); }
        }

        public string TableName
        {
            get;
            set;
        }

        public void CreateDefaultMethods()
        {
            throw new NotImplementedException();
        }

        public Kistl.App.Base.Method CreateMethod()
        {
            throw new NotImplementedException();
        }

        public Kistl.App.Base.Relation CreateRelation()
        {
            throw new NotImplementedException();
        }

        public IList<Kistl.App.Base.Method> GetInheritedMethods()
        {
            throw new NotImplementedException();
        }

        public void ImplementInterfaces()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DataType Members

        public string Name
        {
            get;
            set;
        }

        public Kistl.App.GUI.Icon DefaultIcon
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public ICollection<Kistl.App.Base.MethodInvocation> MethodInvocations
        {
            get { throw new NotImplementedException(); }
        }

        public ICollection<Kistl.App.Base.Method> Methods
        {
            get { throw new NotImplementedException(); }
        }

        public Kistl.App.Base.Module Module
        {
            get;
            set;
        }

        public IList<Kistl.App.Base.Property> Properties
        {
            get { throw new NotImplementedException(); }
        }

        public bool ShowIconInLists
        {
            get;
            set;
        }

        public bool ShowIdInLists
        {
            get;
            set;
        }

        public bool ShowNameInLists
        {
            get;
            set;
        }

        public Type GetDataType()
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeString()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataObject Members

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

        #endregion

        #region IPersistenceObject Members

        public int ID
        {
            get;
            set;
        }

        public DataObjectState ObjectState
        {
            get { throw new NotImplementedException(); }
        }

        public IKistlContext Context
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
            if (PropertyChanging != null) PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(property));
            if (PropertyChangingWithValue != null) PropertyChangingWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        public void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(property));
            if (PropertyChangedWithValue != null) PropertyChangedWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
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

        public InterfaceType GetInterfaceType()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INotifyingObject Members

        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue;

        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue;

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyPropertyChanging Members

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region IStreamable Members

        public void ToStream(System.IO.BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            throw new NotImplementedException();
        }

        public void FromStream(System.IO.BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public void ToStream(System.Xml.XmlWriter xml)
        {
            throw new NotImplementedException();
        }

        public void FromStream(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }

        public void ReloadReferences()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IChangedBy Members

        public Kistl.App.Base.Identity ChangedBy
        {
            get;
            set;
        }

        public DateTime? ChangedOn
        {
            get;
            set;
        }

        public Kistl.App.Base.Identity CreatedBy
        {
            get;
            set;
        }

        public DateTime? CreatedOn
        {
            get;
            set;
        }

        #endregion

        #region IExportable Members

        public Guid ExportGuid
        {
            get;
            set;
        }

        #endregion

        #region ObjectClass Members


        public bool IsAbstract
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICustomTypeDescriptor Members

        public System.ComponentModel.AttributeCollection GetAttributes()
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

        public System.ComponentModel.TypeConverter GetConverter()
        {
            throw new NotImplementedException();
        }

        public System.ComponentModel.EventDescriptor GetDefaultEvent()
        {
            throw new NotImplementedException();
        }

        public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
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

        public System.ComponentModel.EventDescriptorCollection GetEvents()
        {
            throw new NotImplementedException();
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties()
        {
            throw new NotImplementedException();
        }

        public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
