using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

namespace Kistl.GUI.Tests
{
    public sealed class DoubleValues : ValuesAdapter<double?>
    {
        public override double?[] Valids
        {
            get
            {
                return new double?[] {   
                    0.0, 0.1, 0.2, 1.1, 123.123e12,
                    -0.0, -0.1, -0.2, -1.1, -123.123e12,
                    Double.Epsilon, Double.MaxValue, Double.MinValue,
                    Double.NaN, Double.NegativeInfinity, Double.PositiveInfinity,
                };
            }
        }
        public override double?[] Invalids { get { return new double?[] { }; } }
    }

    [TestFixture]
    public class DoublePresenterTests : NullablePresenterTests<TestObject, double, TestDoubleControl, DoublePresenter>
    {
        public DoublePresenterTests()
            : base(
                new PresenterHarness<TestObject, TestDoubleControl, DoublePresenter>(
                    new TestObjectHarness(),
                    typeof(DoubleProperty),
                    new ControlHarness<TestDoubleControl>(TestObject.TestDoubleVisual, Toolkit.TEST)),
                new DoubleValues())
        { }

        protected override double? GetObjectValue() { return Object.TestDouble; }
        protected override double? GetWidgetValue() { return Widget.Value; }
        protected override void SetObjectValue(double? v) { Object.TestDouble = v; }
        protected override void UserInput(double? v) { Widget.SimulateUserInput(v); }
    }
}
