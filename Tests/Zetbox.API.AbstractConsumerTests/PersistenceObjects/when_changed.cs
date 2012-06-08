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
using System.Globalization;
using System.Linq;
using System.Text;

using Zetbox.API;

using NUnit.Framework;

namespace Zetbox.API.AbstractConsumerTests.PersistenceObjects
{
    
    public abstract class when_changed
        : ObjectLoadFixture
    {

        Random rnd = new Random();
        string testName;

        [SetUp]
        public void InitTestValue()
        {
            testName = rnd.NextDouble().ToString(CultureInfo.InvariantCulture);
            if (testName == obj.PersonName)
            {
                testName += rnd.NextDouble().ToString(CultureInfo.InvariantCulture);
            }
        }

        [Test]
        public void should_be_modified()
        {
            Assume.That(obj.PersonName, Is.Not.EqualTo(testName));
            Assert.That(obj.ObjectState, Is.Not.EqualTo(DataObjectState.Modified), "object already modified at begin of test");
            obj.PersonName = testName;
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified), "object didn't notice change to property");
        }
    }
}
