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
        public void should_be_null()
        {
            Assert.That(newObj.PhoneNumberMobile, Is.Null);
        }

        [Test]
        public void should_be_attached_to_their_parent()
        {
            Assert.That((newObj.PhoneNumberOffice as BaseCompoundObject).ParentObject, Is.SameAs(newObj));
            Assert.That((obj.PhoneNumberOffice as BaseCompoundObject).ParentObject, Is.SameAs(obj));
            // nullable, ignore: Assert.That((obj.PhoneNumberMobile as BaseCompoundObject).ParentObject, Is.SameAs(obj));
        }

        [Test]
        public void should_have_right_property_name_set()
        {
            Assert.That((newObj.PhoneNumberOffice as BaseCompoundObject).ParentProperty, Is.EqualTo("PhoneNumberOffice"));
            Assert.That((obj.PhoneNumberOffice as BaseCompoundObject).ParentProperty, Is.EqualTo("PhoneNumberOffice"));
            // nullable, ignore: Assert.That((obj.PhoneNumberMobile as BaseCompoundObject).ParentProperty, Is.EqualTo("PhoneNumberMobile"));
        }
    }
}
