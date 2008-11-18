using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.Client.Presentables;
using Kistl.Tests;

using NMock2;
using NUnit.Framework;
using NMock2.Matchers;
using System.ComponentModel;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class ObjectListModelTests : MockeryTestFixture
    {
        internal class ModelFactoryMock : ModelFactory
        {
            internal ModelFactoryMock(
                IThreadManager uiManager, IThreadManager asyncManager,
                IKistlContext guiCtx, IKistlContext dataCtx) :
                base(uiManager, asyncManager, guiCtx, dataCtx)
            {
            }

            public override ModelFactory CreateNewFactory(IKistlContext newDataCtx)
            {
                throw new NotImplementedException();
            }

            protected override void CreateSelectionDialog(DataObjectSelectionTaskModel selectionTaskModel, bool activate)
            {
                throw new NotImplementedException();
            }

            protected override void CreateWorkspace(WorkspaceModel workspace, bool activate)
            {
                throw new NotImplementedException();
            }

            protected override void ShowDataObject(DataObjectModel dataObject, bool activate)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestCreation()
        {
            ThreadManagerMock uiThreadMock = new ThreadManagerMock("UI Thread");
            ThreadManagerMock backgroundThreadMock = new ThreadManagerMock("Background Thread");
            ThreadManagerMock.SetDefaultThread(uiThreadMock);

            IKistlContext ctx = MockFactory.CreateContext(m);
            ModelFactoryMock factory = new ModelFactoryMock(new SynchronousThreadManager(), new SynchronousThreadManager(), ctx, ctx);

            TestObject obj = MockFactory.CreateTestObject(m);
            ObjectReferenceProperty orp = MockFactory.CreateObjectReferenceProperty(m, "TestCollection");
            
            Expect.Once
                .On(obj)
                .EventAdd("PropertyChanged", new TypeMatcher(typeof(PropertyChangedEventHandler)));
            
            Expect.Once
                .On(orp)
                .EventAdd("PropertyChanged", new TypeMatcher(typeof(PropertyChangedEventHandler)));

            Expect.AtLeastOnce
                .On(obj)
                .GetProperty("TestCollection")
                .Will(Return.Value(new List<TestObject>()));

            Assert.AreEqual(0, obj.TestCollection.Count);

            // TODO: Test fails because NMock2 doesn't support reflecting on the mocked interface
            // var olm = new ObjectListModel(uiThreadMock, backgroundThreadMock, ctx, ctx, factory, obj, orp);
        }
    }
}
