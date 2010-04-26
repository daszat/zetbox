

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
        : ViewModel
    {
        public new delegate Dashboard Factory(IKistlContext dataCtx);

        private IModelFactory mdlFactory;

        /// <summary>
        /// Initializes a new instance of the Dashboard class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="mdlFactory"></param>
        public Dashboard(IGuiApplicationContext appCtx, IKistlContext dataCtx, IModelFactory mdlFactory)
            : base(appCtx, dataCtx)
        {
            this.mdlFactory = mdlFactory;
        }

        private OpenRecorderCommand _openRecorderCommand;
        public ICommand OpenRecorderCommand
        {
            get
            {
                if (_openRecorderCommand == null)
                {
                    _openRecorderCommand = mdlFactory.CreateViewModel<OpenRecorderCommand.Factory>().Invoke(DataContext);
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
        public new delegate OpenRecorderCommand Factory(IKistlContext dataCtx);

        private readonly Func<IKistlContext> ctxFactory;

        public OpenRecorderCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, "Start recording", "Start recording")
        {
            this.ctxFactory = ctxFactory;
        }

        public override bool CanExecute(object data)
        {
            return data == null;
        }

        protected override void DoExecute(object data)
        {
            var initialWorkspace = AppContext.Factory.CreateViewModel<Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel.Factory>().Invoke(ctxFactory());
            AppContext.Factory.ShowModel(initialWorkspace, true);
        }
    }

}
