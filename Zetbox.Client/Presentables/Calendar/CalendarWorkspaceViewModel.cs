namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.App.Base;
    using cal = Zetbox.App.Calendar;

    public class CalendarSelectionViewModel : ViewModel
    {
        public new delegate CalendarSelectionViewModel Factory(IZetboxContext dataCtx, Zetbox.Client.Presentables.ViewModel parent, cal.Calendar calendar, bool isSelf);

        public CalendarSelectionViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, Zetbox.Client.Presentables.ViewModel parent, cal.Calendar calendar, bool isSelf)
            : base(appCtx, dataCtx, parent)
        {
            if (calendar == null) throw new ArgumentNullException("calendar");

            this.Calendar = calendar;
            this.CalendarViewModel = DataObjectViewModel.Fetch(ViewModelFactory, dataCtx, parent, calendar);
            this.Selected = isSelf;
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
    public class CalendarWorkspaceViewModel : WindowViewModel
    {
        public new delegate CalendarWorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

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
                    _Items = DataContext.GetQuery<cal.Calendar>()
                        .OrderBy(i => i.Name)
                        .ToList()
                        .Select(i => ViewModelFactory.CreateViewModel<CalendarSelectionViewModel.Factory>().Invoke(DataContext, this, i, i.Owner == CurrentIdentity))
                        .ToList();
                    SelectedItem = _Items.FirstOrDefault(i => i.IsSelf);
                }
                return _Items;
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

        #region Calendar
        private WeekCalendarViewModel _WeekCalender = null;
        public WeekCalendarViewModel WeekCalender
        {
            get
            {
                if (_WeekCalender == null)
                {
                    _WeekCalender = ViewModelFactory.CreateViewModel<WeekCalendarViewModel.Factory>()
                        .Invoke(DataContext, this, null);
                    _WeekCalender.NewItemCreating += (dt, e) =>
                    {
                    };
                    _WeekCalender.Refresh();
                }
                return _WeekCalender;
            }
        }
        #endregion
    }
}
