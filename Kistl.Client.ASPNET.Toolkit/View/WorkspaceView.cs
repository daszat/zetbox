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
        public void ShowObject(DataObjectModel obj)
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

        private void ShowObjectInternal(DataObjectModel obj)
        {
            Model.RecentObjects.Add(obj);

            // Load View
            var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(obj);
            var ctrl = loader.LoadControl(Page);

            // Add to Tab Page
            AjaxControlToolkit.TabPanel panel = new AjaxControlToolkit.TabPanel();
            panel.Controls.Add(ctrl);
            panel.HeaderText = obj.Name;

            tabObjectsControl.Tabs.Add(panel);
        }

        private void CreateControls()
        {
            foreach (DataObjectModel obj in Objects)
            {
                ShowObjectInternal(obj);
            }
            SetFirstIndex();
        }

        List<DataObjectModel> _Objects;
        public List<DataObjectModel> Objects
        {
            get
            {
                if (_Objects == null)
                {
                    if (!IsPostBack)
                    {
                        _Objects = new List<DataObjectModel>();
                        // Parse Request
                        if (!string.IsNullOrEmpty(Request["Type"])
                            && string.IsNullOrEmpty(Request["ID"]))
                        {
                            Type type = Type.GetType(Request["Type"] + ",Kistl.Objects.Client");
                            IDataObject obj = KistlContextManagerModule.KistlContext
                                                .Find(type, int.Parse(Request["ID"]));
                            _Objects.Add((DataObjectModel)GuiApplicationContext.Current.Factory.CreateModel(
                                type, KistlContextManagerModule.KistlContext, new object[] { obj }));
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
