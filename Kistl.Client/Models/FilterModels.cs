using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.App.Base;
using Kistl.App.GUI;
using System.Data;
using System.Collections;
using Kistl.Client.Presentables.ValueViewModels;
using System.ComponentModel;

namespace Kistl.Client.Models
{
    public class FilterEvaluator
    {
        public List<FilterModel> Filters { get; private set; }
        public IQueryable Execute(IKistlContext ctx)
        {
            return null;
        }
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
            base.Label = "Name";
            base.ViewModelType = frozenCtx.FindPersistenceObject<ViewModelDescriptor>(new Guid("4ff2b6ec-a47f-431b-aa6d-d10b39f8d628")); // Kistl.Client.Presentables.FilterViewModels.SingleValueFilterViewModel;
            base.FilterArguments.Add(new FilterArgumentConfig(new ClassValueModel<string>(base.Label, "", true, false), frozenCtx.FindPersistenceObject<ViewModelDescriptor>(new Guid("975eee82-e7e1-4a12-ab43-d2e3bc3766e4")))); // ClassValueViewModel<string>
        }

        public override IEnumerable GetResult(IEnumerable src)
        {
            var pattern = FilterArgument.Value.GetUntypedValue().ToString().ToLowerInvariant();
            return src.AsQueryable().Cast<object>().Where(o => o.ToString().ToLowerInvariant().Contains(pattern));
        }
    }

    public class SingleValueFilterModel : FilterModel
    {
        protected override string GetPredicate()
        {
            return string.Format("{0} = @0", ValueSource.Expression);
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


    //public class CollectionConstantFilterModel : FilterModel
    //{
    //    public CollectionConstantFilterModel(Property prop)
    //        : base()
    //    {
    //    }

    //    public bool Enabled { get; set; }

    //    public int? MinCount { get; set; }
    //    public int? MaxCount { get; set; }

    //    public ObservableCollection<IDataObject> Contains { get; private set; }
    //    public ObservableCollection<IDataObject> DoesNotContain { get; private set; }

    //    public string GetPredicate()
    //    {
    //        return String.Format("...");
    //    }
    //}
}
