using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

namespace Kistl.GUI.Tests
{
    [TestFixture]
    public class StringPresenterTests : ReferencePresenterTests<TestObject, string, TestStringControl, StringPresenter>
    {
        public StringPresenterTests()
            : base(
                new PresenterHarness<TestObject, TestStringControl, StringPresenter>(
                    new TestObjectHarness(),
                    new ControlHarness<TestStringControl>(TestObject.TestStringVisual, Toolkit.TEST)))
        { }

        protected override string GetObjectValue() { return Object.TestString; }
        protected override string GetWidgetValue() { return Widget.Value; }
        protected override void SetObjectValue(string v) { Object.TestString = v; }
        protected override void UserInput(string v) { Widget.SimulateUserInput(v); }
        protected override string DefaultValue() { return null; }
        protected override IEnumerable<string> SomeValues()
        {
            return new List<string>(new[] { 
                "foo", "<xss>", "'!\"§!$%!%`id`$(id)§&''''''''#%$/\\\"$%!°\"§!/%()( -- blah /* blubb */ // hallo", "", 
                "00012346789", "0", "0.0", "0.1", "0.002", "10.0e100", "0x120",
                "...", "<xss>", "!'\"§!$§%&/(){}&amp;", "normal string",
                "very long string: very long string: very long string: very long string: very long string: very long string: " +
                "very long string: very long string: very long string: very long string: very long string: very long string: " +
                "very long string: very long string: very long string: very long string: very long string: very long string: " +
                "very long string: very long string: very long string: very long string: very long string: very long string"
            });
        }
    }
}
