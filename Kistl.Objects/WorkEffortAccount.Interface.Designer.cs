
namespace Kistl.App.TimeRecords
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// An account of work efforts. May be used to limit the hours being expended.
    /// </summary>
    public interface WorkEffortAccount : IDataObject 
    {

        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
		double? BudgetHours {
			get;
			set;
		}
        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>

        ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter { get; }
        /// <summary>
        /// Name des TimeRecordsskontos
        /// </summary>
		string Name {
			get;
			set;
		}
        /// <summary>
        /// Space for notes
        /// </summary>
		string Notes {
			get;
			set;
		}
        /// <summary>
        /// Aktuell gebuchte Stunden
        /// </summary>
		double? SpentHours {
			get;
			set;
		}
    }
}