using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_changing_a_CompoundObject_list
        : CompoundObjectFixture
    {
        [Test]
        public void should_not_be_modified_on_add()
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
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        public void should_not_be_modified_on_delete()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.PhoneNumbersOther, Is.Not.Null);
            Assert.That(obj.PhoneNumbersOther.Count, Is.GreaterThanOrEqualTo(MINLISTCOUNT));
            int count = obj.PhoneNumbersOther.Count;

            var c = obj.PhoneNumbersOther.Skip(2).First();
            obj.PhoneNumbersOther.Remove(c);

            Assert.That(obj.PhoneNumbersOther.Count, Is.EqualTo(count - 1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
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
