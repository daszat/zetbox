using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Server.Mocks;
using Kistl.API.Tests.Skeletons;
using Kistl.App.Test;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class BaseServerStructObjectTests
        : IStreamableTests<TestPhoneStruct__Implementation__>
    {

        [SetUp]
        public void SetUpTestObject()
        {
            var testCtx = new ServerApiContextMock();

            obj = new TestPhoneStruct__Implementation__() { AreaCode = "ABC", Number = "123456" };
        }

        [Test]
        public void Clone_creates_memberwise_equal_object()
        {
            var c = (TestPhoneStruct)obj.Clone();
            Assert.That(c.AreaCode, Is.EqualTo(obj.AreaCode));
            Assert.That(c.Number, Is.EqualTo(obj.Number));
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
            bool hasChanged = false;
            bool hasChanging = false;

            PropertyChangedEventHandler changedHandler = new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e) { hasChanged = true; });
            PropertyChangingEventHandler changingHanlder = new PropertyChangingEventHandler(delegate(object sender, PropertyChangingEventArgs e) { hasChanging = true; });

            obj.PropertyChanged += changedHandler;
            obj.PropertyChanging += changingHanlder;

            obj.NotifyPropertyChanging("AreaCode");
            obj.AreaCode = "test";
            obj.NotifyPropertyChanged("AreaCode");

            Assert.That(hasChanged, Is.True);
            Assert.That(hasChanging, Is.True);

            obj.PropertyChanged -= changedHandler;
            obj.PropertyChanging -= changingHanlder;
        }
    }
}
