
namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Eine definierte Leistung eines Mitarbeiters, die auf ein Zeitkonto gebucht worden ist.
    /// </summary>
    public interface LeistungsEintrag : IDataObject 
    {

        /// <summary>
        /// Wann diese Leistung begonnen wurde
        /// </summary>
		DateTime Anfang {
			get;
			set;
		}
        /// <summary>
        /// Eine kurze Überschrift, was gemacht wurde.
        /// </summary>
		string Bezeichnung {
			get;
			set;
		}
        /// <summary>
        /// Wann diese Leistung beendet wurde.
        /// </summary>
		DateTime Ende {
			get;
			set;
		}
        /// <summary>
        /// Der Mitarbeiter der diese Leistung erbracht hat.
        /// </summary>
		Kistl.App.Projekte.Mitarbeiter Mitarbeiter {
			get;
			set;
		}
        /// <summary>
        /// Notizen zu dieser Leistung
        /// </summary>
		string Notizen {
			get;
			set;
		}
    }
}