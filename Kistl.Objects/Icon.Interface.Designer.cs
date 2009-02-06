
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Icon : IDataObject 
    {

        /// <summary>
        /// Filename of the Icon
        /// </summary>

		string IconFile { get; set; }
    }
}