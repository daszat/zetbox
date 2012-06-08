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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Zetbox.API;
using Zetbox.Client.ASPNET.Toolkit.Pages;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/PropertyGroupBoxView.ascx")]
    public abstract class PropertyGroupBoxView : ModelUserControl<PropertyGroupViewModel>
    {
        protected abstract Panel panelCtrl { get; }
        protected abstract Repeater repPropertiesCtrl { get; }

        public PropertyGroupBoxView()
        {
            this.Init += new EventHandler(PropertyGroupBoxView_Init);
        }

        void PropertyGroupBoxView_Init(object sender, EventArgs e)
        {
            panelCtrl.GroupingText = Model.Title;

            repPropertiesCtrl.ItemDataBound += new RepeaterItemEventHandler(repPropertiesCtrl_ItemDataBound);
            repPropertiesCtrl.DataSource = Model is SinglePropertyGroupViewModel ? new ViewModel [] { ((SinglePropertyGroupViewModel)Model).PropertyModel } : ((MultiplePropertyGroupViewModel)Model).PropertyModels.AsEnumerable();
            repPropertiesCtrl.DataBind();
        }

        void repPropertiesCtrl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.In(ListItemType.Item, ListItemType.AlternatingItem))
            {
                var data = (ViewModel)e.Item.DataItem;
                var view = (LabeledView)e.Item.FindControl("lbView");
                view.SetModel(data);
            }
        }
    }
}
