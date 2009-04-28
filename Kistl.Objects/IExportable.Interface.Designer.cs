
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Marks a DataType as exportable
    /// </summary>
    public interface IExportable  
    {

        /// <summary>
        /// Export Guid
        /// </summary>
		System.Guid ExportGuid {
			get;
			set;
		}
    }
}