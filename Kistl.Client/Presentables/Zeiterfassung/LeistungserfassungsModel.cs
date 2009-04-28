// <copyright file="LeistungserfassungsModel.cs" company="dasz.at OG">
//     Copyright (C) 2009 dasz.at OG. All rights reserved.
// </copyright>

namespace Kistl.Client.Presentables.Zeiterfassung
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    /// <summary>
    /// Aufbereitung des Zeiterfassungsmoduls für die Leistungserfassung.
    /// </summary>
    /// Der Use-Case der von diesem PresentableModel abgedeckt wird ist die minuten-aktuelle
    /// Leistungserfassung durch einen Mitarbeiter an seinem Bildschirmarbeitsplatz.
    public class LeistungserfassungsModel
        : WorkspaceModel
    {
        /// <summary>
        /// Initializes a new instance of the LeistungserfassungsModel class.
        /// </summary>
        /// <param name="appCtx">der zu verwendende GuiApplicationContext</param>
        /// <param name="dataCtx">der zu verwendende Datenkontext</param>
        public LeistungserfassungsModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            this.AvailableUsers = new ObservableCollection<DataObjectModel>();
            this.Leistungen = new ObservableCollection<LeistungsEintragModel>();
        }

        /// <summary>Gets or sets the currently working user.</summary>
        /// TODO: This will have to change once there is ACL support
        public DataObjectModel CurrentUser { get; set; }

        /// <summary>Gets a list of users that may record work efforts.</summary>
        public ObservableCollection<DataObjectModel> AvailableUsers { get; private set; }

        /// <summary>Gets or sets a value indicating whether the <see cref="CurrentUser"/> is currently on work time or not.</summary>
        public bool IsCurrentlyWorking { get; set; }

        /// <summary>Gets a list of recorded <see cref="LeisungsEintrag"/> of the <see cref="CurrentUser"/>.</summary>
        public ObservableCollection<LeistungsEintragModel> Leistungen { get; private set; }

        /// <summary>Gets or sets the currently open <see cref="LeisungsEintrag"/>.</summary>
        public LeistungsEintragModel CurrentEintrag { get; set; }

        /// <summary>Gets the total time (in hours) the <see cref="CurrentUser"/> has worked today.</summary>
        public float TotalWorkTimeToday { get; private set; }

        /// <summary>Gets the time (in hours) since the last break of the <see cref="CurrentUser"/>.</summary>
        public float TimeSinceLastBreak { get; private set; }
    }
}
