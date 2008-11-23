using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.API;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.View.WorkspaceView.js", "text/javascript")] 

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public abstract class WorkspaceView : System.Web.UI.UserControl, IView, IScriptControl
    {
        protected WorkspaceModel Model { get; private set; }
        protected abstract AjaxDataControls.DataList listModulesCtrl { get; }
        protected abstract AjaxDataControls.DataList listObjectClassesCtrl { get; }
        protected abstract AjaxDataControls.DataList listInstancesCtrl { get; }
        protected abstract AjaxDataControls.DataList listRecentObjectsCtrl { get; }
        protected abstract Control containerCtrl { get; }
        protected abstract AjaxControlToolkit.TabContainer tabObjectsControl { get; }

        public WorkspaceView()
        {
            this.Load += new EventHandler(WorkspaceView_Load);
        }

        void WorkspaceView_Load(object sender, EventArgs e)
        {
        }

        public void SetModel(PresentableModel mdl)
        {
            Model = (WorkspaceModel)mdl;
        }

        private void ShowObjectInternal(IDataObject obj)
        {
            var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(Model.Modules.First());
            var ctrl = loader.LoadControl(Page);

            AjaxControlToolkit.TabPanel panel = new AjaxControlToolkit.TabPanel();
            panel.Controls.Add(ctrl);
            panel.HeaderText = obj.ToString();

            tabObjectsControl.Tabs.Add(panel);
        }

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
            var desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.View.WorkspaceView", containerCtrl.ClientID);
            desc.AddComponentProperty("ListModules", listModulesCtrl.ClientID);
            desc.AddComponentProperty("ListObjectClasses", listObjectClassesCtrl.ClientID);
            desc.AddComponentProperty("ListInstances", listInstancesCtrl.ClientID);
            desc.AddComponentProperty("ListRecentObjects", listRecentObjectsCtrl.ClientID);
            yield return desc; 
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(WorkspaceView), "Kistl.Client.ASPNET.Toolkit.View.WorkspaceView.js"));
        }

        #endregion
    }
}
