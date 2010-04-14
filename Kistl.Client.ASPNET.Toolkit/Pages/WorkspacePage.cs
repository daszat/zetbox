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
using Kistl.Client.GUI;
using Kistl.Client.Presentables.ObjectBrowser;

namespace Kistl.Client.ASPNET.Toolkit.Pages
{
    public abstract class WorkspacePage : System.Web.UI.Page
    {
        protected abstract Control ctrlMainContent { get; }

        public WorkspacePage()
        {
            this.Init += new EventHandler(WorkspacePage_Init);
        }

        void WorkspacePage_Init(object sender, EventArgs e)
        {
            var mdl = GuiApplicationContext.Current.Factory
                .CreateViewModel<WorkspaceViewModel.Factory>().Invoke(KistlContextManagerModule.KistlContext);

            GuiApplicationContext.Current.Factory.CreateDefaultView(mdl, ctrlMainContent);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}