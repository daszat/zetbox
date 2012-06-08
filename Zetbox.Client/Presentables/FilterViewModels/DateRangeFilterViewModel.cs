
namespace Kistl.Client.Presentables.FilterViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using System.Collections.ObjectModel;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;
    using System.ComponentModel;

    [ViewModelDescriptor]
    public class DateRangeFilterViewModel : FilterViewModel
    {
        public new delegate DateRangeFilterViewModel Factory(IKistlContext dataCtx, ViewModel parent, IUIFilterModel mdl);

        public DateRangeFilterViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IUIFilterModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            this.RangeFilter = (DateRangeFilterModel)mdl;
            InitializeFromRange();
            Arguments[0].IsReadOnly = true;
            Arguments[1].IsReadOnly = true;
        }

        public void InitializeFromRange()
        {
            if (RangeFilter.From.Value != null && RangeFilter.To.Value != null)
            {
                var from = RangeFilter.From.Value.Value;
                var to = RangeFilter.To.Value.Value;
                var diff = (to.Month - from.Month) + 1;

                // Always set year
                Year = Years.SingleOrDefault(i => i.Value == from.Year);
                if (diff == 12)
                {
                    // one year
                }
                else if (diff == 3)
                {
                    // Quater
                    Quater = Quaters.SingleOrDefault(i => i.Value == from.GetQuater());
                }
                else
                {
                    // OK, lets assume it's a Month
                    Month = Months.SingleOrDefault(i => i.Value == from.Month);
                }
            }
            else
            {
                // Default to current year
                Year = Years.SingleOrDefault(i => i.Value == DateTime.Today.Year);
            }
        }

        public void UpdateRange()
        {
            if (_year != null)
            {
                if (_halfYear == null && _quater == null && _month == null)
                {
                    RangeFilter.From.Value = new DateTime(_year, 1, 1);
                    RangeFilter.To.Value = new DateTime(_year, 12, 31);
                }
                else if (_halfYear != null)
                {
                    RangeFilter.From.Value = new DateTime(_year, ((_halfYear - 1) * 6) + 1, 1);
                    RangeFilter.To.Value = RangeFilter.From.Value.Value.AddMonths(6).AddDays(-1);
                }
                else if (_quater != null)
                {
                    RangeFilter.From.Value = new DateTime(_year, ((_quater - 1) * 3) + 1, 1);
                    RangeFilter.To.Value = RangeFilter.From.Value.Value.AddMonths(3).AddDays(-1);
                }
                else
                {
                    RangeFilter.From.Value = new DateTime(_year, _month, 1);
                    RangeFilter.To.Value = RangeFilter.From.Value.Value.AddMonths(1).AddDays(-1);
                }
            }
        }


        private readonly DateRangeFilterModel RangeFilter;

        public IValueViewModel From
        {
            get
            {
                return Arguments[0];
            }
        }

        public IValueViewModel To
        {
            get
            {
                return Arguments[1];
            }
        }

        #region Year
        private ItemViewModel _year;
        public ItemViewModel Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = ItemViewModel.OnlyValid(value);
                UpdateIsSelected(Years, value);
                OnPropertyChanged("Year");
                UpdateRange();
            }
        }

        private int _yearsCount = 100;
        public int YearsCount
        {
            get
            {
                return _yearsCount;
            }
            set
            {
                if (_yearsCount != value)
                {
                    _yearsCount = value;
                    _years = null;
                    OnPropertyChanged("YearsCount");
                    OnPropertyChanged("Years");
                }
            }
        }

        private IList<ItemViewModel> _years = null;
        public IEnumerable<ItemViewModel> Years
        {
            get
            {
                if (_years == null)
                {
                    _years = new List<ItemViewModel>();
                    int current = DateTime.Today.Year;
                    for (int i = current; i >= current - 100; i--)
                    {
                        _years.Add(new ItemViewModel(i, i.ToString("0000")));
                    }

                    AttachChangeEventListener(_years, i => Year = i);
                }
                return _years;
            }
        }

        private int _yearsShortCount = 15;
        public int YearsShortCount
        {
            get
            {
                return _yearsShortCount;
            }
            set
            {
                if (_yearsShortCount != value)
                {
                    _yearsShortCount = value;
                    OnPropertyChanged("YearsShortCount");
                    OnPropertyChanged("YearsShort");
                }
            }
        }

        public IEnumerable<ItemViewModel> YearsShort
        {
            get
            {
                return Years.Take(YearsShortCount);
            }
        }

        #endregion

        #region HalfYear
        private ItemViewModel _halfYear;
        public ItemViewModel HalfYear
        {
            get
            {
                return _halfYear;
            }
            set
            {
                _halfYear = ItemViewModel.OnlyValid(value);
                _month = null;
                _quater = null;
                UpdateIsSelected(Months, _month);
                UpdateIsSelected(Quaters, _quater);
                UpdateIsSelected(HalfYears, _halfYear);
                OnPropertyChanged("HalfYear");
                OnPropertyChanged("Quater");
                OnPropertyChanged("Month");
                AllYear.IsSelected = _halfYear == null || !_halfYear.IsSelected;
                UpdateRange();
            }
        }

        private IList<ItemViewModel> _halfYears = null;
        public IEnumerable<ItemViewModel> HalfYears
        {
            get
            {
                if (_halfYears == null)
                {
                    _halfYears = new List<ItemViewModel>();
                    _halfYears.Add(new ItemViewModel());
                    for (int i = 1; i <= 2; i++)
                    {
                        _halfYears.Add(new ItemViewModel(i, i.ToString() + ". " + FilterViewModelResources.HalfYear));
                    }
                    AttachChangeEventListener(_halfYears, i => HalfYear = i);
                }
                return _halfYears;
            }
        }
        public IEnumerable<ItemViewModel> HalfYearsWithoutEmpty
        {
            get
            {
                return HalfYears.Skip(1);
            }
        }
        #endregion

        #region Quater
        private ItemViewModel _quater;
        public ItemViewModel Quater
        {
            get
            {
                return _quater;
            }
            set
            {
                _quater = ItemViewModel.OnlyValid(value);
                _month = null;
                _halfYear = null;
                UpdateIsSelected(Months, _month);
                UpdateIsSelected(Quaters, _quater);
                UpdateIsSelected(HalfYears, _halfYear);
                OnPropertyChanged("Quater");
                OnPropertyChanged("Month");
                OnPropertyChanged("HalfYear");
                AllYear.IsSelected = _quater == null || !_quater.IsSelected;
                UpdateRange();
            }
        }

        private IList<ItemViewModel> _quaters = null;
        public IEnumerable<ItemViewModel> Quaters
        {
            get
            {
                if (_quaters == null)
                {
                    _quaters = new List<ItemViewModel>();
                    _quaters.Add(new ItemViewModel());
                    for (int i = 1; i <= 4; i++)
                    {
                        _quaters.Add(new ItemViewModel(i, i.ToString() + ". " + FilterViewModelResources.Quater));
                    }
                    AttachChangeEventListener(_quaters, i => Quater = i);
                }
                return _quaters;
            }
        }
        public IEnumerable<ItemViewModel> QuatersWithoutEmpty
        {
            get
            {
                return Quaters.Skip(1);
            }
        }
        #endregion

        #region Month
        private ItemViewModel _month;
        public ItemViewModel Month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = ItemViewModel.OnlyValid(value);
                _quater = null;
                _halfYear = null;

                UpdateIsSelected(Months, _month);
                UpdateIsSelected(Quaters, _quater);
                UpdateIsSelected(HalfYears, _halfYear);
                OnPropertyChanged("Month");
                OnPropertyChanged("Quater");
                OnPropertyChanged("HalfYear");
                AllYear.IsSelected = _month == null || !_month.IsSelected;
                UpdateRange();
            }
        }

        private IList<ItemViewModel> _months = null;
        public IEnumerable<ItemViewModel> Months
        {
            get
            {
                if (_months == null)
                {
                    _months = new List<ItemViewModel>();
                    _months.Add(new ItemViewModel());
                    for (int i = 1; i <= 12; i++)
                    {
                        var dt = new DateTime(2000, i, 1);
                        _months.Add(new ItemViewModel(i, dt.ToString("MMM")));
                    }
                    AttachChangeEventListener(_months, i => Month = i);
                }
                return _months;
            }
        }
        public IEnumerable<ItemViewModel> MonthsWithoutEmpty
        {
            get
            {
                return Months.Skip(1);
            }
        }
        #endregion

        #region AllYear
        private ItemViewModel _allYear;
        public ItemViewModel AllYear
        {
            get
            {
                if (_allYear == null)
                {
                    _allYear = new ItemViewModel(1, FilterViewModelResources.AllYear);
                    _allYear.IsSelectedChangedByUser += (s, e) =>
                    {
                        if (_allYear.IsSelected)
                        {
                            _month = null;
                            _quater = null;
                            _halfYear = null;
                            UpdateIsSelected(Months, null);
                            UpdateIsSelected(Quaters, null);
                            UpdateIsSelected(HalfYears, null);
                            OnPropertyChanged("Month");
                            OnPropertyChanged("Quater");
                            OnPropertyChanged("HalfYear");
                            UpdateRange();
                        }
                    };
                }
                return _allYear;
            }
        }
        #endregion

        #region ItemViewModel
        private void UpdateIsSelected(IEnumerable<ItemViewModel> collection, ItemViewModel value)
        {
            foreach (var i in collection)
            {
                i.SetIsSelected(i == value || (value == null && i.Value == null));
            }
        }

        private void AttachChangeEventListener(IList<ItemViewModel> collection, Action<ItemViewModel> setter)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (setter == null) throw new ArgumentNullException("setter");

            foreach (var i in collection)
            {
                i.IsSelectedChangedByUser += (s, e) =>
                {
                    var item = (ItemViewModel)s;
                    setter(item != null && item.IsSelected ? item : null);
                };
            }
        }

        public class ItemViewModel : INotifyPropertyChanged
        {
            public ItemViewModel()
            {
            }

            public ItemViewModel(int value, string name)
            {
                this.Value = value;
                this.Name = name;
            }

            public static ItemViewModel OnlyValid(ItemViewModel i)
            {
                return i != null && i.Value != null ? i : null;
            }

            public static implicit operator int(ItemViewModel i)
            {
                if (i == null || i.Value == null) return 0;
                return i.Value.Value;
            }

            public int? Value { get; set; }
            public string Name { get; set; }

            private bool _isSelected = false;
            public bool IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    if (_isSelected != value)
                    {
                        _isSelected = value;
                        OnPropertyChanged("IsSelected");
                        var temp = IsSelectedChangedByUser;
                        if (temp != null)
                        {
                            temp(this, EventArgs.Empty);
                        }
                    }
                }
            }

            private void OnPropertyChanged(string name)
            {
                var temp = PropertyChanged;
                if (temp != null)
                {
                    temp(this, new PropertyChangedEventArgs(name));
                }
            }

            public void SetIsSelected(bool value)
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public event EventHandler IsSelectedChangedByUser;
        }
        #endregion
    }
}
