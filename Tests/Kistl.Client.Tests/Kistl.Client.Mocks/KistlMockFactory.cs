
namespace Kistl.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;

    using Moq;

    public static class KistlMockFactory
    {
        public static Mock<ObjectReferenceProperty> CreateObjectReferenceProperty(
            string propertyName
            )
        {
            var mock = new Mock<ObjectReferenceProperty>();

            mock.SetupAllProperties();
            mock.Setup(orp => orp.GetPropertyTypeString()).Returns(typeof(ObjectReferenceProperty).FullName);
            mock.Setup(orp => orp.PropertyName).Returns(propertyName);

            return mock;
        }

        public static Mock<TestObject> CreateTestObject()
        {
            var result = new Mock<TestObject>();

            // TODO: Stub IDataObject here.

            return result;
        }

        public static Mock<IKistlContext> CreateContext()
        {
            var result = new Mock<IKistlContext>();

            return result;
        }
    }

}
