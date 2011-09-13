// <autogenerated/>

namespace Kistl.App.Projekte
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Mitarbeiter : IDataObject, Kistl.App.Base.IChangedBy, Kistl.App.Base.IExportable 
    {

        /// <summary>
        /// Herzlichen Glückwunsch zum Geburtstag
        /// </summary>
        DateTime? Geburtstag {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Kistl.App.Base.Identity Identity {
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
        /// Projekte des/der Mitarbeiters/Mitarbeiterin für die er/sie Verantwortlich ist
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
        DateTime TestMethodForParameter(System.Boolean TestBool, System.Guid TestCLRObjectParameter, System.DateTime TestDateTime, System.Double TestDouble, System.Int32 TestInt, Kistl.App.Projekte.Auftrag TestObjectParameter, System.String TestString);
    }
}
