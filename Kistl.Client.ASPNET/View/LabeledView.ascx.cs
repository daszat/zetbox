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

public partial class View_LabeledView : Kistl.Client.ASPNET.Toolkit.View.LabeledView
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override Label lbCtrl
    {
        get { return lb; }
    }

    protected override Control containerCtrl
    {
        get { return container; }
    }
}
