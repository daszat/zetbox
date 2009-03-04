
namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Taetigkeit : IDataObject 
    {

        /// <summary>
        /// Mitarbeiter
        /// </summary>

		Kistl.App.Projekte.Mitarbeiter Mitarbeiter { get; set; }
        /// <summary>
        /// Zeitkonto
        /// </summary>

		Kistl.App.Zeiterfassung.Zeitkonto Zeitkonto { get; set; }
        /// <summary>
        /// Art der Tätigkeit
        /// </summary>

		Kistl.App.Zeiterfassung.TaetigkeitsArt TaetigkeitsArt { get; set; }
        /// <summary>
        /// Datum
        /// </summary>

		DateTime Datum { get; set; }
        /// <summary>
        /// Dauer in Stunden
        /// </summary>

		double Dauer { get; set; }
    }
}