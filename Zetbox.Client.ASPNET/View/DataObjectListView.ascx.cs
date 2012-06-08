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

public partial class View_DataObjectListView : Zetbox.Client.ASPNET.Toolkit.View.DataObjectListView
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override HiddenField HdItemsControl
    {
        get { return hdItems; }
    }

    protected override AjaxDataControls.DataList LstItemsControl
    {
        get { return lstItems; }
    }

    protected override Control ContainerControl
    {
        get { return container; }
    }

    protected override Control LnkAddControl
    {
        get { return lnkAdd; }
    }

    protected override Control LnkNewControl
    {
        get { return lnkNew; }
    }
}
