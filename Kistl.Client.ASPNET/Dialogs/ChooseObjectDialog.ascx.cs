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

public partial class Dialogs_ChooseObjectDialog : System.Web.UI.UserControl, IScriptControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Include_ChooseObjectDialog", ResolveClientUrl("ChooseObjectDialog.js"));
    }

    public System.Collections.Generic.IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
        ScriptControlDescriptor desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.ChooseObjectDialog", panelChooseObject.ClientID);
        yield return desc;
    }

    public System.Collections.Generic.IEnumerable<ScriptReference> GetScriptReferences()
    {
        yield return new ScriptReference(Page.ResolveUrl("~/Dialogs/ChooseObjectDialog.js"));
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
        if (scriptManager == null)
        {
            throw new InvalidOperationException(
              "ScriptManager required on the page.");
        }

        scriptManager.RegisterScriptControl(this);
    }

    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);

        if (!DesignMode)
        {
            ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
        }
    }
}
