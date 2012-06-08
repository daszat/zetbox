using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using System.Collections.ObjectModel;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.Models;

namespace Zetbox.Client.Presentables.FilterViewModels
{
    [ViewModelDescriptor]
    public class RangeFilterViewModel : FilterViewModel
    {
        public new delegate RangeFilterViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl);

        public RangeFilterViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
            this.RangeFilter = (RangeFilterModel)mdl;
        }

        private readonly RangeFilterModel RangeFilter;

        private object _requestedArgumentKind = null;
        public object RequestedArgumentKind
        {
            get { return _requestedArgumentKind ?? base.RequestedKind; }
            set
            {
                if (_requestedArgumentKind != value)
                {
                    _requestedArgumentKind = value;
                    OnPropertyChanged("RequestedArgumentKind");
                }
            }
        }

        private readonly OperatorViewModel[] _FromOperators = new[] { new OperatorViewModel(FilterOperators.GreaterOrEqual), new OperatorViewModel(FilterOperators.Greater), new OperatorViewModel(FilterOperators.Equals), new OperatorViewModel(FilterOperators.Not), new OperatorViewModel(FilterOperators.IsNull), new OperatorViewModel(FilterOperators.IsNotNull) };
        public IEnumerable<OperatorViewModel> FromOperators
        {
            get
            {
                return _FromOperators;
            }
        }

        private OperatorViewModel _FromOperator = null;
        public OperatorViewModel FromOperator
        {
            get
            {
                if (_FromOperator == null)
                {
                    _FromOperator = _FromOperators.Single(i => i.Operator == RangeFilter.FromOperator);
                }
                return _FromOperator;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("FromOperator");
                if (_FromOperator != value)
                {
                    _FromOperator = value;
                    RangeFilter.FromOperator = value.Operator;
                    OnPropertyChanged("FromOperator");
                }
            }
        }

        public IValueViewModel From
        {
            get
            {
                return Arguments[0];
            }
        }

        private readonly OperatorViewModel[] _ToOperators = new[] { new OperatorViewModel(FilterOperators.LessOrEqual), new OperatorViewModel(FilterOperators.Less), new OperatorViewModel(FilterOperators.Not), new OperatorViewModel(FilterOperators.IsNull), new OperatorViewModel(FilterOperators.IsNotNull) };
        public IEnumerable<OperatorViewModel> ToOperators
        {
            get
            {
                return _ToOperators;
            }
        }

        private OperatorViewModel _ToOperator = null;
        public OperatorViewModel ToOperator
        {
            get
            {
                if (_ToOperator == null)
                {
                    _ToOperator = _ToOperators.Single(i => i.Operator == RangeFilter.ToOperator);
                }
                return _ToOperator;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("ToOperator");
                if (_ToOperator != value)
                {
                    _ToOperator = value;
                    RangeFilter.ToOperator = value.Operator;
                    OnPropertyChanged("ToOperator");
                }
            }
        }

        public IValueViewModel To
        {
            get
            {
                return Arguments[1];
            }
        }
    }
}
