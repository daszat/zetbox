namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class PropertiesPrewiewViewModel : ViewModel
    {
        public new delegate PropertiesPrewiewViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public PropertiesPrewiewViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "PropertiesPrewiewViewModel"; }
        }
    }
}
