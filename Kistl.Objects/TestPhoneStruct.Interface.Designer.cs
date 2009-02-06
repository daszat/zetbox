
namespace Kistl.App.Test
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface TestPhoneStruct : IStruct 
    {

        /// <summary>
        /// Enter a Number
        /// </summary>

		string Number { get; set; }
        /// <summary>
        /// Enter Area Code
        /// </summary>

		string AreaCode { get; set; }
    }
}