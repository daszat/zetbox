
namespace Kistl.Client.Presentables.TimeRecords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Projekte;
    using Kistl.App.TimeRecords;
    using Kistl.API.Configuration;

    /// <summary>
    /// A <see cref="DataObjectViewModel"/> for <see cref="WorkEffort"/>.
    /// </summary>
    public class WorkEffortModel
        : DataObjectViewModel
    {
        public new delegate WorkEffortModel Factory(IKistlContext dataCtx, WorkEffort obj);

        /// <summary>The presented <see cref="WorkEffort"/></summary>
        private WorkEffort _entry;

        /// <summary>
        /// Initializes a new instance of the WorkEffortModel class.
        /// </summary>
        /// <param name="appCtx">which GuiApplicationContext to use</param>
        /// <param name="dataCtx">which <see cref="IKistlContext"/> to use</param>
        /// <param name="obj">the modelled <see cref="WorkEffort"/></param>
        /// <param name="config"></param>
        public WorkEffortModel(
            IViewModelDependencies appCtx, KistlConfig config,
            IKistlContext dataCtx,
            WorkEffort obj)
            : base(appCtx, config, dataCtx, obj)
        {
            this._entry = obj;
            this._entry.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case "Name":
                    case "Notes":
                    case "From":
                    case "Thru":
                    case "Mitarbeiter":
                        OnPropertyChanged(args.PropertyName);
                        break;
                }
            };
        }

        #region WorkEffort Members

        /// <summary>Gets or sets the "From"s value of the underlying WorkEffort</summary>
        public DateTime From
        {
            get { return this._entry.From; }
            set { this._entry.From = value; }
        }

        /// <summary>Gets or sets the "Name"s value of the underlying WorkEffort</summary>
        public new string Name
        {
            get { return this._entry.Name; }
            set { this._entry.Name = value; }
        }

        /// <summary>Gets or sets the "Thru"s value of the underlying WorkEffort</summary>
        public DateTime? Thru
        {
            get { return this._entry.Thru; }
            set { this._entry.Thru = value; }
        }

        /// <summary>Gets or sets the "Mitarbeiter"s value of the underlying WorkEffort, wrapped in a DataObjectViewModel</summary>
        public DataObjectViewModel Mitarbeiter
        {
            get
            {
                return this.ModelFactory.CreateViewModel<DataObjectViewModel.Factory>(this._entry.Mitarbeiter).Invoke(this.DataContext, this._entry.Mitarbeiter);
            }
            set
            {
                if (value == null)
                {
                    this._entry.Mitarbeiter = null;
                }
                else
                {
                    this._entry.Mitarbeiter = (Mitarbeiter)value.Object;
                }
            }
        }

        /// <summary>Gets or sets the "Notizen"s value of the underlying WorkEffort</summary>
        public string Notes
        {
            get { return this._entry.Notes; }
            set { this._entry.Notes = value; }
        }

        #endregion
    }
}
