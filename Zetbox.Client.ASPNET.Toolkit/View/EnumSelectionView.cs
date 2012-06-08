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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;
using Zetbox.Client.GUI;
using System.Web.UI.WebControls;
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/EnumSelectionView.ascx")]
    public abstract class EnumSelectionView: ModelUserControl<EnumerationValueViewModel>
    {
        protected abstract ListControl listCtrl { get; }

        public EnumSelectionView()
        {
            this.Init += new EventHandler(EnumSelectionView_Init);
        }

        void EnumSelectionView_Init(object sender, EventArgs e)
        {
            listCtrl.DataTextField = "Value";
            listCtrl.DataValueField = "Key";
            listCtrl.DataSource = Model.PossibleValues;
            listCtrl.DataBind();
            listCtrl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            listCtrl.SelectedValue = Model.Value != null ? Model.Value.ToString() : String.Empty;
        }
    }
}
