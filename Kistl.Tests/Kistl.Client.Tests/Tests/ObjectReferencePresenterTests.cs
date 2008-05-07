using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using Kistl.Client.Mocks;
using Kistl.GUI;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class ObjectReferencePresenterTests : PresenterTest<TestObjectReferenceControl, PointerPresenter>
    {
        protected void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, Is.True, "the widget should be in a valid state after this operation");
        }

        [Test]
        public void HandleNoUserInput()
        {
            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceProperty);
            Assert.That(obj.TestObjectReference, Is.Empty, "ObjectReferenceProperty should default to empty");
            AssertWidgetHasValidValue();
        }
    }
}
