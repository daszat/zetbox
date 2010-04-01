using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/DataObjectFullView.ascx")]
    public abstract class DataObjectFullView : ModelUserControl<DataObjectModel>
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

            GuiApplicationContext.Current.Factory.CreateDefaultView(mdl, divPlaceHolder);
        }
    }
}
