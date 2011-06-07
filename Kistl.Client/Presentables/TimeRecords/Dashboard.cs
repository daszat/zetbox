

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
        public new delegate Dashboard Factory(IKistlContext dataCtx, ViewModel parent);

        /// <summary>
        /// Initializes a new instance of the Dashboard class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">Parent ViewModel</param>
        public Dashboard(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        private OpenRecorderCommand _openRecorderCommand;
        public ICommandViewModel OpenRecorderCommand
        {
            get
            {
                if (_openRecorderCommand == null)
                {
                    _openRecorderCommand = ViewModelFactory.CreateViewModel<OpenRecorderCommand.Factory>().Invoke(DataContext, this);
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
        : CommandViewModel
    {
        public new delegate OpenRecorderCommand Factory(IKistlContext dataCtx, ViewModel parent);

        private readonly Func<IKistlContext> ctxFactory;

        public OpenRecorderCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, parent, "Start recording", "Start recording")
        {
            this.ctxFactory = ctxFactory;
        }

        public override bool CanExecute(object data)
        {
            return data == null;
        }

        protected override void DoExecute(object data)
        {
            var initialWorkspace = ViewModelFactory.CreateViewModel<Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel.Factory>().Invoke(ctxFactory(), null);
            ViewModelFactory.ShowModel(initialWorkspace, true);
        }
    }

}
