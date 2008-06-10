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

public partial class ObjectPage : System.Web.UI.Page
{
    IKistlContext ctx;
    protected void Page_Init(object sender, EventArgs e)
    {
        ctx = KistlContext.GetContext();
        CreateControls();
    }

    public override void Dispose()
    {
        base.Dispose();
        if (ctx != null)
        {
            ctx.Dispose();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void CreateControls()
    {
        IDataObject obj = ctx.Find(new ObjectType(Request["Type"]), int.Parse(Request["ID"]));

        var template = obj.FindTemplate(TemplateUsage.EditControl);
        var widget = (Control)Manager.Renderer.CreateControl(obj, template.VisualTree);

        this.divMainPanel.Controls.Add(widget);
    }
}
