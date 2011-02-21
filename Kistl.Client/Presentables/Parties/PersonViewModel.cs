namespace Kistl.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using ZBox.Basic.Parties;

    [ViewModelDescriptor]
    public class PersonViewModel : PartyViewModel
    {
        public new delegate PersonViewModel Factory(IKistlContext dataCtx,
            IDataObject obj);

        public PersonViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx,
            Party obj)
            : base(appCtx, dataCtx, obj)
        {
        }

    }
}
