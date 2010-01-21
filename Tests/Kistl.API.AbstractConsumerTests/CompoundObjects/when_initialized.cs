using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_initialized
        : CompoundObjectFixture
    {
        [Test]
        public void should_exist()
        {
            Assert.That(obj.PhoneNumberMobile , Is.Not.Null);
        }

        [Test]
        public void should_be_attached_to_their_parent()
        {
            Assert.That((obj.PhoneNumberMobile as BaseCompoundObject).ParentObject, Is.SameAs(obj));
        }

        [Test]
        public void should_have_right_property_name_set()
        {
            Assert.That((obj.PhoneNumberMobile as BaseCompoundObject).ParentProperty, Is.EqualTo("PhoneNumberMobile"));
        }
    }
}
