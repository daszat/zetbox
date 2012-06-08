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
using Kistl.Client.Presentables;
using Kistl.Client.ASPNET.Toolkit;
using Kistl.Client;
using Kistl.Client.ASPNET.Toolkit.View;

public partial class View_DataObjectFullView : Kistl.Client.ASPNET.Toolkit.View.DataObjectFullView
{
    protected override Literal litTitleCtrl { get { return litTitle; } }
    protected override Repeater repPropertiesCtrl { get { return repProperties; } }
}
