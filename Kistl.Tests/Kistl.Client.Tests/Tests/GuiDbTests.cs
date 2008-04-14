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

        protected Visual CreateVisual()
        {
            return new Visual() { Name = TestStringControl.Info.Control, Property = TestObject.TestStringProperty };
        }

        [Test]
        public void FindControlInfo()
        {
            var cInfo = KistlGUIContext.FindControlInfo(TestStringControl.Info.Platform, CreateVisual());
            Assert.That(cInfo.Equals(TestStringControl.Info));
        }

        [Test]
        public void FindPresenterInfo()
        {
            var cInfo = KistlGUIContext.FindControlInfo(TestStringControl.Info.Platform, CreateVisual());
            var pInfo = KistlGUIContext.FindPresenterInfo(new Visual() { Name = TestStringControl.Info.Control });
            Assert.That(pInfo.Control == TestStringControl.Info.Control);
        }

        [Test]
        public void CreateWidget()
        {
            var cInfo = KistlGUIContext.FindControlInfo(TestStringControl.Info.Platform, CreateVisual());
            var widget = KistlGUIContext.CreateControl(cInfo);
            Assert.That(widget is TestStringControl);
        }

        [Test]
        public void CreatePresenter()
        {
            var visual = CreateVisual();
            var cInfo = KistlGUIContext.FindControlInfo(Toolkit.TEST, visual);
            var pInfo = KistlGUIContext.FindPresenterInfo(visual);

            var widget = KistlGUIContext.CreateControl(cInfo);
            var presenter = KistlGUIContext.CreatePresenter(pInfo, new TestObject(), visual, widget); 
            
            Assert.That(widget, Is.InstanceOfType(typeof(TestStringControl)));
            Assert.That(presenter, Is.InstanceOfType(typeof(StringPresenter)));
        }
    }
}
