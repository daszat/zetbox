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
    public class GuiDbTests : PresenterTests<TestObject, TestStringControl, StringPresenter>
    {

        public GuiDbTests()
            : base(
                new PresenterHarness<TestObject, TestStringControl, StringPresenter>(
                    new TestObjectHarness(),
                    new ControlHarness<TestStringControl>(TestObject.TestStringVisual, Toolkit.TEST)))
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
            KistlGUIContext.CreatePresenter(KistlGUIContext.FindPresenterInfo(Visual), null, Visual, Widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoVisual()
        {
            KistlGUIContext.CreatePresenter(KistlGUIContext.FindPresenterInfo(Visual), Object, null, Widget);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFailNoWidget()
        {
            KistlGUIContext.CreatePresenter(KistlGUIContext.FindPresenterInfo(Visual), Object, Visual, null);
        }

    }
}
