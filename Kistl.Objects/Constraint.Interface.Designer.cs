
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Constraint : IDataObject 
    {

        /// <summary>
        /// The reason of this constraint
        /// </summary>

		string Reason { get; set; }
        /// <summary>
        /// The property to be constrained
        /// </summary>

		Kistl.App.Base.BaseProperty ConstrainedProperty { get; set; }
        /// <summary>
        /// 
        /// </summary>

		 bool IsValid(System.Object constrainedValue, System.Object constrainedObj) ;
        /// <summary>
        /// 
        /// </summary>

		 string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) ;
    }
}