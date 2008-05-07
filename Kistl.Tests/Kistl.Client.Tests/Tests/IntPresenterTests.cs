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
    public class IntPresenterTests : NullablePresenterTests<int, TestIntControl, IntPresenter>
    {
        [SetUp]
        public void InitControls()
        {
            Init(TestIntControl.Info, TestObject.TestIntDescriptor);
        }

        protected override int? GetObjectValue() { return obj.TestInt; }
        protected override int? GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(int? v) { obj.TestInt = v; }
        protected override void UserInput(int? v) { widget.SimulateUserInput(v); }
        protected override IEnumerable<int> SomeValues()
        {
            return new List<int>(new [] {
                Int16.MinValue, Int16.MaxValue,
                Int32.MinValue, Int32.MaxValue,
                Int16.MinValue + 1, Int16.MaxValue + 1,
                Int16.MinValue - 1, Int16.MaxValue - 1,
                Int32.MinValue + 1, Int32.MaxValue - 1,
                0, +1, -1, 100, 123, 200, 6000
            });
        }
    }
}
