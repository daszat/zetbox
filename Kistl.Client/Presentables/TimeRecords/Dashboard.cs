

namespace Kistl.Client.Presentables.TimeRecords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;

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

        private OpenRecorderCommand _openRecorderCommand;
        public ICommand OpenRecorderCommand
        {
            get
            {
                if (_openRecorderCommand == null)
                {
                    _openRecorderCommand = new OpenRecorderCommand(AppContext, DataContext);
                }
                return _openRecorderCommand;
            }
        }

        public override string Name
        {
            get { return "Timerecord's Dashboard"; }
        }
    }

    internal class OpenRecorderCommand
        : CommandModel
    {
        internal OpenRecorderCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx, "Start recording")
        {
        }

        public override bool CanExecute(object data)
        {
            return data == null;
        }

        protected override void DoExecute(object data)
        {
            var initialWorkspace = AppContext.Factory.CreateSpecificModel<Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel>(KistlContext.GetContext());
            AppContext.Factory.ShowModel(initialWorkspace, true);
        }
    }

}
