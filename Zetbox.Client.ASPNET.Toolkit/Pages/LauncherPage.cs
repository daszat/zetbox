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
using Zetbox.App.GUI;
using Zetbox.Client;
using Zetbox.Client.ASPNET.Toolkit;
using Zetbox.Client.ASPNET.Toolkit.View;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ObjectBrowser;

namespace Zetbox.Client.ASPNET.Toolkit.Pages
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
            var mdl = ZetboxContextManagerModule.ViewModelFactory
                .CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ZetboxContextManagerModule.ZetboxContext, null);
            ControlKind launcher = Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_LauncherKind.Find(ZetboxContextManagerModule.ZetboxContext);
            ZetboxContextManagerModule.ViewModelFactory.CreateSpecificView(mdl, launcher, ctrlMainContent);
        }
    }
}