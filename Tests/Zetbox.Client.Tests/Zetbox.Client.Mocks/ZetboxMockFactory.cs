
namespace Zetbox.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    using Moq;
    using Zetbox.Client.Presentables;

    public class ZetboxMockFactory
    {
        public ZetboxMockFactory(InterfaceType.Factory iftFactory)
        {
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

        public static Mock<IZetboxContext> CreateContext()
        {
            var result = new Mock<IZetboxContext>();
            return result;
        }

        public static Mock<IViewModelFactory> CreateFactory(Dictionary<IDataObject, ViewModel> backingStore)
        {
            var result = new Mock<IViewModelFactory>();
            // TODO: ????
            //result.Setup(mf => mf.CreateDefaultModel(It.IsAny<IZetboxContext>(), It.IsAny<IDataObject>(), It.Is<object>(o => o == null))).Returns((IZetboxContext ctx, IDataObject obj, object nothing) =>
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
