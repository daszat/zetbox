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
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit.Pages
{
    public abstract class WorkspacePage : System.Web.UI.Page
    {
        protected abstract HiddenField hdObjectsControl { get; }
        protected abstract Control ctrlMainContent { get; }
        protected WorkspaceModel Workspace;

        public WorkspacePage()
        {
            this.Init += new EventHandler(WorkspacePage_Init);
        }

        void WorkspacePage_Init(object sender, EventArgs e)
        {
            Workspace = GuiApplicationContext.Current.Factory
                .CreateSpecificModel<WorkspaceModel>(KistlContextManagerModule.KistlContext);
            var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(Workspace);
            ctrlMainContent.Controls.Add(loader.LoadControl(this));

            CreateControls();
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            hdObjectsControl.Value = Objects.ToJSONArray();
        }

        private void ShowObjectInternal(IDataObject obj)
        {
            //var template = obj.FindTemplate(TemplateUsage.EditControl);
            //var widget = (Control)GuiApplicationContext.Current.Renderer.CreateControl(obj, template.VisualTree);

            //AjaxControlToolkit.TabPanel panel = new AjaxControlToolkit.TabPanel();
            //panel.Controls.Add(widget);
            //panel.HeaderText = obj.ToString();

            //tabObjectsControl.Tabs.Add(panel);
        }

        private void CreateControls()
        {
            foreach (IDataObject obj in Objects)
            {
                DataObjectModel objMdl = (DataObjectModel)Workspace.Factory.CreateModel(
                    obj.GetInterfaceType(), KistlContextManagerModule.KistlContext, new object[] {});
                Workspace.RecentObjects.Add(objMdl);
            }

            //if (tabObjectsControl.ActiveTab == null)
            //{
            //    tabObjectsControl.ActiveTabIndex = 0;
            //}
        }

        //#region IWorkspaceControl Members

        //public void ShowObject(IDataObject obj, IBasicControl ctrl)
        //{
        //    if (!Objects.Contains(obj))
        //    {
        //        Objects.Add(obj);
        //        ShowObjectInternal(obj);
        //    }
        //    tabObjectsControl.ActiveTabIndex = Objects.IndexOf(obj);
        //}

        //public void RemoveObject(IDataObject dataObject)
        //{
        //    // TODO
        //    throw new NotImplementedException();
        //}

        //public event EventHandler UserSaveRequest;

        //public event EventHandler UserAbortRequest;

        //public event EventHandler UserNewObjectRequest;

        //public event EventHandler<GenericEventArgs<IDataObject>> UserDeleteObjectRequest;

        //#endregion

        //#region IBasicControl Members

        //public string ShortLabel { get; set; }
        //public string Description { get; set; }
        //public FieldSize Size { get; set; }
        //IKistlContext IBasicControl.Context { get; set; }

        //#endregion

    }
}