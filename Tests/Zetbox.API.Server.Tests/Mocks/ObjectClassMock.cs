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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.App.GUI;

namespace Zetbox.API.Server.Mocks
{
    public class ObjectClassMock : Zetbox.App.Base.ObjectClass
    {
        #region ObjectClass Members

        public ICollection<Zetbox.App.Base.AccessControl> AccessControlList
        {
            get { return new List<Zetbox.App.Base.AccessControl>(); }
        }

        public Zetbox.App.Base.ObjectClass BaseObjectClass
        {
            get;
            set;
        }

        public ICollection<Zetbox.App.Base.InstanceConstraint> Constraints
        {
            get { throw new NotImplementedException(); }
        }

        public Zetbox.App.GUI.ViewModelDescriptor DefaultViewModelDescriptor
        {
            get;
            set;
        }

        public ICollection<Zetbox.App.Base.Interface> ImplementsInterfaces
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

        public virtual Guid ObjectClassID { get { return Guid.Empty; } }

        public ICollection<Zetbox.App.Base.ObjectClass> SubClasses
        {
            get { throw new NotImplementedException(); }
        }

        public string TableName
        {
            get;
            set;
        }

        public string CodeTemplate
        {
            get;
            private set;
        }

        public void CreateDefaultMethods()
        {
            throw new NotImplementedException();
        }

        public Zetbox.App.Base.Method CreateMethod()
        {
            throw new NotImplementedException();
        }

        public Zetbox.App.Base.Relation CreateRelation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Zetbox.App.Base.Method> GetInheritedMethods()
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

        public Zetbox.App.GUI.Icon DefaultIcon
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public ICollection<Zetbox.App.Base.Method> Methods
        {
            get { throw new NotImplementedException(); }
        }

        public Zetbox.App.Base.Module Module
        {
            get;
            set;
        }

        public IList<Zetbox.App.Base.Property> Properties
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

        public IZetboxContext Context
        {
            get { throw new NotImplementedException(); }
        }

        public IReadOnlyZetboxContext ReadOnlyContext
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

        public void AttachToContext(IZetboxContext ctx)
        {
            throw new NotImplementedException();
        }

        public void DetachFromContext(IZetboxContext ctx)
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

        public void ToStream(System.IO.BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
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

        public Zetbox.App.Base.Identity ChangedBy
        {
            get;
            set;
        }

        public DateTime ChangedOn
        {
            get;
            set;
        }

        public Zetbox.App.Base.Identity CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
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

        #region INamedObject

        public string GetName()
        {
            throw new NotImplementedException();
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

        public ControlKind RequestedKind
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

        public ICollection<ObjectClassFilterConfiguration> FilterConfigurations
        {
            get { throw new NotImplementedException(); }
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

        #region IComparable
        int System.IComparable.CompareTo(object other)
        {
            if (other == null) return 1;
            var aStr = this.ToString();
            var bStr = other.ToString();
            if (aStr == null && bStr == null) return 0;
            if (aStr == null) return -1;
            return aStr.CompareTo(bStr);
        }
        #endregion

    }
}
