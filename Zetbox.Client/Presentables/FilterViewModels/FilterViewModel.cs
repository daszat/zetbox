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

namespace Zetbox.Client.Presentables.FilterViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Not a real view model
    /// Used to display internal enum FilterOperators
    /// </summary>
    public class OperatorViewModel
    {
        public OperatorViewModel()
        {
            _Operator = FilterOperators.Equals;
            Name = FilterViewModelResources.OperatorEquals;
        }

        public OperatorViewModel(FilterOperators op)
            : this()
        {
            Operator = op;
        }

        private FilterOperators _Operator;
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
                            Name = FilterViewModelResources.OperatorEquals;
                            break;
                        case FilterOperators.Contains:
                            Name = FilterViewModelResources.OperatorContains;
                            break;
                        case FilterOperators.Less:
                            Name = FilterViewModelResources.OperatorLess;
                            break;
                        case FilterOperators.LessOrEqual:
                            Name = FilterViewModelResources.OperatorLessOrEqual;
                            break;
                        case FilterOperators.Greater:
                            Name = FilterViewModelResources.OperatorGreater;
                            break;
                        case FilterOperators.GreaterOrEqual:
                            Name = FilterViewModelResources.OperatorGreaterOrEqual;
                            break;
                        case FilterOperators.Not:
                            Name = FilterViewModelResources.OperatorNot;
                            break;
                        case FilterOperators.IsNull:
                            Name = FilterViewModelResources.OperatorIsNull;
                            break;
                        case FilterOperators.IsNotNull:
                            Name = FilterViewModelResources.OperatorIsNotNull;
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
        public new delegate FilterViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl);

        public static FilterViewModel Fetch(IViewModelFactory f, IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl)
        {
            return (FilterViewModel)dataCtx.GetViewModelCache(f.PerfCounter).LookupOrCreate(mdl, () => f.CreateViewModel<FilterViewModel.Factory>(mdl.ViewModelType).Invoke(dataCtx, parent, mdl));
        }

        public FilterViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl)
            : base(dependencies, dataCtx, parent)
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
                            .Select(f =>
                            {
                                var mdl = BaseValueViewModel.Fetch(ViewModelFactory, DataContext, this, f.ViewModelType, f.Value);
                                // I know, a hack, but better then allowing user to create new objects during a search
                                if (mdl is ObjectReferenceViewModel)
                                {
                                    var objRefMdl = (ObjectReferenceViewModel)mdl;
                                    objRefMdl.AllowCreateNewItem = false;
                                    objRefMdl.AllowDelete = false;
                                }
                                return mdl;
                            })
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
                return RespectRequiredFilter && Filter.Required;
            }
        }

        private bool _RespectRequiredFilter = true;
        /// <summary>
        /// If set to false, no filter is required. Default value is true. Use this setting if a small, preselected list (query) is provides as data source.
        /// </summary>
        public bool RespectRequiredFilter
        {
            get
            {
                return _RespectRequiredFilter;
            }
            set
            {
                if (_RespectRequiredFilter != value)
                {
                    _RespectRequiredFilter = value;
                    OnPropertyChanged("RespectRequiredFilter");
                    OnPropertyChanged("Required");
                }
            }
        }
    }
}
