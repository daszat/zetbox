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

public partial class View_DataObjectReferenceView : Kistl.Client.ASPNET.Toolkit.View.DataObjectReferenceView
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override Label lbItemCtrl
    {
        get { return lbItem; }
    }

    protected override HtmlControl btnNewCtrl
    {
        get { return btnNew; }
    }

    protected override HtmlControl btnOpenCtrl
    {
        get { return btnOpen; }
    }

    protected override Control containerCtrl
    {
        get { return container; }
    }
}
