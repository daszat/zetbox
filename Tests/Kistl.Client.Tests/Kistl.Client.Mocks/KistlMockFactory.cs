
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
    using Kistl.Client.Presentables;

    public static class KistlMockFactory
    {
        public static Mock<ObjectReferenceProperty> CreateObjectReferenceProperty(
            string propertyName,
            bool isList
            )
        {
            var result = new Mock<ObjectReferenceProperty>();

            result.SetupAllProperties();
            result.Setup(orp => orp.GetPropertyTypeString()).Returns(typeof(ObjectReferenceProperty).FullName);
            result.Setup(orp => orp.Name).Returns(propertyName);
            result.Setup(orp => orp.GetIsList()).Returns(isList);

            return result;
        }

        public static Mock<TestObject> CreateTestObject()
        {
            var result = new Mock<TestObject>();
            result.Setup(obj => obj.GetInterfaceType()).Returns(new InterfaceType(typeof(TestObject)));
            return result;
        }

        public static Mock<IKistlContext> CreateContext()
        {
            var result = new Mock<IKistlContext>();
            return result;
        }

        public static Mock<IModelFactory> CreateFactory(Dictionary<IDataObject, PresentableModel> backingStore)
        {
            var result = new Mock<IModelFactory>();
            result.Setup(mf => mf.CreateDefaultModel(It.IsAny<IKistlContext>(), It.IsAny<IDataObject>(), It.Is<object>(o => o == null))).Returns((IKistlContext ctx, IDataObject obj, object nothing) =>
            {
                if (backingStore.ContainsKey(obj))
                {
                    return backingStore[obj];
                }
                else
                {
                    return (backingStore[obj] = new DataObjectModel(null, ctx, obj));
                }
            });
            return result;
        }
    }
}
