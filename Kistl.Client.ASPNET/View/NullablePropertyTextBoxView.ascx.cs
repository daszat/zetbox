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

public partial class View_NullablePropertyTextBoxView : Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxView
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected override Label lbCtrl
    {
        get { return lbLabel; }
    }

    protected override TextBox txtCtrl
    {
        get { return txtString; }
    }
}
