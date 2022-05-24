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
    using System.Linq.Expressions;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

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
        /// <summary>
        /// Only for IDataObjects
        /// </summary>
        ContainsObject = 10,
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
        string HelpText { get; }

        ObservableCollection<FilterArgumentConfig> FilterArguments { get; }
        FilterArgumentConfig FilterArgument { get; }

        event EventHandler FilterChanged;

        bool IsExclusiveFilter { get; }
    }

    public abstract class FilterModel
        : IUIFilterModel
    {
        public static readonly string PREDICATE_PLACEHOLDER = "@@PREDICATE@@";

        public static async Task<FilterModel> FromProperty(IZetboxContext ctx, IFrozenContext frozenCtx, Property prop)
        {
            return await FromProperty(ctx, frozenCtx, new[] { prop });
        }

        public static async Task<FilterModel> FromProperty(IZetboxContext ctx, IFrozenContext frozenCtx, IEnumerable<Property> props)
        {
            var last = props.Last();
            var label = string.Join(", ", (await props.Select(async i => await i.GetLabel()).WhenAll()).ToArray());
            var cfg = last.FilterConfiguration;
            var kind = cfg.IfNotNull(c => c.RequestedKind);
            ViewModelDescriptor argVMDL = null /* cfg.ArgumentViewModel*/ ?? last.ValueModelDescriptor;
            var argKind = argVMDL.IfNotNull(a => a.DefaultEditorKind);

            FilterModel mdl;
            if (last is DateTimeProperty)
            {
                mdl = RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(DateTime), kind, argKind, argumentViewModelDescriptor: argVMDL);
            }
            else if (last is IntProperty)
            {
                mdl = RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(int), kind, argKind, argumentViewModelDescriptor: argVMDL);
            }
            else if (last is DecimalProperty)
            {
                mdl = RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(decimal), kind, argKind, argumentViewModelDescriptor: argVMDL);
            }
            else if (last is DoubleProperty)
            {
                mdl = RangeFilterModel.Create(frozenCtx, label, FilterValueSource.FromProperty(props), typeof(double), kind, argKind, argumentViewModelDescriptor: argVMDL);
            }
            else
            {
                var isList = last.GetIsList();
                mdl = await SingleValueFilterModel.Create(ctx, frozenCtx, label, props, kind, !isList ? argKind : null, argumentViewModelDescriptor: !isList ? argVMDL : null);
            }

            // Don't set requiered here - this method is used for custom filter. Caller can set it self.
            if (cfg != null)
            {
                mdl.RefreshOnFilterChanged = cfg.RefreshOnFilterChanged;
            }
            return mdl;
        }

        public FilterModel()
        {
            this.FilterArguments = new ObservableCollection<FilterArgumentConfig>();
            this.FilterArguments.CollectionChanged += FilterArguments_CollectionChanged;
            this.IsServerSideFilter = true;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} ({2})", this.GetType().Name, Label, ValueSource);
        }

        void FilterArguments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (FilterArgumentConfig cfg in e.NewItems)
            {
                cfg.Value.PropertyChanged += new PropertyChangedEventHandler(delegate (object s, PropertyChangedEventArgs a)
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
        protected virtual Task<string> GetPredicate()
        {
            return Task.FromResult(string.Empty);
        }

        public virtual async Task<LambdaExpression> GetExpression(IQueryable src)
        {
            if (src == null) throw new ArgumentNullException("src");
            var p = await GetPredicate();
            if (!string.IsNullOrEmpty(p))
            {
                return System.Linq.Dynamic.DynamicExpression.ParseLambda(src.ElementType, typeof(bool), p, FilterArgumentValues);
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<IQueryable> GetQuery(IQueryable src)
        {
            var p = await GetPredicate();
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

        public string HelpText
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

        #region IUIFilterModel Members
        public bool IsExclusiveFilter
        {
            get;
            protected set;
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
                ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_StringValueViewModel.Find(frozenCtx)
            ));

            base.RefreshOnFilterChanged = false;
        }

        public override IEnumerable GetResult(IEnumerable src)
        {
            var pattern = FilterArgument.Value.GetUntypedValue().ToString().ToLowerInvariant();
            return src.AsQueryable().Cast<object>().Where(o => o.ToString().ToLowerInvariant().Contains(pattern));
        }
    }

    public class WithDeactivatedFilterModel : FilterModel
    {
        public WithDeactivatedFilterModel(IReadOnlyZetboxContext frozenCtx)
            : base()
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            base.IsServerSideFilter = true;
            base.Label = FilterModelsResources.WithDeactivatedFilterModel_Label;
            base.ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx);
            var valueMdl = new BoolValueModel(base.Label, FilterModelsResources.WithDeactivatedFilterModel_Description, false, false);
            valueMdl.Value = false;
            base.FilterArguments.Add(new FilterArgumentConfig(
                valueMdl,
                ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableBoolPropertyViewModel.Find(frozenCtx)
            ));

            base.RefreshOnFilterChanged = true;
        }

        public override async Task<IQueryable> GetQuery(IQueryable src)
        {
            if ((bool)(await FilterArgument.Value.GetUntypedValue()) == true)
                return src.WithDeactivated();
            else
                return src;
        }
    }

    public class FulltextFilterModel : FilterModel
    {
        public FulltextFilterModel(IReadOnlyZetboxContext frozenCtx)
            : base()
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            base.IsServerSideFilter = true;
            base.IsExclusiveFilter = true;
            base.Label = FilterModelsResources.FulltextFilterModel_Label;
            base.HelpText = FilterModelsResources.FulltextFilterModel_HelpText;
            base.ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx);
            base.FilterArguments.Add(new FilterArgumentConfig(
                new ClassValueModel<string>(base.Label, FilterModelsResources.FulltextFilterModel_Description, true, false),
                ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_StringValueViewModel.Find(frozenCtx)
            ));

            base.RefreshOnFilterChanged = false;
        }

        public override Task<IQueryable> GetQuery(IQueryable src)
        {
            if (FilterArgument.Value != null)
                return Task.FromResult(src.FulltextMatch(FilterArgument.Value.GetUntypedValue().ToString()));
            else
                return Task.FromResult(src);
        }
    }

    public class SingleValueFilterModel : FilterModel
    {
        public static async Task<SingleValueFilterModel> Create(IFrozenContext frozenCtx, string label, string predicate, Guid enumDef)
        {
            return await Create(frozenCtx, label, predicate, enumDef, null, null);
        }

        public static async Task<SingleValueFilterModel> Create(IFrozenContext frozenCtx, string label, string predicate, Guid enumDef, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return await Create(frozenCtx, label, FilterValueSource.FromExpression(predicate), enumDef, requestedKind, requestedArgumentKind);
        }

        public static Task<SingleValueFilterModel> Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, Guid enumDef, ControlKind requestedKind, ControlKind requestedArgumentKind, ViewModelDescriptor argumentViewModelDescriptor = null)
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
                argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_EnumerationValueViewModel.Find(frozenCtx)));
            return Task.FromResult(fmdl);
        }

        public static async Task<SingleValueFilterModel> Create(IFrozenContext frozenCtx, string label, string predicate, ObjectClass referencedClass, bool isList)
        {
            return await Create(frozenCtx, label, predicate, referencedClass, null, null, isList);
        }

        public static async Task<SingleValueFilterModel> Create(IFrozenContext frozenCtx, string label, string predicate, ObjectClass referencedClass, ControlKind requestedKind, ControlKind requestedArgumentKind, bool isList)
        {
            return await Create(frozenCtx, label, FilterValueSource.FromExpression(predicate), referencedClass, requestedKind, requestedArgumentKind, isList);
        }

        public static Task<SingleValueFilterModel> Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, ObjectClass referencedClass, ControlKind requestedKind, ControlKind requestedArgumentKind, bool isList, ViewModelDescriptor argumentViewModelDescriptor = null)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var fmdl = new SingleValueFilterModel()
            {
                Label = label,
                ValueSource = predicate,
                Operator = isList ? FilterOperators.ContainsObject : FilterOperators.Equals,
                ViewModelType = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(frozenCtx),
                RequestedKind = requestedKind,
                RefreshOnFilterChanged = true,
            };
            fmdl.FilterArguments.Add(new FilterArgumentConfig(
                new ObjectReferenceValueModel(label, "", true, false, requestedArgumentKind, referencedClass),
                argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_ObjectReferenceViewModel.Find(frozenCtx)));
            return Task.FromResult(fmdl);
        }

        public static Task<SingleValueFilterModel> Create(IZetboxContext ctx, IFrozenContext frozenCtx, string label, IFilterValueSource predicate, CompoundObject cpObj, ControlKind requestedKind, ControlKind requestedArgumentKind, ViewModelDescriptor argumentViewModelDescriptor = null)
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
                new CompoundObjectValueModel(ctx, label, "", true, false, requestedArgumentKind, cpObj),
                argumentViewModelDescriptor ?? cpObj.DefaultPropertyViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_CompoundObjectPropertyViewModel.Find(frozenCtx)));
            return Task.FromResult((SingleValueFilterModel)fmdl);
        }

        public static async Task<SingleValueFilterModel> Create<T>(IFrozenContext frozenCtx, string label, string predicate)
        {
            return await Create<T>(frozenCtx, label, predicate, null, null);
        }

        public static async Task<SingleValueFilterModel> Create<T>(IFrozenContext frozenCtx, string label, string predicate, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return await Create(frozenCtx, label, FilterValueSource.FromExpression(predicate), typeof(T), requestedKind, requestedArgumentKind);
        }

        public static Task<SingleValueFilterModel> Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, Type propType, ControlKind requestedKind, ControlKind requestedArgumentKind, ViewModelDescriptor argumentViewModelDescriptor = null)
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
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDecimalPropertyViewModel.Find(frozenCtx);
                mdl = new DecimalValueModel(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(int))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableIntPropertyViewModel.Find(frozenCtx);
                mdl = new NullableStructValueModel<int>(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(double))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDoublePropertyViewModel.Find(frozenCtx);
                mdl = new NullableStructValueModel<double>(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(DateTime))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDateTimePropertyViewModel.Find(frozenCtx);
                mdl = new DateTimeValueModel(label, "", true, false, DateTimeStyles.Date, requestedArgumentKind);
            }
            else if (propType == typeof(bool))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableBoolPropertyViewModel.Find(frozenCtx);
                fmdl.RefreshOnFilterChanged = true;
                if (requestedArgumentKind == null)
                {
                    requestedArgumentKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_DropdownBoolKind.Find(frozenCtx);
                }
                mdl = new BoolValueModel(label, "", true, false, requestedArgumentKind);
            }
            else if (propType == typeof(string))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_StringValueViewModel.Find(frozenCtx);
                mdl = new ClassValueModel<string>(label, "", true, false, requestedArgumentKind);
                fmdl.Operator = FilterOperators.Contains;
            }
            else
            {
                throw new NotSupportedException(string.Format("Singlevalue filters of Type {0} are not supported yet", propType.Name));
            }

            fmdl.FilterArguments.Add(new FilterArgumentConfig(mdl, vDesc));
            return Task.FromResult(fmdl);
        }

        public static async Task<SingleValueFilterModel> Create(IZetboxContext ctx, IFrozenContext frozenCtx, string label, Property prop)
        {
            return await Create(ctx, frozenCtx, label, new[] { prop });
        }

        public static async Task<SingleValueFilterModel> Create(IZetboxContext ctx, IFrozenContext frozenCtx, string label, IEnumerable<Property> props)
        {
            return await Create(ctx, frozenCtx, label, props, null, null);
        }

        public static async Task<SingleValueFilterModel> Create(IZetboxContext ctx, IFrozenContext frozenCtx, string label, Property prop, ControlKind requestedKind, ControlKind requestedArgumentKind)
        {
            return await Create(ctx, frozenCtx, label, new[] { prop }, requestedKind, requestedArgumentKind);
        }

        public static async Task<SingleValueFilterModel> Create(IZetboxContext ctx, IFrozenContext frozenCtx, string label, IEnumerable<Property> props, ControlKind requestedKind, ControlKind requestedArgumentKind, ViewModelDescriptor argumentViewModelDescriptor = null)
        {
            var predicate = FilterValueSource.FromProperty(props);
            var last = props.Last();
            if (last is DecimalProperty)
            {
                return await Create(frozenCtx, label, predicate, typeof(decimal), requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is IntProperty)
            {
                return await Create(frozenCtx, label, predicate, typeof(int), requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is DoubleProperty)
            {
                return await Create(frozenCtx, label, predicate, typeof(double), requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is DateTimeProperty)
            {
                return await Create(frozenCtx, label, predicate, typeof(DateTime), requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is StringProperty)
            {
                return await Create(frozenCtx, label, predicate, typeof(string), requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is BoolProperty)
            {
                return await Create(frozenCtx, label, predicate, typeof(bool), requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is EnumerationProperty)
            {
                return await Create(frozenCtx, label, predicate, ((EnumerationProperty)last).Enumeration.ExportGuid, requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is ObjectReferenceProperty)
            {
                return await Create(frozenCtx, label, predicate, await ((ObjectReferenceProperty)last).GetReferencedObjectClass(), requestedKind, requestedArgumentKind, last.GetIsList(), argumentViewModelDescriptor: argumentViewModelDescriptor);
            }
            else if (last is CompoundObjectProperty)
            {
                return await Create(ctx, frozenCtx, label, predicate, ((CompoundObjectProperty)last).CompoundObjectDefinition, requestedKind, requestedArgumentKind, argumentViewModelDescriptor: argumentViewModelDescriptor);
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
                    // TODO: .Result
                    return GetStringParts().Result;
                }
                else
                {
                    return base.FilterArgumentValues;
                }
            }
        }

        protected override async Task<string> GetPredicate()
        {
            var expr = ValueSource.Expression;
            switch (Operator)
            {
                case FilterOperators.Equals:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " = @0");
                case FilterOperators.Less:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " < @0");
                case FilterOperators.LessOrEqual:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " <= @0");
                case FilterOperators.Greater:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " > @0");
                case FilterOperators.GreaterOrEqual:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " >= @0");
                case FilterOperators.Not:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " != @0");
                case FilterOperators.IsNotNull:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " != null");
                case FilterOperators.IsNull:
                    return expr.Replace(PREDICATE_PLACEHOLDER, " == null");
                case FilterOperators.ContainsObject:
                    return expr.Replace(PREDICATE_PLACEHOLDER, ".Any(it == @0)");
                case FilterOperators.Contains:
                    {
                        // Only for strings
                        var parts = await GetStringParts();
                        var lastInner = ValueSource.LastInnerExpression;
                        var outerExpr = ValueSource.OuterExpression;
                        var sb = new StringBuilder();
                        sb.Append("( (1==0) ");
                        int counter = 0;
                        foreach (var p in parts)
                        {
                            sb.AppendFormat(" || {0}", outerExpr.Replace(PREDICATE_PLACEHOLDER, string.Format("{0} != null && {0}.ToLower().Contains(@{1}.ToLower())", lastInner, counter++)));
                        }
                        sb.Append(")");
                        return sb.ToString();
                    }
                default:
                    throw new InvalidOperationException("Operator is not defined");
            }
        }

        protected async Task<string[]> GetStringParts()
        {
            var str = (string)await FilterArgument.Value.GetUntypedValue();
            string pattern = @"(?<match>[^\s,;""]+)|\""(?<match>[^""]*)""";
            return Regex.Matches(str, pattern).Cast<Match>().Select(m => m.Groups["match"].Value).ToArray();
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

        protected override Task<string> GetPredicate()
        {
            var sb = new StringBuilder();
            int counter = 0;
            var args = FilterArgumentValues;
            foreach (var prop in PropertyNames)
            {
                if (args[counter] != null)
                {
                    sb.AppendFormat("({0}.{1} == @{2}) && ",
                        ValueSource.LastInnerExpression,
                        prop,
                        counter);
                }
                counter++;
            }
            sb.Remove(sb.Length - 3, 3);
            return Task.FromResult(ValueSource.OuterExpression.Replace(PREDICATE_PLACEHOLDER, sb.ToString()));
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

        protected override Task<string> GetPredicate()
        {
            return Task.FromResult(ValueSource.OuterExpression.Replace(PREDICATE_PLACEHOLDER, string.Format("{0} >= @0 && {0} < @0.AddMonths(1)", ValueSource.LastInnerExpression)));
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
                /*cfg.ArgumentViewModel ?? */ ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableIntPropertyViewModel.Find(frozenCtx)));

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

        protected override Task<string> GetPredicate()
        {
            return Task.FromResult(ValueSource.OuterExpression.Replace(PREDICATE_PLACEHOLDER, string.Format("{0}.Year == @0", ValueSource.LastInnerExpression)));
        }
    }

    public class DateRangeFilterModel : FilterModel
    {
        public static DateRangeFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, ControlKind requestedKind, bool setYearDefault, bool setQuaterDefault, bool setMonthDefault)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var mdl = new DateRangeFilterModel();

            Setup(mdl, frozenCtx, label, valueSource, requestedKind, setYearDefault, setQuaterDefault, setMonthDefault);

            return mdl;
        }

        protected static void Setup(DateRangeFilterModel mdl, IFrozenContext frozenCtx, string label, IFilterValueSource valueSource, ControlKind requestedKind, bool setYearDefault, bool setQuaterDefault, bool setMonthDefault)
        {
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
        }

        protected DateRangeFilterModel()
        {
            base.RefreshOnFilterChanged = true;
        }

        protected override Task<string> GetPredicate()
        {
            return Task.FromResult(ValueSource.OuterExpression.Replace(PREDICATE_PLACEHOLDER, string.Format("{0} >= @0 AND {0} <= @1", ValueSource.LastInnerExpression)));
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

    public class DateRangeIntersectFilterModel : DateRangeFilterModel
    {
        public static DateRangeIntersectFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource fromValueSource, IFilterValueSource toValueSource, ControlKind requestedKind, bool setYearDefault, bool setQuaterDefault, bool setMonthDefault)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            var mdl = new DateRangeIntersectFilterModel();
            DateRangeFilterModel.Setup(mdl, frozenCtx, label, fromValueSource, requestedKind, setYearDefault, setQuaterDefault, setMonthDefault);
            mdl.ToValueSource = toValueSource;
            return mdl;
        }

        protected override Task<string> GetPredicate()
        {
            return Task.FromResult(ValueSource.OuterExpression.Replace(PREDICATE_PLACEHOLDER, string.Format("({0} >= @0 AND {0} < @1) OR ({1} >= @0 AND {1} < @1) OR ({0} < @0 AND {1} >= @1)", ValueSource.LastInnerExpression, ToValueSource.LastInnerExpression)));
        }

        public IFilterValueSource ToValueSource
        {
            get;
            set;
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

        public static RangeFilterModel Create(IFrozenContext frozenCtx, string label, IFilterValueSource predicate, Type type, ControlKind requestedKind, ControlKind requestedArgumentKind, ViewModelDescriptor argumentViewModelDescriptor = null)
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
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDecimalPropertyViewModel.Find(frozenCtx);
                mdl1 = new DecimalValueModel("", "", true, false, requestedArgumentKind);
                mdl2 = new DecimalValueModel("", "", true, false, requestedArgumentKind);
            }
            else if (type == typeof(int))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableIntPropertyViewModel.Find(frozenCtx);
                mdl1 = new NullableStructValueModel<int>("", "", true, false, requestedArgumentKind);
                mdl2 = new NullableStructValueModel<int>("", "", true, false, requestedArgumentKind);
            }
            else if (type == typeof(double))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDoublePropertyViewModel.Find(frozenCtx);
                mdl1 = new NullableStructValueModel<double>("", "", true, false, requestedArgumentKind);
                mdl2 = new NullableStructValueModel<double>("", "", true, false, requestedArgumentKind);
            }
            else if (type == typeof(DateTime))
            {
                vDesc = argumentViewModelDescriptor ?? ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableDateTimePropertyViewModel.Find(frozenCtx);
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

        protected override Task<string> GetPredicate()
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

            return Task.FromResult(ValueSource.OuterExpression.Replace(PREDICATE_PLACEHOLDER, sb.ToString()));
        }

        private string GetPredicate(FilterOperators op, string arg)
        {
            if (op == FilterOperators.Contains)
            {
                return string.Format("{0}.ToLower().Contains({1}.ToLower())", ValueSource.LastInnerExpression, arg);
            }
            else if (op == FilterOperators.IsNull)
            {
                return string.Format("{0} = null", ValueSource.LastInnerExpression);
            }
            else if (op == FilterOperators.IsNotNull)
            {
                return string.Format("{0} != null", ValueSource.LastInnerExpression);
            }
            else
            {
                return string.Format("{0} {1} {2}", ValueSource.LastInnerExpression, GetOperatorExpression(op), arg);
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

        protected override Task<string> GetPredicate()
        {
            // Remove PREDICATE_PLACEHOLDER - a OptionalPredicate contains the ready made expression
            return Task.FromResult(ValueSource.Expression.Replace(PREDICATE_PLACEHOLDER, ""));
        }

        public async Task<bool> IsEnabled()
        {
            if (FilterArgument.Value == null) return false;
            return (bool)(await FilterArgument.Value.GetUntypedValue()) == true;
        }
    }
}
