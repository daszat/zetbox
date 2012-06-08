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

    public abstract class in_state_F_PUI
           : ViewModelTestFixture
    {
        protected string formattedValue;
        protected string partialInput;
        protected string errorString;
        protected bool parseValueCalled;

        public override void SetUp()
        {
            base.SetUp();
            formattedValue = "formattedValue";
            obj.OnFormatValue += value => formattedValue;
            obj.Focus();

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
        public class when_blurring
            : in_state_F_PUI
        {
            [Test]
            public void should_switch_to_B_PUI()
            {
                obj.Blur();

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_remember_partial_input()
            {
                obj.Blur();

                Assert.That(obj.FormattedValue, Is.EqualTo(partialInput));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        [Ignore("TODO: Fix Case 2371")]
        public class when_focusing
            : in_state_F_PUI
        {
            [Test]
            public void should_reject_Focus()
            {
                Assert.That(() => obj.Focus(), Throws.InvalidOperationException);
                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Focused_PartialUserInput));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_model_changes
            : in_state_F_PUI
        {
            private void RaiseValueModelChangedEvent()
            {
                valueModelMock.Raise(o => o.PropertyChanged += null,
                            new PropertyChangedEventArgs("Value"));
            }

            [Test]
            public void should_stay_in_F_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                RaiseValueModelChangedEvent();

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Focused_PartialUserInput));
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
            : in_state_F_PUI
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
            public void should_stay_in_F_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                obj.FormattedValue = newPartialInput;

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Focused_PartialUserInput));
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
            : in_state_F_PUI
        {
            private string finalInput;
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
            public void should_not_FormatValue()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);

                bool formatValueCalled = false;

                obj.OnFormatValue += v =>
                {
                    formatValueCalled = true;
                    return formattedValue;
                };

                obj.FormattedValue = finalInput;

                Assert.That(obj.FormattedValue, Is.EqualTo(finalInput));

                // FormatValue should not be called while user is still editing
                Assert.That(formatValueCalled, Is.False, "should not call FormatValue");

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
            public void should_switch_thru_F_WM()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);
                bool hasReachedIfWm = false;
                obj.StateChanged += (s, e) =>
                {
                    if (e.NewState == ValueViewModelState.Focused_WritingModel)
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
            public void should_return_to_F_UV()
            {
                valueModelMock.SetupSet(o => o.Value = parsedValue);

                obj.FormattedValue = finalInput;

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Focused_UnmodifiedValue));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_setting_Value
            : in_state_F_PUI
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
            public void should_switch_thru_F_WM()
            {
                bool hasReachedIfWm = false;
                obj.StateChanged += (s, e) =>
                {
                    if (e.NewState == ValueViewModelState.Focused_WritingModel)
                    {
                        hasReachedIfWm = true;
                    }
                };

                valueModelMock.SetupProperty(o => o.Value);
                obj.Value = value;

                Assert.That(hasReachedIfWm, Is.True, "has not reached ValueViewModelState.Focused_WritingModel");

                valueModelMock.Verify();
            }

            [Test]
            public void should_return_to_F_UV()
            {
                SetupSetValueWithNotification();

                obj.Value = value;

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Focused_UnmodifiedValue));
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

                // FormatValue is called by the state machine
                Assert.That(formatValueCalled, Is.True, "should call FormatValue");

                Assert.That(obj.FormattedValue, Is.EqualTo(formattedValue));

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
    }
}
