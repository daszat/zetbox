namespace Kistl.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;

    [ViewModelDescriptor]
    public class OrganizationViewModel : PartyViewModel
    {
        public new delegate OrganizationViewModel Factory(IKistlContext dataCtx,
            IDataObject obj);

        public OrganizationViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj)
            : base(appCtx, dataCtx, obj)
        {
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }
    }
}
