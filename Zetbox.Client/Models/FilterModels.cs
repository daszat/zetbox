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

namespace Zetbox.Client.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;
using System.Linq.Expressions;

    public class FilterEvaluator
    {
        public List<FilterModel> Filters { get; private set; }
        public IQueryable Execute(IZetboxContext ctx)
        {
            return null;
        }
    }

    public enum FilterOperators
    {
        Equals = 1,
        /// <summary>
        /// Only for strings
        /// </summary>
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
        ControlKind RequestedKind { get; set; }

        string Label { get; }
        ObservableCollection<FilterArgumentConfig> FilterArguments { get; }
        FilterArgumentConfig FilterArgument { get; }

        event EventHandler FilterChanged;
    }

    public abstract class FilterModel
        : IUIFilterModel
    {
        public static FilterModel FromProperty(IFrozenContext frozenCtx, Property prop)
        {
            return FromProperty(frozenCtx, new[] { prop });
        }

        public static FilterModel FromProperty(IFrozenContext frozenCtx, IEnumerable<Property> props)
        {
            var last = props.Last();
            var label = string.Join(", ", props.Select(i => i.GetLabel()).ToArray());
            if (last is DateTimeProperty)
            {
                return RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(DateTime), null, null);
            }
            else if (last is IntProperty)
            {
                return RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(int), null, null);
            }
            else if (last is DecimalProperty)
            {
                return RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(decimal), null, null);
            }
            else if (last is DoubleProperty)
            {
                return RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(double), null, null);
            }
            else
            {
                return SingleValueFilterModel.Create(frozenCtx, label, props);
            }
        }

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
        protected virtual object[] FilterArgumentValues
        {
            get
            {
                return FilterArguments.Select(i => i.Value.GetUntypedValue()).ToArray();
            }
        }

        // Goes to Linq
        protected virtual string GetPredicate()
        {
            return string.Empty;
        }

        public virtual LambdaExpression GetExpression(IQueryable src)
        {
            if (src == null) throw new ArgumentNullException("src");
            var p = GetPredicate();
            if (!string.IsNullOrEmpty(p))
            {
                return DynamicExpression.ParseLambda(src.ElementType, typeof(bool), p, FilterArgumentValues);
            }
            else
            {
                return null;
            }
        }

        public virtual IQueryable GetQuery(IQueryable src)
        {
            var p = GetPredicate();
            if (!string.IsNullOrEmpty(p))
            {
                return src.Where(p, FilterArgumentValues);
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
        /// <summary>
        /// Used to override DefaultKind in code
        /// </summary>
        private ControlKind _RequestedKind;
        public virtual ControlKind RequestedKind
        {
            get { return _RequestedKind; }
            set { _RequestedKind = value; }
        }

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

        public bool RefreshOnFilterChanged { get; set; }

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
        public ToStringFilterModel(IReadOnlyZetboxContext frozenCtx)
            : base()
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            base.IsServerSideFilter = false;
            base.Label = FilterModelsResources.ToStringFilterModel_Label;
            base.ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx);
            base.FilterArguments.Add(new FilterArgumentConfig(
                new ClassValueModel<string>(base.Label, FilterModelsResources.ToStringFilterModel_Description, true, false),
                ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_ClassValueViewModel_1_System_String_.Find(frozenCtx)
            ));

            base.RefreshOnFilterChanged = false;
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
            return Create(frozenCtx, label, predicate, enumDef, null, null);
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, string predicate, Guid enumDef, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return Create(frozenCtx, label, FilterValueSource.FromExpression(predicate), enumDef, requestedKind, requestedArgumentKind);
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, Guid enumDef, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new SingleValueFilterModel()
            {
                Label = label,
                ValueSource = predicate,
                Operator = FilterOperators.Equals,
                ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx),
                RequestedKind = requestedKind,
                RefreshOnFilterChanged = true,
            };
            fmdl.FilterArguments.Add(new FilterArgumentConfig(
                new EnumerationValueModel(label, "", true, false, requestedArgumentKind, frozenCtx.FindPersistenceObject<Enumeration>(enumDef)),
                ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_EnumerationValueViewModel.Find(frozenCtx)));
            return fmdl;
        }


        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, string predicate, ObjectClass referencedClass)
        {
            return Create(frozenCtx, label, predicate, referencedClass, null, null);
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, string predicate, ObjectClass referencedClass, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return Create(frozenCtx, label, FilterValueSource.FromExpression(predicate), referencedClass, requestedKind, requestedArgumentKind);
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, ObjectClass referencedClass, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new SingleValueFilterModel()
            {
                Label = label,
                ValueSource = predicate,
                Operator = FilterOperators.Equals,
                ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx),
                RequestedKind = requestedKind,
                RefreshOnFilterChanged = true,
            };
            fmdl.FilterArguments.Add(new FilterArgumentConfig(
                new ObjectReferenceValueModel(label, "", true, false, requestedArgumentKind, referencedClass),
                ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_ObjectReferenceViewModel.Find(frozenCtx)));
            return fmdl;
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, CompoundObject cpObj, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new SingleCompoundValueFilterModel()
            {
                Label = label,
                ValueSource = predicate,
                Operator = FilterOperators.Equals,
                ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx),
                RequestedKind = requestedKind,
                RefreshOnFilterChanged = true,
                CompoundObjectDefinition = cpObj,
            };
            fmdl.FilterArguments.Add(new FilterArgumentConfig(
                new CompoundObjectValueModel(label, "", true, false, requestedArgumentKind, cpObj),
                cpObj.DefaultPropertyViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_CompoundObjectPropertyViewModel.Find(frozenCtx)));
            return fmdl;            
        }

        public static SingleValueFilterModel Create<T>(IFrozenContext frozenCtx, string label, string predicate)
        {
            return Create<T>(frozenCtx, label, predicate, null, null);
        }

        public static SingleValueFilterModel Create<T>(IFrozenContext frozenCtx, string label, string predicate, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return Create(frozenCtx, label, FilterValueSource.FromExpression(predicate), typeof(T), requestedKind, requestedArgumentKind);
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, Type propType, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (propType == null) throw new ArgumentNullException("propType");

            var fmdl = new SingleValueFilterModel()
            {
                Label = label,
                ValueSource = predicate,
                Operator = FilterOperators.Equals,
                ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx),
                RequestedKind = requestedKind,
                RefreshOnFilterChanged = false,
            };

            ViewModelDescriptor vDesc = null;
            BaseValueModel mdl = null;
            if (propType == typeof(decimal))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDecimalPropertyViewModel.Find(frozenCtx);
                mdl = new DecimalValueModel(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(int))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableStructValueViewModel_1_System_Int32_.Find(frozenCtx);
                mdl = new NullableStructValueModel<int>(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(double))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableStructValueViewModel_1_System_Double_.Find(frozenCtx);
                mdl = new NullableStructValueModel<double>(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(bool))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableBoolPropertyViewModel.Find(frozenCtx);
                fmdl.RefreshOnFilterChanged = true;
                if (requestedArgumentKind == null)
                {
                    requestedArgumentKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_DropdownBoolKind.Find(frozenCtx);
                }
                mdl = new BoolValueModel(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(string))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_ClassValueViewModel_1_System_String_.Find(frozenCtx);
                mdl = new ClassValueModel<string>(label, "", true, false, requestedArgumentKind);
                fmdl.Operator = FilterOperators.Contains;
            }
            else
            {
                throw new NotSupportedException(string.Format("Singlevalue filters of Type {0} are not supported yet", propType.Name));
            }

            fmdl.FilterArguments.Add(new FilterArgumentConfig(mdl, vDesc));
            return fmdl;
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, Property prop)
        {
            return Create(frozenCtx, label, new[] { prop });
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, IEnumerable<Property> props)
        {
            return Create(frozenCtx, label, props, null, null);
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, Property prop, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return Create(frozenCtx, label, new[] { prop }, requestedKind, requestedArgumentKind);
        }

        public static SingleValueFilterModel Create(IFrozenContext frozenCtx, string label, IEnumerable<Property> props, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            var predicate = FilterValueSource.FromProperty(props);
            var last = props.Last();
            if (last is DecimalProperty)
            {
                return Create(frozenCtx, label, predicate, typeof(decimal), requestedKind, requestedArgumentKind);
            }
            else if (last is IntProperty)
            {
                return Create(frozenCtx, label, predicate, typeof(int), requestedKind, requestedArgumentKind);
            }
            else if (last is DoubleProperty)
            {
                return Create(frozenCtx, label, predicate, typeof(double), requestedKind, requestedArgumentKind);
            }
            else if (last is StringProperty)
            {
                return Create(frozenCtx, label, predicate, typeof(string), requestedKind, requestedArgumentKind);
            }
            else if (last is BoolProperty)
            {
                return Create(frozenCtx, label, predicate, typeof(bool), requestedKind, requestedArgumentKind);
            }
            else if (last is EnumerationProperty)
            {
                return Create(frozenCtx, label, predicate, ((EnumerationProperty)last).Enumeration.ExportGuid, requestedKind, requestedArgumentKind);
            }
            else if (last is ObjectReferenceProperty)
            {
                return Create(frozenCtx, label, predicate, ((ObjectReferenceProperty)last).GetReferencedObjectClass(), requestedKind, requestedArgumentKind);
            }
            else if (last is CompoundObjectProperty)
            {
                return Create(frozenCtx, label, predicate, ((CompoundObjectProperty)last).CompoundObjectDefinition, requestedKind, requestedArgumentKind);
            }
            else
            {
                throw new NotSupportedException(string.Format("Singlevalue filters of Property Type {0} are not supported yet", last.GetType().Name));
            }
        }

        public SingleValueFilterModel()
        {
            Operator = FilterOperators.Equals;
            base.RefreshOnFilterChanged = false;
        }

        public FilterOperators Operator { get; set; }

        protected override object[] FilterArgumentValues
        {
            get
            {
                if (Operator == FilterOperators.Contains)
                {
                    return GetStringParts();
                }
                else
                {
                    return base.FilterArgumentValues;
                }
            }
        }

        protected override string GetPredicate()
        {
            switch (Operator)
            {
                case FilterOperators.Equals:
                    return string.Format("{0} = @0", ValueSource.Expression);
                case FilterOperators.Contains:
                    {
                        // Only for strings
                        var parts = GetStringParts();
                        var sb = new StringBuilder();
                        sb.Append("( (1==0) ");
                        int counter = 0;
                        foreach (var p in parts)
                        {
                            sb.AppendFormat(" || ({0} != null && {0}.ToLower().Contains(@{1}.ToLower()) )", ValueSource.Expression, counter++);
                        }
                        sb.Append(")");
                        return sb.ToString();
                    }
                default:
                    throw new InvalidOperationException("Operator is not defined");
            }
        }

        protected string[] GetStringParts()
        {
            var str = (string)FilterArgument.Value.GetUntypedValue();
            return str.Split(',', ' ', ';').Select(i => i.Trim()).Where(i => !string.IsNullOrEmpty(i)).ToArray();
        }
    }

    public class SingleCompoundValueFilterModel : SingleValueFilterModel
    {
        public CompoundObject CompoundObjectDefinition { get; set; }

        public override bool Enabled
        {
            get
            {
                return FilterArgumentValues.Any(i => i != null);
            }
        }

        protected override string GetPredicate()
        {
            var sb = new StringBuilder();
            int counter = 0;
            var args = FilterArgumentValues;
            foreach (var prop in PropertyNames)
            {
                if (args[counter] != null)
                {
                    sb.AppendFormat("({0}.{1} == @{2}) && ",
                        ValueSource.Expression,
                        prop,
                        counter);
                }
                counter++;
            }
            sb.Remove(sb.Length - 3, 3);
            return sb.ToString(); 
        }

        private string[] _propNames;
        protected string[] PropertyNames
        {
            get
            {
                if (_propNames == null)
                {
                    _propNames = CompoundObjectDefinition.Properties.Select(i => i.Name).ToArray();
                }
                return _propNames;
            }
        }

        protected override object[] FilterArgumentValues
        {
            get
            {
                List<object> result = new List<object>();
                var mdl = (CompoundObjectValueModel)FilterArgument.Value;
                foreach (var prop in PropertyNames)
                {
                    var val = mdl.Value.GetPropertyValue<object>(prop);
                    if (val is string && string.IsNullOrEmpty((string)val))
                    {
                        val = null;
                    }
                    result.Add(val);
                }
                return result.ToArray();
            }
        }
    }

    public class MonthValueFilterModel : FilterModel
    {
        public static MonthValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, bool setDefault)
        {
            return Create(frozenCtx, label, valueSource, setDefault, null);
        }

        public static MonthValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, bool setDefault, ControlKind requestedKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var valMdl = new DateTimeValueModel(label, "", true, false);
            var mdl = new MonthValueFilterModel();
            mdl.Label = label;
            mdl.ValueSource = valueSource;
            mdl.ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx);
            mdl.RequestedKind = requestedKind;
            mdl.FilterArguments.Add(new FilterArgumentConfig(
                valMdl,
                /*cfg.ArgumentViewModel ?? */ ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableMonthPropertyViewModel.Find(frozenCtx)));

            if (setDefault)
            {
                // Defaults to this month
                valMdl.Value = DateTime.Today.FirstMonthDay();
            }

            return mdl;
        }

        protected MonthValueFilterModel()
        {
            base.RefreshOnFilterChanged = true;
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
            return Create(frozenCtx, label, valueSource, setDefault, null);
        }

        public static YearValueFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, bool setDefault, ControlKind requestedKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var valMdl = new NullableStructValueModel<int>(label, "", true, false);
            var mdl = new YearValueFilterModel();
            mdl.Label = label;
            mdl.ValueSource = valueSource;
            mdl.ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx);
            mdl.RequestedKind = requestedKind;
            mdl.FilterArguments.Add(new FilterArgumentConfig(
                valMdl,
                /*cfg.ArgumentViewModel ?? */ ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableStructValueViewModel_1_System_Int32_.Find(frozenCtx)));

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

    public class DateRangeFilterModel : FilterModel
    {
        public static DateRangeFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, ControlKind requestedKind, bool setYearDefault, bool setQuaterDefault, bool setMonthDefault)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var mdl = new DateRangeFilterModel();
            mdl.Label = label;
            mdl.ValueSource = valueSource;
            mdl.ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_DateRangeFilterViewModel.Find(frozenCtx);
            mdl.RequestedKind = requestedKind;

            var fromMdl = new DateTimeValueModel(FilterModelsResources.From, "", true, false, DateTimeStyles.Date);
            var toMdl = new DateTimeValueModel(FilterModelsResources.To, "", true, false, DateTimeStyles.Date);

            mdl.FilterArguments.Add(new FilterArgumentConfig(
                fromMdl,
                /*cfg.ArgumentViewModel ?? */ ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDateTimePropertyViewModel.Find(frozenCtx)));
            mdl.FilterArguments.Add(new FilterArgumentConfig(
                toMdl,
                /*cfg.ArgumentViewModel ?? */ ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDateTimePropertyViewModel.Find(frozenCtx)));

            if (setYearDefault)
            {
                // Defaults to this month
                fromMdl.Value = DateTime.Today.FirstYearDay();
                toMdl.Value = DateTime.Today.LastYearDay();
            }
            else if (setQuaterDefault)
            {
                // Defaults to this month
                fromMdl.Value = DateTime.Today.FirstQuaterDay();
                toMdl.Value = DateTime.Today.LastQuaterDay();
            }
            else if (setMonthDefault)
            {
                // Defaults to this month
                fromMdl.Value = DateTime.Today.FirstMonthDay();
                toMdl.Value = DateTime.Today.LastMonthDay();
            }

            return mdl;
        }

        protected DateRangeFilterModel()
        {
            base.RefreshOnFilterChanged = true;
        }

        protected override string GetPredicate()
        {
            return string.Format("{0} >= @0 AND {0} <= @1", ValueSource.Expression);
        }

        public DateTimeValueModel From
        {
            get
            {
                return (DateTimeValueModel)FilterArguments[0].Value;
            }
        }

        public DateTimeValueModel To
        {
            get
            {
                return (DateTimeValueModel)FilterArguments[1].Value;
            }
        }
    }

    public class RangeFilterModel : FilterModel
    {
        public static RangeFilterModel Create<T>(IFrozenContext frozenCtx, string label, string predicate, ControlKind requestedKind, ControlKind requestedArgumentKind)
                 where T : struct
        {
            return Create(frozenCtx, label, predicate, typeof(T), requestedKind, requestedArgumentKind);
        }

        public static RangeFilterModel Create(IFrozenContext frozenCtx, string label, string predicate, Type type, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return Create(frozenCtx, label, FilterValueSource.FromExpression(predicate), type, requestedKind, requestedArgumentKind);
        }

        public static RangeFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, Type type, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (type == null) throw new ArgumentNullException("type");

            var rfmdl = new RangeFilterModel()
            {
                Label = label,
                ValueSource = predicate,
                ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_RangeFilterViewModel.Find(frozenCtx),
                RequestedKind = requestedKind,
            };

            ViewModelDescriptor vDesc = null;
            BaseValueModel mdl1 = null;
            BaseValueModel mdl2 = null;
            if (type == typeof(decimal))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDecimalPropertyViewModel.Find(frozenCtx);
                mdl1 = new DecimalValueModel("", "", true, false, requestedArgumentKind);
                mdl2 = new DecimalValueModel("", "", true, false, requestedArgumentKind);
            }
            else if (type == typeof(int))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableStructValueViewModel_1_System_Int32_.Find(frozenCtx);
                mdl1 = new NullableStructValueModel<int>("", "", true, false, requestedArgumentKind);
                mdl2 = new NullableStructValueModel<int>("", "", true, false, requestedArgumentKind);
            }
            else if (type == typeof(double))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableStructValueViewModel_1_System_Double_.Find(frozenCtx);
                mdl1 = new NullableStructValueModel<double>("", "", true, false, requestedArgumentKind);
                mdl2 = new NullableStructValueModel<double>("", "", true, false, requestedArgumentKind);
            }
            else if (type == typeof(DateTime))
            {
                vDesc = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDateTimePropertyViewModel.Find(frozenCtx);
                mdl1 = new DateTimeValueModel("", "", true, false, DateTimeStyles.Date, requestedArgumentKind);
                mdl2 = new DateTimeValueModel("", "", true, false, DateTimeStyles.Date, requestedArgumentKind);
            }
            else
            {
                throw new NotSupportedException(string.Format("Rangefilters of Type {0} are not supported yet", type.Name));
            }

            rfmdl.FilterArguments.Add(new FilterArgumentConfig(mdl1, vDesc));
            rfmdl.FilterArguments.Add(new FilterArgumentConfig(mdl2, vDesc));

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
                return string.Format("{0} = null", ValueSource.Expression);
            }
            else if (op == FilterOperators.IsNotNull)
            {
                return string.Format("{0} != null", ValueSource.Expression);
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
            return Create(frozenCtx, label, predicate, null, null);
        }

        public static OptionalPredicateFilterModel Create(IFrozenContext frozenCtx, string label, string predicate, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new OptionalPredicateFilterModel()
            {
                Label = label,
                ValueSource = FilterValueSource.FromExpression(predicate),
                ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_OptionalPredicateFilterViewModel.Find(frozenCtx),
                RequestedKind = requestedKind,
            };
            var valueMdl = new BoolValueModel(label, "", false, false, requestedArgumentKind);
            valueMdl.Value = false;
            fmdl.FilterArguments.Add(new FilterArgumentConfig(valueMdl,
                ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableBoolPropertyViewModel.Find(frozenCtx)));
            return fmdl;
        }

        public OptionalPredicateFilterModel()
        {
            base.RefreshOnFilterChanged = true;
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

        public LambdaExpression GetExpression(IQueryable src)
        {
            if (src == null) throw new ArgumentNullException("src");
            return DynamicExpression.ParseLambda(src.ElementType, typeof(bool), predicate);
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
