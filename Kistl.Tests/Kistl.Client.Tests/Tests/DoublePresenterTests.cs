using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client.Mocks;
using Kistl.GUI;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class DoublePresenterTests : NullablePresenterTests<double, TestDoubleControl, DoublePresenter>
    {
        [SetUp]
        public void InitControls()
        {
            Init(TestDoubleControl.Info, TestObject.TestDoubleDescriptor);
        }

        protected override double? GetObjectValue() { return obj.TestDouble; }
        protected override double? GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(double? v) { obj.TestDouble = v; }
        protected override void UserInput(double? v) { widget.SimulateUserInput(v); }
        protected override IEnumerable<double> SomeValues()
        {
            return new List<double>(new double[] {
                0.0, 0.1, 0.2, 1.1, 123.123e12,
                -0.0, -0.1, -0.2, -1.1, -123.123e12,
                Double.Epsilon, Double.MaxValue, Double.MinValue, Double.NaN, Double.NegativeInfinity, Double.PositiveInfinity,
            });
        }

    }
}
