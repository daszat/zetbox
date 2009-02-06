
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for Enumeration Properties.
    /// </summary>
    public interface EnumerationProperty : Kistl.App.Base.ValueTypeProperty 
    {

        /// <summary>
        /// Enumeration der Eigenschaft
        /// </summary>

		Kistl.App.Base.Enumeration Enumeration { get; set; }
    }
}