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
using System.Collections.ObjectModel;
using Kistl.App.GUI;

namespace Kistl.GUI.Tests
{

    /// <summary>
    /// a structure to hold possible valid and invalid data to test the various parts of the framework
    /// </summary>
    public interface IValues<TYPE>
    {
        TYPE[] Valids { get; }
        TYPE[] Invalids { get; }
        bool IsValid(TYPE item);
    }

    public class ValuesAdapter<TYPE> : IValues<TYPE>
    {
        #region IValues<TYPE> Members
        public virtual TYPE[] Valids { get; protected set; }
        public virtual TYPE[] Invalids { get; protected set; }

        public virtual bool IsValid(TYPE item)
        {
            return Valids.Contains(item);
        }
        #endregion
    }

    public sealed class Values<TYPE> : IValues<TYPE>
    {
        #region IValues<TYPE> Members
        public TYPE[] Valids { get; set; }
        public TYPE[] Invalids { get; set; }

        public bool IsValid(TYPE item)
        {
            return Valids.Contains(item);
        }
        #endregion
    }

    /// <summary>
    /// This class adds a presenter to the ControlTests. 
    /// </summary>
    /// <typeparam name="OBJECT">The IDataObject that will be presented</typeparam>
    /// <typeparam name="TYPE">The Type of the presented Property</typeparam>
    /// <typeparam name="CONTROL">The Control used to present the Value</typeparam>
    /// <typeparam name="PRESENTER">The Presenter under test</typeparam>
    public abstract class PresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where CONTROL : IBasicControl
        where PRESENTER : IPresenter
    {
        protected PresenterTests(
            PresenterHarness<OBJECT, CONTROL, PRESENTER> presenterHarness,
            IValues<TYPE> values)
        {
            Assert.IsNotNull(presenterHarness, "presenterHarness cannot be null");
            Assert.IsNotNull(values, "values cannot be null");
            PresenterHarness = presenterHarness;
            Values = values;
        }

        protected ObjectHarness<OBJECT> ObjectHarness { get { return PresenterHarness.ObjectHarness; } }
        protected ControlHarness<CONTROL> ControlHarness { get { return PresenterHarness.ControlHarness; } }
        protected PresenterHarness<OBJECT, CONTROL, PRESENTER> PresenterHarness { get; private set; }
        protected IValues<TYPE> Values { get; private set; }


        protected PRESENTER Presenter { get { return PresenterHarness.Presenter; } }
        protected CONTROL Widget { get { return ControlHarness.Widget; } }
        protected Visual Visual { get { return ControlHarness.Visual; } }
        protected OBJECT Object { get { return ObjectHarness.Instance; } }

        protected Mockery Mockery { get { return MainSetUp.Mockery; } }
        protected IKistlContext MockContext { get { return MainSetUp.MockContext; } }

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

    /// <summary>
    /// Basic tests for a Presenter presenting struct-based types.
    /// </summary>
    /// <typeparam name="OBJECT"></typeparam>
    /// <typeparam name="TYPE"></typeparam>
    /// <typeparam name="CONTROL"></typeparam>
    /// <typeparam name="PRESENTER"></typeparam>
    public abstract class StructPresenterTests<OBJECT, TYPE, CONTROL>
        : PresenterTests<OBJECT, TYPE?, CONTROL, DefaultStructPresenter<TYPE>>
        where OBJECT : IDataObject
        where TYPE : struct
        where CONTROL : IValueControl<TYPE?>
    {

        protected StructPresenterTests(
            PresenterHarness<OBJECT, CONTROL, DefaultStructPresenter<TYPE>> presenterHarness,
            IValues<TYPE?> values)
            : base(presenterHarness, values)
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

            foreach (TYPE? value in Values.Valids)
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

            foreach (TYPE? value in Values.Invalids)
            {
                UserInput(value);
                AssertWidgetHasInvalidValue();
                Assert.AreEqual(original, GetObjectValue(), "Object should retain original value");
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
            }

            // After having invalid values set, 
            // choosing a valid value again, should 
            // clear all flags and set to the object
            TYPE?[] values = Values.Valids;
            if (values.Length > 0)
            {
                TYPE? value = values[0];
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
            foreach (var value in Values.Valids)
            {
                SetObjectValue(value);
                AssertWidgetHasValidValue();
                Assert.AreEqual(value, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
            }
        }

    }

    /// <summary>
    /// Basic tests for a Presenter presenting class-based types.
    /// </summary>
    /// <typeparam name="OBJECT"></typeparam>
    /// <typeparam name="TYPE"></typeparam>
    /// <typeparam name="CONTROL"></typeparam>
    /// <typeparam name="PRESENTER"></typeparam>
    public abstract class ClassPresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        : PresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where TYPE : class
        where CONTROL : IValueControl<TYPE>
        where PRESENTER : IPresenter
    {

        protected ClassPresenterTests(
            PresenterHarness<OBJECT, CONTROL, PRESENTER> presenterHarness,
            IValues<TYPE> values)
            : base(presenterHarness, values)
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
        protected TYPE GetWidgetValue() { return Widget.Value; }
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
            if (Values.IsValid(null))
            {
                TYPE v = GetObjectValue();
                if (v is System.Collections.ICollection)
                {
                    Assert.IsEmpty((System.Collections.ICollection)v, "valid null userinput should set an empty collection on the object");
                }
                else
                {
                    Assert.IsNull(v, "valid null userinput should set null value on the object");
                }
                AssertWidgetHasValidValue();
            }
            else
            {
                Assert.IsNotNull(GetObjectValue());
                AssertWidgetHasInvalidValue();
            }
        }

        [Test]
        public void HandleValidUserInput()
        {
            AssertWidgetHasValidValue();

            foreach (TYPE value in Values.Valids)
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

            foreach (TYPE value in Values.Invalids)
            {
                if (value is IList<IDataObject>)
                {
                    IList<IDataObject> idol = (IList<IDataObject>)value;
                    System.Console.Out.Write("{0}.HIUI: Testing List <", this.GetType());
                    foreach (IDataObject ido in idol)
                    {
                        System.Console.Out.Write("{0},", ido);
                    }
                    System.Console.Out.WriteLine(">");

                }
                UserInput(value);
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
                Assert.AreEqual(original, GetObjectValue(), "Object should retain original value");
                AssertWidgetHasInvalidValue();
            }

            // After having invalid values set, 
            // choosing a valid value again, should 
            // clear all flags and set to the object
            TYPE[] values = Values.Valids;
            if (values.Length > 0)
            {
                TYPE value = values[0];
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
            foreach (var v in Values.Valids)
            {
                SetObjectValue(v);
                Assert.AreEqual(v, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(v, GetWidgetValue(), "Widget should display new value");
                AssertWidgetHasValidValue();
            }
        }
    }

    /// <summary>
    /// Basic tests for a Presenter presenting IDataObject-based types.
    /// </summary>
    /// <typeparam name="OBJECT"></typeparam>
    /// <typeparam name="TYPE"></typeparam>
    /// <typeparam name="CONTROL"></typeparam>
    /// <typeparam name="PRESENTER"></typeparam>
    public abstract class ReferencePresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        : PresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where TYPE : class, IDataObject
        where CONTROL : IReferenceControl
        where PRESENTER : IPresenter
    {

        protected ReferencePresenterTests(
            PresenterHarness<OBJECT, CONTROL, PRESENTER> presenterHarness,
            IValues<TYPE> values)
            : base(presenterHarness, values)
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
        protected TYPE GetWidgetValue() { return (TYPE)Widget.Value; }
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
            if (Values.IsValid(null))
            {
                TYPE v = GetObjectValue();
                if (v is System.Collections.ICollection)
                {
                    Assert.IsEmpty((System.Collections.ICollection)v, "valid null userinput should set an empty collection on the object");
                }
                else
                {
                    Assert.IsNull(v, "valid null userinput should set null value on the object");
                }
                AssertWidgetHasValidValue();
            }
            else
            {
                Assert.IsNotNull(GetObjectValue());
                AssertWidgetHasInvalidValue();
            }
        }

        [Test]
        public void HandleValidUserInput()
        {
            AssertWidgetHasValidValue();

            foreach (TYPE value in Values.Valids)
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

            foreach (TYPE value in Values.Invalids)
            {
                if (value is IList<IDataObject>)
                {
                    IList<IDataObject> idol = (IList<IDataObject>)value;
                    System.Console.Out.Write("{0}.HIUI: Testing List <", this.GetType());
                    foreach (IDataObject ido in idol)
                    {
                        System.Console.Out.Write("{0},", ido);
                    }
                    System.Console.Out.WriteLine(">");

                }
                UserInput(value);
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
                Assert.AreEqual(original, GetObjectValue(), "Object should retain original value");
                AssertWidgetHasInvalidValue();
            }

            // After having invalid values set, 
            // choosing a valid value again, should 
            // clear all flags and set to the object
            TYPE[] values = Values.Valids;
            if (values.Length > 0)
            {
                TYPE value = values[0];
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
            foreach (var v in Values.Valids)
            {
                SetObjectValue(v);
                Assert.AreEqual(v, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(v, GetWidgetValue(), "Widget should display new value");
                AssertWidgetHasValidValue();
            }
        }
    }

    /// <summary>
    /// Basic tests for a Presenter presenting IList&lt;IDataObject&gt;-based types.
    /// </summary>
    /// <typeparam name="OBJECT"></typeparam>
    /// <typeparam name="TYPE"></typeparam>
    /// <typeparam name="CONTROL"></typeparam>
    /// <typeparam name="PRESENTER"></typeparam>
    public abstract class ReferenceListPresenterTests<OBJECT, TYPE, CONTROL, PRESENTER>
        : PresenterTests<OBJECT, ObservableCollection<TYPE>, CONTROL, PRESENTER>
        where OBJECT : IDataObject
        where TYPE : class, IDataObject
        where CONTROL : IReferenceListControl
        where PRESENTER : IPresenter
    {

        protected ReferenceListPresenterTests(
            PresenterHarness<OBJECT, CONTROL, PRESENTER> presenterHarness,
            IValues<ObservableCollection<TYPE>> values)
            : base(presenterHarness, values)
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
        protected abstract IList<TYPE> GetObjectValue();
        /// <summary>
        /// return the current Value which is displayed by the widget
        /// </summary>
        protected IList<TYPE> GetWidgetValue() { return Widget.Value.Cast<TYPE>().ToList(); }
        /// <summary>
        /// set the Value of the tested Property on the Object
        /// </summary>
        protected abstract void SetObjectValue(IList<TYPE> v);
        /// <summary>
        /// Simulate user input on the widget
        /// </summary>
        protected abstract void UserInput(IList<TYPE> v);
        /// <summary>
        /// return the expected Default Value at creation time
        /// </summary>
        protected abstract IList<TYPE> DefaultValue();

        [Test]
        public void HandleNoUserInput()
        {
            Assert.AreEqual(DefaultValue(), GetObjectValue(), String.Format("property {0} should have proper default", Visual.Property));
            Assert.AreEqual(DefaultValue(), GetWidgetValue(), String.Format("widget should show default value", Visual.Property, DefaultValue()));
            AssertWidgetHasValidValue();
        }

        [Test]
        [Ignore("Has to be re-evaluated after List/generic Presenter refactoring")]
        public void HandleNullUserInput()
        {
            AssertWidgetHasValidValue();

            UserInput(null);
            if (Values.IsValid(null))
            {
                IList<TYPE> v = GetObjectValue();
                if (v is System.Collections.ICollection)
                {
                    Assert.IsEmpty((System.Collections.ICollection)v, "valid null userinput should set an empty collection on the object");
                }
                else
                {
                    Assert.IsNull(v, "valid null userinput should set null value on the object");
                }
                AssertWidgetHasValidValue();
            }
            else
            {
                Assert.IsNotNull(GetObjectValue());
                AssertWidgetHasInvalidValue();
            }
        }

        [Test]
        public void HandleValidUserInput()
        {
            AssertWidgetHasValidValue();

            foreach (IList<TYPE> value in Values.Valids)
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

            IList<TYPE> original = GetObjectValue();

            foreach (IList<TYPE> value in Values.Invalids)
            {
                IList<IDataObject> idol = (IList<IDataObject>)value;
                System.Console.Out.Write("{0}.HIUI: Testing List <", this.GetType());
                foreach (IDataObject ido in idol)
                {
                    System.Console.Out.Write("{0},", ido);
                }
                System.Console.Out.WriteLine(">");
                UserInput(value);
                Assert.AreEqual(value, GetWidgetValue(), "Widget should display new value");
                Assert.AreEqual(original, GetObjectValue(), "Object should retain original value");
                AssertWidgetHasInvalidValue();
            }

            // After having invalid values set, 
            // choosing a valid value again, should 
            // clear all flags and set to the object
            IList<TYPE>[] values = Values.Valids;
            if (values.Length > 0)
            {
                IList<TYPE> value = values[0];
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
            foreach (var v in Values.Valids)
            {
                SetObjectValue(v);
                Assert.AreEqual(v, GetObjectValue(), "Object should have value set");
                Assert.AreEqual(v, GetWidgetValue(), "Widget should display new value");
                AssertWidgetHasValidValue();
            }
        }
    }


}
