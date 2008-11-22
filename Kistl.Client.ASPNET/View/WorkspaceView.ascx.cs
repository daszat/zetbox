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
using Kistl.Client.ASPNET.Toolkit;
using Kistl.Client;

public partial class View_WorkspaceView : Kistl.Client.ASPNET.Toolkit.View.WorkspaceView
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(Model.Modules.First());
        divObjectPlaceholder.Controls.Add(loader.LoadControl(Page));
    }
}
