using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Kistl.API;
using Kistl.GUI;
using Kistl.Client.ASPNET.Toolkit.Pages;
using Kistl.Client.Presentables;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.JavascriptRenderer.js", "text/javascript")] 

namespace Kistl.Client.ASPNET.Toolkit
{
    [ToolboxData("<{0}:JavascriptRenderer runat=server></{0}:JavascriptRenderer>")]
    public class JavascriptRenderer : Control, IScriptControl, IPostBackEventHandler
    {
        public const string actionShowObject = "ShowObject";

        // HiddenControls
        string hdAction = "";
        string hdArgument = "";

        public JavascriptRenderer()
        {
            this.Init += new EventHandler(JavascriptRenderer_Init);
        }

        void JavascriptRenderer_Init(object sender, EventArgs e)
        {
            hdAction = Page.Request["__JavascriptRenderer_Action"] ?? "";
            hdArgument = Page.Request["__JavascriptRenderer_Argument"] ?? "";
        }

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            yield return new ScriptControlDescriptor("Kistl.Client.ASPNET.JavascriptRenderer", ClientID);
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(JavascriptRenderer), "Kistl.Client.ASPNET.Toolkit.JavascriptRenderer.js"));
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
            string script = string.Format("function __JavascriptRenderer_PostBack() {{ {0}; }}\n", 
                Page.ClientScript.GetPostBackEventReference(this, ""));
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PostBackScript", script, true);
        }

        private void RenderHiddenInput(HtmlTextWriter writer, string name, string value)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, name);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, name);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            RenderHiddenInput(writer, "__JavascriptRenderer_Action", hdAction);
            RenderHiddenInput(writer, "__JavascriptRenderer_Argument", hdArgument);           
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            switch (hdAction)
            {
                case actionShowObject:
                    {
                        DataObjectModel obj = hdArgument.FromJSON(KistlContextManagerModule.KistlContext);
                        if (HttpContext.Current.CurrentHandler is IWorkspaceView)
                        {
                            IWorkspaceView page = (IWorkspaceView)HttpContext.Current.CurrentHandler;
                            page.ShowObject(obj);
                        }
                        else
                        {
                            throw new InvalidOperationException("ShowObject can only be executed on a IWorkspaceView Page");
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Action " + hdAction + " is unknown");
            }
        }
    }
}
