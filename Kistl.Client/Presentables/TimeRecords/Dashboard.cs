

namespace Kistl.Client.Presentables.TimeRecords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    /// <summary>
    /// An executive summary of the Time Records module's data.
    /// </summary>
    public class Dashboard
        : PresentableModel
    {
        /// <summary>
        /// Initializes a new instance of the Dashboard class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        public Dashboard(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
        }
    }
}
