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
using Kistl.Client;
using Kistl.Client.Presentables;
using Kistl.Client.ASPNET.Toolkit;

public partial class _Default : Kistl.Client.ASPNET.Toolkit.Pages.LauncherPage
{
    protected override Control ctrlMainContent
    {
        get { return divMainContent; }
    }
}