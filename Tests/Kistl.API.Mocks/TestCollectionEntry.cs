
namespace Kistl.API.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class TestCollectionEntry
        : IRelationCollectionEntry
    {
        private int _ID = -1;
        public int ID { get { return _ID; } set { _ID = value; } }
        public bool IsReadonly { get; private set; }

        public Guid RelationID { get { return Guid.NewGuid(); } }
        public IDataObject AObject { get; set; }
        public IDataObject BObject { get; set; }


        private string _TestName;
        public string TestName { get { return _TestName; } set { _TestName = value; } }

        public override bool Equals(object obj)
        {
            if (obj is TestCollectionEntry)
            {
                TestCollectionEntry x = (TestCollectionEntry)obj;
                return
                       this.ID == x.ID
                    && this.TestName == x.TestName;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public void CopyTo(IRelationCollectionEntry obj)
        {
            ((TestCollectionEntry)obj).ID = this.ID;
            ((TestCollectionEntry)obj).TestName = this.TestName;
        }

        public void FromStream(BinaryReader sr)
        {
            BinarySerializer.FromStream(out _ID, sr);
            BinarySerializer.FromStream(out _TestName, sr);
        }

        public void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            BinarySerializer.ToStream(new SerializableType(new InterfaceType(typeof(IRelationCollectionEntry))), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream(TestName, sw);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue;
        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue;


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

        public void AttachToContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public IKistlContext Context
        {
            get { throw new NotImplementedException(); }
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
            if (obj.GetType().ToInterfaceType() != this.GetType().ToInterfaceType())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }


        public DataObjectState ObjectState
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

        #region IPersistenceObject Members


        public bool IsAttached
        {
            get { throw new NotImplementedException(); }
        }

        public InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(IRelationCollectionEntry));
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
    }
}
