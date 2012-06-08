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
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.Client;
using Zetbox.Client.Presentables;
using Zetbox.Client.ASPNET.Toolkit;

public partial class _Default : Zetbox.Client.ASPNET.Toolkit.Pages.LauncherPage
{
    protected override Control ctrlMainContent
    {
        get { return divMainContent; }
    }
}