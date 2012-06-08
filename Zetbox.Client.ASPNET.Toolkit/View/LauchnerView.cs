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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Zetbox.API;
using Zetbox.Client.ASPNET.Toolkit.Pages;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ObjectBrowser;

[assembly: WebResource("Zetbox.Client.ASPNET.Toolkit.View.LauncherView.js", "text/javascript")] 

namespace Zetbox.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/LauncherView.ascx")]
    public abstract class LauchnerView : ModelUserControl<WorkspaceViewModel>, IScriptControl
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
            var desc = new ScriptControlDescriptor("Zetbox.Client.ASPNET.View.LauncherView", containerCtrl.ClientID);
            desc.AddComponentProperty("ListModules", listModulesCtrl.ClientID);
            desc.AddComponentProperty("ListObjectClasses", listObjectClassesCtrl.ClientID);
            desc.AddComponentProperty("ListInstances", listInstancesCtrl.ClientID);
            yield return desc;
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(WorkspaceView), "Zetbox.Client.ASPNET.Toolkit.View.LauncherView.js"));
        }

        #endregion
    }
}
