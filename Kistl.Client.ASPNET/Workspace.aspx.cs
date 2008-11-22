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
using Kistl.Client;
using Kistl.Client.ASPNET.Toolkit;
using Kistl.Client.Presentables;

public partial class Workspace : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var initialWorkspace = GuiApplicationContext.Current.Factory
            .CreateSpecificModel<WorkspaceModel>(KistlContextManagerModule.KistlContext);
        var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(initialWorkspace);
        mainContent.Controls.Add(loader.LoadControl(this));
    }
}
