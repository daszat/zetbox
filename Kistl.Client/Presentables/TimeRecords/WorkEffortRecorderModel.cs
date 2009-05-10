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
    using Kistl.App.Projekte;
    using Kistl.App.TimeRecords;

    /// <summary>
    /// Main workspace presenter of the TimeRecords module for recording work efforts.
    /// </summary>
    /// This <see cref="WorkspaceModel"/> implements the use case of capturing fine grained
    /// work effort information "on the go" while employees are working on their PC.
    public sealed class WorkEffortRecorderModel
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
        }

        #region The currently logging user

        /// <summary>The backing store for the <see cref="CurrentUser"/> property.</summary>
        private DataObjectModel _currentUser;

        /// <summary>Gets or sets the currently working user.</summary>
        /// TODO: This will have to change once there is ACL support
        public DataObjectModel CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    ReloadEfforts();
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        #endregion

        #region List of Available Users

        /// <summary>The backing store for the <see cref="AvailableUsers"/> property.</summary>
        private ReadOnlyCollection<DataObjectModel> _availableUsers;

        /// <summary>Gets a list of users that may record work efforts.</summary>
        public ReadOnlyCollection<DataObjectModel> AvailableUsers
        {
            get
            {
                if (_availableUsers == null)
                {
                    _availableUsers = new ReadOnlyCollection<DataObjectModel>(DataContext.GetQuery<Mitarbeiter>().Select(o => (DataObjectModel)Factory.CreateDefaultModel(DataContext, o)).ToList());
                }
                return _availableUsers;
            }
        }

        #endregion

        #region IsCurrentlyWorking

        /// <summary>The backing store for the <see cref="IsCurrentlyWorking"/> property.</summary>
        private bool _isCurrentlyWorking = false;

        /// <summary>Gets or sets a value indicating whether the <see cref="CurrentUser"/> is currently on work time or not.</summary>
        public bool IsCurrentlyWorking
        {
            get
            {
                return _isCurrentlyWorking;
            }
            set
            {
                if (_isCurrentlyWorking != value)
                {
                    _isCurrentlyWorking = value;
                    OnPropertyChanged("IsCurrentlyWorking");
                }
            }
        }

        #endregion

        #region List of work efforts

        /// <summary>The backing store for the <see cref="Efforts"/> property.</summary>
        private ObservableCollection<WorkEffortModel> _efforts;

        /// <summary>Gets a list of recorded <see cref="WorkEffort"/> of the <see cref="CurrentUser"/>.</summary>
        public ObservableCollection<WorkEffortModel> Efforts
        {
            get
            {
                if (_efforts == null)
                {
                    _efforts = new ObservableCollection<WorkEffortModel>();

                    // quick hack to update CurrentEffort, could be improved to only trigger if CurrentEffort _really_ changes
                    _efforts.CollectionChanged += (obj, args) =>
                    {
                        OnPropertyChanged("CurrentEffort");
                    };

                    ReloadEfforts();
                }
                return _efforts;
            }
        }

        /// <summary>
        /// Reload the <see cref="Efforts"/> collection according to the <see cref="CurrentUser"/>. If no user is set, the Efforts collection is cleared;
        /// </summary>
        private void ReloadEfforts()
        {
            _efforts.Clear();
            if (CurrentUser != null)
            {
                var effortModels = DataContext.GetQuery<WorkEffort>()
                    .Where(o => o.Mitarbeiter.ID == CurrentUser.ID)
                    .Select(o => (WorkEffortModel)Factory.CreateDefaultModel(DataContext, o));

                foreach (var wem in effortModels)
                {
                    _efforts.Add(wem);
                }
            }
            OnPropertyChanged("CurrentEffort");
        }

        /// <summary>Gets the currently open <see cref="WorkEffort"/>.</summary>
        public WorkEffortModel CurrentEffort
        {
            get
            {
                if (Efforts.Count > 0)
                {
                    return Efforts[Efforts.Count - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Summary Values

        /// <summary>
        /// Indicates whether the timer to update the various ticking time displays already was started.
        /// </summary>
        private bool _sumTimerStarted = false;

        /// <summary>Gets the total time (in hours) the <see cref="CurrentUser"/> has worked today.</summary>
        public double TotalWorkTimeToday
        {
            get
            {
                if (!_sumTimerStarted)
                {
                    // set flag before starting the timer to avoid any potential races
                    _sumTimerStarted = true;
                    this.Factory.CreateTimer(TimeSpan.FromMilliseconds(300), () => OnPropertyChanged("TotalWorkTimeToday"));
                }
                return Efforts.Sum(e => ((e.Thru ?? DateTime.Now) - e.From).TotalHours);
            }
        }

        /// <summary>Gets the time (in hours) since the last break of the <see cref="CurrentUser"/>.</summary>
        public float TimeSinceLastBreak { get; private set; }

        #endregion

        #region Commands

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
                _parent = parent;
            }

            /// <summary>
            /// Whether or not this Command is applicable to the current state.
            /// </summary>
            /// <param name="data">may be <value>null</value> if no data is expected</param>
            /// <returns>true if the command can execute with this <paramref name="data"/></returns>
            public override bool CanExecute(object data)
            {
                throw new NotImplementedException();
            }

            protected override void DoExecute(object data)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
