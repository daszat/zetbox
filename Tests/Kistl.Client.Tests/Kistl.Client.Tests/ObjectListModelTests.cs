
namespace Kistl.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Mocks;
    using Kistl.App.Base;
    using Kistl.Client.Mocks;
    using Kistl.Client.Presentables;

    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ObjectListModelTests
    {
        [SetUp]
        public void SetUp()
        {
            var appCtx = new TestApplicationContext("Kistl.Client.Tests.Config.xml") ;
        }

        [TearDown]
        public void TearDown()
        {

        }


        [Test]
        public void TestCreation()
        {
            Dictionary<IDataObject, PresentableModel> models = new Dictionary<IDataObject, PresentableModel>();
            Mock<IModelFactory> facMock = KistlMockFactory.CreateFactory(models);
            Mock<IGuiApplicationContext> appCtxMock = new Mock<IGuiApplicationContext>();
            appCtxMock.Setup(appCtx => appCtx.Factory).Returns(facMock.Object);

            Mock<TestObject> objMock = KistlMockFactory.CreateTestObject();

            var list = new ObservableCollection<TestObject>();
            objMock.Setup(obj => obj.TestCollection).Returns(list);

            Mock<ObjectReferenceProperty> orpMock = KistlMockFactory.CreateObjectReferenceProperty("TestCollection", true);

            var olm = new ObjectListModel(appCtxMock.Object, null, objMock.Object, orpMock.Object);
            Assert.That(olm.Value.Count, Is.EqualTo(0));

            var dom = new DataObjectModel(null, null, objMock.Object);
            models[objMock.Object] = dom;
            olm.AddItem(dom);

            Assert.That(olm.Value.Count, Is.EqualTo(1));
            Assert.That(objMock.Object.TestCollection.Count, Is.EqualTo(1));
            Assert.That(objMock.Object.TestCollection.Single(), Is.SameAs(objMock.Object));
        }
    }
}
