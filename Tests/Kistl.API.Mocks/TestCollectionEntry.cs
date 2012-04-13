
namespace Kistl.API.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class TestCollectionEntry
        : IRelationEntry
    {
        private int _ID = -1;
        public int ID { get { return _ID; } set { _ID = value; } }
        public bool IsReadonly { get; private set; }
        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// </summary>
        public AccessRights CurrentAccessRights { get { return Kistl.API.AccessRights.Full; } }

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

        public void CopyTo(IRelationEntry obj)
        {
            ((TestCollectionEntry)obj).ID = this.ID;
            ((TestCollectionEntry)obj).TestName = this.TestName;
        }

        public IEnumerable<IPersistenceObject> FromStream(BinaryReader sr)
        {
            BinarySerializer.FromStream(out _ID, sr);
            BinarySerializer.FromStream(out _TestName, sr);
            return null;
        }

        public void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            BinarySerializer.ToStream(ReadOnlyContext.GetInterfaceType(this).ToSerializableType(), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream(TestName, sw);
        }

        public IEnumerable<IPersistenceObject> FromStream(KistlStreamReader sr)
        {
            sr.Read(out _ID);
            sr.Read(out _TestName);
            return null;
        }

        public void ToStream(KistlStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            sw.Write(ReadOnlyContext.GetInterfaceType(this).ToSerializableType());
            sw.Write(ID);
            sw.Write(TestName);
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

        public virtual IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
            return null;
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
            Context = ctx;
        }

        public IKistlContext Context
        {
            get;
            private set;
        }
        public IReadOnlyKistlContext ReadOnlyContext
        {
            get { return Context; }
        }

        public void DetachFromContext(IKistlContext ctx)
        {
            ctx = null;
        }

        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void ApplyChangesFrom(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (((TestCollectionEntry)obj).GetImplementedInterface() != this.GetImplementedInterface())
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

        public Type GetImplementedInterface()
        {
            return typeof(IRelationEntry);
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
