using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Kistl.GUI.DB;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.Client.Tests.Mocks;
using Kistl.App.Base;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class GuiDbTests
    {

        private TestObject obj;
        private Visual visual;
        private ControlInfo cInfo;
        private PresenterInfo pInfo;
        private TestStringControl widget;
        private StringPresenter presenter;

        protected void Init()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
        }

        protected void Init(ControlInfo ci, BaseProperty bp)
        {
            visual = new Visual() { Name = ci.Control, Property = bp };
            cInfo = KistlGUIContext.FindControlInfo(Toolkit.TEST, visual);
            pInfo = KistlGUIContext.FindPresenterInfo(visual);
            widget = (TestStringControl)KistlGUIContext.CreateControl(cInfo);
            presenter = (StringPresenter)KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);
        }


        [SetUp]
        public void SetUp()
        {
            obj = new TestObject();
        }

        [Test]
        public void FindControlInfo()
        {
            Init();
            Assert.That(cInfo.Equals(TestStringControl.Info));
        }

        [Test]
        public void FindPresenterInfo()
        {
            Init();
            Assert.That(pInfo.Control == TestStringControl.Info.Control);
        }

        [Test]
        public void CreateControl()
        {
            Init();
            Assert.That(widget, Is.Not.Null);
            Assert.That(widget.HasValidValue, Is.True);
        }

        [Test]
        public void CreatePresenter()
        {
            Init();
            Assert.That(presenter, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoInfo()
        {
            KistlGUIContext.CreatePresenter(null, obj, visual, widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoObject()
        {
            KistlGUIContext.CreatePresenter(pInfo, null, visual, widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoVisual()
        {
            KistlGUIContext.CreatePresenter(pInfo, obj, null, widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoWidget()
        {
            KistlGUIContext.CreatePresenter(pInfo, obj, visual, null);
        }

        [Test]
        public void HandleNoUserInput()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
            widget.SimulateUserInput(null);
            Assert.That(obj.TestString, Is.Null);
            Assert.That(widget.HasValidValue, Is.True);
        }

        [Test]
        public void HandleNoUserInputInvalid()
        {
            Init(TestStringControl.Info, TestObject.TestStringNotNullProperty);
            widget.SimulateUserInput(null);
            Assert.That(obj.TestStringNotNull, Is.Not.Null);
            Assert.That(widget.HasValidValue, Is.False);
        }

        [Test]
        public void HandleUserInput()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
            string newStringValue = "new Value";
            widget.SimulateUserInput(newStringValue);
            Assert.That(obj.TestString, Is.EqualTo(newStringValue));
            Assert.That(widget.HasValidValue, Is.True);
        }

    }
}
