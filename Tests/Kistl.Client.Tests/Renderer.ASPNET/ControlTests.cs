using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;
using Kistl.GUI.Tests;

using NUnit.Framework;
using Kistl.Client.ASPNET.Toolkit;

namespace Kistl.GUI.Renderer.ASPNET.Tests
{
    [TestFixture]
    [Ignore("TODO: ASP.NET Testframework")]
    public class BoolControlTests : ValueControlTests<bool?, BoolPropertyControlLoader>
    {
        public BoolControlTests()
            : base(
                new ControlHarness<BoolPropertyControlLoader>(TestObject.TestBoolVisual, Toolkit.ASPNET),
                new BoolValues()
            )
        {
        }

        [Test]
        [Ignore()]
        public override void TestUserInput()
        {
            //TestUserInput((w, v) => w.SetValue(BoolPropertyControl.ValueProperty, v));
        }

    }

    [TestFixture]
    [Ignore("TODO: ASP.NET Testframework")]
    public class DateTimeControlTests : ValueControlTests<DateTime?, DateTimePropertyControlLoader>
    {
        public DateTimeControlTests()
            : base(
                new ControlHarness<DateTimePropertyControlLoader>(TestObject.TestDateTimeVisual, Toolkit.ASPNET),
                new DateTimeValues()
            )
        {
        }

        [Test]
        [Ignore()]
        public override void TestUserInput()
        {
            //TestUserInput((w, v) => w.SetValue(EditDateTimeProperty.ValueProperty, v));
        }

    }

    [TestFixture]
    [Ignore("TODO: ASP.NET Testframework")]
    public class DoubleControlTests : ValueControlTests<double?, DoublePropertyControlLoader>
    {
        public DoubleControlTests()
            : base(
                new ControlHarness<DoublePropertyControlLoader>(TestObject.TestDoubleVisual, Toolkit.ASPNET),
                new DoubleValues()
            )
        {
        }
        [Test]
        [Ignore()]
        public override void TestUserInput()
        {
            //TestUserInput((w, v) => w.SetValue(EditDoubleProperty.ValueProperty, v));
        }
    }

    [TestFixture]
    [Ignore("TODO: ASP.NET Testframework")]
    public class IntControlTests : ValueControlTests<int?, IntPropertyControlLoader>
    {
        public IntControlTests()
            : base(
                new ControlHarness<IntPropertyControlLoader>(TestObject.TestIntVisual, Toolkit.ASPNET),
                new IntValues()
            )
        {
        }

        [Test]
        [Ignore()]
        public override void TestUserInput()
        {
            //TestUserInput((w, v) => w.SetValue(EditIntProperty.ValueProperty, v));
        }
    }

    [TestFixture]
    [Ignore("TODO: ASP.NET Testframework")]
    public class StringControlTests : ValueControlTests<string, StringPropertyControlLoader>
    {
        public StringControlTests()
            : base(
                new ControlHarness<StringPropertyControlLoader>(TestObject.TestStringVisual, Toolkit.ASPNET),
                new StringValues()
            )
        {
        }
        [Test]
        [Ignore()]
        public override void TestUserInput()
        {
            //TestUserInput((w, v) => w.SetValue(EditSimpleProperty.ValueProperty, v));
        }

    }

}
