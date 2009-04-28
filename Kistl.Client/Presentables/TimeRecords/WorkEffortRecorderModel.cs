// <copyright file="WorkEffortRecorderModel.cs" company="dasz.at OG">
//     Copyright (C) 2009 dasz.at OG. All rights reserved.
// </copyright>

namespace Kistl.Client.Presentables.TimeRecords
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    /// <summary>
    /// Main workspace presenter of the TimeRecords module for recording work efforts.
    /// </summary>
    /// This <see cref="WorkspaceModel"/> implements the use case of capturing fine grained
    /// work effort information "on the go" while employees are working on their PC.
    public class WorkEffortRecorderModel
        : WorkspaceModel
    {
        /// <summary>
        /// Initializes a new instance of the WorkEffortRecorderModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        public WorkEffortRecorderModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            this.AvailableUsers = new ObservableCollection<DataObjectModel>();
            this.Efforts = new ObservableCollection<WorkEffortModel>();
        }

        /// <summary>Gets or sets the currently working user.</summary>
        /// TODO: This will have to change once there is ACL support
        public DataObjectModel CurrentUser { get; set; }

        /// <summary>Gets a list of users that may record work efforts.</summary>
        public ObservableCollection<DataObjectModel> AvailableUsers { get; private set; }

        /// <summary>Gets or sets a value indicating whether the <see cref="CurrentUser"/> is currently on work time or not.</summary>
        public bool IsCurrentlyWorking { get; set; }

        /// <summary>Gets a list of recorded <see cref="WorkEffort"/> of the <see cref="CurrentUser"/>.</summary>
        public ObservableCollection<WorkEffortModel> Efforts { get; private set; }

        /// <summary>Gets or sets the currently open <see cref="LeisungsEintrag"/>.</summary>
        public WorkEffortModel CurrentEffort { get; set; }

        /// <summary>Gets the total time (in hours) the <see cref="CurrentUser"/> has worked today.</summary>
        public float TotalWorkTimeToday { get; private set; }

        /// <summary>Gets the time (in hours) since the last break of the <see cref="CurrentUser"/>.</summary>
        public float TimeSinceLastBreak { get; private set; }
    }
}
