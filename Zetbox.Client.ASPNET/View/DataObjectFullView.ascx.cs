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
using Zetbox.Client.Presentables;
using Zetbox.Client.ASPNET.Toolkit;
using Zetbox.Client;
using Zetbox.Client.ASPNET.Toolkit.View;

public partial class View_DataObjectFullView : Zetbox.Client.ASPNET.Toolkit.View.DataObjectFullView
{
    protected override Literal litTitleCtrl { get { return litTitle; } }
    protected override Repeater repPropertiesCtrl { get { return repProperties; } }
}
