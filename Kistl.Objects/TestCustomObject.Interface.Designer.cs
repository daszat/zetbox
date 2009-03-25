
namespace Kistl.App.Test
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface TestCustomObject : IDataObject 
    {

        /// <summary>
        /// Happy Birthday!
        /// </summary>
		DateTime? Birthday {
			get;
			set;
		}
        /// <summary>
        /// Persons Name
        /// </summary>
		string PersonName {
			get;
			set;
		}
        /// <summary>
        /// Mobile Phone Number
        /// </summary>
		Kistl.App.Test.TestPhoneStruct PhoneNumberMobile {
			get;
		}
        /// <summary>
        /// Office Phone Number
        /// </summary>
		Kistl.App.Test.TestPhoneStruct PhoneNumberOffice {
			get;
		}
    }
}