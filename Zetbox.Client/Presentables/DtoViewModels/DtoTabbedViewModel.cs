
namespace Zetbox.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;

    public class DtoTabbedViewModel : DtoGroupedViewModel
    {
        public DtoTabbedViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
        {
        }
    }
}
