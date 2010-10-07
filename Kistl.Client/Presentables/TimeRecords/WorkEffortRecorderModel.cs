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
    /// This WorkspaceViewModel implements the use case of capturing fine grained
    /// work effort information "on the go" while employees are working on their PC.
    public sealed partial class WorkEffortRecorderModel
        : WindowViewModel
    {
        /// <summary>
        /// Initializes a new instance of the WorkEffortRecorderModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        public WorkEffortRecorderModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
        }

        public override string Name
        {
            get
            {
                return "WorkEffortRecorderModel";
            }
        }

        #region The currently logging user

        /// <summary>The backing store for the <see cref="CurrentUser"/> property.</summary>
        private DataObjectViewModel _currentUser;

        /// <summary>Gets or sets the currently working user.</summary>
        /// TODO: This will have to change once there is ACL support
        public DataObjectViewModel CurrentUser
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
                    ReloadPresence();
                    ReloadPresenceRecords();
                    ReloadEfforts();
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        #endregion

        #region List of Available Users

        /// <summary>The backing store for the <see cref="AvailableUsers"/> property.</summary>
        private ReadOnlyCollection<DataObjectViewModel> _availableUsers;

        /// <summary>Gets a list of users that may record work efforts.</summary>
        public ReadOnlyCollection<DataObjectViewModel> AvailableUsers
        {
            get
            {
                if (_availableUsers == null)
                {
                    _availableUsers = new ReadOnlyCollection<DataObjectViewModel>(DataContext.GetQuery<Mitarbeiter>().Select(o => ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>(o).Invoke(DataContext, o)).ToList());
                }
                return _availableUsers;
            }
        }

        #endregion

        #region IsCurrentlyPresent

        private void ReloadPresence()
        {
            var currentPresence = DataContext.GetQuery<PresenceRecord>()
                .Where(pr => pr.Mitarbeiter.ID == CurrentUser.ID && !pr.Thru.HasValue)
                .SingleOrDefault();

            _isCurrentlyPresent = (currentPresence != null);
            OnPropertyChanged("IsCurrentlyPresent");
        }

        /// <summary>The backing store for the <see cref="IsCurrentlyPresent"/> property.</summary>
        private bool _isCurrentlyPresent = false;

        /// <summary>Gets or sets a value indicating whether the <see cref="CurrentUser"/> is currently on work time or not. Can only be set if there is a CurrentUser.</summary>
        public bool IsCurrentlyPresent
        {
            get
            {
                return _isCurrentlyPresent;
            }
            set
            {
                if (_isCurrentlyPresent != value && CurrentUser != null)
                {
                    _isCurrentlyPresent = value;

                    if (_isCurrentlyPresent)
                    {
                        PresenceRecord nowPresent = DataContext.Create<PresenceRecord>();
                        nowPresent.From = DateTime.Now;
                        nowPresent.Mitarbeiter = (Mitarbeiter)CurrentUser.Object;
                        InitialisePresenceRecords();
                        _presenceRecords.Add(ViewModelFactory.CreateViewModel<PresenceRecordModel.Factory>(nowPresent).Invoke(DataContext, nowPresent));
                        DataContext.SubmitChanges();
                    }
                    else
                    {
                        var closingDate = DateTime.Now;
                        if (CurrentEffort != null)
                        {
                            CurrentEffort.Thru = closingDate;
                        }
                        PresenceRecord nowPresent = PresenceRecords.Select(mdl => mdl.Entry).LastOrDefault(rec => rec.Thru == null);
                        if (nowPresent != null)
                        {
                            nowPresent.Thru = closingDate;
                        }
                    }
                }
                OnPropertyChanged("IsCurrentlyPresent");
            }
        }

        #endregion

        #region List of work efforts

        /// <summary>The backing store of the <see cref="Efforts"/> property.</summary>
        private ObservableCollection<WorkEffortModel> _efforts;

        /// <summary>The read-only proxy for the backing store of the <see cref="Efforts"/> property.</summary>
        private ReadOnlyObservableCollection<WorkEffortModel> _readOnlyEfforts;

        /// <summary>Gets a list of recorded <see cref="WorkEffort"/> of the <see cref="CurrentUser"/>.</summary>
        public ReadOnlyObservableCollection<WorkEffortModel> Efforts
        {
            get
            {
                InitialiseEfforts();
                return _readOnlyEfforts;
            }
        }

        /// <summary>
        /// Ensure that the backing stores for <see cref="Efforts"/> are initialised.
        /// </summary>
        private void InitialiseEfforts()
        {
            if (_efforts == null)
            {
                _efforts = new ObservableCollection<WorkEffortModel>();

                // quick hack to update CurrentEffort
                // TODO: could be improved to only trigger if CurrentEffort _really_ changes
                _efforts.CollectionChanged += (obj, args) =>
                {
                    OnPropertyChanged("CurrentEffort");
                };

                _readOnlyEfforts = new ReadOnlyObservableCollection<WorkEffortModel>(_efforts);

                ReloadEfforts();
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
                    .Where(o => o.Mitarbeiter.ID == CurrentUser.ID && (o.From > DateTime.Today || !o.Thru.HasValue || (o.Thru.HasValue && o.Thru.Value > DateTime.Today)))
                    .Select(o => ViewModelFactory.CreateViewModel<WorkEffortModel.Factory>(o).Invoke(DataContext, o));

                foreach (var wem in effortModels)
                {
                    _efforts.Add(wem);
                }
            }
            SelectedEffort = CurrentEffort ?? _efforts.LastOrDefault();
            OnPropertyChanged("CurrentEffort");
        }

        /// <summary>Gets the currently open <see cref="WorkEffort"/>.</summary>
        public WorkEffortModel CurrentEffort
        {
            get
            {
                if (Efforts == null)
                {
                    return null;
                }

                if (Efforts.Count > 0)
                {
                    WorkEffortModel currentEffort = Efforts[Efforts.Count - 1];
                    if (currentEffort.Thru == null)
                    {
                        return currentEffort;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The backing store for the <see cref="SelectedEffort"/> property.
        /// </summary>
        private WorkEffortModel _selectedEffort;

        /// <summary>Gets the currently selected <see cref="WorkEffort"/>.</summary>
        public WorkEffortModel SelectedEffort
        {
            get
            {
                return _selectedEffort;
            }
            set
            {
                if (_selectedEffort != value)
                {
                    _selectedEffort = value;
                    DataContext.SubmitChanges();
                    OnPropertyChanged("SelectedEffort");
                }
            }
        }

        #endregion

        #region List of PresenceRecords

        /// <summary>The backing store for the <see cref="PresenceRecords"/> property.</summary>
        private ObservableCollection<PresenceRecordModel> _presenceRecords;

        /// <summary>The read-only proxy for the backing store of the <see cref="PresenceRecords"/> property.</summary>
        private ReadOnlyObservableCollection<PresenceRecordModel> _readOnlyPresenceRecords;

        /// <summary>Gets a list of recorded <see cref="PresenceRecord"/> of the <see cref="CurrentUser"/>.</summary>
        public ReadOnlyObservableCollection<PresenceRecordModel> PresenceRecords
        {
            get
            {
                InitialisePresenceRecords();
                return _readOnlyPresenceRecords;
            }
        }

        /// <summary>
        /// Ensure that the backing stores for <see cref="PresenceRecords"/> are initialised.
        /// </summary>
        private void InitialisePresenceRecords()
        {
            if (_presenceRecords == null)
            {
                _presenceRecords = new ObservableCollection<PresenceRecordModel>();
                ReloadPresenceRecords();
                _readOnlyPresenceRecords = new ReadOnlyObservableCollection<PresenceRecordModel>(_presenceRecords);
            }
        }

        /// <summary>
        /// Reload the <see cref="PresenceRecords"/> collection according to the <see cref="CurrentUser"/>. If no user is set, the PresenceRecords collection is cleared;
        /// </summary>
        private void ReloadPresenceRecords()
        {
            _presenceRecords.Clear();
            if (CurrentUser != null)
            {
                var recordModels = DataContext.GetQuery<PresenceRecord>()
                    .Where(o => o.Mitarbeiter.ID == CurrentUser.ID && (o.From > DateTime.Today || !o.Thru.HasValue || (o.Thru.HasValue && o.Thru.Value > DateTime.Today)))
                    .OrderBy(o => o.From);

                foreach (var pr in recordModels)
                {
                    _presenceRecords.Add(ViewModelFactory.CreateViewModel<PresenceRecordModel.Factory>(pr).Invoke(DataContext, pr));
                }
            }
        }

        #endregion

        #region Summary Values

        /// <summary>
        /// Indicates whether the timer to update the TotalWorkTimeToday time display is started.
        /// </summary>
        private bool _totalWorkTimeTodayTimerStarted = false;

        /// <summary>Gets the total time (in hours) the <see cref="CurrentUser"/> has worked today.</summary>
        public double TotalWorkTimeToday
        {
            get
            {
                if (!_totalWorkTimeTodayTimerStarted)
                {
                    // set flag before starting the timer to avoid any potential races
                    _totalWorkTimeTodayTimerStarted = true;
                    this.ViewModelFactory.CreateTimer(TimeSpan.FromMilliseconds(300), () => OnPropertyChanged("TotalWorkTimeToday"));
                }
                return Efforts
                    .Where(e => e.From.Date == DateTime.Now.Date)
                    .Sum(e => ((e.Thru ?? DateTime.Now) - e.From).TotalHours);
            }
        }

        /// <summary>
        /// Indicates whether the timer to update the TotalWorkTimeToday time display is started.
        /// </summary>
        private bool _totalPresenceTimeTodayTimerStarted = false;

        /// <summary>Gets the total time (in hours) the <see cref="CurrentUser"/> was present today.</summary>
        public double TotalPresenceTimeToday
        {
            get
            {
                if (!_totalPresenceTimeTodayTimerStarted)
                {
                    // set flag before starting the timer to avoid any potential races
                    _totalPresenceTimeTodayTimerStarted = true;
                    this.ViewModelFactory.CreateTimer(TimeSpan.FromMilliseconds(300), () => OnPropertyChanged("TotalPresenceTimeToday"));
                }
                return PresenceRecords
                    .Select(mdl => mdl.Entry)
                    .Where(e => e.From.Date == DateTime.Now.Date)
                    .Sum(e => ((e.Thru ?? DateTime.Now) - e.From).TotalHours);
            }
        }

        /// <summary>Gets the time (in hours) since the last break of the <see cref="CurrentUser"/>.</summary>
        public float TimeSinceLastBreak { get; private set; }

        #endregion

    }
}
