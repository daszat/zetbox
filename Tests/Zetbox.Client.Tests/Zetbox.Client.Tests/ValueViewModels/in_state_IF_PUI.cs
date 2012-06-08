
namespace Zetbox.Client.Tests.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Moq;
    using NUnit.Framework;

    public abstract class in_state_IF_PUI
           : ViewModelTestFixture
    {
        protected string partialInput;
        protected string errorString;
        protected bool parseValueCalled;

        public override void SetUp()
        {
            base.SetUp();
            partialInput = "partialInput";
            errorString = "errorString";
            parseValueCalled = false;
            obj.OnParseValue += ParseValue;
            obj.FormattedValue = partialInput;
        }

        protected virtual KeyValuePair<string, object> ParseValue(string value)
        {
            Assert.That(value, Is.EqualTo(partialInput));
            parseValueCalled = true;
            return new KeyValuePair<string, object>(errorString, null);
        }

        [TestFixture]
        public class when_changing_focus
            : in_state_IF_PUI
        {
            [Test]
            public void should_reject_Blur()
            {
                Assert.That(() => obj.Blur(), Throws.InvalidOperationException);
                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.ImplicitFocus_PartialUserInput));
                valueModelMock.Verify();
            }

            [Ignore("TODO: Fix Case 2371")]
            [Test]
            public void should_reject_Focus()
            {
                Assert.That(() => obj.Focus(), Throws.InvalidOperationException);
                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.ImplicitFocus_PartialUserInput));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_model_changes
            : in_state_IF_PUI
        {
            private void RaiseValueModelChangedEvent()
            {
                valueModelMock.Raise(o => o.PropertyChanged += null,
                            new PropertyChangedEventArgs("Value"));
            }

            [Test]
            public void should_stay_in_IF_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                RaiseValueModelChangedEvent();

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.ImplicitFocus_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_Value()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("Value"));
                };

                RaiseValueModelChangedEvent();
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_FormattedValue()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("FormattedValue"));
                };

                RaiseValueModelChangedEvent();
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_clear_Error()
            {
                var oldError = obj.Error;

                RaiseValueModelChangedEvent();

                Assert.That(obj.Error, Is.EqualTo(oldError));
                valueModelMock.Verify();
            }

            [Test]
            [Ignore("TODO: needs log4net asserts")]
            public void should_warn_about_lost_data()
            {
                Assert.Fail();
            }
        }


        [TestFixture]
        public class when_setting_partial_FormattedValue
            : in_state_IF_PUI
        {
            private string newPartialInput;

            public override void SetUp()
            {
                newPartialInput = partialInput + "Modification";
                base.SetUp();
            }

            protected override KeyValuePair<string, object> ParseValue(string value)
            {
                Assert.That(value, Is.EqualTo(partialInput).Or.EqualTo(newPartialInput));
                parseValueCalled = true;
                return new KeyValuePair<string, object>(errorString, null);
            }

            [Test]
            public void should_stay_in_IF_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                obj.FormattedValue = newPartialInput;

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.ImplicitFocus_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_remember_partial_input()
            {
                obj.FormattedValue = newPartialInput;

                Assert.That(obj.FormattedValue, Is.EqualTo(newPartialInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_set_Error()
            {
                obj.FormattedValue = newPartialInput;

                Assert.That(obj.Error, Is.EqualTo(errorString));
                valueModelMock.Verify();
            }

            [Test]
            public void should_call_ParseValue()
            {
                obj.FormattedValue = newPartialInput;

                Assert.That(parseValueCalled, Is.True);
                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_FormattedValue()
            {
                TestChangedNotification(
                    obj,
                    "FormattedValue",
                    () => obj.FormattedValue = newPartialInput,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_not_set_Value()
            {
                obj.FormattedValue = newPartialInput;

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

                obj.FormattedValue = newPartialInput;

                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_setting_valid_FormattedValue
            : in_state_IF_PUI
        {
            private string finalInput;
            private string formattedValue;
            private string parsedValue;

            public override void SetUp()
            {
                finalInput = partialInput + "Final";
                formattedValue = "formattedValue";
                parsedValue = "parsedValue";
                base.SetUp();
            }

            protected override KeyValuePair<string, object> ParseValue(string value)
            {
                Assert.That(value, Is.EqualTo(partialInput).Or.EqualTo(finalInput));
                parseValueCalled = true;
                if (value == finalInput)
                {
                    return new KeyValuePair<string, object>(String.Empty, parsedValue);
                }
                else
                {
                    return new KeyValuePair<string, object>(errorString, null);
                }
            }

            [Test]
            public void should_call_ParseValue()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);
                obj.FormattedValue = finalInput;

                Assert.That(parseValueCalled, Is.True);
                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_FormattedValue()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);
                TestChangedNotification(
                    obj,
                    "FormattedValue",
                    () => obj.FormattedValue = finalInput,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_notify_about_Value()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);
                TestChangedNotification(
                    obj,
                    "Value",
                    () => obj.FormattedValue = finalInput,
                    null);

                valueModelMock.Verify();
            }

            [Test]
            public void should_FormatValue()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);

                bool formatValueCalled = false;

                obj.OnFormatValue += v =>
                {
                    formatValueCalled = true;
                    return formattedValue;
                };

                obj.FormattedValue = finalInput;

                Assert.That(obj.FormattedValue, Is.EqualTo(formattedValue));

                // FormatValue only called when accessing FormattedValue
                Assert.That(formatValueCalled, Is.True, "should call FormatValue");

                valueModelMock.Verify();
            }

            [Test]
            public void should_not_set_Error()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);
                obj.FormattedValue = finalInput;

                Assert.That(obj.Error, Is.Null.Or.Empty);
                valueModelMock.Verify();
            }

            [Test]
            public void should_set_Value_on_Model()
            {
                valueModelMock.SetupProperty(o => o.Value);

                obj.FormattedValue = finalInput;

                valueModelMock.VerifySet(o => o.Value = parsedValue);

                Assert.That(obj.Value, Is.SameAs(parsedValue));

                valueModelMock.Verify();
            }

            [Test]
            public void should_switch_thru_IF_WM()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);
                bool hasReachedIfWm = false;
                obj.StateChanged += (s, e) =>
                {
                    if (e.NewState == ValueViewModelState.ImplicitFocus_WritingModel)
                    {
                        hasReachedIfWm = true;
                    }
                };

                valueModelMock.SetupProperty(o => o.Value);
                obj.FormattedValue = finalInput;

                Assert.That(hasReachedIfWm, Is.True, "has not reached ValueViewModelState.ImplicitFocus_WritingModel");

                valueModelMock.Verify();
            }

            [Test]
            public void should_return_to_B_UV()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);

                obj.FormattedValue = finalInput;

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_UnmodifiedValue));
                valueModelMock.Verify();
            }
        }
    }
}
