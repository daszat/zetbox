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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.App.Test;

using NUnit.Framework;

namespace Zetbox.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_changing_a_CompoundObject_list
        : CompoundObjectFixture
    {
        [SetUp]
        public new void SetUp()
        {
            obj = ctx.GetQuery<TestCustomObject>().Where(o => o.PhoneNumbersOther.Count >= MINLISTCOUNT).FirstOrDefault();
            Assert.That(obj, Is.Not.Null);
        }

        [Test]
        public void should_be_modified_on_add()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumbersOther, Is.Not.Null);
            Assert.That(obj.PhoneNumbersOther.Count, Is.GreaterThanOrEqualTo(MINLISTCOUNT));
            int count = obj.PhoneNumbersOther.Count;

            var c = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
            c.AreaCode = "123";
            c.Number = testNumber;
            obj.PhoneNumbersOther.Add(c);

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count + 1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void should_be_modified_on_remove()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumbersOther, Is.Not.Null);
            Assert.That(obj.PhoneNumbersOther.Count, Is.GreaterThanOrEqualTo(MINLISTCOUNT));
            int count = obj.PhoneNumbersOther.Count;

            var c = obj.PhoneNumbersOther.Skip(2).First();
            obj.PhoneNumbersOther.Remove(c);

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count - 1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void should_not_be_modified_on_change_member()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumbersOther, Is.Not.Null);
            Assert.That(obj.PhoneNumbersOther.Count, Is.GreaterThanOrEqualTo(MINLISTCOUNT));

            int count = obj.PhoneNumbersOther.Count;
            
            var c = obj.PhoneNumbersOther.Skip(2).First();
            Assert.That(c, Is.Not.Null);
            c.Number = testNumber;

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }
    }
}
