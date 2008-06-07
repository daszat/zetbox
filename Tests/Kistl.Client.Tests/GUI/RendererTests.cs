using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI;

namespace Kistl.GUI.Tests
{
    [TestFixture]
    public class RendererTests
    {
        [Test]
        public void TestCreateControl()
        {
            TestRenderer r = new TestRenderer();
            Assert.AreEqual(Toolkit.TEST, r.Platform, "TestRenderer.Platform should be Toolkit.TEST");
            TestObject o = new TestObject();
            object c = r.CreateControl(o, TestObject.TestStringVisual);
            Assert.IsInstanceOfType(typeof(IValueControl<String>), c, "TestStringVisual control should be of type IValueControl<String>");
            Assert.IsInstanceOfType(typeof(TestStringControl), c, "TestStringVisual control should be of type TestStringControl");
        }

        [Test]
        public void TestCreateObjectControl()
        {
            TestRenderer r = new TestRenderer();
            TestObject o = new TestObject();
            object c = r.CreateControl(o, new Visual() { ControlType = VisualType.Object, Description = "The whole Object" });
            Assert.IsInstanceOfType(typeof(IObjectControl), c, "TestStringVisual control should be of type IObjectControl");
            // Assert.IsInstanceOfType(typeof(TestObjectControl), c, "TestStringVisual control should be of type TestObjectControl");
        }
    }
}
