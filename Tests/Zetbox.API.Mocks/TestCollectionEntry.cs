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

    public class TestCollectionEntry
        : IRelationEntry
    {
        private int _ID = -1;
        public int ID { get { return _ID; } set { _ID = value; } }
        public bool IsReadonly { get; private set; }
        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// </summary>
        public AccessRights CurrentAccessRights { get { return Zetbox.API.AccessRights.Full; } }

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

        public IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            sr.Read(out _ID);
            sr.Read(out _TestName);
            return null;
        }

        public void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
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

        public void AttachToContext(IZetboxContext ctx, Func<IFrozenContext> lazyFrozenContext)
        {
            Context = ctx;
        }

        public IZetboxContext Context
        {
            get;
            private set;
        }
        public IReadOnlyZetboxContext ReadOnlyContext
        {
            get { return Context; }
        }

        public void DetachFromContext(IZetboxContext ctx)
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
