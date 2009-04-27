
namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface ArbeitszeitEintrag : IDataObject 
    {

        /// <summary>
        /// Wann die Anwesenheit angefangen hat.
        /// </summary>
		DateTime Anfang {
			get;
			set;
		}
        /// <summary>
        /// Wann die Anwesenheit geendet hat.
        /// </summary>
		DateTime Ende {
			get;
			set;
		}
        /// <summary>
        /// Welcher Mitarbeiter diese Arbeitszeit geleistet hat.
        /// </summary>
		Kistl.App.Projekte.Mitarbeiter Mitarbeiter {
			get;
			set;
		}
    }
}