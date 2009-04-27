
namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// en:TimeAccount; Ein Konto für die Leistungserfassung. Es können nicht mehr als MaxStunden auf ein Konto gebucht werden.
    /// </summary>
    public interface Zeitkonto : IDataObject 
    {

        /// <summary>
        /// Aktuell gebuchte Stunden
        /// </summary>
		double? AktuelleStunden {
			get;
			set;
		}
        /// <summary>
        /// Name des Zeiterfassungskontos
        /// </summary>
		string Kontoname {
			get;
			set;
		}
        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
		double? MaxStunden {
			get;
			set;
		}
        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>

        ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter { get; }
        /// <summary>
        /// Platz für Notizen
        /// </summary>
		string Notizen {
			get;
			set;
		}
    }
}