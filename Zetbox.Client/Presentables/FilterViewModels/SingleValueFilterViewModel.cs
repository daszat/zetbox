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
    [ViewModelDescriptor]
    public class SingleValueFilterViewModel : FilterViewModel
    {
        public new delegate SingleValueFilterViewModel Factory(IKistlContext dataCtx, ViewModel parent, IUIFilterModel mdl);

        public SingleValueFilterViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IUIFilterModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
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
