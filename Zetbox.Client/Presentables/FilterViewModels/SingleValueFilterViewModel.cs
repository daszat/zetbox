using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using System.Collections.ObjectModel;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.Models;

namespace Zetbox.Client.Presentables.FilterViewModels
{
    [ViewModelDescriptor]
    public class SingleValueFilterViewModel : FilterViewModel
    {
        public new delegate SingleValueFilterViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl);

        public SingleValueFilterViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl)
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
