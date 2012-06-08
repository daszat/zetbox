// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class RoleMembershipViewModel : DataObjectViewModel
    {
        public new delegate RoleMembershipViewModel Factory(IZetboxContext dataCtx, ViewModel parent, RoleMembership roleMembership);

        public RoleMembershipViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
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
