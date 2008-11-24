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
using Kistl.Client.ASPNET.Toolkit.Pages;
using Kistl.Client.GUI.DB;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.View.WorkspaceView.js", "text/javascript")] 

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public abstract class WorkspaceView : System.Web.UI.UserControl, IView, IScriptControl, IWorkspaceView
    {
        protected WorkspaceModel Model { get; private set; }
        protected abstract AjaxDataControls.DataList listModulesCtrl { get; }
        protected abstract AjaxDataControls.DataList listObjectClassesCtrl { get; }
        protected abstract AjaxDataControls.DataList listInstancesCtrl { get; }
        protected abstract AjaxDataControls.DataList listRecentObjectsCtrl { get; }
        protected abstract Control containerCtrl { get; }

        protected abstract HiddenField hdObjectsControl { get; }
        protected abstract AjaxControlToolkit.TabContainer tabObjectsControl { get; }

        public WorkspaceView()
        {
            this.Init += new EventHandler(WorkspaceView_Init);
        }

        void WorkspaceView_Init(object sender, EventArgs e)
        {
            CreateControls();
        }
 
        public void SetModel(PresentableModel mdl)
        {
            Model = (WorkspaceModel)mdl;
        }

        #region Object Management
        public void ShowObject(IDataObject obj)
        {
            this.Objects.Add(obj);
            ShowObjectInternal(obj);
            SetFirstIndex();
        }

        private void SetFirstIndex()
        {
            if (tabObjectsControl.ActiveTab == null && tabObjectsControl.Tabs.Count > 0)
            {
                tabObjectsControl.ActiveTabIndex = 0;
            }
        }

        private void ShowObjectInternal(IDataObject obj)
        {
            // Load Model
            DataObjectModel objMdl = (DataObjectModel)Model.Factory
                .CreateSpecificModel<DataObjectModel>(KistlContextManagerModule.KistlContext, obj);
            Model.RecentObjects.Add(objMdl);

            // Load View
            var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(objMdl);
            var ctrl = loader.LoadControl(Page);

            // Add to Tab Page
            AjaxControlToolkit.TabPanel panel = new AjaxControlToolkit.TabPanel();
            panel.Controls.Add(ctrl);
            panel.HeaderText = obj.ToString();

            tabObjectsControl.Tabs.Add(panel);
        }

        private void CreateControls()
        {
            foreach (IDataObject obj in Objects)
            {
                ShowObjectInternal(obj);
            }
            SetFirstIndex();
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
                        if (!string.IsNullOrEmpty(Request["Type"])
                            && string.IsNullOrEmpty(Request["ID"]))
                        {
                            IDataObject obj = KistlContextManagerModule.KistlContext
                                                .Find(Type.GetType(Request["Type"] + ",Kistl.Objects.Client"), int.Parse(Request["ID"]));
                            _Objects.Add(obj);
                        }
                    }
                    else
                    {
                        // We are to early! So we need to parse the Request Variable directly
                        // If anyone knows a better way -> pls. get in touch with me.
                        _Objects = Request[hdObjectsControl.UniqueID].FromJSONArray(KistlContextManagerModule.KistlContext)
                                        .ToList();
                    }
                }

                return _Objects;
            }
        }
        #endregion

        #region Render
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            hdObjectsControl.Value = Objects.ToJSONArray();

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
