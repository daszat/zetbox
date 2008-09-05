using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Tests;
using Kistl.GUI.Renderer.WPF;
using Kistl.GUI.Mocks;
using Kistl.App.GUI;

namespace Kistl.GUI.Integration.WPF
{
    [TestFixture]
    public class ObjectReferenceControlIntegrationTests
        : ObjectReferencePresenterInfrastructure<TestObject, ObjectReferenceControl, ObjectReferencePresenter<TestObject>>
    {
        public ObjectReferenceControlIntegrationTests()
            : base(
                new PresenterHarness<TestObject, ObjectReferenceControl, ObjectReferencePresenter<TestObject>>(
                    new TestObjectHarness(),
                    typeof(ObjectReferenceProperty),
                    new ControlHarness<ObjectReferenceControl>(
                        TestObject.TestObjectReferenceVisual,
                        Toolkit.WPF)),
                Toolkit.WPF,
                TestObjectValues.TestValues
            )
        {
        }

        protected override TestObject GetObjectValue() { return Object.TestObjectReference; }
        protected override void SetObjectValue(TestObject v) { Object.TestObjectReference = v; }

        protected override void UserInput(TestObject v)
        {
            ((System.Windows.DependencyObject)Widget).SetValue(ObjectReferenceControl.ValueProperty, v);
        }


    }
}

namespace Kistl.GUI.Integration.ASPNET
{

    // TODO: re-enable when ASPNET is implemented
    // [TestFixture]
    public class ObjectReferenceControlIntegrationTests
    // : ObjectReferencePresenterInfrastructure<IDataObject, ObjectReferenceControl, ObjectReferencePresenter>
    {
        public ObjectReferenceControlIntegrationTests()
        // : base(Toolkit.ASPNET)
        {
        }
    }
}
