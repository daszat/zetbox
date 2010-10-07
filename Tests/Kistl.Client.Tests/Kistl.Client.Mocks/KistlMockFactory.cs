
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

    public class KistlMockFactory
    {
        InterfaceType.Factory _iftFactory;
        public KistlMockFactory(InterfaceType.Factory iftFactory)
        {
            this._iftFactory = iftFactory;
        }

        public Mock<ObjectReferenceProperty> CreateObjectReferenceProperty(
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

        public Mock<TestObject> CreateTestObject()
        {
            var result = new Mock<TestObject>();
            //result.Setup(obj => obj.GetInterfaceType()).Returns(new InterfaceType(typeof(TestObject)));
            return result;
        }

        public static Mock<IKistlContext> CreateContext()
        {
            var result = new Mock<IKistlContext>();
            return result;
        }

        public static Mock<IModelFactory> CreateFactory(Dictionary<IDataObject, ViewModel> backingStore)
        {
            var result = new Mock<IModelFactory>();
            // TODO: ????
            //result.Setup(mf => mf.CreateDefaultModel(It.IsAny<IKistlContext>(), It.IsAny<IDataObject>(), It.Is<object>(o => o == null))).Returns((IKistlContext ctx, IDataObject obj, object nothing) =>
            //{
            //    if (backingStore.ContainsKey(obj))
            //    {
            //        return backingStore[obj];
            //    }
            //    else
            //    {
            //        return (backingStore[obj] = new DataObjectViewModel(null, ctx, obj));
            //    }
            //});
            return result;
        }
    }
}
