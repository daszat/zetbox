
namespace Kistl.App.Test
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface TestObjClass : IDataObject 
    {

        /// <summary>
        /// testtest
        /// </summary>

		Kistl.App.Projekte.Kunde ObjectProp { get; set; }
        /// <summary>
        /// String Property
        /// </summary>

		string StringProp { get; set; }
        /// <summary>
        /// Test Enumeration Property
        /// </summary>

		Kistl.App.Test.TestEnum TestEnumProp { get; set; }
        /// <summary>
        /// test
        /// </summary>

		int? MyIntProperty { get; set; }
        /// <summary>
        /// testmethod
        /// </summary>

		 void TestMethod(System.DateTime DateTimeParamForTestMethod) ;
    }
}