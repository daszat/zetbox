
namespace Kistl.Client.Presentables.TimeRecords
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Projekte;
    using Kistl.App.TimeRecords;

    /// <content>
    /// This class contains all relevant <see cref="ICommand"/> implementations.
    /// </content>
    public sealed partial class WorkEffortRecorderModel
    {
        #region StartNewWorkEffort command

        /// <summary>The backing store for the <see cref="StartNewWorkEffort"/> property.</summary>
        private StartNewWorkEffortCommand _startNewWorkEffortCommand;

        /// <summary>
        /// Gets a command to start a new work effort. If there is currently a <see cref="WorkEffort"/> open, it'll be closed.
        /// </summary>
        public ICommand StartNewWorkEffort
        {
            get
            {
                if (_startNewWorkEffortCommand == null)
                {
                    _startNewWorkEffortCommand = new StartNewWorkEffortCommand(AppContext, DataContext, this);
                }
                return _startNewWorkEffortCommand;
            }
        }

        /// <summary>
        /// This <see cref="ICommand"/> takes care of closing any open work efforts of the current user and starts a new one.
        /// </summary>
        private class StartNewWorkEffortCommand : CommandModel
        {
            /// <summary>The <see cref="WorkEffortRecorderModel"/> to work on.</summary>
            private WorkEffortRecorderModel _parent;

            /// <summary>
            /// Initializes a new instance of the StartNewWorkEffortCommand class.
            /// </summary>
            /// <param name="appCtx">the application context to use</param>
            /// <param name="dataCtx">the data context to use</param>
            /// <param name="parent">which <see cref="WorkEffortRecorderModel"/> to work on</param>
            public StartNewWorkEffortCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx, WorkEffortRecorderModel parent)
                : base(appCtx, dataCtx)
            {
                if (parent == null)
                {
                    throw new ArgumentNullException("parent");
                }
                _parent = parent;
            }

            /// <summary>
            /// Whether or not this Command is applicable to the current state.
            /// </summary>
            /// <param name="data">may be <value>null</value> if no data is expected</param>
            /// <returns>true if the command can execute with this <paramref name="data"/></returns>
            public override bool CanExecute(object data)
            {
                return _parent.CurrentUser != null && _parent.IsCurrentlyPresent;
            }

            /// <summary>
            /// Execute this Command
            /// </summary>
            /// <param name="data">the data to process</param>
            protected override void DoExecute(object data)
            {
            }
        }

        #endregion
    }
}