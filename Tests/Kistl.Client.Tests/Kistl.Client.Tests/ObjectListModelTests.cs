
namespace Kistl.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Mocks;
    using Kistl.App.Base;
    using Kistl.Client.Mocks;
    using Kistl.Client.Presentables;

    using Moq;
    using NUnit.Framework;
    using System.ComponentModel;

    [TestFixture]
    public class ObjectListModelTests
    {
        Dictionary<IDataObject, PresentableModel> models;
        Mock<IModelFactory> facMock;
        Mock<IGuiApplicationContext> appCtxMock;
        Mock<TestObject> objMock;
        ObservableCollection<TestObject> list;
        Mock<ObjectReferenceProperty> orpMock;
        ObjectListModel olm;

        [SetUp]
        public void SetUp()
        {
            // setup assembly information
            var appCtx = new TestApplicationContext("Kistl.Client.Tests.Config.xml");

            // setup an ObjectListModel
            models = new Dictionary<IDataObject, PresentableModel>();
            facMock = KistlMockFactory.CreateFactory(models);
            appCtxMock = new Mock<IGuiApplicationContext>();
            appCtxMock.Setup(ac => ac.Factory).Returns(facMock.Object);

            objMock = KistlMockFactory.CreateTestObject();

            list = new ObservableCollection<TestObject>();
            objMock.Setup(obj => obj.TestCollection).Returns(list);
            objMock.Setup(obj => obj.GetProperties()).Returns(new PropertyDescriptorCollection(new PropertyDescriptor[] { }));

            orpMock = KistlMockFactory.CreateObjectReferenceProperty("TestCollection", true);

            olm = new ObjectListModel(appCtxMock.Object, null, objMock.Object, orpMock.Object);

        }

        [TearDown]
        public void TearDown()
        {

        }

        private DataObjectModel CreateNewDataObjectModel()
        {
            var newMock = KistlMockFactory.CreateTestObject();
            var dom = new DataObjectModel(null, null, newMock.Object);
            models[objMock.Object] = dom;
            return dom;
        }

        [Test]
        public void TestCreation()
        {
            Assert.That(olm.Value.Count, Is.EqualTo(0));

            var dom = CreateNewDataObjectModel();
            olm.AddItem(dom);

            Assert.That(olm.Value.Count, Is.EqualTo(1));
            Assert.That(objMock.Object.TestCollection.Count, Is.EqualTo(1));
            Assert.That(objMock.Object.TestCollection.Single(), Is.SameAs(dom.Object));
        }

        [Test]
        public void TestAdd()
        {
            var objs = new[] { CreateNewDataObjectModel(), CreateNewDataObjectModel(), CreateNewDataObjectModel(), CreateNewDataObjectModel() };

            foreach (var dom in objs)
            {
                olm.AddItem(dom);
            }

            Assert.That(olm.Value.Count, Is.EqualTo(objs.Length));
            Assert.That(objMock.Object.TestCollection, Is.EquivalentTo(objs.Select(dom => dom.Object).ToArray()));
            Assert.That(list, Is.EquivalentTo(objs.Select(dom => dom.Object).ToArray()));
        }

        [Test]
        public void TestMoveUp([Range(1, 3)]int item)
        {
            var objs = new List<DataObjectModel>() { CreateNewDataObjectModel(), CreateNewDataObjectModel(), CreateNewDataObjectModel(), CreateNewDataObjectModel() };

            foreach (var dom in objs)
            {
                olm.AddItem(dom);
            }

            olm.MoveItemUp(objs[item]);

            var tmp = objs[item - 1];
            objs[item - 1] = objs[item];
            objs[item] = tmp;

            Assert.That(olm.Value.Count, Is.EqualTo(objs.Count));
            Assert.That(objMock.Object.TestCollection, Is.EquivalentTo(objs.Select(dom => dom.Object)));
            Assert.That(list, Is.EquivalentTo(objs.Select(dom => dom.Object).ToArray()));
        }

        [Test]
        public void TestMoveDown([Range(0, 2)]int item)
        {
            var objs = new List<DataObjectModel>() { CreateNewDataObjectModel(), CreateNewDataObjectModel(), CreateNewDataObjectModel(), CreateNewDataObjectModel() };

            foreach (var dom in objs)
            {
                olm.AddItem(dom);
            }

            olm.MoveItemDown(objs[item]);

            var tmp = objs[item + 1];
            objs[item + 1] = objs[item];
            objs[item] = tmp;

            Assert.That(olm.Value.Count, Is.EqualTo(objs.Count));
            Assert.That(objMock.Object.TestCollection, Is.EquivalentTo(objs.Select(dom => dom.Object)));
            Assert.That(list, Is.EquivalentTo(objs.Select(dom => dom.Object).ToArray()));
        }
    }
}
