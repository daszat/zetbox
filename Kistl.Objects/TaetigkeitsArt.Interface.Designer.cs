
namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface TaetigkeitsArt : IDataObject 
    {

        /// <summary>
        /// Name der Tätigkeitsart
        /// </summary>

		string Name { get; set; }
    }
}