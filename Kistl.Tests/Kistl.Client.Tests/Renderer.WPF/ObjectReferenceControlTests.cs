using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Tests;

namespace Kistl.GUI.Renderer.WPF.Tests
{
    [TestFixture]
    public class ObjectReferenceControlTests
    {
        private List<IDataObject> Items = new List<IDataObject>(new[] {
                    new TestObject() { ID = 20 },
                    new TestObject() { ID = 30 },
                    new TestObject() { ID = 40 },
                });


        protected Mockery mocks { get; set; }
        protected IKistlContext MockContext { get; set; }

        /// <summary>
        /// Override this method to add setup code after the Mockery and the MockContext have been setup.
        /// </summary>
        protected virtual void CustomSetUp() { }

        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            CustomSetUp();
        }

        protected TestObject obj { get; set; }
        protected Visual visual { get; set; }
        protected ControlInfo cInfo { get; set; }
        protected PresenterInfo pInfo { get; set; }
        protected IObjectReferenceControl widget { get; set; }
        protected ObjectReferencePresenter presenter { get; set; }

        protected void Init(ControlInfo ci, BaseProperty bp)
        {
            obj = new TestObject() { ID = 1 };

            visual = new Visual() { Name = ci.Control, Property = bp };
            cInfo = KistlGUIContext.FindControlInfo(Toolkit.WPF, visual);
            pInfo = KistlGUIContext.FindPresenterInfo(visual);
            widget = (IObjectReferenceControl)KistlGUIContext.CreateControl(cInfo);
            // presenter = (PointerPresenter)KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);
        }

        [Test]
        public void TestLoadedType()
        {
            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceDescriptor);
            Assert.IsAssignableFrom(typeof(ObjectReferenceControl), widget);
        }

        private bool _UserInputCalled = false;

        [Test]
        public void TestUserInputSupression()
        {
            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceDescriptor);

            List<IDataObject> items = CreateItems();

            _UserInputCalled = false;
            widget.UserInput += new EventHandler(widget_UserInput);
            widget.ItemsSource = items;
            Assert.IsFalse(_UserInputCalled, "UserInput was called on Set_ItemsSource");
            widget.Value = items[3];
            Assert.IsFalse(_UserInputCalled, "UserInput was called on Set_Value");
        }

        private List<IDataObject> CreateItems()
        {
            List<IDataObject> items = new List<IDataObject>();
            foreach (int i in new[] { 1, 2, 3, 4, 5 })
            {
                IDataObject ido = mocks.NewMock<IDataObject>();
                Stub.On(ido).Method("ToString")
                    .With()
                    .Will(Return.Value(
                        String.Format("MockDataObject ID={0}", i)));
                Stub.On(ido).GetProperty("ID")
                    .Will(Return.Value(i));
                items.Add(ido);
            }
            return items;
        }

        void widget_UserInput(object sender, EventArgs e)
        {
            _UserInputCalled = true;
        }

        [Test]
        public void TestUserInput()
        {
            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceDescriptor);
            List<IDataObject> items = CreateItems();
            _UserInputCalled = false;
            widget.UserInput += new EventHandler(widget_UserInput);
            widget.ItemsSource = items;

            Assert.IsFalse(_UserInputCalled, "UserInput was called on Set_ItemsSource");

            ((System.Windows.DependencyObject)widget).SetValue(ObjectReferenceControl.ValueProperty, items[3]);
            // AssertWidgetIsValid();
            // AssertThatWidget.Value==items[3];
            Assert.IsTrue(_UserInputCalled, "UserInput was not called on simulated user input");
        }

        [Test]
        public void TestWithPresenter()
        {
            MockContext = mocks.NewMock<IKistlContext>("MockContext");
            TestObject.GlobalContext = MockContext;

            Stub.On(MockContext).
                Method("Find").
                With(TestObject.ObjectClass.ID).
                Will(Return.Value(TestObject.ObjectClass));

            Stub.On(MockContext).
                Method("Find").
                With(TestObject.Module.ID).
                Will(Return.Value(TestObject.Module));

            IQueryable<IDataObject> idoq = mocks.NewMock<IQueryable<IDataObject>>("IDataObjectQueryable");

            Stub.On(MockContext).
                Method("GetQuery").
                With(new ObjectType(typeof(TestObject))).
                Will(Return.Value(idoq));

            Stub.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(Items.GetEnumerator()));

            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceDescriptor);

            presenter = (ObjectReferencePresenter)KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);


            _UserInputCalled = false;
            widget.UserInput += new EventHandler(widget_UserInput);

            ((System.Windows.DependencyObject)widget).SetValue(ObjectReferenceControl.ValueProperty, 1);
            Assert.IsTrue(_UserInputCalled, "UserInput was not called on simulated user input");
            Assert.AreEqual(Items[1], obj.TestObjectReference, "TestObjectReference was not set correctly");

            _UserInputCalled = false;
            IDataObject expectedItem = (IDataObject)Items[2];
            obj.TestObjectReference = expectedItem;
            Assert.AreEqual(expectedItem, obj.TestObjectReference, "TestObjectReference was not set correctly");
            Assert.AreEqual(Items[1], widget.Value, "widget's index was not set correctly");
            Assert.IsFalse(_UserInputCalled, "UserInput was called when changing the object");

        }
    }
}
