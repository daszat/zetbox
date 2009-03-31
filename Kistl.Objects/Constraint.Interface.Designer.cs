
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
        /// The property to be constrained
        /// </summary>
		Kistl.App.Base.Property ConstrainedProperty {
			get;
			set;
		}
        /// <summary>
        /// The reason of this constraint
        /// </summary>
		string Reason {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>

		 string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) ;
        /// <summary>
        /// 
        /// </summary>

		 bool IsValid(System.Object constrainedValue, System.Object constrainedObj) ;
    }
}