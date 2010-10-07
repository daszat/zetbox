using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.App.Base;
using Kistl.App.GUI;
using System.Data;
using System.Collections;

namespace Kistl.Client.Presentables.KistlBase
{
    #region Interfaces
    public interface IFilterExpression
    {
        bool Enabled { get; }
        bool Requiered { get; }
    }

    public interface ILinqFilterExpression : IFilterExpression
    {
        string Predicate { get; }
        object[] FilterValues { get; }
    }

    public interface IPostFilterExpression : IFilterExpression
    {
        ReadOnlyObservableCollection<DataObjectViewModel> Execute(IEnumerable<DataObjectViewModel> instances);
    }

    public interface IUIFilterExpression : IFilterExpression
    {
        string Label { get; }
        event EventHandler FilterChanged;
    }

    public interface IValueTypeFilterViewModel<TValue> : IUIFilterExpression
        where TValue : struct
    {
        Nullable<TValue> Value { get; set; }
    }

    public interface IReferenceTypeFilterViewModel<TValue> : IUIFilterExpression
        where TValue : class
    {
        TValue Value { get; set; }
    }

    public interface IListTypeFilterViewModel : IUIFilterExpression
    {
        object Value { get; set; }
        IEnumerable PossibleValues { get; }
    }
    #endregion

    #region BaseFilterExpressions
    public abstract class UIFilterExpressionViewModel<TValue> : ViewModel, IUIFilterExpression
    {
        public UIFilterExpressionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string label)
            : base(appCtx, dataCtx)
        {
            this.Label = label;
            Values = new ObservableCollection<TValue>();
        }

        public event EventHandler FilterChanged = null;

        protected void OnFilterChanged()
        {
            EventHandler temp = FilterChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public string Label
        {
            get;
            protected set;
        }

        public override string Name
        {
            get { return Label; }
        }

        public ObservableCollection<TValue> Values
        {
            get;
            private set;
        }

        public abstract bool Enabled { get; }
        public abstract bool Requiered { get; }

        public string ToolTip { get; set; }

        public bool AllowNullInput { get; set; }
    }

    public abstract class ValueTypeUIFilterExpressionViewModel<TValue> : UIFilterExpressionViewModel<TValue>, IValueTypeFilterViewModel<TValue>, IReferenceTypeFilterViewModel<string>
        where TValue : struct
    {
        public ValueTypeUIFilterExpressionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string label)
            : base(appCtx, dataCtx, label)
        {
        }

        public Nullable<TValue> Value
        {
            get
            {
                if (Values.Count == 0) return null;
                return Values[0];
            }
            set
            {
                if (value == null)
                {
                    Values.RemoveAt(0);
                }
                else if (Values.Count == 0)
                {
                    Values.Add(value.Value);
                }
                else
                {
                    Values[0] = value.Value;
                }

                OnFilterChanged();
            }
        }

