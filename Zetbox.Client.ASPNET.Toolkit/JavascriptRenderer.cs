// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Zetbox.API;
using Zetbox.Client.ASPNET.Toolkit.Pages;
using Zetbox.Client.Presentables;

[assembly: WebResource("Zetbox.Client.ASPNET.Toolkit.JavascriptRenderer.js", "text/javascript")] 

namespace Zetbox.Client.ASPNET.Toolkit
{
    [ToolboxData("<{0}:JavascriptRenderer runat=server></{0}:JavascriptRenderer>")]
    public class JavascriptRenderer : Control, IScriptControl, IPostBackEventHandler
    {
        private const string actionShowObject = "ShowObject";

        // HiddenControls
        string hdAction = String.Empty;
        string hdArgument = String.Empty;

        public JavascriptRenderer()
        {
            this.Init += new EventHandler(JavascriptRenderer_Init);
        }

        void JavascriptRenderer_Init(object sender, EventArgs e)
        {
            hdAction = Page.Request["__JavascriptRenderer_Action"] ?? String.Empty;
            hdArgument = Page.Request["__JavascriptRenderer_Argument"] ?? String.Empty;
        }

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            yield return new ScriptControlDescriptor("Zetbox.Client.ASPNET.JavascriptRenderer", ClientID);
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(JavascriptRenderer), "Zetbox.Client.ASPNET.Toolkit.JavascriptRenderer.js"));
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
                Page.ClientScript.GetPostBackEventReference(this, String.Empty));
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
                        DataObjectViewModel obj = hdArgument.FromJSON(ZetboxContextManagerModule.ZetboxContext);
                        ZetboxContextManagerModule.ViewModelFactory.ShowModel(obj, true);
                        //if (HttpContext.Current.CurrentHandler is IWorkspaceView)
                        //{
                        //    IWorkspaceView page = (IWorkspaceView)HttpContext.Current.CurrentHandler;
                        //    page.ShowObject(obj);
                        //}
                        //else
                        //{
                        //    throw new InvalidOperationException("ShowObject can only be executed on a IWorkspaceView Page");
                        //}
                        break;
                    }
                default:
                    throw new ArgumentException("Action " + hdAction + " is unknown");
            }
        }
    }
}
