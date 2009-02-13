
namespace Kistl.App.Projekte
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Task : IDataObject 
    {

        /// <summary>
        /// Taskname
        /// </summary>

		string Name { get; set; }
        /// <summary>
        /// Start Datum
        /// </summary>

		DateTime? DatumVon { get; set; }
        /// <summary>
        /// Enddatum
        /// </summary>

		DateTime? DatumBis { get; set; }
        /// <summary>
        /// Aufwand in Stunden
        /// </summary>

		double? Aufwand { get; set; }
        /// <summary>
        /// Verknüpfung zum Projekt
        /// </summary>

		Kistl.App.Projekte.Projekt Projekt { get; set; }
    }
}