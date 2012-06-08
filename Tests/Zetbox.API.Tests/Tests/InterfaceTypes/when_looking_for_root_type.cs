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

namespace Zetbox.API.Tests.InterfaceTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    public interface BaseClass : IDataObject { }
    public interface ChildClass : BaseClass { }

    public interface ValueCollectionEntry : IValueCollectionEntry<BaseClass, ChildClass> { }

    [TestFixture]
    public class when_looking_for_root_type : AbstractApiTestFixture
    {
        [Test]
        public void should_recognize_data_objects()
        {
            var baseInterfaceType = iftFactory(typeof(BaseClass));
            Assert.That(baseInterfaceType.GetRootType(), Is.EqualTo(baseInterfaceType));

            var childInterfaceType = iftFactory(typeof(ChildClass));
            Assert.That(childInterfaceType.GetRootType(), Is.EqualTo(baseInterfaceType));
        }

        [Test]
        public void should_recognize_valueCollectionEntries()
        {
            var ceInterfaceType = iftFactory(typeof(ValueCollectionEntry));
            Assert.That(ceInterfaceType.GetRootType(), Is.EqualTo(ceInterfaceType));
        }
    }
}
