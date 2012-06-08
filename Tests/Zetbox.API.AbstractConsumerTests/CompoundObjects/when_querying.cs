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
