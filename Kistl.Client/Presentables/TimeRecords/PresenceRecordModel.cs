
namespace Kistl.Client.Presentables.TimeRecords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Projekte;
    using Kistl.App.TimeRecords;

    /// <summary>
    /// Models <see cref="PresenceRecord"/>s for viewing and editing.
    /// </summary>
    public class PresenceRecordModel
        : DataObjectModel
    {
        public new delegate PresenceRecordModel Factory(IKistlContext dataCtx, PresenceRecord obj);

        /// <summary>Gets the presented <see cref="PresenceRecord"/></summary>
        public PresenceRecord Entry
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the PresenceRecordModel class.
        /// </summary>
        /// <param name="appCtx">which GuiApplicationContext to use</param>
        /// <param name="dataCtx">which <see cref="IKistlContext"/> to use</param>
        /// <param name="obj">the modelled <see cref="PresenceRecord"/></param>
        public PresenceRecordModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            PresenceRecord obj)
            : base(appCtx, dataCtx, obj)
        {
            this.Entry = obj;
        }
    }
}
