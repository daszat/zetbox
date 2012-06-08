// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Mocks;
    using Zetbox.App.Base;
    using Zetbox.Client.Mocks;
    using Zetbox.Client.Presentables;

    using Moq;
    using NUnit.Framework;
    using System.ComponentModel;

    using Autofac;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;

    [TestFixture]
    [Ignore("Mocks not implemented")]
    public class ObjectListModelTests : AbstractClientTestFixture
    {
        Dictionary<IDataObject, ViewModel> models;
        Mock<IViewModelFactory> facMock;
        Mock<IViewModelDependencies> appCtxMock;
        Mock<TestObject> objMock;
        ObservableCollection<TestObject> list;
        Mock<ObjectReferenceProperty> orpMock;
        ObjectListViewModel olm;

        public override void SetUp()
        {
            base.SetUp();
            // setup an ObjectListModel
            models = new Dictionary<IDataObject, ViewModel>();
            facMock = ZetboxMockFactory.CreateFactory(models);
            appCtxMock = new Mock<IViewModelDependencies>();
            appCtxMock.Setup(ac => ac.Factory).Returns(facMock.Object);

            objMock = scope.Resolve<ZetboxMockFactory>().CreateTestObject();

            list = new ObservableCollection<TestObject>();
            objMock.Setup(obj => obj.TestCollection).Returns(list);
            objMock.Setup(obj => obj.GetProperties()).Returns(new PropertyDescriptorCollection(new PropertyDescriptor[] { }));

            orpMock = scope.Resolve<ZetboxMockFactory>().CreateObjectReferenceProperty("TestCollection", true);

            olm = new ObjectListViewModel(appCtxMock.Object, null, null, (IObjectCollectionValueModel<IList<IDataObject>>)orpMock.Object.GetPropertyValueModel(objMock.Object));

        }

        private DataObjectViewModel CreateNewDataObjectViewModel()
        {
            var newMock = scope.Resolve<ZetboxMockFactory>().CreateTestObject();
            var dom = new DataObjectViewModel(null, GetContext(), null, newMock.Object);
            models[objMock.Object] = dom;
            return dom;
        }

        [Test]
        public void TestCreation()
        {
            Assert.That(olm.Value.Count, Is.EqualTo(0));

            var dom = CreateNewDataObjectViewModel();
            olm.AddItem(dom);

            Assert.That(olm.Value.Count, Is.EqualTo(1));
            Assert.That(objMock.Object.TestCollection.Count, Is.EqualTo(1));
            Assert.That(objMock.Object.TestCollection.Single(), Is.SameAs(dom.Object));
        }

        [Test]
        public void TestAdd()
        {
            var objs = new[] { CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel() };

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
            var objs = new List<DataObjectViewModel>() { CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel() };

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
            var objs = new List<DataObjectViewModel>() { CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel(), CreateNewDataObjectViewModel() };

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
