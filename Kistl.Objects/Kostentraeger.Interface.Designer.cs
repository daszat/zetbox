
namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Kostentraeger : Kistl.App.Zeiterfassung.Zeitkonto 
    {

        /// <summary>
        /// Projekt des Kostenträgers
        /// </summary>
		Kistl.App.Projekte.Projekt Projekt {
			get;
			set;
		}
    }
}