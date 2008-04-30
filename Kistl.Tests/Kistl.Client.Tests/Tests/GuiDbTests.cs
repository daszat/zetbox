using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class GuiDbTests : PresenterTest<TestStringControl, StringPresenter>
    {
        protected virtual void Init()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
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
            Assert.That(widget.IsValidValue, Is.True);
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

    }
}
