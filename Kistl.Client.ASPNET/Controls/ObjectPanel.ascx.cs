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

public partial class Controls_ObjectPanel : System.Web.UI.UserControl, IContainerLoader, IObjectControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void AddChild(IControlLoader child)
    {
        divChildren.Controls.Add((Control)child);
    }

    protected void OnSave(object sender, EventArgs e)
    {
        if (UserSaveRequest != null)
        {
            UserSaveRequest(this, EventArgs.Empty);
        }
    }

    #region IBasicControl Members
    IKistlContext IBasicControl.Context { get; set; }

    public string ShortLabel
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public FieldSize Size
    {
        get;
        set;
    }

    #endregion

    #region IObjectControl Members

    public Kistl.API.IDataObject Value
    {
        get;
        set;
    }

    public event EventHandler UserInput;
    public event EventHandler UserSaveRequest;
    public event EventHandler UserDeleteRequest;

    #endregion
}
