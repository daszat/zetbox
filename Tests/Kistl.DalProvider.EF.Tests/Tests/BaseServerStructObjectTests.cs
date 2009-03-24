using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Tests.Skeletons;
using Kistl.App.Test;
using Kistl.DalProvider.EF.Mocks;

using NUnit.Framework;
using Kistl.DALProvider.EF;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class BaseServerStructObjectTests
        : IStreamableTests<TestPhoneStruct__Implementation__>
    {
        TestCustomObject__Implementation__ parent;
        TestPhoneStruct__Implementation__ attachedObj;

        [SetUp]
        public void SetUpTestObject()
        {
            var testCtx = new ServerApiContextMock();

            obj = new TestPhoneStruct__Implementation__() { AreaCode = "ABC", Number = "123456" };

            parent = new TestCustomObject__Implementation__();
            attachedObj = (TestPhoneStruct__Implementation__)parent.PhoneNumberOffice;
            attachedObj.AreaCode = "attachedAreaCode";
            attachedObj.Number = "attachedNumber";
        }

        [Test]
        public void Clone_creates_memberwise_equal_object()
        {
            var c = (TestPhoneStruct)obj.Clone();

            Assert.That(c.AreaCode, Is.EqualTo(obj.AreaCode));
            Assert.That(c.Number, Is.EqualTo(obj.Number));
            Assert.That((c as BaseStructObject).ParentObject, Is.Null, "cloned object should not be attached to foreign object");
            Assert.That((c as BaseStructObject).ParentProperty, Is.Null, "cloned object should forget old ParentProperty");
        }

        [Test]
        public void should_roudtrip_members_correctly()
        {
            var result = this.SerializationRoundtrip(obj);

            Assert.That(result.AreaCode, Is.EqualTo(obj.AreaCode));
            Assert.That(result.Number, Is.EqualTo(obj.Number));
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            const string testValue = "test";

            bool hasChanged = false;
            bool hasChanging = false;

            PropertyChangingEventHandler changingHandler = new PropertyChangingEventHandler(delegate(object sender, PropertyChangingEventArgs e)
            {
                Assert.That(hasChanging, Is.False, "changing event should be only triggered once");
                Assert.That(sender, Is.SameAs(obj), "sender should be the changing object");
                Assert.That(e.PropertyName, Is.EqualTo("AreaCode"), "changing property name should be correct");
                Assert.That(obj.AreaCode, Is.Not.EqualTo(testValue), "changing event should be triggered before the value has changed");
                hasChanging = true;
            });
            PropertyChangedEventHandler changedHandler = new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e)
            {
                Assert.That(hasChanged, Is.False, "changed event should be only triggered once");
                Assert.That(sender, Is.SameAs(obj), "sender should be the changed object");
                Assert.That(e.PropertyName, Is.EqualTo("AreaCode"), "changed property name should be correct");
                Assert.That(obj.AreaCode, Is.EqualTo(testValue), "changed event should be triggered after the value has changed");
                hasChanged = true;
            });

            obj.PropertyChanged += changedHandler;
            obj.PropertyChanging += changingHandler;

            obj.AreaCode = testValue;

            Assert.That(hasChanged, Is.True);
            Assert.That(hasChanging, Is.True);

            obj.PropertyChanged -= changedHandler;
            obj.PropertyChanging -= changingHandler;
        }

        [Test]
        public void Parent_NotifyPropertyChanged_ing()
        {
            const string testValue = "test";

            bool hasChanged = false;
            bool hasChanging = false;

            PropertyChangingEventHandler changingHandler = new PropertyChangingEventHandler(delegate(object sender, PropertyChangingEventArgs e)
            {
                Assert.That(hasChanging, Is.False, "changing event should be only triggered once");
                Assert.That(sender, Is.SameAs(parent), "sender should be the changing object");
                Assert.That(e.PropertyName, Is.EqualTo("PhoneNumberOffice"), "changing property name should be correct");
                Assert.That(parent.PhoneNumberOffice.AreaCode, Is.Not.EqualTo(testValue), "changing event should be triggered before the value has changed");
                hasChanging = true;
            });
            PropertyChangedEventHandler changedHandler = new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e)
            {
                Assert.That(hasChanged, Is.False, "changed event should be only triggered once");
                Assert.That(sender, Is.SameAs(parent), "sender should be the changed object");
                Assert.That(e.PropertyName, Is.EqualTo("PhoneNumberOffice"), "changed property name should be correct");
                Assert.That(parent.PhoneNumberOffice.AreaCode, Is.EqualTo(testValue), "changed event should be triggered after the value has changed");
                hasChanged = true;
            });

            parent.PropertyChanged += changedHandler;
            parent.PropertyChanging += changingHandler;

            parent.PhoneNumberOffice.AreaCode = testValue;

            Assert.That(hasChanged, Is.True);
            Assert.That(hasChanging, Is.True);

            parent.PropertyChanged -= changedHandler;
            parent.PropertyChanging -= changingHandler;
        }


        [Test]
        public void should_be_initialised_as_members()
        {
            var parent = new TestCustomObject__Implementation__();
            Assert.That(parent.PhoneNumberMobile, Is.Not.Null, "structs should be initialised when creating an object");
            Assert.That((parent.PhoneNumberMobile as BaseServerStructObject_EntityFramework).ParentObject,
                Is.SameAs(parent),
                "parent should be initialised correctly when creating an object");
            Assert.That((parent.PhoneNumberMobile as BaseServerStructObject_EntityFramework).ParentProperty,
                Is.EqualTo("PhoneNumberMobile"),
                "parent property name should be initialised correctly when creating an object");
        }

    }
}
