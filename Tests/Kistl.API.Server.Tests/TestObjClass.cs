using System;
using System.Collections.Generic;
namespace Kistl.API.Server.Tests
{
    public interface TestObjClass : IDataObject
    {
        TestObjClass BaseTestObjClass { get; set; }
        string StringProp { get; set; }
        ICollection<TestObjClass> SubClasses { get; }
        int TestEnumProp { get; set; }
        void TestMethod(DateTime DateTimeParamForTestMethod);
        ICollection<string> TestNames { get; }
    }
}
