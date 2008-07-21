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
using Kistl.Client.ASPNET.Toolkit;
using Kistl.GUI;
using Kistl.API;

public partial class Controls_ObjectPanel : Kistl.Client.ASPNET.Toolkit.Controls.ObjectPanelControl
{
    public override HtmlGenericControl divChildrenControl
    {
        get { return divChildren; }
    }

    public override IButtonControl btnSaveControl { get { return btnSave; } }

}
