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
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.Client;
using Zetbox.Client.ASPNET.Toolkit;
using Zetbox.Client.ASPNET.Toolkit.View;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ObjectBrowser;

namespace Zetbox.Client.ASPNET.Toolkit.Pages
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
            var mdl = ZetboxContextManagerModule.ViewModelFactory
                .CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ZetboxContextManagerModule.ZetboxContext, null);

            ZetboxContextManagerModule.ViewModelFactory.CreateDefaultView(mdl, ctrlMainContent);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}