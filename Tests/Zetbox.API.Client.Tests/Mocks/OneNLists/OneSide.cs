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

namespace Zetbox.API.Client.Mocks.OneNLists
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.DalProvider.Base.RelationWrappers;
    using Zetbox.API.Async;

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

        public IZetboxContext Context
        {
            get { return null; }
        }

        public IReadOnlyZetboxContext ReadOnlyContext
        {
            get { return null; }
        }

        public virtual Guid ObjectClassID { get { return Guid.Empty; } }


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

        public void UpdateParent(string propertyName, IDataObject parentObj)
        {
            throw new NotImplementedException();
        }

        public ZbTask TriggerFetch(string propName)
        {
            throw new NotImplementedException();
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

        public void AttachToContext(IZetboxContext ctx, Func<IFrozenContext> lazyFrozenContext)
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

        public void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
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

        int System.IComparable.CompareTo(object other)
        {
            if (other == null) return 1;
            var aStr = this.ToString();
            var bStr = other.ToString();
            if (aStr == null && bStr == null) return 0;
            if (aStr == null) return -1;
            return aStr.CompareTo(bStr);
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
