
namespace Kistl.API.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
using System.Xml.Serialization;

    public interface TestDataObject : IDataObject
    {
        bool BoolProperty { get; set; }
        int IntProperty { get; set; }
        string StringProperty { get; set; }
    }

    [Serializable]
    public class TestDataObject__Implementation__
        : IDataObject, ICloneable, INotifyPropertyChanged, TestDataObject
    {
        private int _ID;
        private string _StringProperty;
        private int _IntProperty;
        private bool _BoolProperty;

        public int ID { get { return _ID; } set { _ID = value; } }
        public string StringProperty { get { return _StringProperty; } set { _StringProperty = value; } }
        public int IntProperty { get { return _IntProperty; } set { _IntProperty = value; } }
        public bool BoolProperty { get { return _BoolProperty; } set { _BoolProperty = value; } }
        public DataObjectState ObjectState { get; set; }

        private int PrivateIntProperty { get; set; }

        public Kistl.API.AccessRights CurrentAccessRights { get { return AccessRights.Full; } }

        public string TestField;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj is TestDataObject)
            {
                TestDataObject x = (TestDataObject)obj;
                return
                       this.BoolProperty == x.BoolProperty
                    && this.IntProperty == x.IntProperty
                    && this.StringProperty == x.StringProperty
                    && this.ID == x.ID;
            }
            else
            {
                return false;
            }
        }

        public void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            BinarySerializer.ToStream(Context.GetInterfaceType(this).ToSerializableType(), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream(StringProperty, sw);
            BinarySerializer.ToStream(IntProperty, sw);
            BinarySerializer.ToStream(BoolProperty, sw);
        }

        public void FromStream(BinaryReader sr)
        {
            BinarySerializer.FromStream(out _ID, sr);
            BinarySerializer.FromStream(out _StringProperty, sr);
            BinarySerializer.FromStream(out _IntProperty, sr);
            BinarySerializer.FromStream(out _BoolProperty, sr);
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

        public void ReloadReferences()
        {
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue;
        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue;

        public void CopyTo(IDataObject obj)
        {
            ((TestDataObject__Implementation__)obj).ID = this.ID;
            ((TestDataObject)obj).IntProperty = this.IntProperty;
            ((TestDataObject)obj).StringProperty = this.StringProperty;
            ((TestDataObject)obj).BoolProperty = this.BoolProperty;
        }

        public void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            if (PropertyChangedWithValue != null)
                PropertyChangedWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        public void NotifyPropertyChanging(string property, object oldValue, object newValue)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(property));

            if (PropertyChangingWithValue != null)
                PropertyChangingWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        private IKistlContext _context = null;
        [XmlIgnore]
        public IKistlContext Context
        {
            get
            {
                return _context;
            }
        }
        [XmlIgnore]
        public IReadOnlyKistlContext ReadOnlyContext
        {
            get
            {
                return _context;
            }
        }
        public void AttachToContext(IKistlContext ctx)
        {
            _context = ctx;
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
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (((TestDataObject__Implementation__)obj).GetImplementedInterface() != this.GetImplementedInterface())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }


        [XmlIgnore]
        public bool IsAttached
        {
            get { throw new NotImplementedException(); }
        }

        [XmlIgnore]
        public bool IsReadonly { get; private set; }

        #region IDataErrorInfo Members

        [XmlIgnore]
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        string IDataErrorInfo.Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public Type GetImplementedInterface()
        {
            return typeof(TestDataObject);
        }

        public void NotifyPreSave()
        {
        }

        public void NotifyPostSave()
        {
        }

        public void NotifyCreated()
        {
        }

        public void NotifyDeleting()
        {
        }

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
    }
}
