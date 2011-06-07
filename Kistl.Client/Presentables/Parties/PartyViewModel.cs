namespace Kistl.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
using ZBox.Basic.Parties;

    /// <summary>
    /// No viewmodel decriptor - Party is abstract
    /// </summary>
    public class PartyViewModel : DataObjectViewModel
    {
        public new delegate PartyViewModel Factory(IKistlContext dataCtx, ViewModel parent,
            IDataObject obj);

        public PartyViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            Party obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Party = obj;
        }

        public Party Party { get; private set; }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var groups = base.CreatePropertyGroups();

            foreach (var iftRole in Party.PartyRole.ToList().GroupBy(k => DataContext.GetInterfaceType(k)))
            {
                var mdl = ViewModelFactory.CreateViewModel<PartyRolesViewModel.Factory>().Invoke(DataContext, this, Party, iftRole.Key);
                var lblMdl = ViewModelFactory.CreateViewModel<LabeledViewContainerViewModel.Factory>().Invoke(DataContext, this, "Role", "", mdl);
                var propGrpMdl = ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(DataContext, this, iftRole.Key.Type.Name, new ViewModel[] { lblMdl });
                groups.Add(propGrpMdl);
            }

            return groups;
        }
    }
}
