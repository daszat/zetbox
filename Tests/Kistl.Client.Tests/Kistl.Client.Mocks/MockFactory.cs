using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;

using NMock2;

namespace Kistl.Client.Mocks
{
    public static class MockFactory
    {
        public static ObjectReferenceProperty CreateObjectReferenceProperty(
            Mockery m,
            string propertyName
            )
        {
            var result = m.NewMock<ObjectReferenceProperty>();

            Stub.On(result)
                .Method("GetPropertyTypeString")
                .WithNoArguments()
                .Will(Return.Value(result.GetType().FullName));

            Stub.On(result)
                .GetProperty("PropertyName")
                .Will(Return.Value(propertyName));

            return result;
        }

        public static TestObject CreateTestObject(Mockery m)
        {
            var result = m.NewMock<TestObject>("Some TestObject");

            // TODO: Stub IDataObject here.

            return result;
        }

        public static IKistlContext CreateContext(Mockery m)
        {
            var result = m.NewMock<IKistlContext>();

            return result;
        }
    }

}
