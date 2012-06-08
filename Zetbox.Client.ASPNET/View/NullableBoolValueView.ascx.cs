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

public partial class View_NullableBoolValueView : Zetbox.Client.ASPNET.Toolkit.View.NullableBoolValueView
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override RadioButton rbTrueCtrl
    {
        get { return rbTrue; }
    }

    protected override RadioButton rbFalseCtrl
    {
        get { return rbFalse; }
    }

    protected override RadioButton rbNullCtrl
    {
        get { return rbNull; }
    }
}
