
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

    public abstract class in_state_B_PUI
           : ViewModelTestFixture
    {
        protected string partialInput;
        protected string errorString;

        public override void SetUp()
        {
            base.SetUp();
            obj.OnFormatValue += FormatValue;
            obj.Focus();

            partialInput = "partialInput";
            errorString = "errorString";
            obj.OnParseValue += ParseValue;
            obj.FormattedValue = partialInput;

            obj.Blur();

            obj.OnParseValue -= ParseValue;
            obj.OnFormatValue -= FormatValue;
        }

        protected virtual KeyValuePair<string, object> ParseValue(string value)
        {
            Assert.That(value, Is.EqualTo(partialInput));
            return new KeyValuePair<string, object>(errorString, null);
        }

        protected virtual string FormatValue(object value)
        {
            return "formattedValue";
        }

        [TestFixture]
        public class when_focusing
            : in_state_B_PUI
        {
            [Test]
            public void should_switch_to_F_PUI()
            {
                obj.Focus();

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Focused_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_remember_partial_input()
            {
                obj.Focus();

                Assert.That(obj.FormattedValue, Is.EqualTo(partialInput));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        [Ignore("TODO: Fix Case 2371")]
        public class when_blurring
            : in_state_B_PUI
        {
            [Test]
            public void should_reject()
            {
                Assert.That(() => obj.Blur(), Throws.InvalidOperationException);
                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_remember_partial_input()
            {
                Assert.That(() => obj.Blur(), Throws.InvalidOperationException);

                Assert.That(obj.FormattedValue, Is.EqualTo(partialInput));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_model_changes
            : in_state_B_PUI
        {
            private void RaiseValueModelChangedEvent()
            {
                valueModelMock.Raise(o => o.PropertyChanged += null,
                            new PropertyChangedEventArgs("Value"));
            }

            [Test]
            public void should_stay_in_B_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                RaiseValueModelChangedEvent();

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_PartialUserInput));
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
            : in_state_B_PUI
        {
            private string nextPartialInput;

            public override void SetUp()
            {
                base.SetUp();
                nextPartialInput = partialInput + "Next";
                obj.OnParseValue += str => new KeyValuePair<string, object>("nextParseError", null);
            }

            [Test]
            public void should_reject_FormattedValue()
            {
                var oldFormattedValue = obj.FormattedValue;
                Assert.That(() => obj.FormattedValue = nextPartialInput, Throws.InvalidOperationException);
                Assert.That(obj.FormattedValue, Is.EqualTo(oldFormattedValue));
            }

            [Test]
            public void should_stay_in_B_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                Assert.That(() => obj.FormattedValue = nextPartialInput, Throws.InvalidOperationException);

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_Value()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("Value"));
                };

                Assert.That(() => obj.FormattedValue = nextPartialInput, Throws.InvalidOperationException);
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_FormattedValue()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("FormattedValue"));
                };

                Assert.That(() => obj.FormattedValue = nextPartialInput, Throws.InvalidOperationException);
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_clear_Error()
            {
                var oldError = obj.Error;

                Assert.That(() => obj.FormattedValue = nextPartialInput, Throws.InvalidOperationException);

                Assert.That(obj.Error, Is.EqualTo(oldError));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_setting_valid_FormattedValue
            : in_state_B_PUI
        {
            private string validInput;

            public override void SetUp()
            {
                base.SetUp();
                validInput = partialInput + "Final";
                obj.OnParseValue += str => new KeyValuePair<string, object>(null, "parsedObject");
            }

            [Test]
            public void should_reject_FormattedValue()
            {
                var oldFormattedValue = obj.FormattedValue;
                Assert.That(() => obj.FormattedValue = validInput, Throws.InvalidOperationException);
                Assert.That(obj.FormattedValue, Is.EqualTo(oldFormattedValue));
            }

            [Test]
            public void should_stay_in_B_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                Assert.That(() => obj.FormattedValue = validInput, Throws.InvalidOperationException);

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_Value()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("Value"));
                };

                Assert.That(() => obj.FormattedValue = validInput, Throws.InvalidOperationException);
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_FormattedValue()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("FormattedValue"));
                };

                Assert.That(() => obj.FormattedValue = validInput, Throws.InvalidOperationException);
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_clear_Error()
            {
                var oldError = obj.Error;

                Assert.That(() => obj.FormattedValue = validInput, Throws.InvalidOperationException);

                Assert.That(obj.Error, Is.EqualTo(oldError));
                valueModelMock.Verify();
            }
        }

        [TestFixture]
        public class when_setting_Value
            : in_state_B_PUI
        {
            private object validValue;

            public override void SetUp()
            {
                base.SetUp();
                validValue = new object();
            }

            [Test]
            public void should_reject_Value()
            {
                var oldValue = obj.Value;
                Assert.That(() => obj.Value = validValue, Throws.InvalidOperationException);
                Assert.That(obj.Value, Is.EqualTo(oldValue));
            }

            [Test]
            public void should_stay_in_B_PUI()
            {
                obj.StateChanged += (s, e) => Assert.Fail("Unexpected {0}", e);

                Assert.That(() => obj.Value = validValue, Throws.InvalidOperationException);

                Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_PartialUserInput));
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_Value()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("Value"));
                };

                Assert.That(() => obj.Value = validValue, Throws.InvalidOperationException);
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_notify_FormattedValue()
            {
                obj.PropertyChanged += (sender, e) =>
                {
                    Assert.That(e.PropertyName, Is.Not.EqualTo("FormattedValue"));
                };

                Assert.That(() => obj.Value = validValue, Throws.InvalidOperationException);
                valueModelMock.Verify();
            }

            [Test]
            public void should_not_clear_Error()
            {
                var oldError = obj.Error;

                Assert.That(() => obj.Value = validValue, Throws.InvalidOperationException);

                Assert.That(obj.Error, Is.EqualTo(oldError));
                valueModelMock.Verify();
            }
        }
    }
}