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
    public class OptionalPredicateFilterViewModel : FilterViewModel
    {
        public new delegate OptionalPredicateFilterViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl);

        public OptionalPredicateFilterViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IUIFilterModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }
    }
}
