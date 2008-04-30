using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.Client.Tests
{
    public abstract class PresenterTest<CONTROL, PRESENTER>
        where CONTROL : IBasicControl
        where PRESENTER : Presenter
    {
        protected TestObject obj { get; set; }
        protected Visual visual { get; set; }
        protected ControlInfo cInfo { get; set; }
        protected PresenterInfo pInfo { get; set; }
        protected CONTROL widget { get; set; }
        protected PRESENTER presenter { get; set; }

        protected void Init(ControlInfo ci, BaseProperty bp)
        {
            obj = new TestObject();
            visual = new Visual() { Name = ci.Control, Property = bp };
            cInfo = KistlGUIContext.FindControlInfo(Toolkit.TEST, visual);
            pInfo = KistlGUIContext.FindPresenterInfo(visual);
            widget = (CONTROL)KistlGUIContext.CreateControl(cInfo);
            presenter = (PRESENTER)KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);
        }
    }

    public abstract class NullablePresenterTests<TYPE, CONTROL, PRESENTER> : PresenterTest<CONTROL, PRESENTER>
        where TYPE : struct
        where CONTROL : IValueControl<TYPE?>
        where PRESENTER : Presenter
    {
        protected virtual void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, Is.True, "the widget should be in a valid state after this operation");
            Assert.That(GetWidgetValue(), Is.EqualTo(GetObjectValue()), "the widget should have the same value as the object");
        }

        protected abstract TYPE? GetObjectValue();
        protected abstract TYPE? GetWidgetValue();
        protected abstract void SetObjectValue(TYPE? v);
        protected abstract void UserInput(TYPE? v);

        [Test]
        public void HandleNoUserInput()
        {
            Assert.That(GetObjectValue(), Is.Null, String.Format("{0} should default to null", visual.Property));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInput()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolProperty);
            AssertWidgetHasValidValue();
            UserInput(null);
            Assert.That(obj.TestBool, Is.Null);
            AssertWidgetHasValidValue();
        }
    }

    public abstract class ValuePresenterTests<TYPE, CONTROL, PRESENTER> : PresenterTest<CONTROL, PRESENTER>
        where TYPE : struct
        where CONTROL : IValueControl<TYPE>
        where PRESENTER : Presenter
    {

        protected virtual void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, Is.True, "the widget should be in a valid state after this operation");
            Assert.That(GetWidgetValue(), Is.EqualTo(GetObjectValue()), "the widget should have the same value as the object");
        }

        protected virtual void AssertWidgetHasInvalidValue()
        {
            Assert.That(widget.IsValidValue, Is.False, "the widget should be in a invalid state after this operation");
            Assert.That(GetWidgetValue(), Is.Not.EqualTo(GetObjectValue()), "the widget should not have the same value as the object, because it is invalid");
        }

        protected abstract TYPE GetObjectValue();
        protected abstract TYPE GetWidgetValue();
        protected abstract void SetObjectValue(TYPE v);
        protected abstract void UserInput(TYPE v);

        [Test]
        public void HandleNoUserInput()
        {
            Assert.That(GetObjectValue(), Is.Not.Null, String.Format("{0} should default to a value", visual.Property));
            AssertWidgetHasValidValue();
        }

    }

    public abstract class ReferencePresenterTests<TYPE, CONTROL, PRESENTER> : PresenterTest<CONTROL, PRESENTER>
        where TYPE : class, new()
        where CONTROL : IValueControl<TYPE>
        where PRESENTER : Presenter
    {

        protected virtual void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, Is.True, "the widget should be in a valid state after this operation");
            Assert.That(GetWidgetValue(), Is.EqualTo(GetObjectValue()), "the widget should have the same value as the object");
        }

        protected virtual void AssertWidgetHasInvalidValue()
        {
            Assert.That(widget.IsValidValue, Is.False, "the widget should be in a invalid state after this operation");
            Assert.That(GetWidgetValue(), Is.Not.EqualTo(GetObjectValue()), "the widget should not have the same value as the object, because it is invalid");
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
            Init(TestBoolControl.Info, TestObject.TestBoolProperty);
            AssertWidgetHasValidValue();
            UserInput(null);
            Assert.That(obj.TestBool, Is.Null);
            AssertWidgetHasValidValue();
        }
    }
}
