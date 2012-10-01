// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Mocks
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
    public class TestDataObjectImpl
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

        public Zetbox.API.AccessRights CurrentAccessRights { get { return AccessRights.Full; } }

        public string TestField;

        int System.IComparable.CompareTo(object other)
        {
            return 0;
        }

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

        public void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            sw.Write(ReadOnlyContext.GetInterfaceType(this).ToSerializableType());
            sw.Write(ID);
            sw.Write(StringProperty);
            sw.Write(IntProperty);
            sw.Write(BoolProperty);
        }

        public IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            sr.Read(out _ID);
            sr.Read(out _StringProperty);
            sr.Read(out _IntProperty);
            sr.Read(out _BoolProperty);
            return null;
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
            ((TestDataObjectImpl)obj).ID = this.ID;
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

        private IZetboxContext _context = null;
        [XmlIgnore]
        public IZetboxContext Context
        {
            get
            {
                return _context;
            }
        }
        [XmlIgnore]
        public IReadOnlyZetboxContext ReadOnlyContext
        {
            get
            {
                return _context;
            }
        }
        public void AttachToContext(IZetboxContext ctx, Func<IFrozenContext> lazyFrozenContext)
        {
            _context = ctx;
        }

        public void DetachFromContext(IZetboxContext ctx)
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
            if (((TestDataObjectImpl)obj).GetImplementedInterface() != this.GetImplementedInterface())
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

        public virtual Guid ObjectClassID { get { return Guid.Empty; } }

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


        public void UpdateParent(string propertyName, IDataObject parentObj)
        {
            throw new NotImplementedException();
        }

        public bool IsInitialized(string propName)
        {
            throw new NotImplementedException();
        }
        public void Recalculate(string propName)
        {
            throw new NotImplementedException();
        }
    }
}
