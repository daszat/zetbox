
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

    [ViewModelDescriptor]
    public class DateRangeFilterViewModel : FilterViewModel
    {
        public new delegate DateRangeFilterViewModel Factory(IKistlContext dataCtx, ViewModel parent, IUIFilterModel mdl);

        public DateRangeFilterViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IUIFilterModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            this.RangeFilter = (DateRangeFilterModel)mdl;
            UpdateFromRange();
            Arguments[0].IsReadOnly = true;
            Arguments[1].IsReadOnly = true;
        }

        public void UpdateFromRange()
        {
            if(RangeFilter.From.Value != null && RangeFilter.To.Value != null)
            {
                _year = RangeFilter.From.Value.Value.Year;
                var diff = (RangeFilter.From.Value.Value.Month - RangeFilter.To.Value.Value.Month);
                if (diff == 12)
                {
                    // it's OK
                }
                else if (diff == 3)
                {
                    // Quater
                    _quater = ((RangeFilter.From.Value.Value.Month - 1) / 3) + 1;
                }
                else
                {
                    // OK, it's a Month
                    _month = RangeFilter.From.Value.Value.Month;
                }
            }
        }

        public void UpdateRange()
        {
            if(_year != null && (_quater != null || _month != null))
            {
                if (_quater != null)
                {
                    RangeFilter.From.Value = new DateTime(_year.Value, ((_quater.Value - 1) * 3) + 1, 1);
                    RangeFilter.To.Value = RangeFilter.From.Value.Value.AddMonths(3).AddDays(-1);
                }
                else
                {
                    RangeFilter.From.Value = new DateTime(_year.Value, _month.Value, 1);
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
        private int? _year;
        public int? Year
        {
            get
            {
                return _year;
            }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged("Year");
                    UpdateRange();
                }
            }
        }

        private static IList<ItemViewModel> _Years = null;
        public IEnumerable<ItemViewModel> Years
        {
            get
            {
                if (_Years == null)
                {
                    _Years = new List<ItemViewModel>();
                    int current = DateTime.Today.Year;
                    for (int i = current; i >= current - 100; i--)
                    {
                        _Years.Add(new ItemViewModel(i, i.ToString("0000")));
                    }
                }
                return _Years;
            }
        }

        #endregion

        #region Quater
        private int? _quater;
        public int? Quater
        {
            get
            {
                return _quater;
            }
            set
            {
                if (_quater != value)
                {
                    _quater = value;
                    OnPropertyChanged("Quater");
                    if (_quater != null)
                    {
                        _month = null;
                        OnPropertyChanged("Month");
                    }
                    UpdateRange();
                }
            }
        }

        private static IList<ItemViewModel> _Quaters = null;
        public IEnumerable<ItemViewModel> Quaters
        {
            get
            {
                if (_Quaters == null)
                {
                    _Quaters = new List<ItemViewModel>();
                    _Quaters.Add(new ItemViewModel());
                    for (int i = 1; i <= 4; i++)
                    {
                        _Quaters.Add(new ItemViewModel(i, i.ToString() + ". " + FilterViewModelResources.Quater));
                    }
                }
                return _Quaters;
            }
        }
        #endregion

        #region Month
        private int? _month;
        public int? Month
        {
            get
            {
                return _month;
            }
            set
            {
                if (_month != value)
                {
                    _month = value;
                    OnPropertyChanged("Month");
                    if (_month != null)
                    {
                        _quater = null;
                        OnPropertyChanged("Quater");
                    }
                    UpdateRange();
                }
            }
        }

        private static IList<ItemViewModel> _Months = null;
        public IEnumerable<ItemViewModel> Months
        {
            get
            {
                if (_Months == null)
                {
                    _Months = new List<ItemViewModel>();
                    _Months.Add(new ItemViewModel());
                    for (int i = 1; i <= 12; i++)
                    {
                        var dt = new DateTime(2000, i, 1);
                        _Months.Add(new ItemViewModel(i, dt.ToString("MMMM")));
                    }
                }
                return _Months;
            }
        }
        #endregion

        #region Common
        public class ItemViewModel
        {
            public ItemViewModel()
            {
            }

            public ItemViewModel(int value, string name)
            {
                this.Value = value;
                this.Name = name;
            }

            public int Value { get; set; }
            public string Name { get; set; }
        }        
        #endregion
    }
}
