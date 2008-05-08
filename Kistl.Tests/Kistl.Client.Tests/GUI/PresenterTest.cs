using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;

using NMock2;
using NUnit.Framework;

namespace Kistl.GUI.Tests
{
    public abstract class PresenterTest<CONTROL, PRESENTER>
        where CONTROL : IBasicControl
        where PRESENTER : IPresenter
    {
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
            MockContext = mocks.NewMock<IKistlContext>("MockContext");
            TestObject.GlobalContext = MockContext;

            Stub.On(MockContext).
                Method("Find").
                With(TestObject.ObjectClass.ID).
                // Will(Return.Value(TestObject.ObjectClass.Clone()));
                Will(Return.Value(TestObject.ObjectClass));

            Stub.On(MockContext).
                Method("Find").
                With(TestObject.Module.ID).
                // Will(Return.Value(TestObject.Module.Clone()));
                Will(Return.Value(TestObject.Module));

            CustomSetUp();

        }

        protected TestObject obj { get; set; }
        protected Visual visual { get; set; }
        protected ControlInfo cInfo { get; set; }
        protected PresenterInfo pInfo { get; set; }
        protected CONTROL widget { get; set; }
        protected PRESENTER presenter { get; set; }

        protected void Init(ControlInfo ci, BaseProperty bp)
        {
            obj = new TestObject() { ID = 1 };

            visual = new Visual() { Name = ci.Control, Property = bp };
            cInfo = KistlGUIContext.FindControlInfo(Toolkit.TEST, visual);
            pInfo = KistlGUIContext.FindPresenterInfo(visual);
            widget = (CONTROL)KistlGUIContext.CreateControl(cInfo);
            presenter = (PRESENTER)KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);
        }

        // This test depends a bit on any other test being run before and having already 
        // initialised the presenter once
        [Test]
        [ExpectedException(ExceptionType = typeof(InvalidOperationException))]
        public void ReInit()
        {
            presenter.InitializeComponent(obj, visual, widget);
        }
    }

    public abstract class NullablePresenterTests<TYPE, CONTROL, PRESENTER> : PresenterTest<CONTROL, PRESENTER>
        where TYPE : struct
        where CONTROL : IValueControl<TYPE?>
        where PRESENTER : IPresenter
    {
        protected virtual void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, "the widget should be in a valid state after this operation");
            Assert.AreEqual(GetWidgetValue(), GetObjectValue(), "the widget should have the same value as the object");
        }

        /// <summary>
        /// return the current Value of the tested Property from the Object
        /// </summary>
        protected abstract TYPE? GetObjectValue();
        /// <summary>
        /// return the current Value which is displayed by the widget
        /// </summary>
        protected abstract TYPE? GetWidgetValue();
        /// <summary>
        /// set the Value of the tested Property on the Object
        /// </summary>
        protected abstract void SetObjectValue(TYPE? v);
        /// <summary>
        /// Simulate user input on the widget
        /// </summary>
        protected abstract void UserInput(TYPE? v);
        /// <summary>
        /// return a list of valid values for the property
        /// </summary>
        protected abstract IEnumerable<TYPE> SomeValues();

        [Test]
        public void HandleNoUserInput()
        {
            Assert.IsNull(GetObjectValue(), String.Format("{0} should default to null", visual.Property));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInput()
        {
            AssertWidgetHasValidValue();
            UserInput(null);
            Assert.IsNull(GetObjectValue());
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleUserInput()
        {
            AssertWidgetHasValidValue();

            foreach (TYPE? value in SomeValues())
            {
                UserInput(value);
                AssertWidgetHasValidValue();
                Assert.AreEqual(value, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
            }
        }

        [Test]
        public void HandleProgrammaticChange()
        {
            AssertWidgetHasValidValue();
            foreach (var value in SomeValues())
            {
                SetObjectValue(value);
                AssertWidgetHasValidValue();
                Assert.AreEqual(value, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
            }
        }

    }

    public abstract class ReferencePresenterTests<TYPE, CONTROL, PRESENTER> : PresenterTest<CONTROL, PRESENTER>
        where TYPE : class
        where CONTROL : IValueControl<TYPE>
        where PRESENTER : IPresenter
    {

        protected virtual void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, "the widget should be in a valid state after this operation");
            Assert.AreEqual(GetWidgetValue(), GetObjectValue(), "the widget should have the same value as the object");
        }

        protected virtual void AssertWidgetHasInvalidValue()
        {
            Assert.That(!widget.IsValidValue, "the widget should be in a invalid state after this operation");
            Assert.AreNotEqual(GetWidgetValue(), GetObjectValue(), "the widget should not have the same value as the object, because it is invalid");
        }

        /// <summary>
        /// return the current Value of the tested Property from the Object
        /// </summary>
        protected abstract TYPE GetObjectValue();
        /// <summary>
        /// return the current Value which is displayed by the widget
        /// </summary>
        protected abstract TYPE GetWidgetValue();
        /// <summary>
        /// set the Value of the tested Property on the Object
        /// </summary>
        protected abstract void SetObjectValue(TYPE v);
        /// <summary>
        /// Simulate user input on the widget
        /// </summary>
        protected abstract void UserInput(TYPE v);
        /// <summary>
        /// return a list of valid values for the property
        /// </summary>
        protected abstract IEnumerable<TYPE> SomeValues();

        [Test]
        public void HandleNoUserInput()
        {
            // different properties can default to null or not null.
            // Assert.That(GetObjectValue(), Is.Not.Null, String.Format("{0} should default to a value", visual.Property));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInput()
        {
            AssertWidgetHasValidValue();
            UserInput(null);
            Assert.IsNull(GetObjectValue());
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleProgrammaticChange()
        {
            AssertWidgetHasValidValue();
            foreach (var v in SomeValues())
            {
                SetObjectValue(v);
                Assert.AreEqual(v, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(v, GetWidgetValue(), "Widget should display new value");
                AssertWidgetHasValidValue();
            }
        }
    }
}
