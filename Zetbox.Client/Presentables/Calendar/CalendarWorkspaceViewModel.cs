// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;
    using cal = Zetbox.App.Calendar;
    using Zetbox.Client.Reporting;
    using Zetbox.Client.GUI;

    #region CalendarSelectionViewModel
    public class CalendarSelectionViewModel : ViewModel
    {
        public new delegate CalendarSelectionViewModel Factory(IZetboxContext dataCtx, Zetbox.Client.Presentables.ViewModel parent, cal.Calendar calendar, bool isFavorite);

        public CalendarSelectionViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, Zetbox.Client.Presentables.ViewModel parent, cal.Calendar calendar, bool isFavorite)
            : base(appCtx, dataCtx, parent)
        {
            if (calendar == null) throw new ArgumentNullException("calendar");

            this.Calendar = calendar;
            this._Selected = isFavorite;
            this.IsFavorite = isFavorite;
        }


        public cal.Calendar Calendar { get; private set; }
        private CalendarViewModel _calendarViewModel;
        public CalendarViewModel CalendarViewModel
        {
            get
            {
                if (_calendarViewModel == null)
                {
                    _calendarViewModel = (CalendarViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, Calendar);
                    _calendarViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_calendarViewModel_PropertyChanged);
                }
                return _calendarViewModel;
            }
        }

        void _calendarViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Color":
                    OnPropertyChanged("Color");
                    break;
            }
        }

        private bool _IsFavorite = false;
        public bool IsFavorite
        {
            get
            {
                return _IsFavorite;
            }
            set
            {
                if (_IsFavorite != value)
                {
                    _IsFavorite = value;
                    if (_IsFavorite)
                    {
                        // also select
                        Selected = true;
                    }
                    OnPropertyChanged("IsFavorite");
                }
            }
        }

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

        public string Color
        {
            get
            {
                return CalendarViewModel.Color;
            }
        }

        public override string Name
        {
            get { return CalendarViewModel.Name; }
        }
    }
    #endregion

    [ViewModelDescriptor]
    public class CalendarWorkspaceViewModel : WindowViewModel, IDeleteCommandParameter, IRefreshCommandListener
    {
        public new delegate CalendarWorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public static string[] Colors = new[] { 
            "#F1F5E3",

            "#FFAAAA",
            "#AAFFAA",
            "#AAAAFF",
            "#AAFFFF",
            "#FFAAFF",
        
            "#FFCCCC",
            "#CCFFCC",
            "#CCCCFF",
            "#CCFFFF",
            "#FFCCFF",

            "#69dba4",
            "#ffcc88",
            "#b5ff88",
        };

        private bool _shouldUpdateCalendarItems = true;
        private Func<IZetboxContext> _ctxFactory;
        private Func<ReportingHost> _rptFactory;
        private readonly FetchCache _fetchCache;

        public CalendarWorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Func<IZetboxContext> ctxFactory, Func<ReportingHost> rptFactory)
            : base(appCtx, dataCtx, parent)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            _ctxFactory = ctxFactory;
            _rptFactory = rptFactory;

            _fetchCache = new FetchCache(ViewModelFactory, DataContext, this);
        }

        #region Labels
        public override string Name
        {
            get { return CalendarResources.WorkspaceName; }
        }

        public string DetailsLabel
        {
            get { return CalendarResources.DetailsLabel; }
        }

        public string ItemsLabel
        {
            get { return CalendarResources.CalendarItemsLabel; }
        }
        #endregion

        #region Items
        private IEnumerable<CalendarSelectionViewModel> _Items = null;
        public IEnumerable<CalendarSelectionViewModel> Items
        {
            get
            {
                if (_Items == null)
                {
                    var config = GetSavedConfig();

                    var myID = CurrentIdentity != null ? CurrentIdentity.ID : 0;
                    _Items = DataContext.GetQuery<cal.Calendar>()
                        .ToList()
                        .OrderBy(i => i.Owner != null && i.Owner.ID == myID ?  1 : 2)
                        .ThenBy(i => i.Name)
                        .Select((i, idx) =>
                        {
                            var configItem = ExtractItem(i, config);
                            var isSelf = i.Owner != null ? i.Owner.ID == myID : false;

                            var mdl = ViewModelFactory.CreateViewModel<CalendarSelectionViewModel.Factory>().Invoke(DataContext, this, i, isSelf || configItem != null);
                            mdl.CalendarViewModel.Color = (configItem != null && !string.IsNullOrWhiteSpace(configItem.Color)) ? configItem.Color : Colors[idx % Colors.Length];
                            mdl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(calendarItem_PropertyChanged);
                            return mdl;
                        })
                        .ToList();
                    SelectedItem = _Items.FirstOrDefault(i => i.IsFavorite);
                    _fetchCache.SetCalendars(_Items.Where(i => i.Selected).Select(i => i.Calendar.ID));
                }
                return _Items;
            }
        }

        void calendarItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                _fetchCache.SetCalendars(Items.Where(i => i.Selected).Select(i => i.Calendar.ID));
                if (_shouldUpdateCalendarItems)
                {
                    CurrentView.Refresh();
                }
            }
            else if (e.PropertyName == "IsFavorite")
            {
                var config = GetSavedConfig();
                config.Configs = new List<CalendarConfiguration>();
                foreach (var item in Items.Where(i => i.IsFavorite))
                {
                    config.Configs.Add(new CalendarConfiguration() { Calendar = item.Calendar.ID, Color = item.Color });
                }
                SaveConfig(config);
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

        #region Configuration
        protected CalendarConfigurationList GetSavedConfig()
        {
            if (CurrentIdentity == null) return new CalendarConfigurationList();
            using (var ctx = _ctxFactory())
            {
                var idenity = ctx.Find<Identity>(CurrentIdentity.ID);
                CalendarConfigurationList obj;
                try
                {
                    obj = !string.IsNullOrEmpty(idenity.CalendarConfiguration) ? idenity.CalendarConfiguration.FromXmlString<CalendarConfigurationList>() : new CalendarConfigurationList();
                }
                catch (Exception ex)
                {
                    Logging.Client.Warn("Error during deserializing CalendarConfigurationList, creating a new one", ex);
                    obj = new CalendarConfigurationList();
                }
                return obj;
            }
        }

        protected void SaveConfig(CalendarConfigurationList config)
        {
            if (config == null) throw new ArgumentNullException("config");
            using (var ctx = _ctxFactory())
            {
                var idenity = ctx.Find<Identity>(CurrentIdentity.ID);
                idenity.CalendarConfiguration = config.ToXmlString();
                ctx.SubmitChanges();
            }
        }

        protected CalendarConfiguration ExtractItem(cal.Calendar cal, CalendarConfigurationList config)
        {
            if (config == null) return null;
            return config.Configs.FirstOrDefault(i => i.Calendar == cal.ID);
        }
        #endregion

        #region Commands
        protected override System.Collections.ObjectModel.ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var result = base.CreateCommands();
            result.Add(NewCommand);
            result.Add(OpenCommand);
            result.Add(RefreshCommand);
            result.Add(DeleteCommand);
            result.Add(PrintCommandGroup);
            return result;
        }

        #region Open command
        private ICommandViewModel _OpenCommand = null;
        public ICommandViewModel OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        CommonCommandsResources.OpenDataObjectCommand_Name,
                        CommonCommandsResources.OpenDataObjectCommand_Tooltip,
                        Open,
                        CanOpen,
                        CanOpenReason);
                    _OpenCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext));
                }
                return _OpenCommand;
            }
        }

        public bool CanOpen()
        {
            return CurrentView.SelectedItem != null;
        }

        public string CanOpenReason()
        {
            return CommonCommandsResources.DataObjectCommand_NothingSelected;
        }

        public void Open()
        {
            if (!CanOpen()) return;
            Open(CurrentView.SelectedItem);
        }

        public void Open(EventViewModel evt)
        {
            if (evt == null) return;
            var ctx = _ctxFactory();
            var ws = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(ctx, null);
            var source = evt.Event.Source.GetObject(ctx);
            if (source != null)
                ws.ShowObject(source);
            else
                ws.ShowObject(evt.Event);
            ViewModelFactory.ShowDialog(ws); // TODO: Realy? A Dialog? Discuss

            _fetchCache.Invalidate();
            CurrentView.Refresh(); // A dialog makes it easy to know when the time for a refresh has come
        }
        #endregion

        #region NewCommand
        private ICommandViewModel _NewCommand;
        public ICommandViewModel NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        CommonCommandsResources.NewDataObjectCommand_Name,
                        CommonCommandsResources.NewDataObjectCommand_Tooltip,
                        New,
                        CanNew,
                        CanNewReason);
                    _NewCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext));
                }
                return _NewCommand;
            }
        }

        public bool CanNew()
        {
            return SelectedItem != null && SelectedItem.CalendarViewModel.CanWrite;
        }

        public string CanNewReason()
        {
            if(SelectedItem == null ) return CommonCommandsResources.DataObjectCommand_NothingSelected;
            if(!SelectedItem.CalendarViewModel.CanWrite) return CommonCommandsResources.DataObjectCommand_NotAllowed;
            return string.Empty;
        }

        public void New()
        {
            New(DateTime.Now, false);
        }

        public void New(DateTime selectedDate, bool isAllDay)
        {
            if (!CanNew()) return;

            using (var ctx = _ctxFactory())
            {
                var calendar = ctx.Find<cal.Calendar>(SelectedItem.Calendar.ID);
                var dlg = ViewModelFactory.CreateViewModel<NewEventDialogViewModel.Factory>().Invoke(ctx, null);

                var args = new NewEventViewModelsArgs(ctx, ViewModelFactory, dlg, calendar, selectedDate, isAllDay);
                calendar.GetNewEventViewModels(args);

                dlg.InputViewModels = args.ViewModels;
                ViewModelFactory.ShowDialog(dlg);
                if (dlg.Result == true)
                {
                    var newItem = dlg.CreateNew();
                    if (newItem != null)
                    {
                        ctx.SubmitChanges();
                        _fetchCache.Invalidate();
                        CurrentView.Refresh();
                        CurrentView.SelectedItem = (EventViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, DataContext.Find<cal.Event>(newItem.ID));
                    }
                }
            }
        }
        #endregion

        #region RefreshCommand
        private RefreshCommand _RefreshCommand = null;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(
                        DataContext,
                        this);
                }
                return _RefreshCommand;
            }
        }
        #endregion

        #region DeleteCommand
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
        #endregion

        #region SelectAllCommand
        private ICommandViewModel _SelectAllCommand = null;
        public ICommandViewModel SelectAllCommand
        {
            get
            {
                if (_SelectAllCommand == null)
                {
                    _SelectAllCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, 
                        CalendarResources.SelectAllCommand_Label,
                        CalendarResources.SelectAllCommand_Tooltip, 
                        SelectAll, null, null);
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
                CurrentView.Refresh();
            }
        }
        #endregion

        #region ClearAllCommand
        private ICommandViewModel _ClearAllCommand = null;
        public ICommandViewModel ClearAllCommand
        {
            get
            {
                if (_ClearAllCommand == null)
                {
                    _ClearAllCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        CalendarResources.ClearAllCommand_Label,
                        CalendarResources.ClearAllCommand_Tooltip,
                        ClearAll, null, null);
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
                    if (!p.IsFavorite)
                        p.Selected = false;
                }
            }
            finally
            {
                _shouldUpdateCalendarItems = true;
                CurrentView.Refresh();
            }
        }
        #endregion

        #region Print Commands
        protected void Print(DateTime from, DateTime to)
        {
            var events = _fetchCache.FetchEventsAsync(from, to).Result;
            using (var rpt = _rptFactory())
            {
                Reporting.Calendar.Events.Call(rpt, events.SelectMany(e => e.CreateCalendarItemViewModels(from, to)), from, to);
                rpt.Open("Events.pdf");
            }
        }

        private ContainerCommand _printCommandGroup;
        public ContainerCommand PrintCommandGroup
        {
            get
            {
                if (_printCommandGroup == null)
                {
                    _printCommandGroup = ViewModelFactory.CreateViewModel<ContainerCommand.Factory>().Invoke(DataContext, this, 
                        CalendarResources.PrintCommandGroup_Label, 
                        CalendarResources.PrintCommandGroup_Tooltip,
                        PrintTodayCommand,
                        PrintThisWeekCommand,
                        PrintTwoWeeksCommand,
                        PrintMonthCommand,
                        PrintSheetCommand);
                    _printCommandGroup.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.Printer_png.Find(FrozenContext));
                }
                return _printCommandGroup;
            }
        }

        #region PrintToday command
        private ICommandViewModel _PrintTodayCommand = null;
        public ICommandViewModel PrintTodayCommand
        {
            get
            {
                if (_PrintTodayCommand == null)
                {
                    _PrintTodayCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, 
                        this, 
                        CalendarResources.PrintTodayCommand_Label,
                        CalendarResources.PrintTodayCommand_Tooltip, 
                        PrintToday, 
                        null, null);
                    _PrintTodayCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.Printer_png.Find(FrozenContext));
                }
                return _PrintTodayCommand;
            }
        }

        public void PrintToday()
        {
            Print(DateTime.Today, DateTime.Today.AddDays(1));
        }
        #endregion

        #region PrintThisWeek command
        private ICommandViewModel _PrintThisWeekCommand = null;
        public ICommandViewModel PrintThisWeekCommand
        {
            get
            {
                if (_PrintThisWeekCommand == null)
                {
                    _PrintThisWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        CalendarResources.PrintThisWeekCommand_Label,
                        CalendarResources.PrintThisWeekCommand_Tooltip, 
                        PrintThisWeek, 
                        null, null);
                    _PrintThisWeekCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.Printer_png.Find(FrozenContext));
                }
                return _PrintThisWeekCommand;
            }
        }

        public void PrintThisWeek()
        {
            var start = DateTime.Today.FirstWeekDay();
            Print(start, start.AddDays(7));
        }
        #endregion

        #region PrintTwoWeeks command
        private ICommandViewModel _PrintTwoWeeksCommand = null;
        public ICommandViewModel PrintTwoWeeksCommand
        {
            get
            {
                if (_PrintTwoWeeksCommand == null)
                {
                    _PrintTwoWeeksCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        CalendarResources.PrintTwoWeeksCommand_Label,
                        CalendarResources.PrintTwoWeeksCommand_Tooltip, 
                        PrintTwoWeeks, 
                        null, null);
                    _PrintTwoWeeksCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.Printer_png.Find(FrozenContext));
                }
                return _PrintTwoWeeksCommand;
            }
        }

        public void PrintTwoWeeks()
        {
            var start = DateTime.Today.FirstWeekDay();
            Print(start, start.AddDays(14));
        }
        #endregion

        #region PrintMonth command
        private ICommandViewModel _PrintMonthCommand = null;
        public ICommandViewModel PrintMonthCommand
        {
            get
            {
                if (_PrintMonthCommand == null)
                {
                    _PrintMonthCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        CalendarResources.PrintMonthCommand_Label,
                        CalendarResources.PrintMonthCommand_Tooltip, 
                        PrintMonth,
                        null, null);
                    _PrintMonthCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.Printer_png.Find(FrozenContext));
                }
                return _PrintMonthCommand;
            }
        }

        public void PrintMonth()
        {
            var start = DateTime.Today.FirstMonthDay();
            Print(start, start.AddMonths(1));
        }
        #endregion

        #region PrintSheet command
        private ICommandViewModel _PrintSheetCommand = null;
        public ICommandViewModel PrintSheetCommand
        {
            get
            {
                if (_PrintSheetCommand == null)
                {
                    _PrintSheetCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        CalendarResources.PrintSheetCommand_Label,
                        CalendarResources.PrintSheetCommand_Tooltip,
                        PrintSheet,
                        null, null);
                    _PrintSheetCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.Printer_png.Find(FrozenContext));
                }
                return _PrintSheetCommand;
            }
        }

        public void PrintSheet()
        {
            var dlg = ViewModelFactory.CreateDialog(DataContext, CalendarResources.DlgDateRangeTitle)
                .AddDateTime(CalendarResources.FromLabel, DateTime.Today)
                .AddDateTime(CalendarResources.UntilLabel, DateTime.Today);
            dlg.Show((values) =>
            {
                var from = ((DateTime)values[0]).Date;
                var to = ((DateTime)values[1]).Date.AddDays(1);
                var events = _fetchCache.FetchEventsAsync(from, to).Result;
                using (var rpt = _rptFactory())
                {
                    Reporting.Calendar.SheetDays.Call(rpt, events.SelectMany(e => e.CreateCalendarItemViewModels(from, to)), from, to.AddDays(-1));
                    rpt.Open("SheetDays.pdf");
                }
            });
        }
        #endregion

        #endregion
        #endregion

        #region CurrentView
        private WeekCalendarViewModel _weekCalender = null;
        public ICalendarDisplayViewModel CurrentView
        {
            get
            {
                if (_weekCalender == null)
                {
                    var config = GetSavedConfig();

                    _weekCalender = ViewModelFactory.CreateViewModel<WeekCalendarViewModel.Factory>()
                        .Invoke(DataContext, this, _fetchCache.FetchEventsAsync);
                    _weekCalender.PropertyChanged += _WeekCalender_PropertyChanged;
                    _weekCalender.New += (s, e) => New(e.Date, e.IsAllDay);
                    _weekCalender.Open += (s, e) => Open(e.Event);
                    _weekCalender.ShowFullWeek = config != null && config.ShowFullWeek;
                    // Initial refresh
                    _fetchCache.SetCalendars(Items.Where(i => i.Selected).Select(i => i.Calendar.ID));
                    _weekCalender.Refresh();
                }
                return _weekCalender;
            }
        }

        void _WeekCalender_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                // Me too
                OnPropertyChanged("SelectedItems");
            } 
            else if(e.PropertyName == "ShowFullWeek")
            {
                var config = GetSavedConfig();
                config.ShowFullWeek = _weekCalender.ShowFullWeek;
                SaveConfig(config);
            }
        }
        #endregion

        #region Misc
        public bool IsReadOnly { get { return false; } }
        bool IDeleteCommandParameter.AllowDelete { get { return true; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems
        {
            get
            {
                return new ViewModel[] { CurrentView.SelectedItem }; // return selected events!  
            }
        }

        void IRefreshCommandListener.Refresh()
        {
            _fetchCache.Invalidate();
            CurrentView.Refresh();
        }
        #endregion
    }
}
