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

namespace Kistl.Client.Presentables.KistlBase
{
    public class FilterEvaluator
    {
        public List<FilterModel> Filters { get; private set; }
        public IQueryable Execute(IKistlContext ctx)
        {
            return null;
        }
    }

    public abstract class FilterModel
    {
        public FilterModel()
        {
            FilterArguments = new List<object>();
        }

        // Goes to Linq
        public string Predicate { get; protected set; }
        public List<object> FilterArguments { get; private set; }
        public object FilterArgument
        {
            get
            {
                if (FilterArguments.Count == 0) return null;
                return FilterArguments[0];
            }
            set
            {
                if (value == null)
                {
                    FilterArguments.RemoveAt(0);
                }
                else if (FilterArguments.Count == 0)
                {
                    FilterArguments.Add(value);
                }
                else
                {
                    FilterArguments[0] = value;
                }
            }
        }

        public virtual IQueryable GetQuery(IQueryable src)
        {
            if (!string.IsNullOrEmpty(Predicate))
            {
                return src.Where(Predicate, FilterArguments.ToArray());
            }
            else
            {
                return src;
            }
        }

        public virtual IEnumerable<object> GetResult(IEnumerable<object> src)
        {
            return src;
        }
    }

    public class PostFilterModel : FilterModel
    {
    }

    public class ToStringFilterModel : PostFilterModel
    {
        public override IEnumerable<object> GetResult(IEnumerable<object> src)
        {
            var pattern = FilterArgument.ToString().ToLowerInvariant();
            return src.Where(o => o.ToString().ToLowerInvariant().Contains(pattern));
        }
    }

    //public abstract class ObjectClassFilterModel : FilterModel
    //{
    //    public override IQueryable GetQuery(IQueryable src)
    //    {
    //        return GetObjectClassQuery(src.Cast<ObjectClass>());
    //    }
    //    public abstract IQueryable<ObjectClass> GetObjectClassQuery(IQueryable<ObjectClass> src);
    //}

    public class PropertyFilterModel : FilterModel
    {
        public PropertyFilterModel(Property prop)
        {
            this.Property = prop;
        }

        private Property _prop;
        public Property Property
        {
            get
            {
                return _prop;
            }
            set
            {
                if (_prop != value)
                {
                    _prop = value;
                    Predicate = string.Format("{0} = @0", _prop.Name);
                }
            }
        }
    }

    public class LinqFilterModel : FilterModel
    {
    }

    public class FilterViewModel
    {
        // Input from UI
        public ObservableCollection<IValueViewModel> Arguments;
    }
}
