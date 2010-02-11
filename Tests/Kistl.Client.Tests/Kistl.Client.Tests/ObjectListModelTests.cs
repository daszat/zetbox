
namespace Kistl.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Mocks;
    using Kistl.Client.Presentables;
    using Kistl.Tests;

    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ObjectListModelTests
        : MockeryTestFixture
    {
        [Test]
        public void TestCreation()
        {
            var list = new ObservableCollection<TestObject>();
            Mock<TestObject> objMock = KistlMockFactory.CreateTestObject();
            objMock.Setup(obj => obj.TestCollection).Returns(list);

            Mock<ObjectReferenceProperty> orpMock = KistlMockFactory.CreateObjectReferenceProperty("TestCollection");

            orpMock.Setup(orp => orp.GetIsList()).Returns(true);

            var olm = new ObjectListModel(null, null, objMock.Object, orpMock.Object);

            Assert.That(olm.Value.Count, Is.EqualTo(0));
        }
    }
}
