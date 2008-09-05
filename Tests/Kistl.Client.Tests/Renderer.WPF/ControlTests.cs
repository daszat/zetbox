using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;
using Kistl.GUI.Tests;

using NUnit.Framework;
using Kistl.App.GUI;

namespace Kistl.GUI.Renderer.WPF.Tests
{
    [TestFixture]
    public class BoolControlTests : ValueControlTests<bool?, EditBoolProperty>
    {
        public BoolControlTests()
            : base(
                new ControlHarness<EditBoolProperty>(TestObject.TestBoolVisual, Toolkit.WPF),
                new BoolValues()
            )
        {
        }

        [Test]
        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(EditBoolProperty.ValueProperty, v));
        }

    }

    [TestFixture]
    public class DateTimeControlTests : ValueControlTests<DateTime?, EditDateTimeProperty>
    {
        public DateTimeControlTests()
            : base(
                new ControlHarness<EditDateTimeProperty>(TestObject.TestDateTimeVisual, Toolkit.WPF),
                new DateTimeValues()
            )
        {
        }

        [Test]
        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(EditDateTimeProperty.ValueProperty, v));
        }

    }

    [TestFixture]
    public class DoubleControlTests : ValueControlTests<double?, EditDoubleProperty>
    {
        public DoubleControlTests()
            : base(
                new ControlHarness<EditDoubleProperty>(TestObject.TestDoubleVisual, Toolkit.WPF),
                new DoubleValues()
            )
        {
        }
        [Test]
        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(EditDoubleProperty.ValueProperty, v));
        }
    }

    [TestFixture]
    public class IntControlTests : ValueControlTests<int?, EditIntProperty>
    {
        public IntControlTests()
            : base(
                new ControlHarness<EditIntProperty>(TestObject.TestIntVisual, Toolkit.WPF),
                new IntValues()
            )
        {
        }

        [Test]
        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(EditIntProperty.ValueProperty, v));
        }
    }

    [TestFixture]
    public class StringControlTests : ValueControlTests<string, EditSimpleProperty>
    {
        public StringControlTests()
            : base(
                new ControlHarness<EditSimpleProperty>(TestObject.TestStringVisual, Toolkit.WPF),
                new StringValues()
            )
        {
        }
        [Test]
        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(EditSimpleProperty.ValueProperty, v));
        }

    }

}
