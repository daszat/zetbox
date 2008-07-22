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
using Kistl.GUI;
using System.Collections.Generic;
using Kistl.API;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using Kistl.Client.ASPNET.Toolkit;

public partial class Controls_ObjectListControl : Kistl.Client.ASPNET.Toolkit.Controls.ObjectListControl
{
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
