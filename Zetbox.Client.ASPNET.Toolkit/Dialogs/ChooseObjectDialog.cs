using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

[assembly: WebResource("Zetbox.Client.ASPNET.Toolkit.Dialogs.ChooseObjectDialog.js", "text/javascript")] 

namespace Zetbox.Client.ASPNET.Toolkit.Dialogs
{
    public abstract class ChooseObjectDialog : System.Web.UI.UserControl, IScriptControl
    {
        protected abstract Panel PanelChooseObjectControl { get; }

        public System.Collections.Generic.IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            yield return new ScriptControlDescriptor("Zetbox.Client.ASPNET.ChooseObjectDialog", 
                PanelChooseObjectControl.ClientID);
        }

        public System.Collections.Generic.IEnumerable<ScriptReference> GetScriptReferences()
        {
            // typeof(thisclass) is important!
            // This is a UserControl. ASP.NET will derive from this class.
            // this.GetType() wont return a Type, where Assembly is set to this Assembly
            // -> use typeof(thisclass) instead
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(ChooseObjectDialog), "Zetbox.Client.ASPNET.Toolkit.Dialogs.ChooseObjectDialog.js"));
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
}
