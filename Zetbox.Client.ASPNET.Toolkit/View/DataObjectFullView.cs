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
using System.Web.UI.WebControls;
using System.Web.UI;

using Zetbox.Client.Presentables;
using Zetbox.Client.GUI;

namespace Zetbox.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/DataObjectFullView.ascx")]
    public abstract class DataObjectFullView : ModelUserControl<DataObjectViewModel>
    {
        protected abstract Literal litTitleCtrl { get; }
        protected abstract Repeater repPropertiesCtrl { get; }

        public DataObjectFullView()
        {
            this.Init += new EventHandler(DataObjectFullView_Init);
        }

        void DataObjectFullView_Init(object sender, EventArgs e)
        {
            litTitleCtrl.Text = Model.Name;
            repPropertiesCtrl.ItemDataBound += new RepeaterItemEventHandler(repProperties_OnItemDataBound);
            repPropertiesCtrl.DataSource = Model.PropertyGroups;
            repPropertiesCtrl.DataBind();
        }

        protected void repProperties_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ViewModel mdl = (ViewModel)e.Item.DataItem;
            Control divPlaceHolder = e.Item.FindControl("divPlaceHolder");

            ZetboxContextManagerModule.ViewModelFactory.CreateDefaultView(mdl, divPlaceHolder);
        }
    }
}
