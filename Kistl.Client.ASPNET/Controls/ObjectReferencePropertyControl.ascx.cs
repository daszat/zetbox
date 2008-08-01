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
using Kistl.API;

public partial class Controls_ObjectReferencePropertyControl : Kistl.Client.ASPNET.Toolkit.Controls.ObjectReferencePropertyControl
{
    protected override DropDownList cbListControl
    {
        get { return cbList; }
    }

    protected override HtmlControl btnNewControl
    {
        get { return btnNew; }
    }

    protected override HtmlControl btnOpenControl
    {
        get { return btnOpen; }
    }
}
