using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Kistl.API;
using Kistl.Client.ASPNET.Toolkit.Pages;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.View.LauncherView.js", "text/javascript")] 

namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/LauncherView.ascx")]
    public abstract class LauchnerView : ModelUserControl<WorkspaceModel>, IScriptControl
    {
        protected abstract AjaxDataControls.DataList listModulesCtrl { get; }
        protected abstract AjaxDataControls.DataList listObjectClassesCtrl { get; }
        protected abstract AjaxDataControls.DataList listInstancesCtrl { get; }
        protected abstract Control containerCtrl { get; }

        #region Render
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager == null) throw new InvalidOperationException("ScriptManager required on the page.");
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
        #endregion

        #region IScriptControl Members

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.View.LauncherView", containerCtrl.ClientID);
            desc.AddComponentProperty("ListModules", listModulesCtrl.ClientID);
            desc.AddComponentProperty("ListObjectClasses", listObjectClassesCtrl.ClientID);
            desc.AddComponentProperty("ListInstances", listInstancesCtrl.ClientID);
            yield return desc;
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(WorkspaceView), "Kistl.Client.ASPNET.Toolkit.View.LauncherView.js"));
        }

        #endregion
    }
}
