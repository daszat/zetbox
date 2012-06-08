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

namespace Zetbox.API.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Base;

    public class TestObjClass_TestNameCollectionEntry
        : CollectionEntryBaseImpl, IValueCollectionEntry<TestObjClass, string>
    {
        public TestObjClass_TestNameCollectionEntry()
            : base(null)
        {
        }

        private string _Value;
        private int _fk_Parent;

        public IDataObject ParentObject { get; set; }
        public object ValueObject { get; set; }

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                base.NotifyPropertyChanging("Value", null, null);
                _Value = value;
                base.NotifyPropertyChanged("Value", null, null);
            }
        }

        [XmlIgnore()]
        public TestObjClass Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public int fk_Parent
        {
            get
            {
                return _fk_Parent;
            }
            set
            {
                _fk_Parent = value;
            }
        }

        public override void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            sw.Write(this.Value);
            sw.Write(this.fk_Parent);
        }

        public override IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            var baseResult = base.FromStream(sr);
            sr.Read(out this._Value);
            sr.Read(out this._fk_Parent);
            return baseResult;
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            this._Value = ((TestObjClass_TestNameCollectionEntry)obj)._Value;
            this._fk_Parent = ((TestObjClass_TestNameCollectionEntry)obj)._fk_Parent;
        }

        public override Type GetImplementedInterface()
        {
            return typeof(IValueCollectionEntry<TestObjClass, string>);
        }

        public virtual Guid PropertyID { get { return Guid.Empty; } }
    }
}
