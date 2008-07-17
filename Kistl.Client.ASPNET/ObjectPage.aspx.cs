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

public partial class ObjectPage : System.Web.UI.Page, IWorkspaceControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        CreateControls();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    List<IDataObject> _Objects;
    public List<IDataObject> Objects
    {
        get
        {
            if (_Objects == null)
            {
                if (!IsPostBack)
                {
                    _Objects = new List<IDataObject>();
                    // Parse Request
                    if (string.IsNullOrEmpty(Request["Type"])
                        || string.IsNullOrEmpty(Request["ID"]))
                    {
                        throw new ArgumentNullException("Type, ID", "Type and ID must not be null");
                    }
                    IDataObject obj = KistlContextManagerModule.KistlContext
                                        .Find(Type.GetType(Request["Type"] + ",Kistl.Objects.Client"), int.Parse(Request["ID"]));
                    _Objects.Add(obj);
                }
                else
                {
                    // We are to early! So we need to parse the Request Variable directly
                    // If anyone knows a better way -> pls. get in touch with me.
                    _Objects = Request[hdObjects.UniqueID].FromJSONArray(KistlContextManagerModule.KistlContext)
                                    .ToList();
                }
            }

            return _Objects;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        hdObjects.Value = Objects.ToJSONArray();
    }

    private void ShowObjectInternal(IDataObject obj)
    {
        var template = obj.FindTemplate(TemplateUsage.EditControl);
        var widget = (Control)Manager.Renderer.CreateControl(obj, template.VisualTree);

        AjaxControlToolkit.TabPanel panel = new AjaxControlToolkit.TabPanel();
        panel.Controls.Add(widget);
        panel.HeaderText = obj.ToString();

        tabObjects.Tabs.Add(panel);
    }

    private void CreateControls()
    {
        foreach (IDataObject obj in Objects)
        {
            ShowObjectInternal(obj);
        }

        if (tabObjects.ActiveTab == null)
        {
            tabObjects.ActiveTabIndex = 0;
        }
    }

    #region IWorkspaceControl Members

    public void ShowObject(IDataObject obj, IBasicControl ctrl)
    {
        Objects.Add(obj);
        ShowObjectInternal(obj);
    }

    public event EventHandler UserSaveRequest;

    public event EventHandler UserAbortRequest;

    public event EventHandler UserNewObjectRequest;

    #endregion

    #region IBasicControl Members

    public string ShortLabel { get; set; }
    public string Description { get; set; }
    public FieldSize Size { get; set; }
    IKistlContext IBasicControl.Context { get; set; }

    #endregion
}
