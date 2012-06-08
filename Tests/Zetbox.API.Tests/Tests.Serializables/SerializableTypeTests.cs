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

namespace Zetbox.API.Tests.Serializables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Mocks;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [TestFixture]
    public class SerializableTypeTests : AbstractApiTestFixture
    {
        SerializableType t;

        public override void SetUp()
        {
            base.SetUp();
            t = iftFactory(typeof(TestDataObject)).ToSerializableType();
        }

        [Test]
        public void GetHashCode_returns_right_value()
        {
            // TODO: better GetHashCode() testing
            Assert.That(t.GetHashCode(), Is.EqualTo(t.TypeName.GetHashCode() ^ t.AssemblyQualifiedName.GetHashCode()));
        }

        [Test]
        public void GetSystemType_returns_right_type()
        {
            Type result = t.GetSystemType();
            Assert.That(result, Is.EqualTo(typeof(TestDataObject)));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetSystemType_fails_on_invalid_AssemblyQualifiedName()
        {
            t.AssemblyQualifiedName = "Test";
            t.GetSystemType();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetSystemType_fails_on_invalid_TypeName()
        {
            t.TypeName = "Invalid Test Class Name";
            t.GetSystemType();
        }
    }
}
