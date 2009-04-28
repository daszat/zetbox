// <copyright file="WorkEffortModel.cs" company="dasz.at OG">
//     Copyright (C) 2009 dasz.at OG. All rights reserved.
// </copyright>

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
    /// A <see cref="DataObjectModel"/> for <see cref="WorkEffort"/>.
    /// </summary>
    public class WorkEffortModel
        : DataObjectModel
    {
        /// <summary>The presented <see cref="WorkEffort"/></summary>
        private WorkEffort eintrag;

        /// <summary>
        /// Initializes a new instance of the WorkEffortModel class.
        /// </summary>
        /// <param name="appCtx">which GuiApplicationContext to use</param>
        /// <param name="dataCtx">which <see cref="IDataContext"/> to use</param>
        /// <param name="obj">the modelled <see cref="WorkEffort"/></param>
        public WorkEffortModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            WorkEffort obj)
            : base(appCtx, dataCtx, obj)
        {
            this.eintrag = obj;
        }

        #region WorkEffort Members

        /// <summary>Gets or sets the "From"s value of the underlying WorkEffort</summary>
        public DateTime From
        {
            get { return this.eintrag.From; }
            set { this.eintrag.From = value; }
        }

        /// <summary>Gets or sets the "Name"s value of the underlying WorkEffort</summary>
        public new string Name
        {
            get { return this.eintrag.Name; }
            set { this.eintrag.Name = value; }
        }

        /// <summary>Gets or sets the "Thru"s value of the underlying WorkEffort</summary>
        public DateTime Thru
        {
            get { return this.eintrag.Thru; }
            set { this.eintrag.Thru = value; }
        }

        /// <summary>Gets or sets the "Mitarbeiter"s value of the underlying WorkEffort, wrapped in a DataObjectModel</summary>
        public DataObjectModel Mitarbeiter
        {
            get { return (DataObjectModel)this.Factory.CreateDefaultModel(this.DataContext, this.eintrag.Mitarbeiter); }
            set { this.eintrag.Mitarbeiter = (Mitarbeiter)value.Object; }
        }

        /// <summary>Gets or sets the "Notizen"s value of the underlying WorkEffort</summary>
        public string Notes
        {
            get { return this.eintrag.Notes; }
            set { this.eintrag.Notes = value; }
        }

        #endregion
    }
}