        #region IReferenceTypeFilterViewModel<string> Members
        string IReferenceTypeFilterViewModel<string>.Value
        {
            get
            {
                return Value != null ? Value.ToString() : String.Empty;
            }
            set
            {
                this.Value = String.IsNullOrEmpty(value) ? null : (Nullable<TValue>)System.Convert.ChangeType(value, typeof(TValue));
            }
        }
        #endregion
    }

    public abstract class ReferenceTypeUIFilterExpressionViewModel<TValue> : UIFilterExpressionViewModel<TValue>, IReferenceTypeFilterViewModel<TValue>
        where TValue : class
    {
        public ReferenceTypeUIFilterExpressionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string label)
            : base(appCtx, dataCtx, label)
        {
        }

        public TValue Value
        {
            get
            {
                if (Values.Count == 0) return null;
                return Values[0];
            }
            set
            {
                if (value == null || (value as string) == string.Empty)
                {
                    if (Values.Count > 0) Values.RemoveAt(0);
                }
                else
                {
                    if (Values.Count == 0)
                    {
                        Values.Add(value);
                    }
                    else
                    {
                        Values[0] = value;
                    }
                }

                OnFilterChanged();
            }
        }
    }
    #endregion

    #region CodeOnly Filter Expressions
    // No ViewModelDescriptor - code only filter
    public class ConstantFilterExpression : ILinqFilterExpression
    {
        public ConstantFilterExpression(string filter, params object[] values)
        {
            this.Predicate = filter;
            this.FilterValues = values;
        }

        public string Predicate
        {
            get;
            private set;
        }

        public object[] FilterValues
        {
            get;
            private set;
        }

        public bool Enabled { get { return true; } }
        public bool Requiered { get { return true; } }
    }

    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.SimpleBoolFilterKind", Description = "Code only filter expressions for switchable Filter")]
    public class EnableFilterExpression : ValueTypeUIFilterExpressionViewModel<bool>, ILinqFilterExpression
    {
        public new delegate EnableFilterExpression Factory(IKistlContext dataCtx, string label, string filter, params object[] values);

        public EnableFilterExpression(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string label, string filter, params object[] values)
            : base(appCtx, dataCtx, label)
        {
            this.Label = label;
            this.Predicate = filter;
            this.FilterValues = values;
            this.Values.Add(false);
            AllowNullInput = false;
        }


        public override bool Enabled
        {
            get
            {
                return Values.Count > 0 && Values[0];
            }
        }

        public override bool Requiered { get { return false; } }

        #region ILinqFilterExpression Members
        public string Predicate
        {
            get;
            private set;
        }
        public object[] FilterValues
        {
            get;
            private set;
        }
        #endregion
    }

    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.StringFilterKind", Description = "Code only filter expression for searching in ViewModel names")]
    public class ToStringFilterExpression : ReferenceTypeUIFilterExpressionViewModel<string>, IPostFilterExpression
    {
        public new delegate ToStringFilterExpression Factory(IKistlContext dataCtx, string label);

        public ToStringFilterExpression(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string label)
            : base(appCtx, dataCtx, label)
        {
            this.Label = label;
            AllowNullInput = false;
        }

        public override bool Enabled
        {
            get
            {
                return Values.Count > 0 && !string.IsNullOrEmpty(Values[0]);
            }
        }
        public override bool Requiered { get { return false; } }

        #region IPostFilterExpression Members

        public ReadOnlyObservableCollection<DataObjectViewModel> Execute(IEnumerable<DataObjectViewModel> instances)
        {
            return new ReadOnlyObservableCollection<DataObjectViewModel>(
                    new ObservableCollection<DataObjectViewModel>(
                        instances.Where(
                            o => o.Name.ToLowerInvariant().Contains(Value.ToLowerInvariant())
                            || o.ID.ToString().Contains(Value))));
        }

        #endregion
    }

    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.StringFilterKind", Description = "Code only filter expression for searching in IDataObject Properties")]
    public class StringFilterExpression : ReferenceTypeUIFilterExpressionViewModel<string>, ILinqFilterExpression
    {
        public new delegate StringFilterExpression Factory(IKistlContext dataCtx, string label, string filter);

        public StringFilterExpression(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string label, string filter)
            : base(appCtx, dataCtx, label)
        {
            this.Label = label;
            AllowNullInput = false;
            this.Predicate = filter;
        }

        public override bool Enabled
        {
            get
            {
                return Values.Count > 0 && !string.IsNullOrEmpty(Values[0]);
            }
        }
        public override bool Requiered { get { return false; } }

        public virtual string Predicate
        {
            get;
            private set;
        }

        public virtual object[] FilterValues
        {
            get { return base.Values.Cast<object>().ToArray(); }
        }        
    }
    #endregion

    #region PropertyFilterExpression

    public interface IPropertyFilterExpression : ILinqFilterExpression
    {
        Property Property { get; }
    }

    public delegate IFilterExpression PropertyFilterExpressionFactory(IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg);

    public class ValueTypePropertyFilterExpressionViewModel<TValue> : ValueTypeUIFilterExpressionViewModel<TValue>, ILinqFilterExpression, IPropertyFilterExpression
        where TValue : struct
    {
        public new delegate ValueTypePropertyFilterExpressionViewModel<TValue> Factory(IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg);

        public ValueTypePropertyFilterExpressionViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg)
            : base(appCtx, dataCtx, "")
        {
            if (prop == null) throw new ArgumentNullException("prop");
            this.Property = prop;
            this.Configuration = filterCfg;
            base.Label = prop.Name;
            this.Predicate = string.Format("{0} = @0", prop.Name);
        }

        public Property Property { get; private set; }
        protected FilterConfiguration Configuration { get; private set; }

        public override bool Enabled
        {
            get
            {
                return base.Values.Count > 0;
            }
        }
        public override bool Requiered { get { return Configuration.Requiered; } }

        public virtual string Predicate
        {
            get;
            private set;
        }

        public virtual object[] FilterValues
        {
            get { return base.Values.Cast<object>().ToArray(); }
        }
    }

    public abstract class ReferencePropertyFilterExpressionViewModel<TValue> : ReferenceTypeUIFilterExpressionViewModel<TValue>, ILinqFilterExpression, IPropertyFilterExpression
        where TValue : class
    {
        public ReferencePropertyFilterExpressionViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg)
            : base(appCtx, dataCtx, "")
        {
            if (prop == null) throw new ArgumentNullException("prop");
            this.Property = prop;
            this.Configuration = filterCfg;
            base.Label = prop.Name;
            this.Predicate = string.Format("{0} = @0", prop.Name);
        }

        public Property Property { get; private set; }
        protected FilterConfiguration Configuration { get; private set; }

        public override bool Enabled
        {
            get
            {
                return base.Values.Count > 0;
            }
        }
        public override bool Requiered { get { return Configuration.Requiered; } }

        public virtual string Predicate
        {
            get;
            protected set;
        }

        public virtual object[] FilterValues
        {
            get { return base.Values.Cast<object>().ToArray(); }
        }
    }

    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.StringFilterKind", Description = "Filter expression for String Properties")]
    public class StringPropertyFilterExpressionViewModel : ReferencePropertyFilterExpressionViewModel<string>
    {
        public new delegate StringPropertyFilterExpressionViewModel Factory(IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg);

        public StringPropertyFilterExpressionViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg)
            : base(appCtx, dataCtx, prop, filterCfg)
        {
            this.Predicate = string.Format("{0}.Contains(@0)", prop.Name);
        }
    }

    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.SimpleDropdownFilterKind", Description = "Filter expression for ObjectReference Properties")]
    public class ObjectReferencePropertyFilterExpressionViewModel : ReferencePropertyFilterExpressionViewModel<DataObjectViewModel>, IListTypeFilterViewModel
    {
        public new delegate ObjectReferencePropertyFilterExpressionViewModel Factory(IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg);

        public ObjectReferencePropertyFilterExpressionViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Property prop, FilterConfiguration filterCfg)
            : base(appCtx, dataCtx, prop, filterCfg)
        {
        }

        public override object[] FilterValues
        {
            get
            {
                return Values.Select(v => v.Object).ToArray();
            }
        }

        #region IListTypeFilterViewModel Members

        object IListTypeFilterViewModel.Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = (DataObjectViewModel)value;
            }
        }

        private List<KeyValuePair<DataObjectViewModel, string>> _PossibleValues = null;

        public IEnumerable PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    _PossibleValues = new List<KeyValuePair<DataObjectViewModel, string>>();
                    _PossibleValues.Add(new KeyValuePair<DataObjectViewModel, string>());
                    foreach (var obj in DataContext
                        .GetQuery(DataContext.GetInterfaceType(Property.GetPropertyType()))
                        .ToList() // TODO: remove this
                        .OrderBy(obj => obj.ToString()).ToList())
                    {
                        _PossibleValues.Add(new KeyValuePair<DataObjectViewModel, string>(
                            ModelFactory.CreateViewModel<DataObjectViewModel.Factory>(obj).Invoke(DataContext, obj),
                            obj.ToString())
                        );
                    }
                }
                return _PossibleValues;
            }
        }

        #endregion
    }
    #endregion
}
