
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface StringRangeConstraint : Kistl.App.Base.Constraint 
    {

        /// <summary>
        /// The maximal length of this StringProperty
        /// </summary>

		int MaxLength { get; set; }
        /// <summary>
        /// The minimal length of this StringProperty
        /// </summary>

		int MinLength { get; set; }
    }
}