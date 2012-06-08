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
