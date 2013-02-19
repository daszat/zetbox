namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using cal = Zetbox.App.Calendar;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.App.GUI;
    using Zetbox.API.Utils;

    public class CalendarSelectionViewModel : ViewModel
    {
        public new delegate CalendarSelectionViewModel Factory(IZetboxContext dataCtx, Zetbox.Client.Presentables.ViewModel parent, cal.Calendar calendar, bool isSelf);

        public CalendarSelectionViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, Zetbox.Client.Presentables.ViewModel parent, cal.Calendar calendar, bool isSelf)
            : base(appCtx, dataCtx, parent)
        {
            if (calendar == null) throw new ArgumentNullException("calendar");

            this.Calendar = calendar;
            this.CalendarViewModel = DataObjectViewModel.Fetch(ViewModelFactory, dataCtx, parent, calendar);
            this._Selected = isSelf;
            this.IsSelf = isSelf;
            this.Color = isSelf ? "#F1F5E3" : null;
        }

        public cal.Calendar Calendar { get; private set; }
        public DataObjectViewModel CalendarViewModel { get; private set; }
        public bool IsSelf { get; private set; }

        private bool _Selected = false;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (_Selected != value)
                {
                    _Selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        private string _Color;
        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                    OnPropertyChanged("Color");
                }
            }
        }

        public override string Name
        {
            get { return CalendarViewModel.Name; }
        }
    }

    [ViewModelDescriptor]
    public class CalendarWorkspaceViewModel : WindowViewModel, IDeleteCommandParameter, INewCommandParameter
    {
        public new delegate CalendarWorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public static string[] Colors = new[] { 
            "#FFAAAA",
            "#AAFFAA",
            "#AAAAFF",
            "#FFFFAA",
            "#AAFFFF",
            "#FFAAFF",
        };
        private bool _shouldUpdateCalendarItems = true;

        public CalendarWorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "Calendar Workspace"; }
        }

        #region Items
        private IEnumerable<CalendarSelectionViewModel> _Items = null;
        public IEnumerable<CalendarSelectionViewModel> Items
        {
            get
            {
                if (_Items == null)
                {
                    var myID = CurrentIdentity != null ? CurrentIdentity.ID : 0;
                    _Items = DataContext.GetQuery<cal.Calendar>()
                        .OrderBy(i => i.Name)
                        .ToList()
                        .Select(i =>
                        {
                            var mdl = ViewModelFactory.CreateViewModel<CalendarSelectionViewModel.Factory>().Invoke(DataContext, this, i, i.Owner != null ? i.Owner.ID == myID : false);
                            mdl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                            return mdl;
                        })
                        .ToList();
                    SelectedItem = _Items.FirstOrDefault(i => i.IsSelf);
                }
                return _Items;
            }
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                var obj = (CalendarSelectionViewModel)sender;
                if (!obj.IsSelf)
                {
                    if (obj.Selected)
                    {
                        obj.Color = Colors[Items.Count(i => i.Selected) % Colors.Length];
                    }
                    else
                    {
                        obj.Color = null;
                    }
                }
                if (_shouldUpdateCalendarItems)
                {
                    _WeekCalender.Refresh();
                }
            }
        }

        private CalendarSelectionViewModel _selectedItem;
        public CalendarSelectionViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        #endregion

        #region Commands

        private NewDataObjectCommand _NewCommand;
        public ICommandViewModel NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(
                        DataContext,
                        this,
                        typeof(cal.Event).GetObjectClass(FrozenContext));
                }
                return _NewCommand;
            }
        }

        public void New()
        {
            if (NewCommand.CanExecute(null))
                NewCommand.Execute(null);
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            if (DeleteCommand.CanExecute(null))
                DeleteCommand.Execute(null);
        }

        private ICommandViewModel _SelectAllCommand = null;
        public ICommandViewModel SelectAllCommand
        {
            get
            {
                if (_SelectAllCommand == null)
                {
                    _SelectAllCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Alle", "Alle auswählen", SelectAll, null, null);
                }
                return _SelectAllCommand;
            }
        }

        public void SelectAll()
        {
            _shouldUpdateCalendarItems = false;
            try
            {
                foreach (var p in Items)
                {
                    p.Selected = true;
                }
            }
            finally
            {
                _shouldUpdateCalendarItems = true;
                _WeekCalender.Refresh();
            }
        }

        private ICommandViewModel _ClearAllCommand = null;
        public ICommandViewModel ClearAllCommand
        {
            get
            {
                if (_ClearAllCommand == null)
                {
                    _ClearAllCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Nur selbst", "Nur sich selbst auswählen", ClearAll, null, null);
                }
                return _ClearAllCommand;
            }
        }

        public void ClearAll()
        {
            _shouldUpdateCalendarItems = false;
            try
            {
                foreach (var p in Items)
                {
                    if (!p.IsSelf)
                        p.Selected = false;
                }
            }
            finally
            {
                _shouldUpdateCalendarItems = true;
                _WeekCalender.Refresh();
            }
        }
        #endregion

        #region Calendar
        private WeekCalendarViewModel _WeekCalender = null;
        public WeekCalendarViewModel WeekCalender
        {
            get
            {
                if (_WeekCalender == null)
                {
                    _WeekCalender = ViewModelFactory.CreateViewModel<WeekCalendarViewModel.Factory>()
                        .Invoke(DataContext, this, FetchEvents);
                    _WeekCalender.PropertyChanged += _WeekCalender_PropertyChanged;
                    _WeekCalender.NewEventCreating += (dt, e) =>
                    {
                    };
                    _WeekCalender.Refresh();
                }
                return _WeekCalender;
            }
        }
        void _WeekCalender_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                // Me too
                OnPropertyChanged("SelectedItems");
            }
        }
        private IEnumerable<EventViewModel> FetchEvents(DateTime from, DateTime to)
        {
            using (Logging.Client.InfoTraceMethodCall("CalendarViewModel.GetData()"))
            {
                var calendars = Items.Where(i => i.Selected).Select(i => i.Calendar.ID).ToArray();
                
                var predicateCalendars = LinqExtensions.False<cal.Event>();
                foreach (var id in calendars)
                {
                    var localID = id;
                    predicateCalendars = predicateCalendars.OrElse<cal.Event>(i => i.Calendar.ID == localID);
                }

                var events = DataContext.GetQuery<cal.Event>()
                    .Where(predicateCalendars)
                    .Where(e => (e.StartDate >= from && e.StartDate <= to) || (e.EndDate >= from && e.EndDate <= to) || (e.StartDate <= from && e.EndDate >= to))
                    .ToList();

                var result = new List<EventViewModel>();

                result.AddRange(events.Select(obj =>
                {
                    var vmdl = (EventViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, WeekCalender, obj);
                    // Color ?
                    return vmdl;
                }));

                return result;
            }
        }
        #endregion

        public bool IsReadOnly { get { return false; } }
        bool IDeleteCommandParameter.AllowDelete { get { return true; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems
        {
            get { return new ViewModel[] { (ViewModel)WeekCalender.SelectedItem }; /* return selected events! */ }
        }
        bool INewCommandParameter.AllowAddNew { get { return true; } }
    }
}
