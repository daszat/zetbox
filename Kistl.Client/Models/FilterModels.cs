
namespace Kistl.Client.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables.ValueViewModels;
    
    public class FilterEvaluator
    {
        public List<FilterModel> Filters { get; private set; }
        public IQueryable Execute(IKistlContext ctx)
        {
            return null;
        }
    }

    public enum FilterOperators
    {
        Equals = 1,
        Contains = 2,
        Less = 3,
        LessOrEqual = 4,
        Greater = 5,
        GreaterOrEqual = 6,
        Not = 7,
        IsNull = 8,
        IsNotNull = 9,
    }

    public sealed class FilterArgumentConfig
    {
        public FilterArgumentConfig(IValueModel value, ViewModelDescriptor desc)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (desc == null) throw new ArgumentNullException("desc");

            this.Value = value;
            this.ViewModelType = desc;
        }

        public IValueModel Value { get; private set; }
        public ViewModelDescriptor ViewModelType { get; private set; }
    }

    public interface IUIFilterModel : IFilterModel
    {
        ViewModelDescriptor ViewModelType { get; set; }

        string Label { get; }
        ObservableCollection<FilterArgumentConfig> FilterArguments { get; }
        FilterArgumentConfig FilterArgument { get; }

        event EventHandler FilterChanged;
    }

    public abstract class FilterModel
        : IUIFilterModel
    {
        public FilterModel()
        {
            this.FilterArguments = new ObservableCollection<FilterArgumentConfig>();
            this.FilterArguments.CollectionChanged += FilterArguments_CollectionChanged;
            this.IsServerSideFilter = true;
        }

        void FilterArguments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (FilterArgumentConfig cfg in e.NewItems)
            {
                cfg.Value.PropertyChanged += new PropertyChangedEventHandler(delegate(object s, PropertyChangedEventArgs a)
                {
                    if (a.PropertyName == "Value")
                    {
                        OnFilterChanged();
                    }
                });
            }
        }

        public ObservableCollection<FilterArgumentConfig> FilterArguments { get; private set; }
        public FilterArgumentConfig FilterArgument
        {
            get
            {
                return FilterArguments.Single();
            }
        }

        // Goes to Linq
        protected virtual string GetPredicate()
        {
            return string.Empty;
        }

        public virtual IQueryable GetQuery(IQueryable src)
        {
            var p = GetPredicate();
            if (!string.IsNullOrEmpty(p))
            {
                return src.Where(p, FilterArguments.Select(i => i.Value.GetUntypedValue()).ToArray());
            }
            else
            {
                return src;
            }
        }

        public virtual IEnumerable GetResult(IEnumerable src)
        {
            return src;
        }

        #region IFilterModel Members
        public bool IsServerSideFilter
        {
            get;
            protected set;
        }

        public IFilterValueSource ValueSource
        {
            get;
            set;
        }

        public ViewModelDescriptor ViewModelType { get; set; }

        public string Label
        {
            get;
            set;
        }

        public virtual bool Enabled
        {
            get
            {
                return FilterArguments.Count(i => i.Value.GetUntypedValue() != null) > 0;
            }
        }

        public bool Required { get; set; }

        public event EventHandler FilterChanged;
        protected void OnFilterChanged()
        {
            EventHandler temp = FilterChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        #endregion
    }


    public class ToStringFilterModel : FilterModel
    {
        public ToStringFilterModel(IReadOnlyKistlContext frozenCtx)
            : base()
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            base.IsServerSideFilter = false;
            base.Label = FilterModelsResources.ToStringFilterModel_Label;
            base.ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel);
            base.FilterArguments.Add(new FilterArgumentConfig(
                new ClassValueModel<string>(base.Label, FilterModelsResources.ToStringFilterModel_Description, true, false), 
                frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_ReferencePropertyModel_String)
            )); // ClassValueViewModel<string>
        }

        public override IEnumerable GetResult(IEnumerable src)
        {
            var pattern = FilterArgument.Value.GetUntypedValue().ToString().ToLowerInvariant();
            return src.AsQueryable().Cast<object>().Where(o => o.ToString().ToLowerInvariant().Contains(pattern));
        }
    }

    public class SingleValueFilterModel : FilterModel
    {

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, string predicate, Guid enumDef)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new SingleValueFilterModel()
            {
                Label = label,
                ValueSource = FilterValueSource.FromExpression(predicate),
                Operator = FilterOperators.Equals,
                ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel)
            };
            fmdl.FilterArguments.Add(new FilterArgumentConfig(
                new EnumerationValueModel(label, "", true, false, frozenCtx.FindPersistenceObject<Enumeration>(enumDef)),
                frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Enum)));
            return fmdl;
        }

        public static SingleValueFilterModel Create<T>(IFrozenContext frozenCtx, string label, string predicate)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new SingleValueFilterModel()
            {
                Label = label,
                ValueSource = FilterValueSource.FromExpression(predicate),
                Operator = FilterOperators.Equals,
                ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel)
            };

            ViewModelDescriptor vDesc = null;
            BaseValueModel mdl = null;
            if (typeof(T) == typeof(decimal))
            {
                vDesc = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Decimal);
                mdl = new NullableStructValueModel<decimal>(label, "", true, false);
            }
            else if (typeof(T) == typeof(int))
            {
                vDesc = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Int);
                mdl = new NullableStructValueModel<int>(label, "", true, false);
            }
            else if (typeof(T) == typeof(double))
            {
                vDesc = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Double);
                mdl = new NullableStructValueModel<double>(label, "", true, false);
            }
            else if (typeof(T) == typeof(string))
            {
                vDesc = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_ReferencePropertyModel_String);
                mdl = new ClassValueModel<string>(label, "", true, false);
                fmdl.Operator = FilterOperators.Contains;
            }
            else
            {
                throw new NotSupportedException(string.Format("Singlevalue filters of Type {0} are not supported yet", typeof(T).Name));
            }

            fmdl.FilterArguments.Add(new FilterArgumentConfig(mdl, vDesc));
            return fmdl;
        }

        public SingleValueFilterModel()
        {
            Operator = FilterOperators.Equals;
        }

        public FilterOperators Operator { get; set; }

        protected override string GetPredicate()
        {
            switch (Operator)
            {
                case FilterOperators.Equals:
                    return string.Format("{0} = @0", ValueSource.Expression);
                case FilterOperators.Contains:
                    return string.Format("{0} != null && {0}.ToLower().Contains(@0.ToLower())", ValueSource.Expression);
                default:
                    throw new InvalidOperationException("Operator is not defined");
            }
        }
    }

    public class MonthValueFilterModel : FilterModel
    {
        public static MonthValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, bool setDefault)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var valMdl = new DateTimeValueModel(label, "", true, false); 
            var mdl = new MonthValueFilterModel();
            mdl.Label = label;
            mdl.ValueSource = valueSource;
            mdl.ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel);
            mdl.FilterArguments.Add(new FilterArgumentConfig(
                valMdl, 
                /*cfg.ArgumentViewModel ?? */ frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableMonthPropertyViewModel)));

            if (setDefault)
            {
                // Defaults to this month
                valMdl.Value = DateTime.Today.FirstMonthDay();
            }

            return mdl;
        }

        protected MonthValueFilterModel()
        {
        }

        protected override string GetPredicate()
        {
            return string.Format("{0} >= @0 && {0} < @0.AddMonths(1)", ValueSource.Expression);
        }
    }

    public class YearValueFilterModel : FilterModel
    {
        public static YearValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, bool setDefault)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var valMdl = new NullableStructValueModel<int>(label, "", true, false);
            var mdl = new YearValueFilterModel();
            mdl.Label = label;
            mdl.ValueSource = valueSource;
            mdl.ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel);
            mdl.FilterArguments.Add(new FilterArgumentConfig(
                valMdl,
                /*cfg.ArgumentViewModel ?? */ frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Int)));

            if (setDefault)
            {
                // Defaults to this month
                valMdl.Value = DateTime.Today.FirstYearDay().Year;
            }

            return mdl;
        }

        protected YearValueFilterModel()
        {
        }

        protected override string GetPredicate()
        {
            return string.Format("{0}.Year == @0", ValueSource.Expression);
        }
    }

    public class RangeFilterModel : FilterModel
    {
        public static RangeFilterModel Create<T>(IFrozenContext frozenCtx, string label, string predicate)
            where T : struct
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var rfmdl = new RangeFilterModel()
            {
                Label = label,
                ValueSource = FilterValueSource.FromExpression(predicate),
                ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_RangeFilterViewModel)
            };

            ViewModelDescriptor vDesc = null;
            if (typeof(T) == typeof(decimal))
            {
                vDesc = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Decimal);
            }
            else if (typeof(T) == typeof(int))
            {
                vDesc = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Int);
            }
            else if (typeof(T) == typeof(double))
            {
                vDesc = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Double);
            }
            else
            {
                throw new NotSupportedException(string.Format("Rangefilters of Type {0} are not supported yet", typeof(T).Name));
            }

            rfmdl.FilterArguments.Add(new FilterArgumentConfig(
                new NullableStructValueModel<T>("", "", true, false),
                vDesc));
            rfmdl.FilterArguments.Add(new FilterArgumentConfig(
                new NullableStructValueModel<T>("", "", true, false),
                vDesc));

            return rfmdl;
        }

        protected override string GetPredicate()
        {
            StringBuilder sb = new StringBuilder();

            var hasFrom = From.GetUntypedValue() != null;
            var hasTo = To.GetUntypedValue() != null;

            if (hasFrom)
            {
                sb.Append(GetPredicate(FromOperator, "@0"));
            }

            if (hasFrom && hasTo)
            {
                sb.Append(" AND ");
            }

            if (hasTo)
            {
                sb.Append(GetPredicate(ToOperator, "@1"));
            }

            return sb.ToString();
        }

        private string GetPredicate(FilterOperators op, string arg)
        {
            if (op == FilterOperators.Contains)
            {
                return string.Format("{0}.ToLower().Contains({1}.ToLower())", ValueSource.Expression, arg);
            }
            else if (op == FilterOperators.IsNull)
            {
                return string.Format("{0} is null", arg);
            }
            else if (op == FilterOperators.IsNotNull)
            {
                return string.Format("{0} is not null", arg);
            }
            else
            {
                return string.Format("{0} {1} {2}", ValueSource.Expression, GetOperatorExpression(op), arg);
            }
        }

        private string GetOperatorExpression(FilterOperators op)
        {
            switch (op)
            {
                case FilterOperators.Equals:
                    return "=";
                case FilterOperators.Less:
                    return "<";
                case FilterOperators.LessOrEqual:
                    return "<=";
                case FilterOperators.Greater:
                    return ">";
                case FilterOperators.GreaterOrEqual:
                    return ">=";
                case FilterOperators.Not:
                    return "!=";
                case FilterOperators.IsNull:
                    return "is null";
                case FilterOperators.IsNotNull:
                    return "is not null";
                default:
                    throw new ArgumentOutOfRangeException("op");
            }
        }

        private FilterOperators _fromOperator = FilterOperators.GreaterOrEqual;
        public FilterOperators FromOperator
        {
            get
            {
                return _fromOperator;
            }
            set
            {
                if (_fromOperator != value)
                {
                    _fromOperator = value;
                    OnFilterChanged();
                }
            }
        }

        private FilterOperators _toOperator = FilterOperators.LessOrEqual;
        public FilterOperators ToOperator
        {
            get
            {
                return _toOperator;
            }
            set
            {
                if (_toOperator != value)
                {
                    _toOperator = value;
                    OnFilterChanged();
                }
            }
        }

        public IValueModel From
        {
            get
            {
                return FilterArguments[0].Value;
            }
        }

        public IValueModel To
        {
            get
            {
                return FilterArguments[1].Value;
            }
        }
    }

    /// <summary>
    /// Config -> first ObjectClassFilterConfiguration
    /// </summary>
    public class OptionalPredicateFilterModel : FilterModel
    {
        public static OptionalPredicateFilterModel Create(IFrozenContext frozenCtx, string label, string predicate)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new OptionalPredicateFilterModel()
            {
                Label = label,
                ValueSource = FilterValueSource.FromExpression(predicate),
                ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_OptionalPredicateFilterViewModel)
            };
            var valueMdl = new NullableStructValueModel<bool>(label, "", false, false);
            valueMdl.Value = false;
            fmdl.FilterArguments.Add(new FilterArgumentConfig(valueMdl,
                frozenCtx.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Bool)));
            return fmdl;
        }

        protected override string GetPredicate()
        {
            return ValueSource.Expression;
        }

        public override bool Enabled
        {
            get
            {
                if (FilterArgument.Value == null) return false;
                return (bool)FilterArgument.Value.GetUntypedValue() == true;
            }
        }
    }

    public class ConstantValueFilterModel : IFilterModel
    {
        public ConstantValueFilterModel(string predicate, params object[] values)
            : this(true, predicate, values)
        {
        }

        public ConstantValueFilterModel(bool isServerSideFilter, string predicate, params object[] values)
        {
            this.IsServerSideFilter = isServerSideFilter;
            this.predicate = predicate;
            this.values = values;
        }

        private string predicate;
        private object[] values;

        #region IFilterModel Members

        public IQueryable GetQuery(IQueryable src)
        {
            return src.Where(predicate, values);
        }

        public IEnumerable GetResult(IEnumerable src)
        {
            return src.AsQueryable().Where(predicate, values);
        }

        public bool IsServerSideFilter
        {
            get;
            private set;
        }

        IFilterValueSource IFilterModel.ValueSource
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        
        public bool Enabled
        {
            get { return true; }
        }

        public bool Required
        {
            get { return false; }
        }

        
        #endregion
    }
}
