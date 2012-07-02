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
    public abstract class when_changing : CompoundObjectFixture
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_not_be_set_null()
        {
            Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
            obj.PhoneNumberOffice = null;
        }

        [Test]
        public void getter_should_return_same_instance_twice()
        {
            var co1 = obj.PhoneNumberOffice;
            var co2 = obj.PhoneNumberOffice;
            Assert.AreSame(co1, co2);
        }

        [Test]
        public void should_change_with_reference()
        {
            string orig_number = obj.PhoneNumberOffice.Number;
            string new_number = "Should changed " + DateTime.Now;
            Assert.That(new_number, Is.Not.EqualTo(orig_number));

            var co = obj.PhoneNumberOffice;
            co.Number = new_number;

            Assert.That(obj.PhoneNumberOffice.Number, Is.Not.EqualTo(orig_number));
            Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(new_number));

            Assert.That(co.Number, Is.EqualTo(obj.PhoneNumberOffice.Number));
            
            Assert.That(co.Number, Is.Not.EqualTo(orig_number));
            Assert.That(co.Number, Is.EqualTo(new_number));
        }

        [Test]
        public void should_clone_reference()
        {
            obj = ctx.GetQuery<TestCustomObject>().ToList().Where(o => o.PhoneNumberMobile != null).First();
            string orig_number_mobile = obj.PhoneNumberMobile.Number;
            string orig_number_office = obj.PhoneNumberOffice.Number;
            string new_number = "Should changed " + DateTime.Now;
            Assert.That(new_number, Is.Not.EqualTo(orig_number_office));

            var co = obj.PhoneNumberOffice;
            obj.PhoneNumberMobile = co;
            Assert.AreNotSame(obj.PhoneNumberMobile, co);

            co.Number = new_number;

            Assert.That(obj.PhoneNumberOffice.Number, Is.Not.EqualTo(orig_number_office));
            Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(new_number));

            Assert.That(obj.PhoneNumberMobile.Number, Is.EqualTo(orig_number_office));
            Assert.That(obj.PhoneNumberMobile.Number, Is.Not.EqualTo(new_number));
            Assert.That(obj.PhoneNumberMobile.Number, Is.Not.EqualTo(orig_number_mobile));

            Assert.That(co.Number, Is.EqualTo(obj.PhoneNumberOffice.Number));

            Assert.That(co.Number, Is.Not.EqualTo(orig_number_office));
            Assert.That(co.Number, Is.EqualTo(new_number));
        }

        [Test]
        public void clone_creates_memberwise_equal_object()
        {
            var c = (TestPhoneCompoundObject)obj.PhoneNumberOffice.Clone();

            Assert.AreNotSame(c, obj.PhoneNumberOffice);
            Assert.That(c.AreaCode, Is.EqualTo(obj.PhoneNumberOffice.AreaCode));
            Assert.That(c.Number, Is.EqualTo(obj.PhoneNumberOffice.Number));
            Assert.That((c as BaseCompoundObject).ParentObject, Is.Null, "cloned object should not be attached to foreign object");
            Assert.That((c as BaseCompoundObject).ParentProperty, Is.Null, "cloned object should forget old ParentProperty");
        }

        [Test]
        public void should_be_modified_when_setting_nullable_to_new_reference()
        {
            obj = ctx.GetQuery<TestCustomObject>().ToList().Where(o => o.PhoneNumberMobile != null).First();
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
            
            obj.PhoneNumberMobile = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void should_be_modified_when_changing_to_new_reference()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumberOffice, Is.Not.Null);

            obj.PhoneNumberOffice = ctx.CreateCompoundObject<TestPhoneCompoundObject>();
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void should_be_modified_when_changing_to_existing_reference()
        {
            obj = ctx.GetQuery<TestCustomObject>().ToList().Where(o => o.PhoneNumberMobile != null).First();
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
            Assert.That(obj.PhoneNumberMobile, Is.Not.Null);

            obj.PhoneNumberMobile = obj.PhoneNumberOffice;
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }
    }
}
