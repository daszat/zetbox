
namespace Kistl.Client.Tests.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ValueViewModels;
    using Moq;
    using NUnit.Framework;

    public abstract class in_state_B_UV
        : ViewModelTestFixture
    {
        [TestFixture]
        public class when_focusing
            : in_state_B_UV
        {
            string formattedValue;

            public override void SetUp()
            {
                base.SetUp();
                formattedValue = "formattedValue";
                obj.OnFormatValue += value => formattedValue;

            }

            [Test]
            public void should_switch_to_F_UV()
            {
                obj.Focus();
                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Focused_UnmodifiedValue));
                valueModelMock.Verify();
            }

            [Test]
            public void should_return_FormattedValue_from_FormatValue()
            {
                obj.Focus();
                Assert.That(obj.FormattedValue, Is.EqualTo(formattedValue));
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_FormattedValue()
            {
                // a little white-box testing: 
                // this requirement is necessary to avoid flickering in the UI
                obj.PropertyChanged += (s, e) => Assert.That(e.PropertyName, Is.Not.EqualTo("FormattedValue"));
                obj.Focus();
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_blurring
            : in_state_B_UV
        {
            [Test]
            public void should_reject()
            {
                Assert.That(() => obj.Blur(), Throws.InvalidOperationException);
                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_UnmodifiedValue));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_setting_Value
            : in_state_B_UV
        {
            private object value;

            public override void SetUp()
            {
                base.SetUp();
                value = new object();
            }

            private void SetupSetValueWithNotification()
            {
                valueModelMock
                    .SetupSet(o => o.Value = value)
                    .Callback(() => valueModelMock.Raise(
                            o => o.PropertyChanged += null,
                            new PropertyChangedEventArgs("Value")));
            }

            [Test]
            public void should_set_Value_on_Model()
            {
                valueModelMock.SetupProperty(o => o.Value);

                obj.Value = value;
                Assert.That(obj.Value, Is.SameAs(value));

                valueModelMock.VerifySet(o => o.Value = value);
            }

            [Test]
            public void should_switch_thru_IF_WM()
            {
                bool hasReachedIfWm = false;
                obj.StateChanged += (s, e) =>
                {
                    if (e.NewState == ValueViewModelState.ImplicitFocus_WritingModel)
                    {
                        hasReachedIfWm = true;
                    }
                };

                valueModelMock.SetupProperty(o => o.Value);
                obj.Value = value;

                Assert.That(hasReachedIfWm, Is.True, "has not reached ValueViewModelState.ImplicitFocus_WritingModel");

                valueModelMock.Verify();
            }

            [Test]
            public void should_return_to_B_UV()
            {
                SetupSetValueWithNotification();

                obj.Value = value;

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_UnmodifiedValue));
                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_Value()
            {
                valueModelMock.SetupSet(o => o.Value = value).Verifiable();

                TestChangedNotification(obj, "Value",
                    () => obj.Value = value,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_Value_only_once()
            {
                SetupSetValueWithNotification();

                TestChangedNotification(obj, "Value",
                    () => obj.Value = value,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_FormattedValue()
            {
                valueModelMock.SetupSet(o => o.Value = value).Verifiable();

                TestChangedNotification(obj, "FormattedValue",
                    () => obj.Value = value,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_FormattedValue_only_once()
            {
                SetupSetValueWithNotification();

                TestChangedNotification(obj, "FormattedValue",
                    () => obj.Value = value,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_FormatValue()
            {
                valueModelMock.SetupProperty(o => o.Value);

                bool formatValueCalled = false;
                string formattedValue = "formattedValue";

                obj.OnFormatValue += v =>
                {
                    formatValueCalled = true;
                    return formattedValue;
                };

                obj.Value = value;

                Assert.That(obj.FormattedValue, Is.EqualTo(formattedValue));

                // FormatValue only called when accessing FormattedValue
                Assert.That(formatValueCalled, Is.True, "should call FormatValue");

                valueModelMock.Verify();
            }

            [Test]
            public void should_not_set_Error()
            {
                valueModelMock.SetupProperty(o => o.Value);
                obj.Value = value;

                Assert.That(obj.Error, Is.Null.Or.Empty);
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_setting_partial_FormattedValue
            : in_state_B_UV
        {
            private string partialInput;
            private string errorString;
            bool parseValueCalled;

            public override void SetUp()
            {
                base.SetUp();
                partialInput = "partialInput";
                errorString = "errorString";
                parseValueCalled = false;
                obj.OnParseValue += str =>
                {
                    Assert.That(str, Is.EqualTo(partialInput));
                    parseValueCalled = true;
                    return new KeyValuePair<string, object>(errorString, null);
                };
            }

            [Test]
            public void should_switch_to_state_IF_PUI()
            {
                obj.FormattedValue = partialInput;

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.ImplicitFocus_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_remember_partial_input()
            {
                obj.FormattedValue = partialInput;

                Assert.That(obj.FormattedValue, Is.EqualTo(partialInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_set_Error()
            {
                obj.FormattedValue = partialInput;

                Assert.That(obj.Error, Is.EqualTo(errorString));
                valueModelMock.Verify();
            }

            [Test]
            public void should_call_ParseValue()
            {
                obj.FormattedValue = partialInput;

                Assert.That(parseValueCalled, Is.True);
                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_FormattedValue()
            {
                TestChangedNotification(
                    obj,
                    "FormattedValue",
                    () => obj.FormattedValue = partialInput,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_not_set_Value()
            {
                obj.FormattedValue = partialInput;

                // strict mock automatically verifies that nothing was touched
                Assert.That(valueModelMock.Behavior, Is.EqualTo(MockBehavior.Strict));
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_Value()
            {
                obj.PropertyChanged += (s, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("Value"));
                };

                obj.FormattedValue = partialInput;

                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_setting_valid_FormattedValue
            : in_state_B_UV
        {
            private string inputValue;
            private string formattedValue;
            private string parsedValue;
            bool parseValueCalled;

            public override void SetUp()
            {
                base.SetUp();
                inputValue = "inputValue";
                formattedValue = "formattedValue";
                parsedValue = "parsedValue";
                parseValueCalled = false;
                obj.OnParseValue += str =>
                {
                    Assert.That(str, Is.EqualTo(inputValue));
                    parseValueCalled = true;
                    return new KeyValuePair<string, object>(null, parsedValue);
                };
                valueModelMock.SetupSet(o => o.Value = parsedValue);
            }

            [Test]
            public void should_call_ParseValue()
            {
                obj.FormattedValue = inputValue;

                Assert.That(parseValueCalled, Is.True);
                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_FormattedValue()
            {
                TestChangedNotification(
                    obj,
                    "FormattedValue",
                    () => obj.FormattedValue = inputValue,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_Value()
            {
                TestChangedNotification(
                    obj,
                    "Value",
                    () => obj.FormattedValue = inputValue,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_FormatValue()
            {
                valueModelMock.SetupProperty(o => o.Value);

                bool formatValueCalled = false;

                obj.OnFormatValue += v =>
                {
                    formatValueCalled = true;
                    return formattedValue;
                };

                obj.FormattedValue = inputValue;

                Assert.That(obj.FormattedValue, Is.EqualTo(formattedValue));

                // FormatValue only called when accessing FormattedValue
                Assert.That(formatValueCalled, Is.True, "should call FormatValue");

                valueModelMock.Verify();
            }

            [Test]
            public void should_not_set_Error()
            {
                obj.FormattedValue = inputValue;

                Assert.That(obj.Error, Is.Null.Or.Empty);
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_model_changes
            : in_state_B_UV
        {
            private void RaiseValueModelChangedEvent()
            {
                valueModelMock.Raise(o => o.PropertyChanged += null,
                            new PropertyChangedEventArgs("Value"));
            }

            [Test]
            public void should_stay_in_B_UV()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                RaiseValueModelChangedEvent();

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_UnmodifiedValue));
                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_Value()
            {
                TestChangedNotification(
                    obj,
                    "Value",
                    RaiseValueModelChangedEvent,
                    null);
                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_FormattedValue()
            {
                TestChangedNotification(
                    obj,
                    "FormattedValue",
                    RaiseValueModelChangedEvent,
                    null);
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_set_Error()
            {
                RaiseValueModelChangedEvent();

                Assert.That(obj.Error, Is.Null.Or.Empty);
                valueModelMock.Verify();
            }
        }
    }
}
