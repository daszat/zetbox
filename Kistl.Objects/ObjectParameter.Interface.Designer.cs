
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for Object Parameter.
    /// </summary>
    public interface ObjectParameter : Kistl.App.Base.BaseParameter 
    {

        /// <summary>
        /// Kistl-Typ des Parameters
        /// </summary>

		Kistl.App.Base.DataType DataType { get; set; }
    }
}