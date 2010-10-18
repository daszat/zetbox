using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.Models;

namespace Kistl.Client.Presentables.FilterViewModels
{
    public abstract class FilterViewModel : ViewModel
    {
        public new delegate FilterViewModel Factory(IKistlContext dataCtx, IUIFilterModel mdl);

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
                            .Select(f =>
                                ViewModelFactory
                                    .CreateViewModel<BaseValueViewModel.Factory>(f.ViewModelType)
                                    .Invoke(DataContext, f.Value))
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
            get { return "A Filter"; }
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

    [ViewModelDescriptor("KistlBase", DefaultKind = "Kistl.App.GUI.SingleValueFilterKind", Description = "FilterViewModel for single value filters")]
    public class SingleValueFilterViewModel : FilterViewModel
    {
        public new delegate SingleValueFilterViewModel Factory(IKistlContext dataCtx, IUIFilterModel mdl);

        public SingleValueFilterViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IUIFilterModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }

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
    }
}
