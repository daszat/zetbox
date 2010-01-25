using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_changing : CompoundObjectFixture
    {
        [Test]
        public void should_be_set_null()
        {
            Assert.That(obj.PhoneNumberMobile, Is.Not.Null);
            obj.PhoneNumberMobile = null;
            Assert.That(obj.PhoneNumberMobile, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_not_be_set_null()
        {
            Assert.That(obj.PhoneNumberOffice, Is.Not.Null);
            obj.PhoneNumberOffice = null;
        }
    }
}
