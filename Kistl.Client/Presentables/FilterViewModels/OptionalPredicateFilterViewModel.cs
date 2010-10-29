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
    public class OptionalPredicateFilterViewModel : FilterViewModel
    {
        public new delegate OptionalPredicateFilterViewModel Factory(IKistlContext dataCtx, IUIFilterModel mdl);

        public OptionalPredicateFilterViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IUIFilterModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }
    }
}
