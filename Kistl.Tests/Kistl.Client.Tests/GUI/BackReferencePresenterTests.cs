using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.API;

namespace Kistl.GUI.Tests
{
    [TestFixture]
    public class BackReferencePresenterTests : PresenterTest<TestBackReferenceControl, BackReferencePresenter>
    {
        protected void AssertWidgetHasValidValue()
        {
            Assert.That(widget.HasValidValue, Is.True, "the widget should be in a valid state after this operation");
        }

        [Test]
        public void HandleNoUserInput()
        {
            Init(TestBackReferenceControl.Info, TestObject.TestBackReferenceDescriptor);
            Assert.That(obj.TestBackReference, Is.Empty, "BackReferenceProperty should default to empty");
            AssertWidgetHasValidValue();
        }
    }
}
