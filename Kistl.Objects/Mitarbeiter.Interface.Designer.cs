
namespace Kistl.App.Projekte
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Mitarbeiter : IDataObject 
    {

        /// <summary>
        /// Herzlichen Glückwunsch zum Geburtstag
        /// </summary>
		DateTime? Geburtstag {
			get;
			set;
		}
        /// <summary>
        /// Vorname Nachname
        /// </summary>
		string Name {
			get;
			set;
		}
        /// <summary>
        /// Projekte des Mitarbeiters für die er Verantwortlich ist
        /// </summary>

        IList<Kistl.App.Projekte.Projekt> Projekte { get; }
        /// <summary>
        /// NNNN TTMMYY
        /// </summary>
		string SVNr {
			get;
			set;
		}
        /// <summary>
        /// +43 123 12345678
        /// </summary>
		string TelefonNummer {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>

		 DateTime TestMethodForParameter(System.String TestString, System.Int32 TestInt, System.Double TestDouble, System.Boolean TestBool, System.DateTime TestDateTime, Kistl.App.Projekte.Auftrag TestObjectParameter, System.Guid TestCLRObjectParameter) ;
    }
}