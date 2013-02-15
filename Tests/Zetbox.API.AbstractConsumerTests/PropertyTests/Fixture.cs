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

namespace Zetbox.API.AbstractConsumerTests.PropertyTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    public abstract class Fixture : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected IZetboxContext testCtx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            testCtx = GetContext();
        }

        public override void TearDown()
        {
            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }
            if (testCtx != null)
            {
                testCtx.Dispose();
                testCtx = null;
            }
            base.TearDown();
        }

        public virtual IEnumerable<TestCaseData> NotNullableTestCases()
        {
            yield return new TestCaseData(typeof(PropertyBoolTest), null, true, true, Setter<PropertyBoolTest, bool>((i, o) => i.Standard = o), Getter<PropertyBoolTest, bool>(i => i.Standard));
            // yield return new TestCaseData(typeof(PropertyDateTimeTest), null, ??, new DateTime(2012, 2, 3), Setter<PropertyDateTimeTest, DateTime>((i, o) => i.Standard = o), Getter<PropertyDateTimeTest, DateTime>(i => i.Standard));
            yield return new TestCaseData(typeof(PropertyDecimalTest), null, 5.5m, 123.5m, Setter<PropertyDecimalTest, decimal>((i, o) => i.Standard = o), Getter<PropertyDecimalTest, decimal>(i => i.Standard));
            yield return new TestCaseData(typeof(PropertyDoubleTest), null, 5.5, 123.5, Setter<PropertyDoubleTest, double>((i, o) => i.Standard = o), Getter<PropertyDoubleTest, double>(i => i.Standard));
            yield return new TestCaseData(typeof(PropertyEnumTest), null, TestEnum.Third, TestEnum.Second, Setter<PropertyEnumTest, TestEnum>((i, o) => i.Standard = o), Getter<PropertyEnumTest, TestEnum>(i => i.Standard));
            //yield return new TestCaseData(typeof(PropertyGuidTest), null, ??, new Guid("12345678-abcd-ef01-2345-6789abcdef01") , Setter<PropertyGuidTest, Guid>((i, o) => i.Standard = o), Getter<PropertyGuidTest, Guid>(i => i.Standard));
            yield return new TestCaseData(typeof(PropertyIntTest), null, 5, 123, Setter<PropertyIntTest, int>((i, o) => i.Standard = o), Getter<PropertyIntTest, int>(i => i.Standard));
        }

        public virtual IEnumerable<TestCaseData> DefaultValueTestCases()
        {
            yield return new TestCaseData(typeof(PropertyBoolTest), null, true, true, Setter<PropertyBoolTest, bool>((i, o) => i.StandardWithDefault = o), Getter<PropertyBoolTest, bool>(i => i.StandardWithDefault));
            // yield return new TestCaseData(typeof(PropertyDateTimeTest), null, ??, new DateTime(2012, 2, 3), Setter<PropertyDateTimeTest, DateTime>((i, o) => i.StandardWithDefault = o), Getter<PropertyDateTimeTest, DateTime>(i => i.StandardWithDefault));
            yield return new TestCaseData(typeof(PropertyDecimalTest), null, 5.5m, 123.5m, Setter<PropertyDecimalTest, decimal>((i, o) => i.StandardWithDefault = o), Getter<PropertyDecimalTest, decimal>(i => i.StandardWithDefault));
            yield return new TestCaseData(typeof(PropertyDoubleTest), null, 5.5, 123.5, Setter<PropertyDoubleTest, double>((i, o) => i.StandardWithDefault = o), Getter<PropertyDoubleTest, double>(i => i.StandardWithDefault));
            yield return new TestCaseData(typeof(PropertyEnumTest), null, TestEnum.Third, TestEnum.Second, Setter<PropertyEnumTest, TestEnum>((i, o) => i.StandardWithDefault = o), Getter<PropertyEnumTest, TestEnum>(i => i.StandardWithDefault));
            //yield return new TestCaseData(typeof(PropertyGuidTest), null, ??, new Guid("12345678-abcd-ef01-2345-6789abcdef01") , Setter<PropertyGuidTest, Guid>((i, o) => i.StandardWithDefault = o), Getter<PropertyGuidTest, Guid>(i => i.StandardWithDefault));
            yield return new TestCaseData(typeof(PropertyIntTest), null, 5, 123, Setter<PropertyIntTest, int>((i, o) => i.StandardWithDefault = o), Getter<PropertyIntTest, int>(i => i.StandardWithDefault));
            yield return new TestCaseData(typeof(PropertyStringTest), Init<PropertyStringTest>(i => i.Standard = "empty value"), "five point five", "some value", Setter<PropertyStringTest, string>((i, o) => i.StandardWithDefault = o), Getter<PropertyStringTest, string>(i => i.StandardWithDefault));
        }

        protected virtual IEnumerable<TestCaseData> OtherTestCases()
        {
            // actually, this should be a NotNullableTestCase, but that doesn't work out, yet
            yield return new TestCaseData(typeof(PropertyStringTest), null, "five point five", "some value", Setter<PropertyStringTest, string>((i, o) => i.Standard = o), Getter<PropertyStringTest, string>(i => i.Standard));
        }

        public virtual IEnumerable<TestCaseData> AllTestCases()
        {
            return DefaultValueTestCases().Concat(NotNullableTestCases()).Concat(OtherTestCases());
        }

        #region casting helpers
        protected static Action<PropertyTestBase> Init<T>(Action<T> action)
            where T : PropertyTestBase
        {
            return new Action<PropertyTestBase>(t => action((T)t));
        }

        protected static Action<PropertyTestBase, object> Setter<T, V>(Action<T, V> action)
            where T : PropertyTestBase
        {
            return new Action<PropertyTestBase, object>((t, v) => action((T)t, (V)v));
        }

        protected static Func<PropertyTestBase, object> Getter<T, V>(Func<T, V> action)
            where T : PropertyTestBase
        {
            return new Func<PropertyTestBase, object>(t => action((T)t));
        }
        #endregion

        [Test]
        [TestCaseSource("AllTestCases")]
        public void should_persist_a_write(Type t, Action<PropertyTestBase> init, object defaultValue, object value, Action<PropertyTestBase, object> setter, Func<PropertyTestBase, object> getter)
        {
            var obj = (PropertyTestBase)ctx.Create(ctx.GetInterfaceType(t));
            if (init != null) init(obj);

            setter(obj, value);

            ctx.SubmitChanges();
            var originalId = obj.ID;

            var testObj = testCtx.Find<PropertyTestBase>(originalId);
            Assert.That(getter(testObj), Is.EqualTo(value));
        }

        public abstract class with_default_value : Fixture
        {
            [Test]
            [TestCaseSource("DefaultValueTestCases")]
            public void should_persist_default_value(Type t, Action<PropertyTestBase> init, object defaultValue, object value, Action<PropertyTestBase, object> setter, Func<PropertyTestBase, object> getter)
            {
                var obj = (PropertyTestBase)ctx.Create(ctx.GetInterfaceType(t));
                if (init != null) init(obj);

                ctx.SubmitChanges();
                var originalId = obj.ID;

                var testObj = testCtx.Find<PropertyTestBase>(originalId);
                Assert.That(getter(testObj), Is.EqualTo(defaultValue));
            }

            [Test]
            [TestCaseSource("DefaultValueTestCases")]
            public void should_persist_default_value_after_read(Type t, Action<PropertyTestBase> init, object defaultValue, object value, Action<PropertyTestBase, object> setter, Func<PropertyTestBase, object> getter)
            {
                var obj = (PropertyTestBase)ctx.Create(ctx.GetInterfaceType(t));
                if (init != null) init(obj);

                var actualValue = getter(obj);
                Assert.That(actualValue, Is.EqualTo(defaultValue));

                ctx.SubmitChanges();
                var originalId = obj.ID;

                var testObj = testCtx.Find<PropertyTestBase>(originalId);
                Assert.That(getter(testObj), Is.EqualTo(defaultValue));
            }
        }

        public abstract class when_not_nullable : Fixture
        {
            [Test]
            [TestCaseSource("NotNullableTestCases")]
            public void should_persist_default_t_(Type t, Action<PropertyTestBase> init, object defaultValue, object value, Action<PropertyTestBase, object> setter, Func<PropertyTestBase, object> getter)
            {
                var obj = (PropertyTestBase)ctx.Create(ctx.GetInterfaceType(t));
                if (init != null) init(obj);

                ctx.SubmitChanges();

                var originalId = obj.ID;

                var testObj = testCtx.Find<PropertyTestBase>(originalId);
                var actualValue = getter(testObj);
                // implement default(x)
                var expectedValue = actualValue != null && actualValue.GetType().IsValueType
                    ? Activator.CreateInstance(actualValue.GetType())
                    : null;
                Assert.That(actualValue, Is.EqualTo(expectedValue));
            }
        }
    }
}
