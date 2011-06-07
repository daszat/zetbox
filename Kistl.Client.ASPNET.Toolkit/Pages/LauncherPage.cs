using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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
using Kistl.App.GUI;
using Kistl.Client;
using Kistl.Client.ASPNET.Toolkit;
using Kistl.Client.ASPNET.Toolkit.View;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ObjectBrowser;

namespace Kistl.Client.ASPNET.Toolkit.Pages
{
    public abstract class LauncherPage : System.Web.UI.Page
    {
        protected abstract Control ctrlMainContent { get; }

        public LauncherPage()
        {
            this.Init += new EventHandler(LauncherPage_Init);
        }

        void LauncherPage_Init(object sender, EventArgs e)
        {
            var mdl = KistlContextManagerModule.ViewModelFactory
                .CreateViewModel<WorkspaceViewModel.Factory>().Invoke(KistlContextManagerModule.KistlContext, null);
            ControlKind launcher = KistlContextManagerModule.KistlContext.FindPersistenceObject<ControlKind>(NamedObjects.ControlKind_Kistl_App_GUI_LauncherKind);
            KistlContextManagerModule.ViewModelFactory.CreateSpecificView(mdl, launcher, ctrlMainContent);
        }
    }
}