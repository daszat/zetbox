
namespace Kistl.Client.Presentables.FilterViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Not a real view model
    /// Used to display internal enum FilterOperators
    /// </summary>
    public class OperatorViewModel
    {
        public OperatorViewModel()
        {
            Name = "=";
        }

        public OperatorViewModel(FilterOperators op)
            : this()
        {
            Operator = op;
        }

        private FilterOperators _Operator = FilterOperators.Equals;
        public FilterOperators Operator
        {
            get
            {
                return _Operator;
            }
            set
            {
                if (_Operator != value)
                {
                    _Operator = value;
                    switch (_Operator)
                    {
                        case FilterOperators.Equals:
                            Name = "=";
                            break;
                        case FilterOperators.Contains:
                            Name = "*";
                            break;
                        case FilterOperators.Less:
                            Name = "<";
                            break;
                        case FilterOperators.LessOrEqual:
                            Name = "<=";
                            break;
                        case FilterOperators.Greater:
                            Name = ">";
                            break;
                        case FilterOperators.GreaterOrEqual:
                            Name = ">=";
                            break;
                        case FilterOperators.Not:
                            Name = "!";
                            break;
                        case FilterOperators.IsNull:
                            Name = "is null";
                            break;
                        case FilterOperators.IsNotNull:
                            Name = "is not null";
                            break;
                        default:
                            Name = _Operator.ToString();
                            break;
                    }
                }
            }
        }
        public string Name { get; set; }
    }

    public abstract class FilterViewModel : ViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate FilterViewModel Factory(IKistlContext dataCtx, IUIFilterModel mdl);
#else
        public new delegate FilterViewModel Factory(IKistlContext dataCtx, IUIFilterModel mdl);
#endif

        

        public FilterViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IUIFilterModel mdl)
            : base(dependencies, dataCtx)
        {
            this.Filter = mdl;
            this._label = mdl.Label;
        }

        public IUIFilterModel Filter { get; private set; }

        private ObservableCollection<IValueViewModel> _Arguments = null;
        public ObservableCollection<IValueViewModel> Arguments
        {
            get
            {
                if (_Arguments == null)
                {
                    _Arguments = new ObservableCollection<IValueViewModel>(
                        Filter.FilterArguments
                            .Select(f => BaseValueViewModel.Fetch(ViewModelFactory, DataContext, f.ViewModelType, f.Value))
                            .Cast<IValueViewModel>()
                    );
                }
                return _Arguments;
            }
        }

        public IValueViewModel Argument
        {
            get
            {
                return Arguments.Single();
            }
        }

        public override string Name
        {
            get { return FilterViewModelResources.Name; }
        }

        private string _label;
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        public bool Required
        {
            get
            {
                return Filter.Required;
            }
        }
    }
}
