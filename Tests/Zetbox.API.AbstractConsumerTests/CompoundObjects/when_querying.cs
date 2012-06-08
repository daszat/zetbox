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
using NUnit.Framework;
using Zetbox.App.Test;

namespace Zetbox.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_querying : CompoundObjectFixture
    {
        [Test]
        [Ignore("Pending Case #1963")]
        public void should_find_nulls()
        {
            obj = ctx.GetQuery<TestCustomObject>().Where(o => o.PhoneNumberMobile == null).FirstOrDefault();
            Assert.That(obj, Is.Not.Null, "no object found");
            Assert.That(obj.PhoneNumberMobile, Is.Null, "wrong object found");
        }

        [Test]
        [Ignore("Pending Case #1963")]
        public void should_find_values()
        {
            obj = ctx.GetQuery<TestCustomObject>().Where(o => o.PhoneNumberMobile != null).FirstOrDefault();
            Assert.That(obj, Is.Not.Null, "no object found");
            Assert.That(obj.PhoneNumberMobile, Is.Not.Null, "wrong object found");
        }

        [Test]
        public void should_find_by_property()
        {
            obj = ctx.GetQuery<TestCustomObject>().Where(o => o.PhoneNumberMobile.Number == TEST_MOBILE_NUMBER).FirstOrDefault();
            Assert.That(obj, Is.Not.Null, "no object found");
            Assert.That(obj.PhoneNumberMobile, Is.Not.Null, "wrong object found");
            Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(TEST_MOBILE_NUMBER), "wrong object found");
        }
    }
}
