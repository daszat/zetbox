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
    public class OrganizationViewModel : PartyViewModel
    {
        public new delegate OrganizationViewModel Factory(IKistlContext dataCtx, ViewModel parent,
            IDataObject obj);

        public OrganizationViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            Organization obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }
    }
}
