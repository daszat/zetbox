
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.KistlBase;

    [ViewModelDescriptor]
    public class RoleMembershipViewModel : DataObjectViewModel
    {
        public new delegate RoleMembershipViewModel Factory(IKistlContext dataCtx, ViewModel parent, RoleMembership roleMembership);

        public RoleMembershipViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            RoleMembership roleMembership)
            : base(appCtx, dataCtx, parent, roleMembership)
        {
            _roleMembership = roleMembership;
            _roleMembership.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_roleMembership_PropertyChanged);
        }

        void _roleMembership_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ObjectClass")
            {
                UpdateStartingObjectClass();
            }
        }

        private RoleMembership _roleMembership;

        protected override void OnPropertyModelsByNameCreated()
        {
            base.OnPropertyModelsByNameCreated();

            UpdateStartingObjectClass();
        }

        private void UpdateStartingObjectClass()
        {
            var relChainMdl = (RelationChainViewModel)PropertyModelsByName["Relations"];
            relChainMdl.StartingObjectClass = _roleMembership.ObjectClass != null
                ? DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, _roleMembership.ObjectClass)
                : null;
        }
    }
}
