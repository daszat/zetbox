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

using Zetbox.App.Test;

using NUnit.Framework;

namespace Zetbox.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_initialized
        : CompoundObjectFixture
    {
        protected TestCustomObject newObj;
        protected IZetboxContext newCtx;

        protected override void CreateTestData()
        {
            base.CreateTestData();
            newCtx = GetContext();
            newObj = newCtx.Create<TestCustomObject>();
        }

        public override void ForgetContext()
        {
            base.ForgetContext();
            newCtx = null;
            newObj = null;
        }

        [Test]
        public void should_exist()
        {
            Assert.That(newObj.PhoneNumberOffice, Is.Not.Null);
        }

        [Test]
        public void nullable_should_have_null_values()
        {
            Assert.That(newObj.PhoneNumberMobile, Is.Not.Null);
            Assert.That(newObj.PhoneNumberMobile.AreaCode, Is.Null);
            Assert.That(newObj.PhoneNumberMobile.Number, Is.Null);
        }

        [Test]
        public void should_be_attached_to_their_parent()
        {
            Assert.That((newObj.PhoneNumberOffice as BaseCompoundObject).ParentObject, Is.SameAs(newObj));
            Assert.That((obj.PhoneNumberOffice as BaseCompoundObject).ParentObject, Is.SameAs(obj));
            Assert.That((obj.PhoneNumberMobile as BaseCompoundObject).ParentObject, Is.SameAs(obj));
        }

        [Test]
        public void should_have_right_property_name_set()
        {
            Assert.That((newObj.PhoneNumberOffice as BaseCompoundObject).ParentProperty, Is.EqualTo("PhoneNumberOffice"));
            Assert.That((obj.PhoneNumberOffice as BaseCompoundObject).ParentProperty, Is.EqualTo("PhoneNumberOffice"));
            Assert.That((obj.PhoneNumberMobile as BaseCompoundObject).ParentProperty, Is.EqualTo("PhoneNumberMobile"));
        }
    }
}
