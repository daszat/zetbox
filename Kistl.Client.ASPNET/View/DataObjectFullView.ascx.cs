using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Kistl.Client.Presentables;
using Kistl.Client.ASPNET.Toolkit;
using Kistl.Client;

public partial class View_DataObjectFullView : Kistl.Client.ASPNET.Toolkit.View.DataObjectFullView
{
    protected void Page_Load(object sender, EventArgs e)
    {
        litTitle.Text = Model.Name;

        foreach (PresentableModel mdl in Model.PropertyModels)
        {
            var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(mdl);
            divPropertiesPlaceholder.Controls.Add(loader.LoadControl(Page));
        }
    }
}
