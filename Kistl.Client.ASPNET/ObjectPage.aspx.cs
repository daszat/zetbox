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
using Kistl.API;
using Kistl.API.Client;
using Kistl.GUI.DB;
using Kistl.Client;
using Kistl.Client.ASPNET.Toolkit;
using System.Collections.Generic;
using Kistl.GUI;

public partial class ObjectPage : Kistl.Client.ASPNET.Toolkit.Pages.WorkspacePage
{
    protected override HiddenField hdObjectsControl
    {
        get { return hdObjects; }
    }

    protected override AjaxControlToolkit.TabContainer tabObjectsControl
    {
        get { return tabObjects; }
    }
}
