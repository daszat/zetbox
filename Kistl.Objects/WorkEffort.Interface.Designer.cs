
namespace Kistl.App.TimeRecords
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// A defined work effort of an employee.
    /// </summary>
    public interface WorkEffort : IDataObject 
    {

        /// <summary>
        /// Point in time when the work effort started.
        /// </summary>
		DateTime From {
			get;
			set;
		}
        /// <summary>
        /// Which employee effected this work effort.
        /// </summary>
		Kistl.App.Projekte.Mitarbeiter Mitarbeiter {
			get;
			set;
		}
        /// <summary>
        /// A short label describing this work effort.
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
        /// Point in time (inclusive) when the work effort ended.
        /// </summary>
		DateTime Thru {
			get;
			set;
		}
    }
}