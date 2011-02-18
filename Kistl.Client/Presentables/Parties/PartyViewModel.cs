namespace Kistl.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;

    // No viewmodel decriptor - Party is abstract
    public class PartyViewModel : DataObjectViewModel
    {
        public new delegate PartyViewModel Factory(IKistlContext dataCtx,
            IDataObject obj);

        public PartyViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj)
            : base(appCtx, dataCtx, obj)
        {
        }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var groups = base.CreatePropertyGroups();

            return groups;
        }
    }
}
