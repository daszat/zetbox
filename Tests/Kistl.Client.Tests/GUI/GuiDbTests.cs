using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

namespace Kistl.GUI.Tests
{
    [TestFixture]
    public class GuiDbTests : PresenterTests<TestObject, string, TestStringControl, StringPresenter>
    {

        public GuiDbTests()
            : base(
                new PresenterHarness<TestObject, TestStringControl, StringPresenter>(
                    new TestObjectHarness(),
                    typeof(StringProperty),
                    new ControlHarness<TestStringControl>(TestObject.TestStringVisual, Toolkit.TEST)),
                new StringValues())
        { }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoInfo()
        {
            KistlGUIContext.CreatePresenter(null, Object, Visual, Widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoObject()
        {
            KistlGUIContext.CreatePresenter(KistlGUIContext.FindPresenterInfo(Visual, typeof(StringProperty)), null, Visual, Widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoVisual()
        {
            KistlGUIContext.CreatePresenter(KistlGUIContext.FindPresenterInfo(Visual, typeof(StringProperty)), Object, null, Widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoWidget()
        {
            KistlGUIContext.CreatePresenter(KistlGUIContext.FindPresenterInfo(Visual, typeof(StringProperty)), Object, Visual, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoProperty()
        {
            KistlGUIContext.CreatePresenter(KistlGUIContext.FindPresenterInfo(Visual, null), Object, Visual, Widget);
        }

    }

    [TestFixture]
    public class VisualTests
    {

        private Visual v;

        [SetUp]
        public void SetUp()
        {
            v = new Visual();
        }

        [TearDown]
        public void TearDown()
        {
            Assert.IsNotNull(v, "Tests should not remove 'v'");
            v = null;
        }

        [Test]
        public void TestProperties()
        {
            IList<Visual> children = new List<Visual>();
            v.Children = children;
            Assert.AreEqual(children, v.Children, "Visual.Children should not munge its value");

            VisualType controlType = VisualType.Integer;
            v.ControlType = controlType;
            Assert.AreEqual(controlType, v.ControlType, "Visual.ControlType should not munge its value");

            string desc = "some description";
            v.Description = desc;
            Assert.AreEqual(desc, v.Description, "Visual.Description should not munge its value");

            BaseProperty bp = new BaseProperty();
            v.Property = bp;
            Assert.AreEqual(bp, v.Property, "Visual.Property should not munge its value");
        }

        internal class Defaults
        {
            internal BaseProperty Property { get; set; }
            internal VisualType VisualType { get; set; }
        }

        [Test]
        public void TestDefaults()
        {
            var defaults = new[]{
                new Defaults(){ Property = new BackReferenceProperty(), VisualType = VisualType.ObjectList },
                new Defaults(){ Property = new BoolProperty(), VisualType = VisualType.Boolean },
                new Defaults(){ Property = new DateTimeProperty(), VisualType = VisualType.DateTime },
                new Defaults(){ Property = new DoubleProperty(), VisualType = VisualType.Double },
                new Defaults(){ Property = new IntProperty(), VisualType = VisualType.Integer },
                new Defaults(){ Property = new ObjectReferenceProperty() { IsList = true }, VisualType = VisualType.ObjectList },
                new Defaults(){ Property = new ObjectReferenceProperty() { IsList = false }, VisualType = VisualType.ObjectReference },
                // TODO: new Defaults(){ Property = new PropertyGroup(), VisualType = VisualType.PropertyGroup },
                new Defaults(){ Property = new StringProperty(), VisualType = VisualType.String },
            };

            foreach (var d in defaults)
            {
                Visual v = Visual.CreateDefaultVisual(d.Property);
                Assert.AreEqual(d.VisualType, v.ControlType, "{0} should be displayed in a VisualType.{1}", d.Property, d.VisualType);
            }
        }
    }
}
