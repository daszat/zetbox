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
        public static ControlInfo CreateControlInfo(Mockery m, VisualType visualType, bool isContainer, Type testType)
        {
            var result = m.NewMock<ControlInfo>();

            Stub.On(result)
                .GetProperty("Platform")
                .Will(Return.Value(Toolkit.TEST));

            Stub.On(result)
                .GetProperty("ControlType")
                .Will(Return.Value(visualType));

            Stub.On(result)
                .GetProperty("IsContainer")
                .Will(Return.Value(isContainer));

            Stub.On(result)
                .GetProperty("ClassName")
                .Will(Return.Value(testType.FullName));

            return result;
        }

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

        public static BackReferenceProperty CreateBackReferenceProperty(Mockery m)
        {
            var result = m.NewMock<BackReferenceProperty>();

            Stub.On(result)
                .Method("GetPropertyTypeString")
                .WithNoArguments()
                .Will(Return.Value(result.GetType().FullName));

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
