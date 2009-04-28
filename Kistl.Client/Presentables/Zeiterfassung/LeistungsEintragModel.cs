// <copyright file="LeistungsEintragModel.cs" company="dasz.at OG">
//     Copyright (C) 2009 dasz.at OG. All rights reserved.
// </copyright>

namespace Kistl.Client.Presentables.Zeiterfassung
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Projekte;
    using Kistl.App.Zeiterfassung;

    /// <summary>
    /// A <see cref="DataObjectModel"/> for <see cref="LeistungsEintrag"/>.
    /// </summary>
    public class LeistungsEintragModel
        : DataObjectModel
    {
        /// <summary>The presented <see cref="LeistungsEintrag"/></summary>
        private LeistungsEintrag eintrag;

        /// <summary>
        /// Initializes a new instance of the LeistungsEintragModel class.
        /// </summary>
        /// <param name="appCtx">which GuiApplicationContext to use</param>
        /// <param name="dataCtx">which <see cref="IDataContext"/> to use</param>
        /// <param name="obj">the modelled <see cref="LeistungsEintrag"/></param>
        public LeistungsEintragModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            LeistungsEintrag obj)
            : base(appCtx, dataCtx, obj)
        {
            this.eintrag = obj;
        }

        #region LeistungsEintrag Members

        /// <summary>Gets or sets the "Anfang"s value of the underlying LeistungsEintrag</summary>
        public DateTime Anfang
        {
            get { return this.eintrag.Anfang; }
            set { this.eintrag.Anfang = value; }
        }

        /// <summary>Gets or sets the "Bezeichnung"s value of the underlying LeistungsEintrag</summary>
        public string Bezeichnung
        {
            get { return this.eintrag.Bezeichnung; }
            set { this.eintrag.Bezeichnung = value; }
        }

        /// <summary>Gets or sets the "Ende"s value of the underlying LeistungsEintrag</summary>
        public DateTime Ende
        {
            get { return this.eintrag.Ende; }
            set { this.eintrag.Ende = value; }
        }

        /// <summary>Gets or sets the "Mitarbeiter"s value of the underlying LeistungsEintrag, wrapped in a DataObjectModel</summary>
        public DataObjectModel Mitarbeiter
        {
            get { return (DataObjectModel)this.Factory.CreateDefaultModel(this.DataContext, this.eintrag.Mitarbeiter); }
            set { this.eintrag.Mitarbeiter = (Mitarbeiter)value.Object; }
        }

        /// <summary>Gets or sets the "Notizen"s value of the underlying LeistungsEintrag</summary>
        public string Notizen
        {
            get { return this.eintrag.Notizen; }
            set { this.eintrag.Notizen = value; }
        }

        #endregion
    }
}
