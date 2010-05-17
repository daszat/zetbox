using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Collections.ObjectModel;

namespace Kistl.Client.Presentables.KistlBase
{
    public interface IFilterExpression
    {
        string Predicate { get; }
        object[] FilterValues { get; }
        bool Enabled { get; }
    }

    public interface IUIFilterExpression : IFilterExpression
    {
        string Label { get; }
        event EventHandler FilterChanged;
    }

    public interface IPostFilterExpression : IFilterExpression
    {
        ReadOnlyObservableCollection<DataObjectModel> Execute(IEnumerable<DataObjectModel> instances);
    }

    public class ConstantFilterExpression : IFilterExpression
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
    }

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

        public virtual string Predicate
        {
            get;
            protected set;
        }

        public virtual object[] FilterValues
        {
            get;
            protected set;
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

        public virtual bool Enabled { get { return true; } }

        public string ToolTip { get; set; }

        public bool AllowNullInput { get; set; }
    }

    public abstract class ValueTypeUIFilterExpressionViewModel<TValue> : UIFilterExpressionViewModel<TValue>
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
    }

    public abstract class ReferenceTypeUIFilterExpressionViewModel<TValue> : UIFilterExpressionViewModel<TValue>
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
                if (value == null)
                {
                    Values.RemoveAt(0);
                }
                else if (Values.Count == 0)
                {
                    Values.Add(value);
                }
                else
                {
                    Values[0] = value;
                }

                OnFilterChanged();
            }
        }
    }

    public class EnableFilterExpression : ValueTypeUIFilterExpressionViewModel<bool>
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
    }

    public class NameFilterExpression : ReferenceTypeUIFilterExpressionViewModel<string>, IPostFilterExpression
    {
        public new delegate NameFilterExpression Factory(IKistlContext dataCtx, string label, string filter, params object[] values);

        public NameFilterExpression(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string label, string filter, params object[] values)
            : base(appCtx, dataCtx, label)
        {
            this.Label = label;
            this.Predicate = filter;
            this.FilterValues = values;
            AllowNullInput = false;
        }

        public override bool Enabled
        {
            get
            {
                return Values.Count > 0 && !string.IsNullOrEmpty(Values[0]);
            }
        }

        #region IPostFilterExpression Members

        public ReadOnlyObservableCollection<DataObjectModel> Execute(IEnumerable<DataObjectModel> instances)
        {
            return new ReadOnlyObservableCollection<DataObjectModel>(
                    new ObservableCollection<DataObjectModel>(
                        instances.Where(
                            o => o.Name.ToLowerInvariant().Contains(Value.ToLowerInvariant())
                            || o.ID.ToString().Contains(Value))));
        }

        #endregion
    }
}
