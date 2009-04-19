using System;
using System.Collections;
using System.Collections.Generic;
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
using Kistl.Client;
using Kistl.Client.ASPNET.Toolkit;
using Kistl.Client.ASPNET.Toolkit.View;
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit.Pages
{
    // TODO: Move to David
    public interface IWorkspaceView
    {
        void ShowObject(DataObjectModel obj);
    }

    public abstract class WorkspacePage : System.Web.UI.Page , IWorkspaceView
    {
        protected abstract Control ctrlMainContent { get; }

        protected WorkspaceModel Workspace;
        protected IWorkspaceView Ctrl;

        public WorkspacePage()
        {
            this.Init += new EventHandler(WorkspacePage_Init);
        }

        void WorkspacePage_Init(object sender, EventArgs e)
        {
            Workspace = GuiApplicationContext.Current.Factory
                .CreateSpecificModel<WorkspaceModel>(KistlContextManagerModule.KistlContext);
            var loader = (IViewLoader)GuiApplicationContext.Current.Factory.CreateDefaultView(Workspace);
            Ctrl = (IWorkspaceView)loader.LoadControl(this);
            ctrlMainContent.Controls.Add((Control)Ctrl);
        }

        public void ShowObject(DataObjectModel obj)
        {
            Ctrl.ShowObject(obj);
        }
    }
}