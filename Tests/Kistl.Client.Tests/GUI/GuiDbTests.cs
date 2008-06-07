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
using Kistl.API;

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
        public void FindPresenterFailNoVisual()
        {
            KistlGUIContext.FindPresenterInfo(null, typeof(StringProperty));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindPresenterFailNoProperty()
        {
            KistlGUIContext.FindPresenterInfo(Visual, null);
        }

        [Test]
        public void TestFindPresenter()
        {
            var pi = KistlGUIContext.FindPresenterInfo(Visual, typeof(StringProperty));
            Assert.AreEqual(Visual.ControlType, pi.Control, "FindPresenterInfo should return matching PresenterInfo: ControlType");
            Assert.AreEqual(typeof(StringProperty), pi.SourceType, "FindPresenterInfo should return matching PresenterInfo: SourceType");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindControlFailNoVisualType()
        {
            KistlGUIContext.FindControlInfo(Toolkit.TEST, null);
        }

        [Test]
        public void TestFindControlByVisual()
        {
            Toolkit tk = Toolkit.TEST;
            var ci = KistlGUIContext.FindControlInfo(tk, Visual);
            Assert.AreEqual(tk, ci.Platform, "FindControlInfo should return matching ControlInfo: Platform");
            Assert.AreEqual(Visual.ControlType, ci.ControlType, "FindControlInfo should return matching ControlInfo: ControlType");
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

        [Test]
        [ExpectedException(ExceptionType = typeof(NotImplementedException))]
        public void TestEnumeration()
        {
            Visual v = Visual.CreateDefaultVisual(new EnumerationProperty());
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(NotImplementedException))]
        public void TestValueType()
        {
            Visual v = Visual.CreateDefaultVisual(new ValueTypeProperty());
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(InvalidCastException))]
        public void TestFailure()
        {
            Visual v = Visual.CreateDefaultVisual(new BaseProperty());
        }
    }

    [TestFixture]
    public class TemplateTests
    {
        private Template t;

        [SetUp]
        public void SetUp()
        {
            t = new Template();
        }

        [TearDown]
        public void TearDown()
        {
            Assert.IsNotNull(t, "Tests should not remove 'v'");
            t = null;
        }

        [Test]
        public void TestProperties()
        {
            string name = "some display name";
            t.DisplayName = name;
            Assert.AreEqual(name, t.DisplayName, "Template.DisplayName should not munge its value");

            ObjectType type = new ObjectType();
            t.Type = type;
            Assert.AreEqual(type, t.Type, "Template.Type should not munge its value");

            TemplateUsage usage = TemplateUsage.EditControl;
            t.Usage = usage;
            Assert.AreEqual(usage, t.Usage, "Template.Usage should not munge its value");

            Visual tree = new Visual();
            t.VisualTree = tree;
            Assert.AreEqual(tree, t.VisualTree, "Template.VisualTree should not munge its value");
        }

        [Test]
        [Ignore("unable to test at the moment, because ClientHelper.Objectclasses cannot be mocked")]
        public void TestDefaultUnknownObjectType()
        {
            t = Template.DefaultTemplate(new ObjectType());
        }


        [Test]
        [Ignore("unable to test at the moment, because ClientHelper.Objectclasses cannot be mocked")]
        public void TestDefaultsOther()
        {
            t = Template.DefaultTemplate(new ObjectType());
        }

    }
}
