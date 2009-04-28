
namespace Kistl.App.TimeRecords
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface PresenceRecord : IDataObject 
    {

        /// <summary>
        /// Point in time when the presence started.
        /// </summary>
		DateTime From {
			get;
			set;
		}
        /// <summary>
        /// Which employee was present.
        /// </summary>
		Kistl.App.Projekte.Mitarbeiter Mitarbeiter {
			get;
			set;
		}
        /// <summary>
        /// Point in time (inclusive) when the presence ended.
        /// </summary>
		DateTime? Thru {
			get;
			set;
		}
    }
}