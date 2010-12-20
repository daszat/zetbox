
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
    /// This class contains all relevant <see cref="ICommandViewModel"/> implementations.
    /// </content>
    public sealed partial class WorkEffortRecorderModel
    {
        #region StartNewWorkEffort command

        /// <summary>The backing store for the <see cref="StartNewWorkEffort"/> property.</summary>
        private StartNewWorkEffortCommand _startNewWorkEffortCommand;

        /// <summary>
        /// Gets a command to start a new <see cref="WorkEffort"/>.
        /// </summary>
        public ICommandViewModel StartNewWorkEffort
        {
            get
            {
                if (_startNewWorkEffortCommand == null)
                {
                    _startNewWorkEffortCommand = ViewModelFactory.CreateViewModel<StartNewWorkEffortCommand.Factory>().Invoke(DataContext, this);
                }
                return _startNewWorkEffortCommand;
            }
        }

        /// <summary>
        /// This <see cref="ICommandViewModel"/> starts a new work effort for the current user, if there is none currently active.
        /// </summary>
        private class StartNewWorkEffortCommand : CommandViewModel
        {
#if MONO
            // See https://bugzilla.novell.com/show_bug.cgi?id=660553
            public delegate StartNewWorkEffortCommand Factory(IKistlContext dataCtx, WorkEffortRecorderModel parent);
#else
            public new delegate StartNewWorkEffortCommand Factory(IKistlContext dataCtx, WorkEffortRecorderModel parent);
#endif

            /// <summary>The <see cref="WorkEffortRecorderModel"/> to work on.</summary>
            private WorkEffortRecorderModel _parent;

            /// <summary>
            /// Initializes a new instance of the StartNewWorkEffortCommand class.
            /// </summary>
            /// <param name="appCtx">the application context to use</param>
            /// <param name="dataCtx">the data context to use</param>
            /// <param name="parent">which <see cref="WorkEffortRecorderModel"/> to work on</param>
            public StartNewWorkEffortCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, WorkEffortRecorderModel parent)
                : base(appCtx, dataCtx, "New Work Effort", "Create a new work effort")
            {
                if (parent == null)
                {
                    throw new ArgumentNullException("parent");
                }
                _parent = parent;
                _parent.PropertyChanged += (obj, args) =>
                {
                    switch (args.PropertyName)
                    {
                        case "CurrentUser":
                        case "IsCurrentlyPresent":
                        case "CurrentEffort":
                            OnCanExecuteChanged();
                            break;
                    }
                };
            }

            /// <summary>
            /// Whether or not this Command is applicable to the current state.
            /// </summary>
            /// <param name="data">may be <value>null</value> if no data is expected</param>
            /// <returns>true if the command can execute with this <paramref name="data"/></returns>
            public override bool CanExecute(object data)
            {
                return _parent.CurrentUser != null && _parent.IsCurrentlyPresent && _parent.CurrentEffort == null;
            }

            /// <summary>
            /// Execute this Command
            /// </summary>
            /// <param name="data">the data to process</param>
            protected override void DoExecute(object data)
            {
                WorkEffort effort = DataContext.Create<WorkEffort>();
                WorkEffortModel effortModel = ViewModelFactory.CreateViewModel<WorkEffortModel.Factory>(effort).Invoke(DataContext, effort);
                effortModel.Mitarbeiter = _parent.CurrentUser;
                effortModel.From = DateTime.Now;
                _parent.InitialiseEfforts();
                _parent._efforts.Add(effortModel);
                _parent.SelectedEffort = effortModel;
                DataContext.SubmitChanges();
            }
        }

        #endregion

        #region FinishWorkEffort command

        /// <summary>The backing store for the <see cref="FinishWorkEffort"/> property.</summary>
        private FinishWorkEffortCommand _FinishWorkEffortCommand;

        /// <summary>
        /// Gets a command to finish the currently running work effort.
        /// </summary>
        public ICommandViewModel FinishWorkEffort
        {
            get
            {
                if (_FinishWorkEffortCommand == null)
                {
                    _FinishWorkEffortCommand = ViewModelFactory.CreateViewModel<FinishWorkEffortCommand.Factory>().Invoke(DataContext, this);
                }
                return _FinishWorkEffortCommand;
            }
        }

        /// <summary>
        /// This <see cref="ICommandViewModel"/> takes care of closing any open work efforts of the current user and starts a new one.
        /// </summary>
        private class FinishWorkEffortCommand : CommandViewModel
        {
#if MONO
            // See https://bugzilla.novell.com/show_bug.cgi?id=660553
            public delegate FinishWorkEffortCommand Factory(IKistlContext dataCtx, WorkEffortRecorderModel parent);
#else
            public new delegate FinishWorkEffortCommand Factory(IKistlContext dataCtx, WorkEffortRecorderModel parent);
#endif

            /// <summary>The <see cref="WorkEffortRecorderModel"/> to work on.</summary>
            private WorkEffortRecorderModel _parent;

            /// <summary>
            /// Initializes a new instance of the FinishWorkEffortCommand class.
            /// </summary>
            /// <param name="appCtx">the application context to use</param>
            /// <param name="dataCtx">the data context to use</param>
            /// <param name="parent">which <see cref="WorkEffortRecorderModel"/> to work on</param>
            public FinishWorkEffortCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, WorkEffortRecorderModel parent)
                : base(appCtx, dataCtx, "Finish", "Stops and closes this work effort")
            {
                if (parent == null)
                {
                    throw new ArgumentNullException("parent");
                }
                _parent = parent;
                _parent.PropertyChanged += (obj, args) =>
                {
                    switch (args.PropertyName)
                    {
                        case "CurrentUser":
                        case "IsCurrentlyPresent":
                        case "CurrentEffort":
                            OnCanExecuteChanged();
                            break;
                    }
                };
            }

            /// <summary>
            /// Whether or not this Command is applicable to the current state.
            /// </summary>
            /// <param name="data">may be <value>null</value> if no data is expected</param>
            /// <returns>true if the command can execute with this <paramref name="data"/></returns>
            public override bool CanExecute(object data)
            {
                return _parent.CurrentUser != null && _parent.IsCurrentlyPresent && _parent.CurrentEffort != null;
            }

            /// <summary>
            /// Execute this Command
            /// </summary>
            /// <param name="data">the data to process</param>
            protected override void DoExecute(object data)
            {
                WorkEffortModel effortModel = _parent.CurrentEffort;
                effortModel.Thru = DateTime.Now;
                DataContext.SubmitChanges();
                _parent.ReloadEfforts();
            }
        }

        #endregion
    }
}