namespace Zetbox.Client.ASPNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class SearchViewModel : ViewModel
    {
        public new delegate SearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public SearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "SearchViewModel"; }
        }
    }
}
