
namespace Kistl.API.Client.Mocks.OneNLists
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.DalProvider.Base;

    class OneSide : IOneSide
    {
        private static int _maxId = 0;

        public static int NextId()
        {
            _maxId += 1;
            return _maxId;
        }

        public OneSide(List<INSide> initialObjects)
        {
            _list = new OneNRelationList<INSide>("OneSide", "OneSide_pos", this, () => OnPropertyChanged("NSide"), initialObjects);
        }

        private int _id = NextId();

        private readonly OneNRelationList<INSide> _list;

        public IList<INSide> NSide
        {
            get
            {
                return _list;
            }
        }

        public OneNRelationList<INSide> List
        {
            get
            {
                return _list;
            }
        }

        public string Description { get; set; }

        public int ID
        {
            get { return _id; }
        }

        public IKistlContext Context
        {
            get { return null; }
        }

        public IReadOnlyKistlContext ReadOnlyContext
        {
            get { return null; }
        }

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region unimplemented

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

        public DataObjectState ObjectState
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

        public InterfaceType GetInterfaceType()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INotifyingObject Members

        event PropertyChangeWithValueEventHandler INotifyingObject.PropertyChangedWithValue
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event PropertyChangeWithValueEventHandler INotifyingObject.PropertyChangingWithValue
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion

        #region INotifyPropertyChanging Members

        event System.ComponentModel.PropertyChangingEventHandler System.ComponentModel.INotifyPropertyChanging.PropertyChanging
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion

        #region IStreamable Members

        public void ToStream(System.IO.BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
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

        #region ICustomTypeDescriptor Members

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

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
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

        #endregion

        #endregion
    }
}
