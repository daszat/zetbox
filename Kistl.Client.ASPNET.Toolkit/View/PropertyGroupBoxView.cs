using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Kistl.API;
using Kistl.Client.ASPNET.Toolkit.Pages;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/PropertyGroupBoxView.ascx")]
    public abstract class PropertyGroupBoxView : ModelUserControl<PropertyGroupModel>
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
            repPropertiesCtrl.DataSource = Model.PropertyModels;
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
