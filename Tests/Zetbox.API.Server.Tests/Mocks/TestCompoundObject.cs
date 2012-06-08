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

namespace Zetbox.API.Server.Mocks
{
    public class TestCompoundObject : BaseServerCompoundObject
    {
        public TestCompoundObject()
            : base(null)
        {
            var obj = new TestObjClassImpl();
            base.AttachToObject(obj, "Test");
        }

        public int TestInt { get; set; }
        public string TestString { get; set; }

        public override void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            sw.Write(TestInt);
            sw.Write(TestString);
        }

        public override IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            var baseResult = base.FromStream(sr);
            TestInt = sr.ReadInt32();
            TestString = sr.ReadString();
            return baseResult;
        }

        public override Type GetImplementedInterface()
        {
            return typeof(ICompoundObject);
        }

        public override Guid CompoundObjectID
        {
            get { throw new NotImplementedException(); }
        }
    }
}
