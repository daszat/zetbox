
namespace Kistl.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;

    public class DtoTabbedViewModel : DtoGroupedViewModel
    {
        public DtoTabbedViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, object debugInfo)
            : base(dependencies, dataCtx, parent, debugInfo)
        {
        }
    }
}
