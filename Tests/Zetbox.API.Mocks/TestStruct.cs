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
    using System.IO;
    using System.Linq;
    using System.Text;

    public interface TestCompoundObject : ICompoundObject
    {
        string TestProperty { get; set; }
    }

    public class TestCompoundObjectImpl : BaseCompoundObject, TestCompoundObject
    {
        public TestCompoundObjectImpl()
            : base(null)
        {
        }

        public override void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            sw.Write(TestProperty);
        }

        public override IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            var baseResult = base.FromStream(sr);
            TestProperty = sr.ReadString();
            return baseResult;
        }

        #region TestCompoundObject Members

        public string TestProperty { get; set; }

        #endregion

        public override Type GetImplementedInterface()
        {
            return typeof(TestCompoundObject);
        }

        public override void ApplyChangesFrom(ICompoundObject other)
        {
            throw new NotImplementedException();
        }

        public override Guid CompoundObjectID
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj)) return true;
            var other = obj as TestCompoundObjectImpl;
            if (obj == null) return false;

            return other.TestProperty == this.TestProperty;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * TestProperty.GetHashCode();
        }
    }
}
