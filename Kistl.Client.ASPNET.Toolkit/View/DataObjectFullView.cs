using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public abstract class DataObjectFullView : System.Web.UI.UserControl, IView
    {
        protected DataObjectModel Model { get; private set; }
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
            repPropertiesCtrl.DataSource = Model.PropertyModels;
            repPropertiesCtrl.DataBind();
        }

        protected void repProperties_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            PresentableModel mdl = (PresentableModel)e.Item.DataItem;
            Control divPlaceHolder = e.Item.FindControl("divPlaceHolder");

            var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(mdl);
            divPlaceHolder.Controls.Add(loader.LoadControl(Page));
        }

        public void SetModel(PresentableModel mdl)
        {
            Model = (DataObjectModel)mdl;
        }
    }
}
