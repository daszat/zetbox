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

namespace Kistl.Client.Tests
{
    public abstract class PresenterTest<CONTROL, PRESENTER>
        where CONTROL : IBasicControl
        where PRESENTER : IPresenter
    {
        protected TestObject obj { get; set; }
        protected Visual visual { get; set; }
        protected ControlInfo cInfo { get; set; }
        protected PresenterInfo pInfo { get; set; }
        protected CONTROL widget { get; set; }
        protected PRESENTER presenter { get; set; }
        protected static Mockery mocks { get; set; }
        protected IKistlContext MockContext { get; set; }

        protected void Init(ControlInfo ci, BaseProperty bp)
        {
            if (mocks == null)
            {
                mocks = new Mockery();
            }
            MockContext = mocks.NewMock<IKistlContext>();
            obj = new TestObject(MockContext);

            visual = new Visual() { Name = ci.Control, Property = bp };
            cInfo = KistlGUIContext.FindControlInfo(Toolkit.TEST, visual);
            pInfo = KistlGUIContext.FindPresenterInfo(visual);
            widget = (CONTROL)KistlGUIContext.CreateControl(cInfo);
            presenter = (PRESENTER)KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);
        }

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

        protected abstract TYPE? GetObjectValue();
        protected abstract TYPE? GetWidgetValue();
        protected abstract void SetObjectValue(TYPE? v);
        protected abstract void UserInput(TYPE? v);

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

        protected abstract TYPE GetObjectValue();
        protected abstract TYPE GetWidgetValue();
        protected abstract void SetObjectValue(TYPE v);
        protected abstract void UserInput(TYPE v);

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
    }
}
