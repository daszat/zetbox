using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

using NMock2;
using NUnit.Framework;

namespace Kistl.GUI.Tests
{

    /// <summary>
    /// This class adds a presenter to the ControlTests. 
    /// </summary>
    /// <typeparam name="CONTROL"></typeparam>
    /// <typeparam name="PRESENTER"></typeparam>
    public abstract class PresenterTests<OBJECT, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where CONTROL : IBasicControl
        where PRESENTER : IPresenter
    {
        protected PresenterTests(
            PresenterHarness<OBJECT, CONTROL, PRESENTER> presenterHarness)
        {
            Assert.IsNotNull(presenterHarness, "presenterHarness cannot be null");
            /*
            ObjectHarness = new ObjectHarness<OBJECT>();
            ControlHarness = new ControlHarness<CONTROL>(visualType, toolkit);
            PresenterHarness = new PresenterHarness<OBJECT, CONTROL, PRESENTER>(ObjectHarness, ControlHarness);
             */
            PresenterHarness = presenterHarness;
        }

        protected ObjectHarness<OBJECT> ObjectHarness { get { return PresenterHarness.ObjectHarness; } }
        protected ControlHarness<CONTROL> ControlHarness { get { return PresenterHarness.ControlHarness; } }
        protected PresenterHarness<OBJECT, CONTROL, PRESENTER> PresenterHarness { get; private set; }

        protected PRESENTER Presenter { get { return PresenterHarness.Presenter; } }
        protected CONTROL Widget { get { return ControlHarness.Widget; } }
        protected Visual Visual { get { return ControlHarness.Visual; } }
        protected OBJECT Object { get { return ObjectHarness.Instance; } }

        protected Mockery Mockery { get { return ObjectHarness.Mockery; } }
        protected IKistlContext MockContext { get { return ObjectHarness.MockContext; } }

        [SetUp]
        public void SetUp()
        {
            ObjectHarness.SetUp();
            ControlHarness.SetUp();
            PresenterHarness.SetUp();
        }

        [Test]
        public void TestPresenterSetUpCorrect()
        {
            Assert.IsNotNull(PresenterHarness, "presenter should have been initialised");
            PresenterHarness.TestSetUpCorrect();
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(InvalidOperationException))]
        public void ReInit()
        {
            PresenterHarness.Presenter.InitializeComponent(Object, Visual, Widget);
        }

    }

    public abstract class NullablePresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        : PresenterTests<OBJECT, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where TYPE : struct
        where CONTROL : IValueControl<TYPE?>
        where PRESENTER : IPresenter
    {

        protected NullablePresenterTests(
            PresenterHarness<OBJECT, CONTROL, PRESENTER> presenterHarness)
            : base(presenterHarness)
        {
        }

        protected virtual void AssertWidgetHasValidValue()
        {
            Assert.IsTrue(Widget.IsValidValue, "the widget should be in a valid state after this operation");
            Assert.AreEqual(GetWidgetValue(), GetObjectValue(), "the widget should have the same value as the object");
        }

        protected virtual void AssertWidgetHasInvalidValue()
        {
            Assert.IsFalse(Widget.IsValidValue, "the widget should be in an invalid state after this operation");
            Assert.AreNotEqual(GetWidgetValue(), GetObjectValue(), "the invalid widget should not have the same value as the object");
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
        /// <summary>
        /// return a list of invalid values for the property.
        /// the default implementation returns the empty list.
        /// </summary>
        protected virtual IEnumerable<TYPE> SomeInvalidValues() { return new List<TYPE>(); }

        [Test]
        public void HandleNoUserInput()
        {
            Assert.IsNull(GetObjectValue(), String.Format("{0} should default to null", Visual.Property));
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
        public void HandleValidUserInput()
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
        public void HandleInvalidUserInput()
        {
            AssertWidgetHasValidValue();

            TYPE? original = GetObjectValue();

            foreach (TYPE? value in SomeInvalidValues())
            {
                UserInput(value);
                AssertWidgetHasInvalidValue();
                Assert.AreEqual(original, GetObjectValue(), "Object should retain original value");
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
            }

            // After having invalid values set, 
            // choosing a valid value again, should 
            // clear all flags and set to the object
            IEnumerator<TYPE> validValues = SomeValues().GetEnumerator();
            if (validValues.MoveNext())
            {
                TYPE value = validValues.Current;
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

    public abstract class ReferencePresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        : PresenterTests<OBJECT, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where TYPE : class
        where CONTROL : IValueControl<TYPE>
        where PRESENTER : IPresenter
    {

        protected ReferencePresenterTests(
            PresenterHarness<OBJECT, CONTROL, PRESENTER> presenterHarness)
            : base(presenterHarness)
        {
        }

        protected virtual void AssertWidgetHasValidValue()
        {
            Assert.That(Widget.IsValidValue, "the widget should be in a valid state after this operation");
            Assert.AreEqual(GetWidgetValue(), GetObjectValue(), "the widget should have the same value as the object");
        }

        protected virtual void AssertWidgetHasInvalidValue()
        {
            Assert.That(!Widget.IsValidValue, "the widget should be in a invalid state after this operation");
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
        /// return the expected Default Value at creation time
        /// </summary>
        protected abstract TYPE DefaultValue();
        /// <summary>
        /// return a list of valid values for the property
        /// </summary>
        protected abstract IEnumerable<TYPE> SomeValues();
        /// <summary>
        /// return a list of invalid values for the property.
        /// the default implementation returns the empty list.
        /// </summary>
        protected virtual IEnumerable<TYPE> SomeInvalidValues() { return new List<TYPE>(); }

        [Test]
        public void HandleNoUserInput()
        {
            Assert.AreEqual(DefaultValue(), GetObjectValue(), String.Format("property {0} should have proper default", Visual.Property));
            Assert.AreEqual(DefaultValue(), GetWidgetValue(), String.Format("widget should show default value", Visual.Property, DefaultValue()));
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
        public void HandleValidUserInput()
        {
            AssertWidgetHasValidValue();

            foreach (TYPE value in SomeValues())
            {
                UserInput(value);
                AssertWidgetHasValidValue();
                Assert.AreEqual(value, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
            }
        }

        [Test]
        public void HandleInvalidUserInput()
        {
            AssertWidgetHasValidValue();

            TYPE original = GetObjectValue();

            foreach (TYPE value in SomeInvalidValues())
            {
                UserInput(value);
                AssertWidgetHasInvalidValue();
                Assert.AreEqual(original, GetObjectValue(), "Object should retain original value");
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
            }

            // After having invalid values set, 
            // choosing a valid value again, should 
            // clear all flags and set to the object
            IEnumerator<TYPE> validValues = SomeValues().GetEnumerator();
            if (validValues.MoveNext())
            {
                TYPE value = validValues.Current;
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
